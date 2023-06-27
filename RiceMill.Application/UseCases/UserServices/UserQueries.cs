﻿using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.UserServices.Dto;

namespace RiceMill.Application.UseCases.UserServices
{
    public interface IUserQueries
    {
        Task<Result<int>> GetCountAsync();

        Task<Result<List<DtoUser>>> GetAllAsync();
    }

    public class UserQueries : IUserQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public UserQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<List<DtoUser>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}