using ConsoleApp1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Classes
{
    interface IWhitelistComponent
    {
        IEnumerable<Result> Execute(IEnumerable<int> bsnNummers);
    }
}
