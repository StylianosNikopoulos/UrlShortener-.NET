using System;
using Moq;
using UrlShortener.API.Handlers.Url;
using UrlShortener.API.Requests.Url;
using UrlShortener.API.Services.Interfaces;

namespace UrlShortener.Tests.Handlers
{
	public class ShortenUrlHandlerTests
	{
		private readonly Mock<IUrlService> _mockUrlService;
        private readonly ShortenUrlHandler _shortenHandler;

        public ShortenUrlHandlerTests()
		{
            _mockUrlService = new Mock<IUrlService>();
            _shortenHandler = new ShortenUrlHandler(_mockUrlService.Object);
        }


        [Fact]
        public async Task HandleShortenUrlAsync_ReturnsShortenUrlResponse()
        {
            var longUrl = "https://xyz.com/fas/afs/dsa";
            var expectedShortUrl = "https://localhost/hsajks";

            _mockUrlService.Setup(s => s.CreateShortUrlAsync(longUrl))
                .ReturnsAsync(expectedShortUrl);

            var request = new ShortenUrlRequest { LongUrl = longUrl };

            var response = await _shortenHandler.HandleShortenUrlAsync(request);

            Assert.NotNull(response);
            Assert.Equal(expectedShortUrl, response.ShortUrl);
            _mockUrlService.Verify(s => s.CreateShortUrlAsync(longUrl), Times.Once);
        }
    }
}

