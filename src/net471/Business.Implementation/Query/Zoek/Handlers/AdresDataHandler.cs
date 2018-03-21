using System.Linq;
using Business.Contracts.Query.Zoek;
using Contracts;
using Crosscutting.Contracts;
using Business.Entities;

namespace Business.Implementation.Query.Zoek.Handlers
{
    public class AdresDataHandler : IDataQueryHandler<AdresQuery, ZoekResult>
    {
        readonly IUnitOfWork _unitOfWork;

        public AdresDataHandler(IUnitOfWork unitOfWork)
        {
            Guard.IsNotNull(unitOfWork, nameof(unitOfWork));

            _unitOfWork = unitOfWork;
        }

        public ZoekResult Handle(AdresQuery query)
        {
            return new ZoekResult(
                _unitOfWork
                    .Repository<Adres>()
                    .GetAll()
                    .Any(c => c.Postcode == query.Postcode)
            );
        }
    }
    
}
