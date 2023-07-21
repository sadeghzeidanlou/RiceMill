using Mapster;
using RiceMill.Application.Common.ExtensionMethods;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.ConcernServices.Dto;
using RiceMill.Application.UseCases.UserActivityServices;
using RiceMill.Application.UseCases.UserActivityServices.Dto;
using RiceMill.Domain.Models;
using Shared.Enums;
using Shared.ExtensionMethods;
using System.Net;

namespace RiceMill.Application.UseCases.ConcernServices
{
    public interface IConcernCommands : IBaseUseCaseCommands
    {
        Result<DtoConcern> Create(DtoCreateConcern Concern);

        Result<DtoConcern> Update(DtoUpdateConcern Concern);
    }

    public class ConcernCommands : IConcernCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;
        private readonly IUserActivityCommands _userActivityCommands;
        private readonly EntityTypeEnum _Key = EntityTypeEnum.Concerns;
        private DtoCreateUserActivity _userActivity;

        public ConcernCommands(IApplicationDbContext applicationDbContext, ICurrentRequestService currentRequestService, ICacheService cacheService, IUserActivityCommands userActivityCommands)
        {
            _applicationDbContext = applicationDbContext;
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
            _userActivityCommands = userActivityCommands;
            _userActivity = new DtoCreateUserActivity(_currentRequestService.UserId, _currentRequestService.Ip, UserActivityTypeEnum.New, _Key, ApplicationIdEnum.Mobile, string.Empty, string.Empty, null);
        }

        public Result<DtoConcern> Create(DtoCreateConcern createConcern)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<DtoConcern>.Forbidden();

            var validationResult = createConcern.Validate();
            if (!validationResult.IsValid)
                return Result<DtoConcern>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var concern = createConcern.Adapt<Concern>();
            concern.UserId = _currentRequestService.UserId;
            _applicationDbContext.Concerns.Add(concern);
            _applicationDbContext.SaveChanges();
            _userActivity = _userActivity with { BeforeEdit = concern.SerializeObject(), RiceMillId = concern.RiceMillId };
            _userActivityCommands.Create(_userActivity);
            _cacheService.Maintain(_Key, concern);
            return Result<DtoConcern>.Success(concern.Adapt<DtoConcern>());
        }

        public Result<DtoConcern> Update(DtoUpdateConcern updateConcern)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<DtoConcern>.Forbidden();

            var validationResult = updateConcern.Validate();
            if (!validationResult.IsValid)
                return Result<DtoConcern>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var concern = _applicationDbContext.Concerns.FirstOrDefault(c => c.Id == updateConcern.Id);
            if (concern == null)
                return Result<DtoConcern>.Failure(new Error(ResultStatusEnum.ConcernNotFound), HttpStatusCode.NotFound);

            var beforeEdit = concern.SerializeObject();
            concern = updateConcern.Adapt(concern);
            _applicationDbContext.SaveChanges();
            _userActivity = _userActivity with { UserActivityType = UserActivityTypeEnum.Edit, BeforeEdit = beforeEdit, AfterEdit = concern.SerializeObject(), RiceMillId = concern.RiceMillId };
            _userActivityCommands.Create(_userActivity);
            _cacheService.Maintain(_Key, concern);
            return Result<DtoConcern>.Success(concern.Adapt<DtoConcern>());
        }

        public Result<bool> Delete(Guid id)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<bool>.Forbidden();

            var concern = _applicationDbContext.Concerns.Where(c => c.Id == id).FirstOrDefault();
            if (concern == null)
                return Result<bool>.Failure(new Error(ResultStatusEnum.ConcernNotFound), HttpStatusCode.NotFound);

            _applicationDbContext.Concerns.Remove(concern);
            _applicationDbContext.SaveChanges();
            var serialized = concern.SerializeObject();
            _userActivity = _userActivity with { UserActivityType = UserActivityTypeEnum.Delete, BeforeEdit = serialized, AfterEdit = serialized, RiceMillId = concern.RiceMillId };
            _userActivityCommands.Create(_userActivity);
            _cacheService.Maintain(_Key, concern);
            return Result<bool>.Success(true);
        }
    }
}