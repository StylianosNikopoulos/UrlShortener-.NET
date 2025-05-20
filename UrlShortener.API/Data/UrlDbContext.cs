using System;
using Microsoft.EntityFrameworkCore;
using UrlShortener.API.Models;

namespace UrlShortener.API.Data
{
	public class UrlDbContext : DbContext
	{
		public UrlDbContext(DbContextOptions<UrlDbContext> options) : base(options)
		{
		}

		public DbSet<UrlMapping> UrlMappings => Set<UrlMapping>();
	}
}