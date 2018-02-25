using Contracts;

namespace Implementation.Query.Service
{
    public class ServiceQuery : ICachedQuery<ServiceResult>
    {
        public string CacheKey => "ServiceQuery";
    }
}
