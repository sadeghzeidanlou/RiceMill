using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.ConcernServices.Dto;
using RiceMill.Application.Common.Models.Enums;
using System.Net;
using Mapster;
using RiceMill.Domain.Models;

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

        public Task<Result<DtoConcern>> CreateAsync(DtoCreateConcern createConcern)
        {
            if (!_currentRequestService.IsAuthenticated)
                return Task.FromResult(Result<DtoConcern>.Failure(new Error(ResultStatusEnum.NotAuthenticated), HttpStatusCode.Unauthorized));

            if (!createConcern.IsValid)
                return Task.FromResult(Result<DtoConcern>.Failure(new Error(ResultStatusEnum.ConcernTitleIsNotValid), HttpStatusCode.BadRequest));

            var concern = createConcern.Adapt<Concern>();
            _applicationDbContext.Concerns.Add(concern);
            _applicationDbContext.SaveChangesAsync();
            concern.Id = Guid.NewGuid();
            return Task.FromResult(Result<DtoConcern>.Success(concern.Adapt<DtoConcern>()));
        }

        public Task<Result<int>> DeleteAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<DtoConcern>> UpdateAsync(DtoUpdateConcern Concern)
        {
            throw new NotImplementedException();
        }
    }
}