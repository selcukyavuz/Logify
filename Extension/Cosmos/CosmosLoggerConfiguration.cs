namespace Logify.Cosmos;
public class CosmosLoggerConfiguration
{
    public int EventId { get; set; }

    public List<LogLevel> LogLevels { get; set; } = new()
    {
        LogLevel.Trace,
        LogLevel.Debug,
        LogLevel.Information,
        LogLevel.Warning,
        LogLevel.Error,
        LogLevel.Critical,
    };

    public string? EndpointUri { get; set; }

    public string? PrimaryKey { get; set; }
}