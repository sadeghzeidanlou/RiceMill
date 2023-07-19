using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.DeliveryServices.Dto;

namespace RiceMill.Application.UseCases.DeliveryServices
{
    public interface IDeliveryCommands : IBaseUseCaseCommands
    {
        Result<DtoDelivery> Create(DtoCreateDelivery delivery);

        Result<DtoDelivery> Update(DtoUpdateDelivery delivery);
    }

    public class DeliveryCommands : IDeliveryCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DeliveryCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Result<DtoDelivery> Create(DtoCreateDelivery delivery)
        {
            throw new NotImplementedException();
        }

        public Result<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Result<DtoDelivery> Update(DtoUpdateDelivery delivery)
        {
            throw new NotImplementedException();
        }
    }
}