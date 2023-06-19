﻿using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.Base;
using RiceMill.Application.UseCases.Village.Dto;

namespace RiceMill.Application.UseCases.Village
{
    public interface IVillageQueries : IBaseUseCaseQueries
    {
        Task<Result<DtoVillage>> GetAsync(int id);

        Task<Result<List<DtoVillage>>> GetAllAsync();
    }

    public class VillageQueries : IVillageQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public VillageQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<List<DtoVillage>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<DtoVillage>> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}