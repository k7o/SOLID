using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IDataQueryHandler<in TQuery, out TResult> : IQueryHandler<TQuery, TResult> where TQuery : IDataQuery<TResult>
    {
    }
}
