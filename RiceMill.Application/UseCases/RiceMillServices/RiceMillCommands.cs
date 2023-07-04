using Mapster;
using RiceMill.Application.Common.ExtensionMethods;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.RiceMillServices.Dto;
using System.Net;

namespace RiceMill.Application.UseCases.RiceMillServices
{
    public interface IRiceMillCommands : IBaseUseCaseCommands
    {
        Task<Result<DtoRiceMill>> CreateAsync(DtoCreateRiceMill riceMill);

        Task<Result<DtoRiceMill>> UpdateAsync(DtoUpdateRiceMill riceMill);
    }

    public class RiceMillCommands : IRiceMillCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentRequestService _currentRequestService;

        public RiceMillCommands(IApplicationDbContext applicationDbContext, ICurrentRequestService currentRequestService)
        {
            _applicationDbContext = applicationDbContext;
            _currentRequestService = currentRequestService;
        }

        public async Task<Result<DtoRiceMill>> CreateAsync(DtoCreateRiceMill createRiceMill)
        {
            if (_currentRequestService.IsNotAdmin)
                return await Task.FromResult(Result<DtoRiceMill>.Failure(new Error(ResultStatusEnum.Forbidden), HttpStatusCode.Forbidden));

            var validationResult = createRiceMill.Validate();
            if (!validationResult.IsValid)
                return await Task.FromResult(Result<DtoRiceMill>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest));

            var riceMill = createRiceMill.Adapt<Domain.Models.RiceMill>();
            _applicationDbContext.RiceMills.Add(riceMill);
            await _applicationDbContext.SaveChangesAsync();
            return await Task.FromResult(Result<DtoRiceMill>.Success(riceMill.Adapt<DtoRiceMill>()));
        }

        public async Task<Result<bool>> DeleteAsync(Guid id, Guid riceMillId)
        {
            if (_currentRequestService.IsNotAdmin)
                return await Task.FromResult(Result<bool>.Failure(new Error(ResultStatusEnum.Forbidden), HttpStatusCode.Forbidden));

            var riceMill = _applicationDbContext.RiceMills.FirstOrDefault(c => c.Id == id);
            if (riceMill == null)
                return await Task.FromResult(Result<bool>.Failure(new Error(ResultStatusEnum.RiceMillNotFound), HttpStatusCode.NotFound));

            _applicationDbContext.RiceMills.Remove(riceMill);
            await _applicationDbContext.SaveChangesAsync();
            return await Task.FromResult(Result<bool>.Success(true));
        }

        public async Task<Result<DtoRiceMill>> UpdateAsync(DtoUpdateRiceMill updateRiceMill)
        {
            if (_currentRequestService.IsNotAdmin)
                return await Task.FromResult(Result<DtoRiceMill>.Failure(new Error(ResultStatusEnum.Forbidden), HttpStatusCode.Forbidden));

            var validationResult = updateRiceMill.Validate();
            if (!validationResult.IsValid)
                return await Task.FromResult(Result<DtoRiceMill>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest));

            var riceMill = _applicationDbContext.RiceMills.FirstOrDefault(c => c.Id == updateRiceMill.Id);
            if (riceMill == null)
                return await Task.FromResult(Result<DtoRiceMill>.Failure(new Error(ResultStatusEnum.RiceMillNotFound), HttpStatusCode.NotFound));

            riceMill = updateRiceMill.Adapt(riceMill);
            await _applicationDbContext.SaveChangesAsync();
            return await Task.FromResult(Result<DtoRiceMill>.Success(riceMill.Adapt<DtoRiceMill>()));
        }
    }
}