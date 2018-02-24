using ClassLibrary1.Agents;
using Infrastructure;
using System.Collections.Generic;

namespace ClassLibrary1.Query.Service.Handlers
{
    public class ServiceQueryHandler : IQueryHandler<ServiceQuery, ServiceResult>
    {
        readonly IServiceAgent _serviceAgent;

        public ServiceQueryHandler(IServiceAgent serviceAgent)
        {
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
