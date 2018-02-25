using Contracts;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Decorators
{ 
    public class CommandTransactionDecorator<TCommand, TContext> : IDataCommandHandler<TCommand> 
        where TCommand : IDataCommand 
        where TContext : IContext<TContext>
    {
        readonly TContext _context;
        readonly IDataCommandHandler<TCommand> _decorated;

        public CommandTransactionDecorator(IDataCommandHandler<TCommand> decorated, TContext context)
        {
            Guard.IsNotNull(decorated, nameof(decorated));
            Guard.IsNotNull(context, nameof(context));

            _decorated = decorated;
            _context = context;
        }

        public void Handle(TCommand command)
        {
            _context.StartTransaction();

            _decorated.Handle(command);

            
        }
    }
}
