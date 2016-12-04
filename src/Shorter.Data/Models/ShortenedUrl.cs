using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Shorter.Data.Interfaces;

namespace Shorter.Data.Models
{
    public class ShortenedUrl : IEntity, IAuditable
    {
        [Key]
        public int Id { get; set; }

        [StringLength(16), Index(IsUnique = true)]
        public string Slug { get; set; }

        [StringLength(256)]
        public string Url { get; set; }

        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public DateTimeOffset? LastUsedOn { get; set; }
    }
}