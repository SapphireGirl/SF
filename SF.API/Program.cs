using SF.API.Extensions;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;
using SF.Logger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.RegisterLogging();
builder.Services.RegisterRepos();

IHostEnvironment env = builder.Environment;

builder.Services.AddLogging(loggingBuilder =>
{
    var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json").Build();

    loggingBuilder.AddSeq(config.GetSection("Seq:MinimumLevel"))
                  .AddConfiguration(config.GetSection("Seq:ServerUrl"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
