

namespace Core.Interfaces
{
	 public interface IResponseCacheService
	{
		Task CacheResponseAsync(string cacheKey, object respnse, TimeSpan timeSpan);

		Task<string?> GetCacheResponseAsync(string cacheKey);

		Task RemoveCacheByPattern(string pattern);
	}
}
