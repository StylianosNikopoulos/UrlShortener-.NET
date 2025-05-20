using System;
using UrlShortener.API.Services.Interfaces;

namespace UrlShortener.API.Services.Factories
{
	public class DefaultShortCodeGenerator : IShortCodeGenerator
    {
        public string Generate()
        {
            return Guid.NewGuid().ToString("N")[..6];
        }
    }
}