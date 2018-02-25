using BTDB.ODBLayer;
using Entities;
using Contracts;
using System.Collections.Generic;

namespace Contexts
{
    class BTDBAdresContext : IContext<Adres>
    {
        readonly IObjectDB _objectDB;
        readonly IObjectDBTransaction _objectDBTransaction;

        public BTDBAdresContext(IObjectDB objectDB)
        {
            Guard.IsNotNull(objectDB, nameof(objectDB));

            _objectDB = objectDB;
        }

        public IEnumerable<Adres> GetAll( )
        {
            return null; //_objectDB.Enumerate<Adres>();
        }

        public void Add(Adres entity)
        {
            //_objectDB.Store(entity);
        }

        public void SaveChanges()
        {
            // transaction.Commit();
            //_objectDB.Commit();
        }

        public ITransaction StartTransaction()
        {
            return null;// _objectDB.StartTransaction();
            
        }

    }
}
