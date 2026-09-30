using SampleApplication.Server.Data;
using Microsoft.EntityFrameworkCore;
using SampleApplication.Server.Repositories;
using SampleApplication.Server.Services;
using SampleApplication.Server.Middleware;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Configure CORS for local development and SPA clients
builder.Services.AddCors(options =>
{
    // Allow common local development origins (adjust ports as needed for your front-end dev server)
    options.AddPolicy("DefaultCorsPolicy", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});
// Register repository and service for Employee
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
// Register EF Core with Sqlite for local development (use local file by default)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();
// Enable routing so UseCors and endpoint middleware work correctly
// Enable CORS early so preflight (OPTIONS) requests are handled before middleware that may redirect
app.UseRouting();
app.UseCors("DefaultCorsPolicy");

// Ensure database is created and apply migrations at startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // If the project contains explicit EF Core migrations, apply them.
    // Otherwise (no migrations in the assembly) create the database schema directly for simple local/dev scenarios.
    if (db.Database.GetMigrations().Any())
    {
        db.Database.Migrate();
    }
    else
    {
        db.Database.EnsureCreated();
    }
}

// Global exception handling
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// If your front-end calls HTTP and the app redirects to HTTPS, preflight requests can fail.
// Prefer calling the API using the final HTTPS URL from the front-end to avoid CORS failures on redirects.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
