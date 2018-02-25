using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IContext<TEntity>
    {
        IEnumerable<TEntity> GetAll();

        void Add(TEntity entity);

        void SaveChanges();

        ITransaction StartTransaction();
    }
}
