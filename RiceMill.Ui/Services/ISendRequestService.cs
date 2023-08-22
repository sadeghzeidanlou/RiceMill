namespace RiceMill.Ui.Services
{
    public interface ISendRequestService
    {
        Task<TOut> SendRequestAsync<TIn, TOut>(TIn requestObject, Uri baseAddress, Dictionary<string, string> customHeaders);

        TOut SendRequest<TIn, TOut>(TIn requestObject, Uri baseAddress, Dictionary<string, string> customHeaders);
    }
}