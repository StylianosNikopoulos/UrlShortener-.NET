using System;
using UrlShortener.API.Requests.Url;
using UrlShortener.API.Responses.Url;
using UrlShortener.API.Services.Interfaces;

namespace UrlShortener.API.Handlers.Url
{
	public class RedirectUrlHandler
	{
		private readonly IUrlService _urlService;

		public RedirectUrlHandler(IUrlService urlService)
		{
			_urlService = urlService;
		}


		public async Task<RedirectUrlResponse> HandleRedirectUrlAsync(RedirectUrlRequest request)
		{
			var mapping = await _urlService.GetByShortCodeAsync(request.ShortCode);

			if (mapping == null)
				return new RedirectUrlResponse { Found = false };

			await _urlService.IncrementVisitCountAsync(mapping);

			return new RedirectUrlResponse
			{
				Found = true,
				LongUrl = mapping.LongUrl
			};
		}
	}
}