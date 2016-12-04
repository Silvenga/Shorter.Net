using System.ComponentModel.DataAnnotations;

namespace Shorter.Data.Interfaces
{
    public interface IEntity
    {
        [Key]
        int Id { get; set; }
    }
}