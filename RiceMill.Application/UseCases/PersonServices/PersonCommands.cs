using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.PersonServices.Dto;

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

        public PersonCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Result<DtoPerson> Create(DtoCreatePerson person)
        {
            throw new NotImplementedException();
        }

        public Result<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Result<DtoPerson> Update(DtoUpdatePerson person)
        {
            throw new NotImplementedException();
        }
    }
}