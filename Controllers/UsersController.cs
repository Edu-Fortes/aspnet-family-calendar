using Microsoft.EntityFrameworkCore;
using server.DB;
using server.Models;
using System.Text.Json;

namespace server.Controllers
{
    public class UsersController
    {
        public static void MapUserEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/users", GetAllUsers);
            endpoints.MapGet("/users/{id}", GetUser);
            endpoints.MapGet("/users/{id}/events", (int id, AppDbContext context, DateTime? start, DateTime? end) => GetUserEvents(id, context, start, end));
            endpoints.MapPost("/users", CreateUser);
            endpoints.MapPut("/users/{id}", (int id, User user, AppDbContext context) => UpdateUser(id, user, context));
            endpoints.MapDelete("/users/{id}", DeleteUser);
        }

        public static async Task<IResult> GetAllUsers(AppDbContext context)
        {
            var users = await context.Users
                .Select(user => new
                {
                    user.UserId,
                    user.Name,
                    user.Color,
                    user.TextColor
                })
                .ToListAsync();
            return Results.Ok(users);
        }

        public static async Task<IResult> GetUser(int id, AppDbContext context)
        {
            var user = await context.Users
                .Include(u => u.Events)
                .Select(u => new
                {
                    u.UserId,
                    u.Name,
                    u.Color,
                    u.TextColor,
                    Events = u.Events == null ? null : u.Events.Select(e => new
                    {
                        e.EventId,
                        e.Title,
                        e.Start,
                        e.End,
                        e.AllDay
                    }).ToList()
                })
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null) return Results.NotFound();
            return Results.Ok(user);
        }

        public static async Task<IResult> GetUserEvents(int id, AppDbContext context, DateTime? start, DateTime? end)
        {
            var query = context.Events.AsQueryable();

            if (start.HasValue) query = query.Where(e => e.Start >= start.Value);
            if (end.HasValue) query = query.Where(e => e.End <= end.Value);

            var userEvents = await query
                .Where(e => e.UserId == id)
                .Select(e => new
                {
                    e.EventId,
                    e.Title,
                    e.Start,
                    e.End,
                    e.AllDay,
                    ExtendedProps = e.User != null ? new { user = e.User.Name } : null,
                })
                .ToListAsync();

            if (userEvents == null) Results.NotFound();
            return Results.Ok(userEvents);
        }

        public static async Task<IResult> CreateUser(User user, AppDbContext context)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return Results.Created($"/users/{user.UserId}", user);
        }

        public static async Task<IResult> UpdateUser(int id, User updateUser, AppDbContext context)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return Results.NotFound();
            }

            user.Name = updateUser.Name;
            user.Color = updateUser.Color;
            user.TextColor = updateUser.TextColor;

            context.Users.Update(user);
            await context.SaveChangesAsync();
            return Results.Ok(user);
        }

        public static async Task<IResult> DeleteUser(int id, AppDbContext context)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null) return Results.NotFound();
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return Results.NoContent();
        }
    }
}
