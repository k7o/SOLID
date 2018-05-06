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
            Unit result;

            using (var transaction = await _context.BeginTransactionAsync())
            {
                result = await next();

                await _context.SaveChangesAsync(cancellationToken);

                // Commit transaction if all commands succeed, transaction will auto-rollback
                // when disposed if either commands fails
                transaction.Commit();
            }

            return result;
        }
    }
}
