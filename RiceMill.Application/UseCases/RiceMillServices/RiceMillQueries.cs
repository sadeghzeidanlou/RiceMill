﻿using Microsoft.EntityFrameworkCore;
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
            if (_currentRequestService.HasNotAccessToRiceMills)
                return await Task.FromResult(Result<PaginatedList<DtoRiceMill>>.Forbidden());

            var riceMilles = GetFilter(filter);
            PagingInfo.ApplyPaging(filter, out var pageNumber, out var pageSize);
            var result = PaginatedList<DtoRiceMill>.CreateAsync(riceMilles, pageNumber, pageSize).Result;
            return await Task.FromResult(Result<PaginatedList<DtoRiceMill>>.Success(result));
        }

        private IQueryable<Domain.Models.RiceMill> GetFilter(DtoRiceMillFilter filter)
        {
            var riceMilles = _applicationDbContext.RiceMills.AsNoTracking().AsQueryable();
            if (filter == null || (_currentRequestService.IsNotAdmin && filter.Id.IsNullOrEmpty()))
                return riceMilles.Where(rm => false);

            if (filter.Id.IsNotNullOrEmpty())
                riceMilles = riceMilles.Where(rm => rm.Id.Equals(filter.Id.Value));

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