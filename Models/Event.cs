namespace server.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public required string Title { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required bool AllDay { get; set; }

        // Foreign key
        public int UserId { get; set; }

        // Navigation property
        public User? User { get; set; }
    }
}
