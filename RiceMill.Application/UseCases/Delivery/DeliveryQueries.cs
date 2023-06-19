using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.Base;
using RiceMill.Application.UseCases.Delivery.Dto;

namespace RiceMill.Application.UseCases.Delivery
{
    public interface IDeliveryQueries : IBaseUseCaseQueries
    {
        Task<Result<DtoDelivery>> GetAsync(int id);

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

        public Task<Result<DtoDelivery>> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}