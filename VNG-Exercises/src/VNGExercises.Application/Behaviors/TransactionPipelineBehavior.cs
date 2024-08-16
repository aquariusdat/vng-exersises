using VNGExercises.Domain.Abstractions;
using MediatR;
using System.Transactions;
using VNGExercises.Persistence;

namespace VNGExercises.Application.Behaviors;

public sealed class TransactionPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IUnitOfWork _unitOfWork; // SQL-SERVER-STRATEGY-2
    private readonly ApplicationDbContext _context; // SQL-SERVER-STRATEGY-1

    public TransactionPipelineBehavior(IUnitOfWork unitOfWork, ApplicationDbContext context)
    {
        _unitOfWork = unitOfWork;
        _context = context;
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!IsCommand()) // In case TRequest is QueryRequest just ignore
            return await next();

        using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var response = await next();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            transaction.Complete();
            await _unitOfWork.DisposeAsync();
            return response;
        }
    }

    private bool IsCommand()
        => typeof(TRequest).Name.EndsWith("Command");
}
