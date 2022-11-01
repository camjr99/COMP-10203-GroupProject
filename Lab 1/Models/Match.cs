using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lab_1.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }
        public int? TeamOneID { get; set; }
        [DisplayName("Team A")]
        public Team? TeamOne { get; set; }

        public int? TeamTwoID { get; set; }
        [DisplayName("Team B")]
        public Team? TeamTwo { get; set; }

        public int? ArenaID { get; set; }
        public Arena? Arena { get; set; }

        [Required, DisplayName("Team A Score")]
        public int TeamOneScore { get; set; }
        [Required, DisplayName("Team B Score")]
        public int TeamTwoScore { get; set; }
        [DataType(DataType.DateTime), DisplayName("Match Date")]
        public DateTime MatchDate { get; set; }
    }
}
