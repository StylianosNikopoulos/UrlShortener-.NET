using System;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.API.Handlers.Url;
using UrlShortener.API.Requests.Url;
using UrlShortener.API.Responses.Url;

namespace UrlShortener.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UrlController : ControllerBase
    {
        private readonly ShortenUrlHandler _shortenUrlHandler;
        private readonly RedirectUrlHandler _redirectUrlHandler;

        public UrlController(ShortenUrlHandler shortenUrlHandler, RedirectUrlHandler redirectUrlHandler)
        {
            _shortenUrlHandler = shortenUrlHandler;
            _redirectUrlHandler = redirectUrlHandler;
        }


        [HttpPost("shorten")]
        public async Task<ActionResult<ShortenUrlResponse>> ShortenUrl([FromBody] ShortenUrlRequest request)
        {
            var response = await _shortenUrlHandler.HandleShortenUrlAsync(request);
            return Ok(response);
        }


        [HttpGet("/{shortCode}")]
        public async Task<IActionResult> RedirectToLongUrl(string shortCode)
        {
            var response = await _redirectUrlHandler.HandleRedirectUrlAsync(new RedirectUrlRequest { ShortCode = shortCode });

            if (!response.Found)
                return NotFound("Short URL not found.");

            return Redirect(response.LongUrl); 
        }
    }
}