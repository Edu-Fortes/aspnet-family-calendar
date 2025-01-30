namespace Events.DB
{
    public record Event
    {
        public int Id { get; init; }
        public string? Title { get; init; }
        public string? Date { get; init; }
        public bool AllDay { get; init; }
        public string? Color { get; init; }
        public string? TextColor { get; init; }
    }

    public class EventDB
    {
        private static List<Event> _events = new List<Event>()
            {
                new() { Id = 1, Title="Evento 1",Date="2025-01-29",AllDay=true,Color="red",TextColor="white"},
                new() { Id = 2, Title="Evento 2",Date="2025-01-30",AllDay=true,Color="blue",TextColor="white"},
                new() { Id = 3, Title="Evento 3",Date="2025-01-31",AllDay=true,Color="green",TextColor="white"},
                new() { Id = 4, Title="Evento 4",Date="2025-02-01",AllDay=true,Color="yellow",TextColor="black"},
            };

        public static List<Event> GetEvents()
        {
            return _events;
        }

        public static Event? GetEvent(int id)
        {
            return _events.SingleOrDefault(e => e.Id == id); // e para evento. 'event' é uma palavra reservada no C#
        }

        public static Event CreateEvent(Event newEvent)
        {
            _events.Add(newEvent);
            return newEvent;
        }

        public static Event UpdateEvent(Event updateEvent)
        {
            var existingEvent = _events.SingleOrDefault(e => e.Id == updateEvent.Id);
            if (existingEvent != null)
            {
                _events.Remove(existingEvent);
                _events.Add(updateEvent);
            }
            return updateEvent;
        }

        public static void DeleteEvent(int id)
        {
            _events = _events.FindAll(e => e.Id != id).ToList();
        }
    }
}
