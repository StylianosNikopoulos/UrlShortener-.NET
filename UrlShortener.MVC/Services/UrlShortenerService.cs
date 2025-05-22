using System;
using System.Text;
using System.Text.Json;
using UrlShortener.MVC.Requests;
using UrlShortener.MVC.Responses;
using UrlShortener.MVC.Services.Interfaces;

namespace UrlShortener.MVC.Services
{
	public class UrlShortenerService : IUrlShortenerService
    {
        private readonly HttpClient _httpClient;

		public UrlShortenerService(HttpClient httpClient)
		{
            _httpClient = httpClient;
		}


        public async Task<ShortenResponse?> ShortenUrlAsync(string longUrl)
        {
            var request = new ShortenRequest { LongUrl = longUrl };
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Url/shorten", content);

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ShortenResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }


        public async Task<string?> ResolveUrlAsync(string shortCode)
        {
            var response = await _httpClient.GetAsync($"api/Url/{shortCode}");

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<string>(json);
        }
    }
}