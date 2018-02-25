using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Query.Service
{
    public class ServiceResult
    {
        /// <summary>
        /// Geldige bsn uzovi combinaties (9000+)
        /// Gedefinieerd als dictionary voor performance redenen
        /// https://stackoverflow.com/questions/150750/hashset-vs-list-performance
        /// </summary>
        public IDictionary<int, short> BsnUzovis { get; private set; }
                
        /// <summary>
        /// Geldige adressen
        /// </summary>
        public IEnumerable<string> Adresses { get; private set; }

        public ServiceResult(IDictionary<int, short> bsnUzovis, IEnumerable<string> adresses)
        {
            BsnUzovis = bsnUzovis;
            Adresses = adresses;
        }
    }
}
