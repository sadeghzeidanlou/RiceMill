namespace RiceMill.Application.Common.Interfaces
{
    public interface ICacheService
    {
        T Get<T>(string key);

        void Set<T>(string key, T value);
    }
}