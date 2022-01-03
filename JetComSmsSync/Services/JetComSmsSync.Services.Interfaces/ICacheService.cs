using System.Collections.Generic;

namespace JetComSmsSync.Services.Interfaces
{
    public interface ICacheService
    {
        void Append<T>(string key, IEnumerable<T> items);
        void ClearCache();
        long GetCacheSize();
        string GetCacheSizeStr();
        bool TryRead<T>(string key, out T[] output);
    }
}