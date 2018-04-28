using Contracts;
using Crosscutting.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Contexts.Decorators
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

        public Task Handle(TRequest request, CancellationToken cancellationToken)
        {
            _decoratee.Handle(request, cancellationToken);
            _unitOfWork.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
