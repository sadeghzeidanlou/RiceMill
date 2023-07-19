﻿using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.ConcernServices.Dto;
using RiceMill.Domain.Models;
using Shared.Enums;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.ConcernServices
{
    public interface IConcernQueries
    {
        Result<PaginatedList<DtoConcern>> GetAll(DtoConcernFilter filter);
    }

    public class ConcernQueries : IConcernQueries
    {
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;

        public ConcernQueries(ICurrentRequestService currentRequestService, ICacheService cacheService)
        {
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
        }

        public Result<PaginatedList<DtoConcern>> GetAll(DtoConcernFilter filter)
        {
            var concerns = GetFilter(filter);
            PagingInfo.ApplyPaging(filter, out var pageNumber, out var pageSize);
            var result = PaginatedList<DtoConcern>.Create(concerns, pageNumber, pageSize);
            return Result<PaginatedList<DtoConcern>>.Success(result);
        }

        private IQueryable<Concern> GetFilter(DtoConcernFilter filter)
        {
            var concerns = _cacheService.Get<List<Concern>>(nameof(EntityTypeEnum.Concerns)).Where(c => !c.IsDeleted).AsQueryable();
            if (filter == null || (_currentRequestService.IsNotAdmin && filter.RiceMillId.IsNullOrEmpty()))
                return concerns.Where(c => false);

            if (filter.RiceMillId.IsNotNullOrEmpty())
                concerns = concerns.Where(c => c.RiceMillId.Equals(filter.RiceMillId));

            if (filter.Title.IsNotNullOrEmpty())
                concerns = concerns.Where(c => c.Title.Contains(filter.Title));

            return concerns;
        }
    }
}