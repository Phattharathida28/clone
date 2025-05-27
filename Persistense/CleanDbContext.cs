using Application.Interfaces;
using Domain.Entities;
using Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Persistense;

public partial class CleanDbContext : DbContext, ICleanDbContext
{
    private IDbContextTransaction _currentTransaction;
    private readonly ICurrentUserAccessor _currentUserAccessor;
    private readonly IDomainEventService _domainEventService;
    public bool HasActiveTransaction => _currentTransaction != null;

    protected CleanDbContext(DbContextOptions options, ICurrentUserAccessor currentUserAccessor, IDomainEventService domainEventService) : base(options)
    {
        _currentUserAccessor = currentUserAccessor;
        _domainEventService = domainEventService;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    public CleanDbContext(DbContextOptions<CleanDbContext> options, ICurrentUserAccessor currentUserAccessor, IDomainEventService domainEventService) : this((DbContextOptions)options, currentUserAccessor, domainEventService)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.EntityTypes().Configure(e => e.SetTableName(e.ClrType.Name.ToLowercaseNamingConvention()));
        modelBuilder.Properties().Configure(p => p.SetColumnName(p.Name.ToLowercaseNamingConvention()));
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CleanDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken) => await this.SaveChangesAsync(_currentUserAccessor.UserName,_currentUserAccessor.ProgramCode, cancellationToken);

    public async Task<int> SaveChangesAsync(string userName, string programCode, CancellationToken cancellationToken)
    {
        this.SetAudit(userName, programCode);

        DomainEvent[] events = ChangeTracker.Entries<IHasDomainEvent>()
        .Select(x => x.Entity.DomainEvents)
        .SelectMany(x => x)
        .Where(domainEvent => !domainEvent.IsPublished)
        .ToArray();

        int result = await base.SaveChangesAsync(cancellationToken);

        await DispatchEvents(events);

        return result;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false);

        return _currentTransaction;
    }
    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await base.SaveChangesAsync().ConfigureAwait(false);
            transaction.Commit();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        catch
        {
           
        }
        finally
        {
            this.ClearTracker();
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void ClearTracker() => this.ChangeTracker.Clear();
    private void SetAudit(string username, string programCode)
    {
        foreach (var entry in ChangeTracker.Entries<EntityDomain>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreateAudit(username, programCode);
                    entry.Entity.UpdateAudit(username, programCode);
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdateAudit(username, programCode);
                    break;
                case EntityState.Deleted:
                    break;
            }
        }
    }

    private async Task DispatchEvents(DomainEvent[] events)
    {
        foreach (var @event in events)
        {
            @event.IsPublished = true;
            await _domainEventService.Publish(@event);
        }
    }
}
