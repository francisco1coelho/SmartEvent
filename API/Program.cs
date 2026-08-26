using Microsoft.EntityFrameworkCore;
using SmartEvent.Application.Interfaces;
using SmartEvent.Application.Services;
using SmartEvent.Infrastructure.Persistence;
using SmartEvent.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// register the DbContext in the system's dependency injection container and configure it to use PostgreSQL with the connection string from the configuration.
builder.Services.AddDbContext<SmartEventDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//activate the use of controllers in the application, which allows for handling HTTP requests and responses.
builder.Services.AddControllers();
// registers a service that investigates the API endpoints in the application and generates metadata for them, which can be used to create API documentation.
builder.Services.AddEndpointsApiExplorer();
// registers a service that generates API documentation
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //activates the /swagger endpoint, which provides a user interface for exploring and testing the API endpoints.
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

//http to https
app.UseHttpsRedirection();
app.UseAuthentication();
//activates the authentication middleware, which allows the application to authenticate users based on the configured authentication scheme.
//e.g.:[Authorize(Roles = "Admin")] 
app.UseAuthorization();
//to route incoming HTTP requests to the appropriate controller actions based on the request URL and HTTP method.
app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
