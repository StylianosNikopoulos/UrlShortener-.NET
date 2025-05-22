using System;
using System.ComponentModel.DataAnnotations;

namespace UrlShortener.MVC.Requests
{
	public class ShortenRequest
	{
        [Required]
        [Url]
        public string LongUrl { get; set; }
    }
}