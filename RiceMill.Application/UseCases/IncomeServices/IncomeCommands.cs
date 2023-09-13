using Mapster;
using RiceMill.Application.Common.ExtensionMethods;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.IncomeServices.Dto;
using RiceMill.Application.UseCases.UserActivityServices;
using RiceMill.Domain.Models;
using Shared.Enums;
using Shared.ExtensionMethods;
using System.Net;

namespace RiceMill.Application.UseCases.IncomeServices
{
    public interface IIncomeCommands : IBaseUseCaseCommands
    {
        Result<DtoIncome> Create(DtoCreateIncome income);

        Result<DtoIncome> Update(DtoUpdateIncome income);
    }

    public sealed class IncomeCommands : IIncomeCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;
        private readonly IUserActivityCommands _userActivityCommands;
        private readonly EntityTypeEnum _Key = EntityTypeEnum.Incomes;

        public IncomeCommands(IApplicationDbContext applicationDbContext, ICurrentRequestService currentRequestService, ICacheService cacheService, IUserActivityCommands userActivityCommands)
        {
            _applicationDbContext = applicationDbContext;
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
            _userActivityCommands = userActivityCommands;
        }

        public Result<DtoIncome> Create(DtoCreateIncome createIncome)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<DtoIncome>.Forbidden();

            var validationResult = createIncome.Validate();
            if (!validationResult.IsValid)
                return Result<DtoIncome>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var validateIncome = ValidateIncome(createIncome);
            if (validateIncome != null)
                return validateIncome;

            var income = createIncome.Adapt<Income>();
            income.UserId = _currentRequestService.UserId;
            _applicationDbContext.Incomes.Add(income);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.New, _Key, string.Empty, income.SerializeObject(), income.RiceMillId);
            _cacheService.Maintain(_Key, income);
            return Result<DtoIncome>.Success(income.Adapt<DtoIncome>());
        }

        public Result<DtoIncome> Update(DtoUpdateIncome updateIncome)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<DtoIncome>.Forbidden();

            var validationResult = updateIncome.Validate();
            if (!validationResult.IsValid)
                return Result<DtoIncome>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var income = GetIncomeById(updateIncome.Id);
            if (income == null)
                return Result<DtoIncome>.Failure(Error.CreateError(ResultStatusEnum.IncomeNotFound), HttpStatusCode.NotFound);

            var createIncome = updateIncome.Adapt<DtoCreateIncome>();
            createIncome = createIncome with { RiceMillId = income.RiceMillId };
            var validateIncome = ValidateIncome(createIncome);
            if (validateIncome != null)
                return validateIncome;

            var beforeEdit = income.SerializeObject();
            income = updateIncome.Adapt(income);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Edit, _Key, beforeEdit, income.SerializeObject(), income.RiceMillId);
            _cacheService.Maintain(_Key, income);
            return Result<DtoIncome>.Success(income.Adapt<DtoIncome>());
        }

        public Result<bool> Delete(Guid id)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<bool>.Forbidden();

            var income = GetIncomeById(id);
            if (income == null)
                return Result<bool>.Failure(Error.CreateError(ResultStatusEnum.IncomeNotFound), HttpStatusCode.NotFound);

            var beforeEdit = income.SerializeObject();
            _applicationDbContext.Incomes.Remove(income);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Delete, _Key, beforeEdit, income.SerializeObject(), income.RiceMillId);
            _cacheService.Maintain(_Key, income);
            return Result<bool>.Success(true);
        }

        private Income GetIncomeById(Guid id) => _applicationDbContext.Incomes.FirstOrDefault(c => c.Id.Equals(id));

        private Result<DtoIncome> ValidateIncome(DtoCreateIncome income) =>
            !_cacheService.GetRiceMills().Any(rm => rm.Id.Equals(income.RiceMillId)) ? Result<DtoIncome>.Failure(Error.CreateError(ResultStatusEnum.RiceMillNotFound), HttpStatusCode.NotFound) : null;
    }
}