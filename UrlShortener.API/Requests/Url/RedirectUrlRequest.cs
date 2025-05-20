using System;
namespace UrlShortener.API.Requests.Url
{
	public class RedirectUrlRequest
	{
        public string ShortCode { get; set; } = string.Empty;
    }
}