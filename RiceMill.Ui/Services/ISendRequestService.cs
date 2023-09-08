using RiceMill.Ui.Common.Models;

namespace RiceMill.Ui.Services
{
    internal interface ISendRequestService
    {
        Task<TOut> SendRequestAsync<TIn, TOut>(TIn requestObject, DtoSendRequest sendRequest) where TIn : class where TOut : class;
    }
}