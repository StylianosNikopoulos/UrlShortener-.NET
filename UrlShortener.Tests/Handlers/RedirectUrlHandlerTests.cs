using System;
using Moq;
using UrlShortener.API.Handlers.Url;
using UrlShortener.API.Models;
using UrlShortener.API.Requests.Url;
using UrlShortener.API.Services.Interfaces;

namespace UrlShortener.Tests.Handlers
{
	public class RedirectUrlHandlerTests
	{
		private readonly Mock<IUrlService> _mockUrlService;
		private readonly RedirectUrlHandler _redirectHandler;

		public RedirectUrlHandlerTests()
		{
			_mockUrlService = new Mock<IUrlService>();
            _redirectHandler = new RedirectUrlHandler(_mockUrlService.Object); 
		}


		[Fact]
		public async Task HandleRedirectUrlAsync_ReturnsFound()
		{
            var shortCode = "abcabc";
            var longUrl = "https://xyz.com";
            var mapping = new UrlMapping { ShortCode = shortCode, LongUrl = longUrl };

            _mockUrlService.Setup(s => s.GetByShortCodeAsync(shortCode))
                .ReturnsAsync(mapping);

            _mockUrlService.Setup(s => s.IncrementVisitCountAsync(mapping))
                .Returns(Task.CompletedTask);

            var request = new RedirectUrlRequest { ShortCode = shortCode };

            var result = await _redirectHandler.HandleRedirectUrlAsync(request);

            Assert.True(result.Found);
            Assert.Equal(longUrl, result.LongUrl);
            _mockUrlService.Verify(s => s.IncrementVisitCountAsync(mapping), Times.Once);
        }


        [Fact]
        public async Task HandleRedirectUrlAsync_ReturnsNotFound()
		{
            var shortCode = "abcabc";
            _mockUrlService.Setup(s => s.GetByShortCodeAsync(shortCode))
                .ReturnsAsync((UrlMapping?)null);

            var request = new RedirectUrlRequest { ShortCode = shortCode };

            var result = await _redirectHandler.HandleRedirectUrlAsync(request);

            Assert.False(result.Found);
            Assert.Equal(string.Empty, result.LongUrl);
            _mockUrlService.Verify(s => s.IncrementVisitCountAsync(It.IsAny<UrlMapping>()), Times.Never);
        }
    }
}

