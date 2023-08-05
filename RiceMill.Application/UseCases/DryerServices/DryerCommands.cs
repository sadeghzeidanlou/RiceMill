using Mapster;
using RiceMill.Application.Common.ExtensionMethods;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.DryerServices.Dto;
using RiceMill.Application.UseCases.UserActivityServices;
using RiceMill.Domain.Models;
using Shared.Enums;
using Shared.ExtensionMethods;
using System.Net;

namespace RiceMill.Application.UseCases.DryerServices
{
    public interface IDryerCommands : IBaseUseCaseCommands
    {
        Result<DtoDryer> Create(DtoCreateDryer dryer);

        Result<DtoDryer> Update(DtoUpdateDryer dryer);
    }

    public class DryerCommands : IDryerCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;
        private readonly IUserActivityCommands _userActivityCommands;
        private readonly EntityTypeEnum _Key = EntityTypeEnum.Dryers;

        public DryerCommands(IApplicationDbContext applicationDbContext, ICurrentRequestService currentRequestService, ICacheService cacheService, IUserActivityCommands userActivityCommands)
        {
            _applicationDbContext = applicationDbContext;
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
            _userActivityCommands = userActivityCommands;
        }

        public Result<DtoDryer> Create(DtoCreateDryer createDryer)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<DtoDryer>.Forbidden();

            var validationResult = createDryer.Validate();
            if (!validationResult.IsValid)
                return Result<DtoDryer>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var dryer = createDryer.Adapt<Dryer>();
            dryer.UserId = _currentRequestService.UserId;
            _applicationDbContext.Dryers.Add(dryer);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.New, _Key, string.Empty, dryer.SerializeObject(), dryer.RiceMillId);
            _cacheService.Maintain(_Key, dryer);
            return Result<DtoDryer>.Success(dryer.Adapt<DtoDryer>());
        }

        public Result<DtoDryer> Update(DtoUpdateDryer updateDryer)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<DtoDryer>.Forbidden();

            var validationResult = updateDryer.Validate();
            if (!validationResult.IsValid)
                return Result<DtoDryer>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var dryer = GetDryerById(updateDryer.Id);
            if (dryer == null)
                return Result<DtoDryer>.Failure(new Error(ResultStatusEnum.DryerNotFound), HttpStatusCode.NotFound);

            var beforeEdit = dryer.SerializeObject();
            dryer = updateDryer.Adapt(dryer);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Edit, _Key, beforeEdit, dryer.SerializeObject(), dryer.RiceMillId);
            _cacheService.Maintain(_Key, dryer);
            return Result<DtoDryer>.Success(dryer.Adapt<DtoDryer>());
        }

        public Result<bool> Delete(Guid id)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<bool>.Forbidden();

            var dryer = GetDryerById(id);
            if (dryer == null)
                return Result<bool>.Failure(new Error(ResultStatusEnum.DryerNotFound), HttpStatusCode.NotFound);

            var beforeEdit = dryer.SerializeObject();
            _applicationDbContext.Dryers.Remove(dryer);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Delete, _Key, beforeEdit, dryer.SerializeObject(), dryer.RiceMillId);
            _cacheService.Maintain(_Key, dryer);
            return Result<bool>.Success(true);
        }

        private Dryer GetDryerById(Guid id) => _applicationDbContext.Dryers.FirstOrDefault(c => c.Id == id);
    }
}