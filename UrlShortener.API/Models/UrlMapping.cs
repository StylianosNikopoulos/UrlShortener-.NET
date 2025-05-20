using System;
using System.ComponentModel.DataAnnotations;

namespace UrlShortener.API.Models
{
    public class UrlMapping
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string LongUrl { get; set; } = string.Empty;

        [Required]
        public string ShortCode { get; set; } = string.Empty;

        public int VisitCount { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}