using ClassLibrary1.Agents;
using Infrastructure;
using System.Collections.Generic;

namespace ClassLibrary1.Query.Service
{
    public class ServiceQuery : ICachedQuery<ServiceResult>
    {
        public string CacheKey => "ServiceQuery";
    }
}
