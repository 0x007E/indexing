using System.ComponentModel.DataAnnotations;

namespace RaGae.Indexing.Model
{
    public class FileModel : AbstractModel
    {
        [StringLength(255)]
        public string Extension { get; set; }
    }
}