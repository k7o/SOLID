using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Infrastructure
{
    public interface IAmTraceable
    {
        void Start();

        void Stop();

        void Excute();

        void Excuted();

        void Failure(Exception ex);
    }
}
