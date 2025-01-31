using Microsoft.EntityFrameworkCore;
using server.DB;
using server.Models;

namespace server.Controllers
{
    public class EventsController
    {
        public static void MapUserEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/users", GetAllUsers);
            endpoints.MapPost("/users", CreateUser);
        }

        public static async Task<IResult> GetAllUsers(AppDbContext context)
        {
            var users = await context.Users.ToListAsync();
            return Results.Ok(users);
        }

        public static async Task<IResult> CreateUser(User user, AppDbContext context)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return Results.Created($"/users/{user.UserId}", user);
        }
    }
}
