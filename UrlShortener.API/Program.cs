using Microsoft.EntityFrameworkCore;
using UrlShortener.API.Data;
using UrlShortener.API.Handlers.Url;
using UrlShortener.API.Requests.Url;
using UrlShortener.API.Services;
using UrlShortener.API.Services.Factories;
using UrlShortener.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UrlDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ShortenUrlHandler>();
builder.Services.AddScoped<RedirectUrlHandler>();

builder.Services.AddScoped<IUrlService, UrlService>();
builder.Services.AddScoped<IShortCodeGenerator, DefaultShortCodeGenerator>();


var app = builder.Build();

app.MapGet("/{shortCode}", async (string shortCode, HttpContext http, RedirectUrlHandler handler) =>
{
    var result = await handler.HandleRedirectUrlAsync(new RedirectUrlRequest { ShortCode = shortCode });

    if (!result.Found || string.IsNullOrWhiteSpace(result.LongUrl))
        return Results.NotFound("Short URL not found");

    return Results.Redirect(result.LongUrl);
});


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();