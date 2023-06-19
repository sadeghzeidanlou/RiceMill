using RiceMill.Application.Common.Models.ExternalDto;

namespace RiceMill.Application.Common.Interfaces.ExternalServices
{
    public interface ISmsService
    {
        Task SendAsync(DtoSmsDetail smsDetail);
    }
}