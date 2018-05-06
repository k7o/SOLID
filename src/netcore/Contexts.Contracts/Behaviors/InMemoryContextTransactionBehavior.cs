using Crosscutting.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Contexts.Contracts.Behaviors
{
    public class InMemoryContextTransactionBehavior : IPipelineBehavior<IRequest, Unit>
    {
        readonly IContext _context;

        public InMemoryContextTransactionBehavior(IContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            _context = context;
        }

        public async Task<Unit> Handle(IRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Unit> next)
        {
            var result = await next();

            await _context.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
