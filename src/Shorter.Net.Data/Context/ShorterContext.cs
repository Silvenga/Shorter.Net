namespace Shorter.Net.Data.Context
{
    using System.Data.Entity;

    using Shorter.Net.Data.Models;

    public class ShorterContext : DbContext
    {
        public virtual DbSet<ShortenedUrl> ShortenedUrls { get; set; }
    }
}