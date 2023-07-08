using Mapster;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.RiceMillServices.Dto;
using Shared.ExtensionMethods;
using System.Net;

namespace RiceMill.Application.UseCases.RiceMillServices
{
    public interface IRiceMillQueries
    {
        Task<Result<PaginatedList<DtoRiceMill>>> GetAllAsync(DtoRiceMillFilter riceMillFilter);
    }

    public class RiceMillQueries : IRiceMillQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentRequestService _currentRequestService;

        public RiceMillQueries(IApplicationDbContext applicationDbContext, ICurrentRequestService currentRequestService)
        {
            _applicationDbContext = applicationDbContext;
            _currentRequestService = currentRequestService;
        }

        public async Task<Result<PaginatedList<DtoRiceMill>>> GetAllAsync(DtoRiceMillFilter filter)
        {
            if (!_currentRequestService.HasAccessToRiceMills)
                return await Task.FromResult(Result<PaginatedList<DtoRiceMill>>.Failure(new Error(ResultStatusEnum.Forbidden), HttpStatusCode.Forbidden));

            var riceMilles = GetFilter(filter);
            PagingInfo.ApplyPaging(filter, out var pageNumber, out var pageSize);
            var tempResult = PaginatedList<Domain.Models.RiceMill>.CreateAsync(riceMilles, pageNumber, pageSize).Result;
            var result = new PaginatedList<DtoRiceMill>(tempResult.Items.Adapt<List<DtoRiceMill>>(), tempResult.TotalCount, pageNumber, pageSize);
            return await Task.FromResult(Result<PaginatedList<DtoRiceMill>>.Success(result));
        }

        private IQueryable<Domain.Models.RiceMill> GetFilter(DtoRiceMillFilter filter)
        {
            var riceMilles = _applicationDbContext.RiceMills.AsQueryable();
            if (_currentRequestService.IsNotAdmin)
            {
                var currentUser = _applicationDbContext.Users.Where(u => u.Id == _currentRequestService.UserId).FirstOrDefault();
                if (currentUser == null)
                    return riceMilles.Where(rm => false);

                riceMilles = riceMilles.Where(rm => rm.Id == currentUser.RiceMillId.Value);
            }
            else
            {
                if (filter != null && filter.Id.IsNotNullOrEmpty())
                    riceMilles = riceMilles.Where(rm => rm.Id == filter.Id.Value);
            }
            if (filter == null)
                return riceMilles;

            if (filter.Title.IsNotNullOrEmpty())
                riceMilles = riceMilles.Where(rm => rm.Title.Contains(filter.Title));

            if (filter.Address.IsNotNullOrEmpty())
                riceMilles = riceMilles.Where(rm => rm.Address.Contains(filter.Address));

            if (filter.Wage.HasValue)
                riceMilles = riceMilles.Where(rm => rm.Wage == filter.Wage.Value);

            if (filter.WageGreeterThan.HasValue)
                riceMilles = riceMilles.Where(rm => rm.Wage > filter.WageGreeterThan.Value);

            if (filter.WageLowerThan.HasValue)
                riceMilles = riceMilles.Where(rm => rm.Wage < filter.WageLowerThan.Value);

            if (filter.Phone.IsNotNullOrEmpty())
                riceMilles = riceMilles.Where(rm => rm.Phone.Contains(filter.Phone));

            if (filter.PostalCode.IsNotNullOrEmpty())
                riceMilles = riceMilles.Where(rm => rm.PostalCode.Contains(filter.PostalCode));

            if (filter.Description.IsNotNullOrEmpty())
                riceMilles = riceMilles.Where(rm => rm.Description.Contains(filter.Description));

            if (filter.OwnerPersonId.IsNotNullOrEmpty())
                riceMilles = riceMilles.Where(rm => rm.OwnerPersonId == filter.OwnerPersonId.Value);

            return riceMilles;
        }
    }
}