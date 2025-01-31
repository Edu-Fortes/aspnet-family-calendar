using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using server.DB;
using server.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Log the connection string to verify it's being read correctly
var logger = app.Services.GetRequiredService<ILogger<Program>>();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
logger.LogInformation("Using connection string: {ConnectionString}", connectionString);

app.MapGet("/", () => "Hello World!");
//app.MapGet("/events", () => EventDB.GetEvents());
//app.MapGet("/event/{id}", (int id) => EventDB.GetEvent(id));
//app.MapPost("/event", (Event newEvent) => EventDB.CreateEvent(newEvent));
//app.MapPut("/event", (Event updateEvent) => EventDB.UpdateEvent(updateEvent));
//app.MapDelete("/event/{id}", (int id) => EventDB.DeleteEvent(id));

app.Run();
