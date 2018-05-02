using Crosscutting.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contexts.Contracts.Behaviors
{
    public class InMemoryContextTransactionBehavior : IPipelineBehavior<IRequest, Unit>
    {
        readonly DbContext _context;

        public InMemoryContextTransactionBehavior(DbContext context)
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
