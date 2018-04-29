using Crosscutting.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Contexts.Contracts.Decorators
{
    public class ContextTransactionDecorator<TRequest> : IRequestHandler<TRequest> 
        where TRequest : IRequest 
    {
        readonly IRequestHandler<TRequest> _decoratee;
        readonly IUnitOfWork _unitOfWork;

        public ContextTransactionDecorator(IUnitOfWork unitOfWork, IRequestHandler<TRequest> decoratee)
        {
            Guard.IsNotNull(decoratee, nameof(decoratee));
            Guard.IsNotNull(unitOfWork, nameof(unitOfWork));

            _decoratee = decoratee;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(TRequest request, CancellationToken cancellationToken)
        {
            await _decoratee
                .Handle(request, cancellationToken)
                .ConfigureAwait(false);

            await _unitOfWork
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
