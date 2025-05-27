using Application.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Helpers;

public class ResilientTransaction
{
    private IList<IDbContext> _contexts;
    private IList<Tuple<IDbContext, IDbContextTransaction>> _openTransaction;

    private ResilientTransaction(params IDbContext[] contexts)
    {
        _contexts = new List<IDbContext>();

        foreach (var context in contexts)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            _contexts.Add(context);
        }
    }

    public static ResilientTransaction New(params IDbContext[] contexts) => new ResilientTransaction(contexts);

    public async Task ExecuteAsync(Func<Task> action)
    {
        _openTransaction = new List<Tuple<IDbContext, IDbContextTransaction>>();

        try
        {
            foreach (var context in _contexts)
            {
                if (context != null)
                {
                    var transaction = await context.BeginTransactionAsync();
                    _openTransaction.Add(Tuple.Create(context, transaction));
                }
            }
            await action();

            foreach (var transaction in _openTransaction)
            {
                if (transaction != null) await transaction.Item1.CommitTransactionAsync(transaction.Item2);
            }
        }
        catch (Exception ex)
        {
            foreach (var transaction in _openTransaction)
            {
                if (transaction != null) transaction.Item1?.RollbackTransaction();
            }
            throw;
        }
        finally
        {
            foreach (var transaction in _openTransaction) transaction.Item2?.Dispose();
        }

    }
}
