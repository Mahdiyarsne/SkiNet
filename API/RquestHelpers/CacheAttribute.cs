using System.Text;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualBasic;

namespace API.RquestHelpers
{
	[AttributeUsage(AttributeTargets.All)]
	public class CacheAttribute (int timeSpan) : Attribute, IAsyncActionFilter
	{
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

			var cacheKey = GenericCacheKeyFromRequest(context.HttpContext.Request);

			var cacheResponse = await cacheService.GetCacheResponseAsync(cacheKey);

			if (!string.IsNullOrEmpty(cacheResponse))
			{
				var contentResult = new ContentResult
				{
					Content = cacheResponse,
					ContentType = "application/json",
					StatusCode = 200,
				};
				context.Result = contentResult;


				var executedContext = await next();
				if (executedContext.Result is OkObjectResult okObjectResult) 
				{ 
				  if(okObjectResult.Value != null)
					{
						await cacheService.CacheResponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(timeSpan));
					}
				
				}
			}
		}

		private string GenericCacheKeyFromRequest(HttpRequest request)
		{
			var keyBuilder = new StringBuilder();
			keyBuilder.Append($"{request.Path}");

            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
				keyBuilder.Append($"|{key}-{value}");
            }
			return keyBuilder.ToString();
        }
	}
}
