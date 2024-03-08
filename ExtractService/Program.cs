using ExtractService;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddDbContext<CardContext>(
    options => options.UseMySQL(
        builder.Configuration.GetConnectionString("DefaultConnection")!,
        x => x.MigrationsAssembly("ExtractService")));



var host = builder.Build();
host.Run();
