﻿using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.Base;
using RiceMill.Application.UseCases.RiceMill.Dto;

namespace RiceMill.Application.UseCases.RiceMill
{
    public interface IRiceMillQueries : IBaseUseCaseQueries
    {
        Task<Result<DtoRiceMill>> GetAsync(int id);

        Task<Result<List<DtoRiceMill>>> GetAllAsync();
    }

    public class RiceMillQueries : IRiceMillQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public RiceMillQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<List<DtoRiceMill>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<DtoRiceMill>> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}