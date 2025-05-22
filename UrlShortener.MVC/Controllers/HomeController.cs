using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.MVC.Models;
using UrlShortener.MVC.Requests;
using UrlShortener.MVC.Services.Interfaces;

namespace UrlShortener.MVC.Controllers;

public class HomeController : Controller
{
    private readonly IUrlShortenerService _shortener;

    public HomeController(IUrlShortenerService shortener)
    {
        _shortener = shortener;
    }


    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> ShortenUrl([FromBody] ShortenRequest request)
    {
        if (!ModelState.IsValid || string.IsNullOrWhiteSpace(request?.LongUrl))
            return BadRequest(new { error = "Please enter a valid URL." });

        try
        {
            var shortUrl = await _shortener.ShortenUrlAsync(request.LongUrl);
            if (shortUrl == null)
                return BadRequest(new { error = "Some error occurred." });

            return Json(shortUrl);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }


    [HttpGet("{shortCode}")]
    public async Task<IActionResult> RedirectToLongUrl(string shortCode)
    {
        var longUrl = await _shortener.ResolveUrlAsync(shortCode);

        if (string.IsNullOrWhiteSpace(longUrl))
            return NotFound("Short URL not found.");

        return Redirect(longUrl);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}