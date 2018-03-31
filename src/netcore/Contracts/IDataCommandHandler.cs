using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IDataCommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : IDataCommand
    {
    }
}
