using Crosscutting.Contracts;
using MediatR;
using System;
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
            Unit result;

            using (var transaction = 
                await _context.BeginTransactionAsync().ConfigureAwait(false))
            {
                try
                {
                    result = await next()
                        .ConfigureAwait(false);

                    await _context
                        .SaveChangesAsync(cancellationToken)
                        .ConfigureAwait(false);

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Commit();
                }
                catch (Exception)
                {
                    // TODO: Handle failure
                }
            }

            return result;
        }
    }
}
