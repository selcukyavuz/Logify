namespace Logify.Cosmos;

using System.Collections.Concurrent;
using System.Runtime.Versioning;
using Microsoft.Extensions.Options;

[UnsupportedOSPlatform("browser")]
[ProviderAlias("Cosmos")]
public sealed class CosmosLoggerProvider : ILoggerProvider
{
    private readonly IDisposable _onChangeToken;
    private CosmosLoggerConfiguration _currentConfig;
    private readonly ConcurrentDictionary<string, CosmosLogger> _loggers =
        new(StringComparer.OrdinalIgnoreCase);

    public CosmosLoggerProvider(
        IOptionsMonitor<CosmosLoggerConfiguration> config)
    {
        _currentConfig = config.CurrentValue;
        _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
    }

    public ILogger CreateLogger(string categoryName) =>
        _loggers.GetOrAdd(categoryName, name => new CosmosLogger(name, GetCurrentConfig));

    private CosmosLoggerConfiguration GetCurrentConfig() => _currentConfig;

    public void Dispose()
    {
        _loggers.Clear();
        _onChangeToken.Dispose();
    }
}