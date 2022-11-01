namespace Lab_1.Models
{
    public class PlayerViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public byte[]? ProfilePicture { get; set; }
    }
}
