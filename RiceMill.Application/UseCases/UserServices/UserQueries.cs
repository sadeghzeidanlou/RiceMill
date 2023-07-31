using Mapster;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.UserActivityServices;
using RiceMill.Application.UseCases.UserServices.Dto;
using RiceMill.Domain.Models;
using Shared.Enums;
using Shared.ExtensionMethods;
using System.Net;

namespace RiceMill.Application.UseCases.UserServices
{
    public interface IUserQueries
    {
        Result<PaginatedList<DtoUser>> GetAll(DtoUserFilter dtoUserFilter);

        Result<DtoUser> Login(DtoLogin login);
    }

    public class UserQueries : IUserQueries
    {
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;
        private readonly IUserActivityCommands _userActivityCommands;

        public UserQueries(ICacheService cacheService, ICurrentRequestService currentRequestService, IUserActivityCommands userActivityCommands)
        {
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
            _userActivityCommands = userActivityCommands;
        }

        public Result<PaginatedList<DtoUser>> GetAll(DtoUserFilter filter)
        {
            var users = GetFilter(filter);
            PagingInfo.ApplyPaging(filter, out var pageNumber, out var pageSize);
            var result = PaginatedList<DtoUser>.Create(users, pageNumber, pageSize);
            return Result<PaginatedList<DtoUser>>.Success(result);
        }

        public Result<DtoUser> Login(DtoLogin login)
        {
            var user = _cacheService.Get<List<User>>(EntityTypeEnum.Users)
                .Where(u => !u.IsDeleted && u.Username.Equals(login.UserName, StringComparison.InvariantCultureIgnoreCase) && u.Password.Equals(login.Password, StringComparison.InvariantCulture))
                .FirstOrDefault();

            if (user == null)
                return Result<DtoUser>.Failure(new Error(ResultStatusEnum.UserNotFound), HttpStatusCode.NotFound);

            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Login, EntityTypeEnum.Users, string.Empty, string.Empty, null);
            return Result<DtoUser>.Success(user.Adapt<DtoUser>());
        }

        private IQueryable<User> GetFilter(DtoUserFilter filter)
        {
            var users = _cacheService.Get<List<User>>(EntityTypeEnum.Users).AsQueryable();
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

            return users;
        }
    }
}