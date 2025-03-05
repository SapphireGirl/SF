using SF.API.Extensions;
using Serilog;
using Serilog.Exceptions;
using Serilog.Core;


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

    builder.Host.UseSerilog();

    // set app files
    var config = new ConfigurationBuilder()
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile("appsettings.json")
                        .AddJsonFile("appSettings.Development.json")
                        .Build();


    builder.Services.AddLogging(loggingBuilder =>
    {
        var config = new ConfigurationBuilder()
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile("appsettings.json")
                        .AddJsonFile("appSettings.Development.json")
                        .Build();

        loggingBuilder.AddSeq(config.GetSection("Seq:MinimumLevel"))
                        .AddConfiguration(config.GetSection("Seq:ServerUrl"));
    });


    var levelSwitch = new LoggingLevelSwitch();
    
    var log = new LoggerConfiguration()
        .MinimumLevel.ControlledBy(levelSwitch)
        .WriteTo.Seq("http://localhost:5341")
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .CreateLogger();

    builder.Services.AddSingleton<Serilog.ILogger>(log);

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



