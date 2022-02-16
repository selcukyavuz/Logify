namespace Logify.Cosmos;

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;

public static class CosmosLoggerExtensions
{
    public static ILoggingBuilder AddCosmosLogger(this ILoggingBuilder builder)
    {
        builder.AddConfiguration();
        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, CosmosLoggerProvider>());
        LoggerProviderOptions.RegisterProviderOptions <CosmosLoggerConfiguration, CosmosLoggerProvider>(builder.Services);
        return builder;
    }

    public static ILoggingBuilder AddCosmosLogger( this ILoggingBuilder builder, Action<CosmosLoggerConfiguration> configure)
    {
        builder.AddCosmosLogger();
        builder.Services.Configure(configure);
        return builder;
    }
}