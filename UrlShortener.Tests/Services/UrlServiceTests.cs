using System;
using Microsoft.EntityFrameworkCore;
using Moq;
using UrlShortener.API.Data;
using UrlShortener.API.Models;
using UrlShortener.API.Services;
using UrlShortener.API.Services.Interfaces;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace UrlShortener.Tests.Services
{
	public class UrlServiceTests
	{
        private readonly UrlDbContext _dbContext;
        private readonly Mock<IShortCodeGenerator> _mockShortCodeGenerator;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly UrlService _urlService;

        public UrlServiceTests()
		{
            var options = new DbContextOptionsBuilder<UrlDbContext>()
                .UseInMemoryDatabase(databaseName: "UrlShortenerTestDb")
                .Options;

            _dbContext = new UrlDbContext(options); 

            _mockShortCodeGenerator = new Mock<IShortCodeGenerator>();
            _mockConfiguration = new Mock<IConfiguration>();

            _mockConfiguration.Setup(c => c["Urls:BaseShortUrl"])
                    .Returns("https://localhost");

            _urlService = new UrlService(_dbContext, _mockShortCodeGenerator.Object, _mockConfiguration.Object);
        }


        [Fact]
        public async Task CreateShortUrlAsync_ShouldReturnShortUrlAndSave()
        {
            var longUrl = "https://xyz.com";
            var expectedShortCode = "abcabc";
            _mockShortCodeGenerator.Setup(s => s.Generate())
                .Returns(expectedShortCode);

            var result = await _urlService.CreateShortUrlAsync(longUrl);

            Assert.Equal($"https://localhost/{expectedShortCode}", result);
            var mapping = await _dbContext.UrlMappings.FirstOrDefaultAsync(m => m.ShortCode == expectedShortCode);
            Assert.NotNull(mapping);
            Assert.Equal(longUrl, mapping.LongUrl);
        }


        [Fact]
        public async Task GetByShortCodeAsync_ShouldReturnMappingUrl()
        {
            var mapping = new UrlMapping
            {
                LongUrl = "https://xyz.com",
                ShortCode = "abcabc"
            };

            _dbContext.UrlMappings.Add(mapping);
            await _dbContext.SaveChangesAsync();

            var result = await _urlService.GetByShortCodeAsync(mapping.ShortCode);

            Assert.NotNull(result);
            Assert.Equal(mapping.LongUrl, result.LongUrl);
        }


        [Fact]
        public async Task IncrementVisitCountAsync_ShouldIncreaseVisitCount()
        {
            var mapping = new UrlMapping
            {
                LongUrl = "https://xyz.com",
                ShortCode = "abcabc",
                VisitCount = 0
            };

            _dbContext.UrlMappings.Add(mapping);
            await _dbContext.SaveChangesAsync();

            await _urlService.IncrementVisitCountAsync(mapping);

            var updateCount = await _dbContext.UrlMappings.FirstOrDefaultAsync(u => u.ShortCode == mapping.ShortCode);
            Assert.Equal(1, updateCount.VisitCount);
        }
    }
}

