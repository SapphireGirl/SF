using SF.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            // app url
            .WithOrigins("https://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.RegisterRepos();

IHostEnvironment env = builder.Environment;

// set app files
var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile("appSettings.Development.json")
                    .Build();
// Logger Setup
var log = ServiceExtensions.RegisterAndSetupLogging(builder, config);

// adds a singleton for DapperContext
ServiceExtensions.SetupDbContext(builder.Services, config);

log.Information("Starting up application");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

// Use the CORS policy
app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();



