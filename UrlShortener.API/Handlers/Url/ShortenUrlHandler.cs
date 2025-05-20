using System;
using UrlShortener.API.Requests.Url;
using UrlShortener.API.Responses.Url;
using UrlShortener.API.Services.Interfaces;

namespace UrlShortener.API.Handlers.Url
{
	public class ShortenUrlHandler
	{
		private readonly IUrlService _urlService;

		public ShortenUrlHandler(IUrlService urlService)
		{
			_urlService = urlService;
		}


		public async Task<ShortenUrlResponse> HandleShortenUrlAsync(ShortenUrlRequest request)
		{
			var shortUrl = await _urlService.CreateShortUrlAsync(request.LongUrl);

			return new ShortenUrlResponse
			{
				ShortUrl = shortUrl
			};
        }
	}
}