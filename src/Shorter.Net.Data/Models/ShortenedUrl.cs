namespace Shorter.Net.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ShortenedUrl
    {
        public long ShortenedUrlId { get; set; }

        [Index, StringLength(255)]
        public string Long { get; set; }

        [Index, StringLength(32)]
        public string Alias { get; set; }
    }
}