using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Lab_1.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public bool isPlayer { get; set; }
        public int? TeamId { get; set; }
        public Team? Team { get; set; }
    }
}
