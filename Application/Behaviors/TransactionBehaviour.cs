using Application.Interfaces;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Application.Behaviors;

public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ICleanDbContext _context;
    public TransactionBehaviour(ICleanDbContext context) => _context = context;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = default(TResponse);

        if (_context.HasActiveTransaction || !(request is ICommand || request is ICommand<TResponse>)) return await next();

        using (var transaction = await _context.BeginTransactionAsync())
        {
            response = await next();

            await _context.CommitTransactionAsync(transaction);
        }

        return response;
    }
}