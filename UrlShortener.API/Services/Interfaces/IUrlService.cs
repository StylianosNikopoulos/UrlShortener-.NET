using System;
using UrlShortener.API.Models;

namespace UrlShortener.API.Services.Interfaces
{
	public interface IUrlService
	{
		Task<string> CreateShortUrlAsync(string longUrl);
		Task<UrlMapping?> GetByShortCodeAsync(string shortCode);
		Task IncrementVisitCountAsync(UrlMapping mapping);
	}
}