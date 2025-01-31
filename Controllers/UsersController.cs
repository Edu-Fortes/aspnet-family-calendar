using Microsoft.EntityFrameworkCore;
using server.DB;
using server.Models;

namespace server.Controllers
{
    public class UsersController
    {
        public static void MapUserEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/users", GetAllUsers);
            endpoints.MapGet("/users/{id}", GetUser);
            endpoints.MapPost("/users", CreateUser);
            endpoints.MapPut("/users/{id}", (int id, User user, AppDbContext context) => UpdateUser(id, user, context));
            endpoints.MapDelete("/users/{id}", DeleteUser);
        }

        public static async Task<IResult> GetAllUsers(AppDbContext context)
        {
            var users = await context.Users.ToListAsync();
            return Results.Ok(users);
        }

        public static async Task<IResult> GetUser(int id, AppDbContext context)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(user);
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
