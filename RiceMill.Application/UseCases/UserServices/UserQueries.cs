using Mapster;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.UserServices.Dto;
using RiceMill.Domain.Models;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.UserServices
{
    public interface IUserQueries
    {
        Task<Result<PaginatedList<DtoUser>>> GetAllAsync(DtoUserFilter dtoUserFilter);
    }

    public class UserQueries : IUserQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        private readonly ICurrentRequestService _currentRequestService;

        public UserQueries(IApplicationDbContext applicationDbContext, ICurrentRequestService currentRequestService)
        {
            _applicationDbContext = applicationDbContext;
            _currentRequestService = currentRequestService;
        }
        public async Task<Result<PaginatedList<DtoUser>>> GetAllAsync(DtoUserFilter filter)
        {
            var users = GetFilter(filter);
            PagingInfo.ApplyPaging(filter, out var pageNumber, out var pageSize);
            var tempResult = PaginatedList<User>.CreateAsync(users, pageNumber, pageSize).Result;
            var result = new PaginatedList<DtoUser>(tempResult.Items.Adapt<List<DtoUser>>(), tempResult.TotalCount, pageNumber, pageSize);
            return await Task.FromResult(Result<PaginatedList<DtoUser>>.Success(result));
        }

        private IQueryable<User> GetFilter(DtoUserFilter filter)
        {
            var concerns = _applicationDbContext.Users.AsQueryable();
            if (filter == null || (_currentRequestService.IsNotAdmin && filter.RiceMillId.IsNullOrEmpty()))
                return concerns.Where(c => false);

            if (_currentRequestService.IsNotAdmin)
                concerns = concerns.Where(c => c.RiceMillId == filter.RiceMillId.Value);

            return concerns;
        }
    }
}