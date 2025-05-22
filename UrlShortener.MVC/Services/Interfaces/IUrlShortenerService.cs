using System;
using UrlShortener.MVC.Responses;

namespace UrlShortener.MVC.Services.Interfaces
{
	public interface IUrlShortenerService
	{
        Task<ShortenResponse?> ShortenUrlAsync(string longUrl);
        Task<string?> ResolveUrlAsync(string shortCode);
    }
}