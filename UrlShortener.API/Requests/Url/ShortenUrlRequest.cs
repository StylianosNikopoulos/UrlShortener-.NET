using System;
namespace UrlShortener.API.Requests.Url
{
	public class ShortenUrlRequest
	{
        public string LongUrl { get; set; } = string.Empty;
    }
}