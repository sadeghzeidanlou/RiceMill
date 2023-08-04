using Mapster;
using RiceMill.Application.Common.ExtensionMethods;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.PersonServices.Dto;
using RiceMill.Application.UseCases.UserActivityServices;
using RiceMill.Domain.Models;
using Shared.Enums;
using Shared.ExtensionMethods;
using System.Net;

namespace RiceMill.Application.UseCases.PersonServices
{
    public interface IPersonCommands : IBaseUseCaseCommands
    {
        Result<DtoPerson> Create(DtoCreatePerson person);

        Result<DtoPerson> Update(DtoUpdatePerson person);
    }

    public class PersonCommands : IPersonCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;
        private readonly IUserActivityCommands _userActivityCommands;
        private readonly EntityTypeEnum _Key = EntityTypeEnum.People;

        public PersonCommands(IApplicationDbContext applicationDbContext, ICurrentRequestService currentRequestService, ICacheService cacheService, IUserActivityCommands userActivityCommands)
        {
            _applicationDbContext = applicationDbContext;
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
            _userActivityCommands = userActivityCommands;
        }

        public Result<DtoPerson> Create(DtoCreatePerson createPerson)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<DtoPerson>.Forbidden();

            var validationResult = createPerson.Validate();
            if (!validationResult.IsValid)
                return Result<DtoPerson>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var person = createPerson.Adapt<Person>();
            _applicationDbContext.People.Add(person);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.New, _Key, string.Empty, person.SerializeObject(), person.RiceMillId);
            _cacheService.Maintain(_Key, person);
            return Result<DtoPerson>.Success(person.Adapt<DtoPerson>());
        }

        public Result<DtoPerson> Update(DtoUpdatePerson updatePerson)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<DtoPerson>.Forbidden();

            var validationResult = updatePerson.Validate();
            if (!validationResult.IsValid)
                return Result<DtoPerson>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var person = GetPersonById(updatePerson.Id);
            if (person == null)
                return Result<DtoPerson>.Failure(new Error(ResultStatusEnum.PersonNotFound), HttpStatusCode.NotFound);

            var beforeEdit = person.SerializeObject();
            person = updatePerson.Adapt(person);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Edit, _Key, beforeEdit, person.SerializeObject(), person.RiceMillId);
            _cacheService.Maintain(_Key, person);
            return Result<DtoPerson>.Success(person.Adapt<DtoPerson>());
        }

        public Result<bool> Delete(Guid id)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<bool>.Forbidden();

            var person = GetPersonById(id);
            if (person == null)
                return Result<bool>.Failure(new Error(ResultStatusEnum.PersonNotFound), HttpStatusCode.NotFound);

            var beforeEdit = person.SerializeObject();
            _applicationDbContext.People.Remove(person);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Delete, _Key, beforeEdit, person.SerializeObject(), person.RiceMillId);
            _cacheService.Maintain(_Key, person);
            return Result<bool>.Success(true);
        }

        private Person GetPersonById(Guid id) => _applicationDbContext.People.FirstOrDefault(c => c.Id == id);
    }
}