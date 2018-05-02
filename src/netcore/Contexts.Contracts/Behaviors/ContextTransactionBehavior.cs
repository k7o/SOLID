using Crosscutting.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Contexts.Contracts.Behaviors
{
    public class ContextTransactionBehavior : IPipelineBehavior<IRequest, Unit>
    {
        readonly IContext _context;

        public ContextTransactionBehavior(IContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            _context = context;
        }

        public async Task<Unit> Handle(IRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Unit> next)
        {
            var result = await next()
                .ConfigureAwait(false);
            
            await _context
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            return result;
        }
    }
}
