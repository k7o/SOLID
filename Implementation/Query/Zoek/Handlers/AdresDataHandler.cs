using System.Linq;
using Contracts;
using Entities;

namespace Implementation.Query.Zoek.Handlers
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
