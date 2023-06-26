namespace RiceMill.Application.Common.Interfaces
{
    public interface ICurrentRequestService
    {
        public Guid UserId { get; }

        bool IsAuthenticated => UserId != Guid.Empty && UserId != default;
    }
}