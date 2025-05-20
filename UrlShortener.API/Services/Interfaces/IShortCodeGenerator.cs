using System;
namespace UrlShortener.API.Services.Interfaces
{
	public interface IShortCodeGenerator
	{
		string Generate();
	}
}