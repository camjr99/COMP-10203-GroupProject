
using System.ComponentModel.DataAnnotations;

namespace Lab_1.Models
{
    public class Arena
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
