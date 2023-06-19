using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.Base;
using RiceMill.Application.UseCases.Person.Dto;

namespace RiceMill.Application.UseCases.Person
{
    public interface IPersonCommands : IBaseUseCaseCommands
    {
        Task<Result<DtoPerson>> CreateAsync(DtoCreatePerson person);

        Task<Result<DtoPerson>> UpdateAsync(DtoUpdatePerson person);
    }

    public class PersonCommands : IPersonCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public PersonCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<DtoPerson>> CreateAsync(DtoCreatePerson person)
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

        public Task<Result<DtoPerson>> UpdateAsync(DtoUpdatePerson person)
        {
            throw new NotImplementedException();
        }
    }
}