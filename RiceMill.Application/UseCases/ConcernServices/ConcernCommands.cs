﻿using Mapster;
using RiceMill.Application.Common.ExtensionMethods;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.ConcernServices.Dto;
using RiceMill.Application.UseCases.UserActivityServices;
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

    public sealed class ConcernCommands : IConcernCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;
        private readonly IUserActivityCommands _userActivityCommands;
        private readonly EntityTypeEnum _Key = EntityTypeEnum.Concerns;

        public ConcernCommands(IApplicationDbContext applicationDbContext, ICurrentRequestService currentRequestService, ICacheService cacheService, IUserActivityCommands userActivityCommands)
        {
            _applicationDbContext = applicationDbContext;
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
            _userActivityCommands = userActivityCommands;
        }

        public Result<DtoConcern> Create(DtoCreateConcern createConcern)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<DtoConcern>.Forbidden();

            var validationResult = createConcern.Validate();
            if (!validationResult.IsValid)
                return Result<DtoConcern>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var validateConcern = ValidateConcern(createConcern);
            if (validateConcern != null)
                return validateConcern;

            var concern = createConcern.Adapt<Concern>();
            concern.UserId = _currentRequestService.UserId;
            _applicationDbContext.Concerns.Add(concern);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.New, _Key, string.Empty, concern.SerializeObject(), concern.RiceMillId);
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

            var concern = GetConcernById(updateConcern.Id);
            if (concern == null)
                return Result<DtoConcern>.Failure(Error.CreateError(ResultStatusEnum.ConcernNotFound), HttpStatusCode.NotFound);

            var createConcern = updateConcern.Adapt<DtoCreateConcern>();
            createConcern = createConcern with { RiceMillId = concern.RiceMillId };
            var validateConcern = ValidateConcern(createConcern);
            if (validateConcern != null)
                return validateConcern;

            var beforeEdit = concern.SerializeObject();
            concern = updateConcern.Adapt(concern);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Edit, _Key, beforeEdit, concern.SerializeObject(), concern.RiceMillId);
            _cacheService.Maintain(_Key, concern);
            return Result<DtoConcern>.Success(concern.Adapt<DtoConcern>());
        }

        public Result<bool> Delete(Guid id)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<bool>.Forbidden();

            var concern = GetConcernById(id);
            if (concern == null)
                return Result<bool>.Failure(Error.CreateError(ResultStatusEnum.ConcernNotFound), HttpStatusCode.NotFound);

            var beforeEdit = concern.SerializeObject();
            _applicationDbContext.Concerns.Remove(concern);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Delete, _Key, beforeEdit, concern.SerializeObject(), concern.RiceMillId);
            _cacheService.Maintain(_Key, concern);
            return Result<bool>.Success(true);
        }

        private Concern GetConcernById(Guid id) => _applicationDbContext.Concerns.FirstOrDefault(c => c.Id.Equals(id));

        private Result<DtoConcern> ValidateConcern(DtoCreateConcern concern) =>
            !_cacheService.GetRiceMills().Any(x => x.Id.Equals(concern.RiceMillId)) ? Result<DtoConcern>.Failure(Error.CreateError(ResultStatusEnum.RiceMillNotFound), HttpStatusCode.NotFound) : null;
    }
}