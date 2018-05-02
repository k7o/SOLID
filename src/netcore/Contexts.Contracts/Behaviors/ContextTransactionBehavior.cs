using Crosscutting.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contexts.Contracts.Behaviors
{
    public class ContextTransactionBehavior : IPipelineBehavior<IRequest, Unit> 
    {
        readonly DbContext _context;

        public ContextTransactionBehavior(DbContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            _context = context;
        }

        public async Task<Unit> Handle(IRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Unit> next)
        {
            Unit result;

            using (var transaction = 
                await _context.Database.BeginTransactionAsync().ConfigureAwait(false))
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
