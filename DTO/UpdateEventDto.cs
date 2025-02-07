namespace server.DTO
{
    public class UpdateEventDto
    {
        public string? Title { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public bool? AllDay { get; set; }
        public int? UserId { get; set; }

    }
}
