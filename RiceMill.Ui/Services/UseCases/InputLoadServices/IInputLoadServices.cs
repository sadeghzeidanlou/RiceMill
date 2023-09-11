using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.InputLoadServices.Dto;

namespace RiceMill.Ui.Services.UseCases.InputLoadServices
{
    internal interface IInputLoadServices
    {
        Task<Result<PaginatedList<DtoInputLoad>>> Get(DtoInputLoadFilter filter);

        Task<Result<DtoInputLoad>> Update(DtoUpdateInputLoad dtoUpdate);

        Task<Result<DtoInputLoad>> Add(DtoCreateInputLoad dtoCreate);

        Task<Result<bool>> Delete(Guid id);
    }
}