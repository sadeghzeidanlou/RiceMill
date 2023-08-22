using RiceMill.Ui.Common.Models;

namespace RiceMill.Ui.Services
{
    public interface ISendRequestService
    {
        TOut SendRequest<TIn, TOut>(TIn requestObject, DtoSendRequest sendRequest) where TIn : class where TOut : class;
    }
}