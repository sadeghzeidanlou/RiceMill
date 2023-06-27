using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.DeliveryServices.Dto;

namespace RiceMill.Application.UseCases.DeliveryServices
{
    public interface IDeliveryQueries
    {
        Task<Result<int>> GetCountAsync();

        Task<Result<List<DtoDelivery>>> GetAllAsync();
    }

    public class DeliveryQueries : IDeliveryQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DeliveryQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<List<DtoDelivery>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}