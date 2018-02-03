using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Model;

namespace ConsoleApp1.Classes
{
    class WhitelistCore : IWhitelistComponent
    {
        IWhitelistComponent _component;

        public WhitelistCore(IWhitelistComponent component)
        {
            _component = component;
        }

        public IEnumerable<Result> Execute(IEnumerable<int> bsnNummers)
        {
            // 
        }
    }
}
