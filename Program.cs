using Microsoft.Extensions.Logging;
using Events.DB;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/events", () => EventDB.GetEvents());
app.MapGet("/event/{id}", (int id) => EventDB.GetEvent(id));
app.MapPost("/event", (Event newEvent) => EventDB.CreateEvent(newEvent));
app.MapPut("/event", (Event updateEvent) => EventDB.UpdateEvent(updateEvent));
app.MapDelete("/event/{id}", (int id) => EventDB.DeleteEvent(id));

app.Run();
