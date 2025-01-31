using Microsoft.EntityFrameworkCore;
using server.DB;
using server.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

UsersController.MapUserEndpoints(app);
EventsController.MapEventEndpoints(app);
app.Run();
