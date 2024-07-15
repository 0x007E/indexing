using System.ComponentModel.DataAnnotations;

namespace RaGae.Indexing.Model
{
    public class AbstractModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Range(0, long.MaxValue)]
        public virtual long Size { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime LastModified { get; set; }
    }
}