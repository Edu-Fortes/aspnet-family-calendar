using Microsoft.EntityFrameworkCore;
using server.DB;
using server.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
//app.MapGet("/events", () => EventDB.GetEvents());
//app.MapGet("/event/{id}", (int id) => EventDB.GetEvent(id));
//app.MapPost("/event", (Event newEvent) => EventDB.CreateEvent(newEvent));
//app.MapPut("/event", (Event updateEvent) => EventDB.UpdateEvent(updateEvent));
//app.MapDelete("/event/{id}", (int id) => EventDB.DeleteEvent(id));
UsersController.MapUserEndpoints(app);
app.Run();
