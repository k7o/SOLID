using System;
using System.Linq;
using ClassLibrary1.Entities;
using Infrastructure;

namespace ClassLibrary1.Query.Zoek
{
    public partial class BsnQuery : Bsn, IQuery<ZoekResult>
    {
        public BsnQuery(int bsnnummer) 
            : base(bsnnummer)
        {
        }
    }
}
