using System;
using Microsoft.EntityFrameworkCore;
using UrlShortener.API.Data;
using UrlShortener.API.Models;
using UrlShortener.API.Services.Interfaces;

namespace UrlShortener.API.Services
{
	public class UrlService : IUrlService
	{
        private readonly UrlDbContext _context;
        private readonly IShortCodeGenerator _generator;
        private readonly IConfiguration _configuration;

		public UrlService(UrlDbContext context, IShortCodeGenerator generator, IConfiguration configuration)
		{
            _configuration = configuration;
            _context = context;
            _generator = generator;
		}


        public async Task<string> CreateShortUrlAsync(string longUrl)
        {
            var existingUrl = await _context.UrlMappings.FirstOrDefaultAsync(x => x.LongUrl == longUrl);
            var baseUrl = _configuration["Urls:BaseShortUrl"] ?? throw new InvalidOperationException("BaseShortUrl is missing");

            if (existingUrl != null)
            {
                return $"{baseUrl}/{existingUrl.ShortCode}"; 
            }

            string shortCode;

            do
            {
                shortCode = _generator.Generate();
            }
            while (await _context.UrlMappings.AnyAsync(x => x.ShortCode == shortCode));

            var mapping = new UrlMapping
            {
                LongUrl = longUrl,
                ShortCode = shortCode,
                CreatedAt = DateTime.UtcNow
            };

            _context.UrlMappings.Add(mapping);
            await _context.SaveChangesAsync();

            return $"{baseUrl}/{shortCode}";
        }


        public async Task<UrlMapping?> GetByShortCodeAsync(string shortCode)
        {
            return await _context.UrlMappings.FirstOrDefaultAsync(x => x.ShortCode == shortCode);
        }


        public async Task IncrementVisitCountAsync(UrlMapping mapping)
        {
            mapping.VisitCount += 1;
            await _context.SaveChangesAsync();
        }
    }
}