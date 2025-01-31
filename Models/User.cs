namespace server.Models
{
    public class User
    {
        public int UserID { get; set; }
        public required string Name { get; set; }
        public required string Color { get; set; }
        public required string TextColor { get; set; }

        // Navigation property
        public ICollection<Event> Events { get; set; }
    }
}
