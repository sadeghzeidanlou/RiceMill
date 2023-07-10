using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.UserServices.Dto;
using RiceMill.Domain.Models;
using Shared.Enums;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.UserServices
{
    public interface IUserQueries
    {
        Task<Result<PaginatedList<DtoUser>>> GetAllAsync(DtoUserFilter dtoUserFilter);
    }

    public class UserQueries : IUserQueries
    {
        private readonly ICacheService _cacheService;
        private readonly ICurrentRequestService _currentRequestService;

        public UserQueries(ICacheService cacheService, ICurrentRequestService currentRequestService)
        {
            _cacheService = cacheService;
            _currentRequestService = currentRequestService;
        }

        public async Task<Result<PaginatedList<DtoUser>>> GetAllAsync(DtoUserFilter filter)
        {
            var users = GetFilter(filter);
            PagingInfo.ApplyPaging(filter, out var pageNumber, out var pageSize);
            var result = PaginatedList<DtoUser>.CreateAsync(users, pageNumber, pageSize).Result;
            return await Task.FromResult(Result<PaginatedList<DtoUser>>.Success(result));
        }

        private IQueryable<User> GetFilter(DtoUserFilter filter)
        {
            var users = _cacheService.Get<List<User>>(nameof(EntityTypeEnum.Users)).Where(c => !c.IsDeleted).AsQueryable();
            if (filter == null || (_currentRequestService.IsNotAdmin && filter.RiceMillId.IsNullOrEmpty()))
                return users.Where(u => false);

            if (filter.RiceMillId.IsNotNullOrEmpty())
                users = users.Where(u => u.RiceMillId.Equals(filter.RiceMillId.Value));

            if (filter.Id.IsNotNullOrEmpty())
                users = users.Where(u => u.Id.Equals(filter.Id.Value));

            if (filter.Username.IsNotNullOrEmpty())
                users = users.Where(u => u.Username.Contains(filter.Username));

            if (filter.Password.IsNotNullOrEmpty())
                users = users.Where(u => u.Password.Contains(filter.Password));

            if (filter.Role.HasValue)
                users = users.Where(u => u.Role.Equals(filter.Role.Value));

            if (filter.UserPersonId.IsNotNullOrEmpty())
                users = users.Where(u => u.UserPersonId.Equals(filter.UserPersonId.Value));

            if (filter.ParentUserId.IsNotNullOrEmpty())
                users = users.Where(u => u.ParentUserId.Equals(filter.ParentUserId.Value));

            return users;
        }
    }
}