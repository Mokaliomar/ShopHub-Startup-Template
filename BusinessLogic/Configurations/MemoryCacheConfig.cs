using System;
using Microsoft.Extensions.Caching.Memory;

namespace BusinessLogic.Configurations;

public static class MemoryCacheConfig
{
    public static MemoryCacheEntryOptions Configuration()
    {
        var cacheEntryConfig = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromSeconds(45))
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
            .SetPriority(CacheItemPriority.Normal);

        return cacheEntryConfig;
    }
}
