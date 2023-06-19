using RiceMill.Application.Common.Models.ExternalDto;

namespace RiceMill.Application.Common.Interfaces.ExternalServices
{
    internal interface IEmailService
    {
        Task Send(DtoEmailDetail emailDetail);
    }
}