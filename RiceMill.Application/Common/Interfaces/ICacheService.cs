using Shared.Enums;

namespace RiceMill.Application.Common.Interfaces
{
    public interface ICacheService
    {
        T Get<T>(EntityTypeEnum key);

        void Set<T>(EntityTypeEnum key, T value);

        void Maintain<T>(EntityTypeEnum key, T value);

        void LoadCache(List<EntityTypeEnum> entityTypes);
    }
}