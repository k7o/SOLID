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
        readonly IContext _context;

        public ContextTransactionDecorator(IContext context, IRequestHandler<TRequest> decoratee)
        {
            Guard.IsNotNull(decoratee, nameof(decoratee));
            Guard.IsNotNull(context, nameof(context));

            _decoratee = decoratee;
            _context = context;
        }

        public async Task Handle(TRequest request, CancellationToken cancellationToken)
        {
            await _decoratee
                .Handle(request, cancellationToken)
                .ConfigureAwait(false);

            await _context
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
