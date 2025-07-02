using Microsoft.Extensions.Caching.Memory;

namespace LearnLink_Backend.Services;

public class CacheService(IMemoryCache memoryCache)
{
    public dynamic? GetValue(string key, string identifier)
    {
        return memoryCache.Get<dynamic>($"{key}:{identifier}");
    }

    public void SetValue(string key, string identifier, object value, int expiration)
    {
        memoryCache.Set
        (
            $"{key}:{identifier}",
            value,
            DateTime.Now.AddMinutes(expiration)
        );
    }
}