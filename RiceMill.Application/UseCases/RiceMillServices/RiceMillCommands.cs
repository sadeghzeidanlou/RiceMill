using Mapster;
using RiceMill.Application.Common.ExtensionMethods;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.RiceMillServices.Dto;
using RiceMill.Application.UseCases.UserActivityServices;
using Shared.Enums;
using Shared.ExtensionMethods;
using System.Net;

namespace RiceMill.Application.UseCases.RiceMillServices
{
    public interface IRiceMillCommands : IBaseUseCaseCommands
    {
        Result<DtoRiceMill> Create(DtoCreateRiceMill riceMill);

        Result<DtoRiceMill> Update(DtoUpdateRiceMill riceMill);
    }

    public sealed class RiceMillCommands : IRiceMillCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;
        private readonly IUserActivityCommands _userActivityCommands;
        private readonly EntityTypeEnum _Key = EntityTypeEnum.RiceMills;

        public RiceMillCommands(IApplicationDbContext applicationDbContext, ICurrentRequestService currentRequestService, ICacheService cacheService, IUserActivityCommands userActivityCommands)
        {
            _applicationDbContext = applicationDbContext;
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
            _userActivityCommands = userActivityCommands;
        }

        public Result<DtoRiceMill> Create(DtoCreateRiceMill createRiceMill)
        {
            if (_currentRequestService.IsNotAdmin)
                return Result<DtoRiceMill>.Forbidden();

            var validationResult = createRiceMill.Validate();
            if (!validationResult.IsValid)
                return Result<DtoRiceMill>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var riceMill = createRiceMill.Adapt<Domain.Models.RiceMill>();
            _applicationDbContext.RiceMills.Add(riceMill);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.New, _Key, string.Empty, riceMill.SerializeObject(), riceMill.Id);
            _cacheService.Maintain(_Key, riceMill);
            return Result<DtoRiceMill>.Success(riceMill.Adapt<DtoRiceMill>());
        }

        public Result<bool> Delete(Guid id)
        {
            if (_currentRequestService.IsNotAdmin)
                return Result<bool>.Forbidden();

            var riceMill = GetRiceMillById(id);
            if (riceMill == null)
                return Result<bool>.Failure(new Error(ResultStatusEnum.RiceMillNotFound), HttpStatusCode.NotFound);

            var beforeEdit = riceMill.SerializeObject();
            _applicationDbContext.RiceMills.Remove(riceMill);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Delete, _Key, beforeEdit, riceMill.SerializeObject(), riceMill.Id);
            _cacheService.Maintain(_Key, riceMill);
            return Result<bool>.Success(true);
        }

        public Result<DtoRiceMill> Update(DtoUpdateRiceMill updateRiceMill)
        {
            if (_currentRequestService.HasNotAccessToRiceMills)
                return Result<DtoRiceMill>.Forbidden();

            var validationResult = updateRiceMill.Validate();
            if (!validationResult.IsValid)
                return Result<DtoRiceMill>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var riceMill = GetRiceMillById(updateRiceMill.Id);
            if (riceMill == null)
                return Result<DtoRiceMill>.Failure(new Error(ResultStatusEnum.RiceMillNotFound), HttpStatusCode.NotFound);

            var beforeEdit = riceMill.SerializeObject();
            riceMill = updateRiceMill.Adapt(riceMill);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Edit, _Key, beforeEdit, riceMill.SerializeObject(), riceMill.Id);
            _cacheService.Maintain(_Key, riceMill);
            return Result<DtoRiceMill>.Success(riceMill.Adapt<DtoRiceMill>());
        }

        private Domain.Models.RiceMill GetRiceMillById(Guid id) => _applicationDbContext.RiceMills.FirstOrDefault(c => c.Id == id);
    }
}