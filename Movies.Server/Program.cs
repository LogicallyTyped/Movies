using Microsoft.EntityFrameworkCore;
using Movies.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MoviesDBContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DbConnectionString"));
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapFallbackToFile("/index.html");

app.Run();
