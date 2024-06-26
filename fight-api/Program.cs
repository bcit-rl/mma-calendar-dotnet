using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CardContextFactory;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CreateDbContextController>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CreateDbContextController") ?? throw new InvalidOperationException("Connection string 'CreateDbContextController' not found.")));

builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connection_string = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connection_string))
{
    throw new Exception("Connection string not found");
}
builder.Services.AddDbContext<CardContext>(
    options => options.UseMySQL(connection_string)
);

builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
