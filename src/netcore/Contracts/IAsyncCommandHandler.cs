using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAsyncCommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, CancellationToken cancellationToken);
    }
}
