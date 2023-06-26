using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.ConcernServices.Dto
{
    public record DtoCreateConcern(string Title)
    {
        public bool IsValid => Title.IsNotNullOrEmpty();
    }
}