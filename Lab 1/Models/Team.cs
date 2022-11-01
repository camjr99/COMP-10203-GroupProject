using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Media;
using System.Runtime.InteropServices;

namespace Lab_1.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        [Required, DisplayName("Team Name")]
        public string TeamName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.DateTime), DisplayName("Established Date")]
        public DateTime EstablishedDate { get; set; }
        [DisplayName("Team Picture")]
        public byte[]? TeamPicture { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }
    }
}
