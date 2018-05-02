using Crosscutting.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Contexts.Contracts.Behaviors
{
    public class ContextTransactionBehavior<TContext> : IPipelineBehavior<IRequest, Unit> 
        where TContext : IContext
    {
        readonly IContext _context;

        public ContextTransactionBehavior(TContext context)
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
