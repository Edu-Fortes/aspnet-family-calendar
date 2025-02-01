using Microsoft.EntityFrameworkCore;
using server.DB;
using server.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost4200",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

var app = builder.Build();

// Apply CORS policy
app.UseCors("AllowLocalhost4200");

// Use HTTPS redirection
app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    AppDbContext.Seed(context);
}

UsersController.MapUserEndpoints(app);
EventsController.MapEventEndpoints(app);
app.Run();
