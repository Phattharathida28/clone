using Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IDbContext
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitTransactionAsync(IDbContextTransaction transaction);
    void RollbackTransaction();
    DatabaseFacade Database { get; }
    bool HasActiveTransaction { get; }
    DbSet<T> Set<T>() where T : class;
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task<int> SaveChangesAsync(string userName, string programCode, CancellationToken cancellationToken);
    void ClearTracker();
    Task<int> ExecuteAsync(string sql, object param, CancellationToken token = default, bool isStore = false);
    Task<T> ExecuteScalarAsync<T>(string sql, object param, CancellationToken token = default, bool isStore = false);
    Task<T> QueryFirstAsync<T>(string sql, object param, CancellationToken token = default, bool isStore = false);
    Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param, CancellationToken token = default, bool isStore = false);
    Task<T> QuerySingleAsync<T>(string sql, object param, CancellationToken token = default, bool isStore = false);
    Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param, CancellationToken token = default, bool isStore = false);
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object param, CancellationToken token = default, bool isStore = false);
    Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, string splitOn = "Id", object param = null, CancellationToken token = default, bool isStore = false);
    Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, string splitOn = "Id", object param = null, CancellationToken token = default, bool isStore = false);
    Task<T> GetParameterValue<T>(string group, string code, CancellationToken token = default);
    Task<IEnumerable<(string, T)>> GetParameterValues<T>(string group, CancellationToken token = default);
}
