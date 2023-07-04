using Mapster;
using RiceMill.Application.Common.ExtensionMethods;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.ConcernServices.Dto;
using RiceMill.Domain.Models;
using System.Net;

namespace RiceMill.Application.UseCases.ConcernServices
{
    public interface IConcernCommands : IBaseUseCaseCommands
    {
        Task<Result<DtoConcern>> CreateAsync(DtoCreateConcern Concern);

        Task<Result<DtoConcern>> UpdateAsync(DtoUpdateConcern Concern);
    }

    public class ConcernCommands : IConcernCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;

        private readonly ICurrentRequestService _currentRequestService;

        public ConcernCommands(IApplicationDbContext applicationDbContext, ICurrentRequestService currentRequestService)
        {
            _applicationDbContext = applicationDbContext;
            _currentRequestService = currentRequestService;
        }

        public async Task<Result<DtoConcern>> CreateAsync(DtoCreateConcern createConcern)
        {
            if (_currentRequestService.HaveNotAccessDoAnyThing)
                return await Task.FromResult(Result<DtoConcern>.Failure(new Error(ResultStatusEnum.Forbidden), HttpStatusCode.Forbidden));

            var validationResult = createConcern.Validate();
            if (!validationResult.IsValid)
                return await Task.FromResult(Result<DtoConcern>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest));

            var concern = createConcern.Adapt<Concern>();
            concern.UserId = _currentRequestService.UserId;
            _applicationDbContext.Concerns.Add(concern);
            await _applicationDbContext.SaveChangesAsync();
            return await Task.FromResult(Result<DtoConcern>.Success(concern.Adapt<DtoConcern>()));
        }

        public async Task<Result<bool>> DeleteAsync(Guid id, Guid riceMillId)
        {
            if (_currentRequestService.HaveNotAccessDoAnyThing)
                return await Task.FromResult(Result<bool>.Failure(new Error(ResultStatusEnum.Forbidden), HttpStatusCode.Forbidden));

            var concern = _applicationDbContext.Concerns.Where(c => c.Id == id && _currentRequestService.IsNotAdmin ? c.RiceMillId == riceMillId : true).FirstOrDefault();
            if (concern == null)
                return await Task.FromResult(Result<bool>.Failure(new Error(ResultStatusEnum.ConcernNotFound), HttpStatusCode.NotFound));

            _applicationDbContext.Concerns.Remove(concern);
            await _applicationDbContext.SaveChangesAsync();
            return await Task.FromResult(Result<bool>.Success(true));
        }

        public async Task<Result<DtoConcern>> UpdateAsync(DtoUpdateConcern updateConcern)
        {
            if (_currentRequestService.HaveNotAccessDoAnyThing)
                return await Task.FromResult(Result<DtoConcern>.Failure(new Error(ResultStatusEnum.Forbidden), HttpStatusCode.Forbidden));

            var validationResult = updateConcern.Validate();
            if (!validationResult.IsValid)
                return await Task.FromResult(Result<DtoConcern>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest));

            var concern = _applicationDbContext.Concerns.FirstOrDefault(c => c.Id == updateConcern.Id && _currentRequestService.IsNotAdmin ? c.RiceMillId == updateConcern.RiceMillId : true);
            if (concern == null)
                return await Task.FromResult(Result<DtoConcern>.Failure(new Error(ResultStatusEnum.ConcernNotFound), HttpStatusCode.NotFound));

            concern = updateConcern.Adapt(concern);
            await _applicationDbContext.SaveChangesAsync();
            return await Task.FromResult(Result<DtoConcern>.Success(concern.Adapt<DtoConcern>()));
        }
    }
}