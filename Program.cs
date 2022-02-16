using Logify.ColorConsole;
using Logify.Cosmos;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddColorConsoleLogger(options =>
{
    options.LogLevels.Add(LogLevel.Trace, ConsoleColor.DarkGray);
    options.LogLevels.Add(LogLevel.Debug, ConsoleColor.Gray);
    //options.LogLevels.Add(LogLevel.Information, ConsoleColor.White);
    options.LogLevels.Add(LogLevel.Warning, ConsoleColor.Yellow);
    options.LogLevels.Add(LogLevel.Error, ConsoleColor.Red);
    options.LogLevels.Add(LogLevel.Critical, ConsoleColor.Magenta);
    options.LogLevels.Add(LogLevel.None, ConsoleColor.DarkGray);

    options.EventId = 0;
});

builder.Logging.AddCosmosLogger(options =>
{
    options.LogLevels.Add(LogLevel.Trace);
    options.LogLevels.Add(LogLevel.Debug);
    options.LogLevels.Add(LogLevel.Information);
    options.LogLevels.Add(LogLevel.Warning);
    options.LogLevels.Add(LogLevel.Error);
    options.LogLevels.Add(LogLevel.Critical);
    options.LogLevels.Add(LogLevel.None);
    options.EndpointUri = builder.Configuration.GetValue<string>("Values:EndpointUri");
    options.PrimaryKey = builder.Configuration.GetValue<string>("Values:PrimaryKey");

    options.EventId = 0;
});


// Add services to the container.

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
