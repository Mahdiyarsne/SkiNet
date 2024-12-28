using System.Text.Json;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Services
{
	public class ResponseCacheService(IConnectionMultiplexer radis) : IResponseCacheService
	{

		private readonly IDatabase _database = radis.GetDatabase(1);
		public async Task CacheResponseAsync(string cacheKey, object respnse, TimeSpan timeSpan)
		{
		  var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

			var serializedResponse =  JsonSerializer.Serialize(respnse, options);

			await _database.StringSetAsync(cacheKey, serializedResponse, timeSpan);
		}

		public async Task<string?> GetCacheResponseAsync(string cacheKey)
		{
			var cacheResponse = await _database.StringGetAsync(cacheKey);

			if(cacheResponse.IsNullOrEmpty) return null;

			return cacheResponse;
		}

		public Task RemoveCacheByPattern(string pattern)
		{
			throw new NotImplementedException();
		}
	}
}
