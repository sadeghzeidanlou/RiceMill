using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.ConcernServices.Dto;
using RiceMill.Domain.Models;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.ConcernServices
{
    public interface IConcernQueries
    {
        Task<Result<List<DtoConcern>>> GetAllAsync(DtoConcernFilter filter);

        Task<Result<int>> GetCountAsync(DtoConcernFilter filter);
    }

    public class ConcernQueries : IConcernQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        private readonly ICurrentRequestService _currentRequestService;

        private readonly ICacheService _cacheService;

        public ConcernQueries(IApplicationDbContext applicationDbContext, ICurrentRequestService currentRequestService, ICacheService cacheService)
        {
            _applicationDbContext = applicationDbContext;
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
        }

        public Task<Result<List<DtoConcern>>> GetAllAsync(DtoConcernFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> GetCountAsync(DtoConcernFilter filter)
        {
            var concerns = _applicationDbContext.Concerns.Where(c => c.RiceMillId == _currentRequestService.RiceMillId).AsQueryable();
            if (filter != null && filter.Title.IsNotNullOrEmpty())
                concerns = concerns.Where(c => c.Title.Contains(filter.Title));

            return Task.FromResult(Result<int>.Success(concerns.Count()));
        }
    }
}