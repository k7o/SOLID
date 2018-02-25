using Implementation.Agents;
using Infrastructure;
using Contracts;
using System.Collections.Generic;

namespace Implementation.Query.Service.Handlers
{
    public class ServiceQueryHandler : IQueryHandler<ServiceQuery, ServiceResult>
    {
        readonly IServiceAgent _serviceAgent;

        public ServiceQueryHandler(IServiceAgent serviceAgent)
        {
            Guard.IsNotNull(serviceAgent, nameof(serviceAgent));

            _serviceAgent = serviceAgent;
        }

        public ServiceResult Handle(ServiceQuery query)
        {
            var response = _serviceAgent.Get();

            var bsnUzovis = new Dictionary<int, short>();
            foreach (var bsnUzovi in response.BsnUzovis)
            {
                bsnUzovis.Add(bsnUzovi.Bsnnummer, bsnUzovi.Uzovi);
            }

            return new ServiceResult(bsnUzovis, response.Adresses);
        }
    }
}
