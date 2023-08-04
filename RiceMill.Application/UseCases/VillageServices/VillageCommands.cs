using Mapster;
using RiceMill.Application.Common.ExtensionMethods;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.UserActivityServices;
using RiceMill.Application.UseCases.VillageServices.Dto;
using RiceMill.Domain.Models;
using Shared.Enums;
using Shared.ExtensionMethods;
using System.Net;

namespace RiceMill.Application.UseCases.VillageServices
{
    public interface IVillageCommands : IBaseUseCaseCommands
    {
        Result<DtoVillage> Create(DtoCreateVillage village);

        Result<DtoVillage> Update(DtoUpdateVillage village);
    }

    public class VillageCommands : IVillageCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;
        private readonly IUserActivityCommands _userActivityCommands;
        private readonly EntityTypeEnum _Key = EntityTypeEnum.Villages;

        public VillageCommands(IApplicationDbContext applicationDbContext, ICurrentRequestService currentRequestService, ICacheService cacheService, IUserActivityCommands userActivityCommands)
        {
            _applicationDbContext = applicationDbContext;
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
            _userActivityCommands = userActivityCommands;
        }

        public Result<DtoVillage> Create(DtoCreateVillage createVillage)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<DtoVillage>.Forbidden();

            var validationResult = createVillage.Validate();
            if (!validationResult.IsValid)
                return Result<DtoVillage>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var village = createVillage.Adapt<Village>();
            village.UserId = _currentRequestService.UserId;
            _applicationDbContext.Villages.Add(village);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.New, _Key, string.Empty, village.SerializeObject(), village.RiceMillId);
            _cacheService.Maintain(_Key, village);
            return Result<DtoVillage>.Success(village.Adapt<DtoVillage>());
        }

        public Result<DtoVillage> Update(DtoUpdateVillage updateVillage)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<DtoVillage>.Forbidden();

            var validationResult = updateVillage.Validate();
            if (!validationResult.IsValid)
                return Result<DtoVillage>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var village = GetVillageById(updateVillage.Id);
            if (village == null)
                return Result<DtoVillage>.Failure(new Error(ResultStatusEnum.VillageNotFound), HttpStatusCode.NotFound);

            var beforeEdit = village.SerializeObject();
            village = updateVillage.Adapt(village);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Edit, _Key, beforeEdit, village.SerializeObject(), village.RiceMillId);
            _cacheService.Maintain(_Key, village);
            return Result<DtoVillage>.Success(village.Adapt<DtoVillage>());
        }

        public Result<bool> Delete(Guid id)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<bool>.Forbidden();

            var village = GetVillageById(id);
            if (village == null)
                return Result<bool>.Failure(new Error(ResultStatusEnum.VillageNotFound), HttpStatusCode.NotFound);

            var beforeEdit = village.SerializeObject();
            _applicationDbContext.Villages.Remove(village);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Delete, _Key, beforeEdit, village.SerializeObject(), village.RiceMillId);
            _cacheService.Maintain(_Key, village);
            return Result<bool>.Success(true);
        }

        private Village GetVillageById(Guid id) => _applicationDbContext.Villages.FirstOrDefault(c => c.Id == id);
    }
}