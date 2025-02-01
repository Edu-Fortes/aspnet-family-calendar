using Microsoft.EntityFrameworkCore;
using server.DB;
using server.Models;

namespace server.Controllers
{
    public class EventsController
    {
        public static void MapEventEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/events", GetAllCalendarEvents);
            endpoints.MapGet("/events/{id}", GetCalendarEvent);
            endpoints.MapPost("/events", CreateCalendarEvent);
            endpoints.MapPut("/events/{id}", (int id, Event calendatEvent, AppDbContext context) => UpdateCalendarEvent(id, calendatEvent, context));
            endpoints.MapDelete("/events/{id}", DeleteCalendarEvent);
        }

        public static async Task<IResult> GetAllCalendarEvents(AppDbContext context)
        {
            var events = await context.Events
                .Include(e => e.User)
                .Select(e => new
                {
                    e.EventId,
                    e.Title,
                    e.Start,
                    e.End,
                    e.AllDay,
                    User = e.User != null ? new
                    {
                        e.User.UserId,
                        e.User.Name,
                        e.User.Color,
                        e.User.TextColor
                    } : null
                })
                .ToListAsync();
            return Results.Ok(events);
        }

        public static async Task<IResult> GetCalendarEvent(int id, AppDbContext context)
        {
            var calendarEvent = await context.Events
                .Include(e => e.User)
                .Select(e=> new
                {
                    e.EventId,
                    e.Title,
                    e.Start,
                    e.End,
                    e.AllDay,
                    User = e.User != null ? new
                    {
                        e.User.UserId,
                        e.User.Name,
                        e.User.Color,
                        e.User.TextColor
                    } : null
                })
                .FirstOrDefaultAsync(e => e.EventId == id);
            if (calendarEvent == null) return Results.NotFound();
            return Results.Ok(calendarEvent);
        }

        public static async Task<IResult> CreateCalendarEvent(Event calendarEvent, AppDbContext context)
        {
            calendarEvent.Start = calendarEvent.Start.ToUniversalTime();
            calendarEvent.End = calendarEvent.End.ToUniversalTime();

            context.Events.Add(calendarEvent);
            await context.SaveChangesAsync();
            return Results.Created($"/events/{calendarEvent.EventId}", calendarEvent);
        }

        public static async Task<IResult> UpdateCalendarEvent(int id, Event updateCalendarEvent, AppDbContext context)
        {
            var calendarEvent = await context.Events.FindAsync(id);
            if (calendarEvent == null) return Results.NotFound();

            calendarEvent.Start = updateCalendarEvent.Start;
            calendarEvent.End = updateCalendarEvent.End;
            calendarEvent.AllDay = updateCalendarEvent.AllDay;
            calendarEvent.Title = updateCalendarEvent.Title;
            calendarEvent.User = updateCalendarEvent.User;

            context.Events.Update(calendarEvent);
            await context.SaveChangesAsync();
            return Results.Ok(calendarEvent);
        }

        public static async Task<IResult> DeleteCalendarEvent(int id, AppDbContext context)
        {
            var calendarEvent = await context.Events.FindAsync(id);
            if (calendarEvent == null) return Results.NotFound();
            context.Events.Remove(calendarEvent);
            await context.SaveChangesAsync();
            return Results.NoContent();
        }
    }
}
