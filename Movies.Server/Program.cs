using Microsoft.EntityFrameworkCore;
using Movies.Application;
using Movies.Infrastructure;
using Movies.Server.EndPoints;
using Movies.Server.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MoviesDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DbConnectionString"));
});

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
        {
            policy.WithOrigins("https://localhost:58292") // Replace with the origin of your React app
                   .AllowAnyMethod() // Allows all HTTP methods (GET, POST, PUT, DELETE, etc.)
                   .AllowAnyHeader(); // Allows all request headers
                                      // .AllowCredentials(); // Uncomment if you need to handle cookies or authorization headers
        });
});

builder.Services.AddApplication();
builder.Services.AddExceptionHandler<ExceptionHandler>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(_ => { });
// Enable CORS middleware
app.UseCors("AllowReactApp"); // Make sure this comes BEFORE your endpoint mapping

app.UseHttpsRedirection();
app.MapMoviesEndpoints();
app.MapFallbackToFile("/index.html");

app.Run();
