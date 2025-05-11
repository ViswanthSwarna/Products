using System.ComponentModel.DataAnnotations;

namespace Products.Domain.Entities
{
    public class CodeTracker
    {
        [Required]
        public int Id {  get; set; }
        public int LastCode { get; set; }
    }
}
