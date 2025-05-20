using System;
namespace UrlShortener.API.Responses.Url
{
	public class RedirectUrlResponse
	{
        public string LongUrl { get; set; } = string.Empty;
        public bool Found { get; set; } = false;
    }
}