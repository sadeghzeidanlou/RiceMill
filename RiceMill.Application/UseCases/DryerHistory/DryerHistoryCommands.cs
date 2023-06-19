﻿using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.Base;
using RiceMill.Application.UseCases.DryerHistory.Dto;

namespace RiceMill.Application.UseCases.DryerHistory
{
    public interface IDryerHistoryCommands : IBaseUseCaseCommands
    {
        Task<Result<DtoDryerHistory>> CreateAsync(DtoCreateDryerHistory dryerHistory);

        Task<Result<DtoDryerHistory>> UpdateAsync(DtoUpdateDryerHistory dryerHistory);
    }

    public class DryerHistoryCommands : IDryerHistoryCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DryerHistoryCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<DtoDryerHistory>> CreateAsync(DtoCreateDryerHistory dryerHistory)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> DeleteAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<DtoDryerHistory>> UpdateAsync(DtoUpdateDryerHistory dryerHistory)
        {
            throw new NotImplementedException();
        }
    }
}