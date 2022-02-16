namespace Logify.Cosmos;

public sealed class CosmosLogger : ILogger
{
    private readonly string _name;
    private readonly Func<CosmosLoggerConfiguration> _getCurrentConfig;

    public CosmosLogger(string name, Func<CosmosLoggerConfiguration> getCurrentConfig) =>
        (_name, _getCurrentConfig) = (name, getCurrentConfig);

    public IDisposable BeginScope<TState>(TState state) => default!;

    public bool IsEnabled(LogLevel logLevel) => _getCurrentConfig().LogLevels.Contains(logLevel);

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        CosmosLoggerConfiguration config = _getCurrentConfig();
        if (config.EventId == 0 || config.EventId == eventId.Id)
        {
            LogItemHelper entryHelper = new LogItemHelper(config.EndpointUri!, config.PrimaryKey!); 
            entryHelper.AddItemsToContainerAsync(new LogItem()
            {
                Message = formatter(state, exception),
            }).Wait();
        }
    }
}