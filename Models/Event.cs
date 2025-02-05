namespace server.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public required string Title { get; set; }
        public required DateTime Start { get; set; }
        public required DateTime End { get; set; }
        public required bool AllDay { get; set; }
        public string? ExtendedProps { get; set; }

        // Foreign key
        public int UserId { get; set; }

        // Navigation property
        public User? User { get; set; }
    }
}
