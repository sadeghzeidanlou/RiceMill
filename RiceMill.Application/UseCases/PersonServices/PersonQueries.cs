using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.PersonServices.Dto;
using RiceMill.Domain.Models;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.PersonServices
{
    public interface IPersonQueries
    {
        Result<PaginatedList<DtoPerson>> GetAll(DtoPersonFilter filter);
    }

    public sealed class PersonQueries : IPersonQueries
    {
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;

        public PersonQueries(ICurrentRequestService currentRequestService, ICacheService cacheService)
        {
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
        }

        public Result<PaginatedList<DtoPerson>> GetAll(DtoPersonFilter filter)
        {
            var people = GetFilter(filter).OrderByDescending(x => x.UpdateTime);
            PagingInfo.ApplyPaging(filter, out var pageNumber, out var pageSize);
            var result = PaginatedList<DtoPerson>.Create(people, pageNumber, pageSize);
            return Result<PaginatedList<DtoPerson>>.Success(result);
        }

        private IQueryable<Person> GetFilter(DtoPersonFilter filter)
        {
            var people = _cacheService.GetPeople();
            if (filter == null)
                return people.Where(rm => false);

            if (_currentRequestService.IsNotAdmin)
            {
                if (_currentRequestService.RiceMillId.IsNullOrEmpty())
                    return people.Where(rm => false);

                people = people.Where(rm => rm.RiceMillId.Equals(_currentRequestService.RiceMillId.Value));
            }
            if (filter.Id.IsNotNullOrEmpty())
                people = people.Where(p => p.Id.Equals(filter.Id.Value));

            if (filter.Ids.IsCollectionNotNullOrEmpty())
                people = people.Where(p => filter.Ids.Contains(p.Id));

            if (filter.RiceMillId.IsNotNullOrEmpty())
                people = people.Where(p => p.RiceMillId.Equals(filter.RiceMillId));

            if (filter.Name.IsNotNullOrEmpty())
                people = people.Where(p => p.Name.Contains(filter.Name));

            if (filter.Family.IsNotNullOrEmpty())
                people = people.Where(p => p.Family.Contains(filter.Family));

            if (filter.Gender.HasValue)
                people = people.Where(p => p.Gender.Equals(filter.Gender.Value));

            if (filter.MobileNumber.IsNotNullOrEmpty())
                people = people.Where(p => p.MobileNumber.Contains(filter.MobileNumber));

            if (filter.HomeNumber.IsNotNullOrEmpty())
                people = people.Where(p => p.HomeNumber.Contains(filter.HomeNumber));

            if (filter.NoticesType.HasValue)
                people = people.Where(p => p.NoticesType.Equals(filter.NoticesType.Value));

            if (filter.Address.IsNotNullOrEmpty())
                people = people.Where(p => p.Address.Contains(filter.Address));

            if (filter.FatherName.IsNotNullOrEmpty())
                people = people.Where(p => p.FatherName.Contains(filter.FatherName));

            return people;
        }
    }
}