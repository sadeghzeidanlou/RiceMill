using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.DeliveryServices.Dto;

namespace RiceMill.Application.UseCases.DeliveryServices
{
    public interface IDeliveryCommands : IBaseUseCaseCommands
    {
        Task<Result<DtoDelivery>> CreateAsync(DtoCreateDelivery delivery);

        Task<Result<DtoDelivery>> UpdateAsync(DtoUpdateDelivery delivery);
    }

    public class DeliveryCommands : IDeliveryCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DeliveryCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<DtoDelivery>> CreateAsync(DtoCreateDelivery delivery)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<DtoDelivery>> UpdateAsync(DtoUpdateDelivery delivery)
        {
            throw new NotImplementedException();
        }
    }
}