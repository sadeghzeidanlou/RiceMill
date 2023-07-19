using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.DeliveryServices.Dto;

namespace RiceMill.Application.UseCases.DeliveryServices
{
    public interface IDeliveryQueries
    {
        Result<PaginatedList<DtoDelivery>> GetAll();
    }

    public class DeliveryQueries : IDeliveryQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DeliveryQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Result<PaginatedList<DtoDelivery>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}