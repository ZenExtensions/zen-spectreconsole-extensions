using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace Zen.SpectreConsole.Extensions
{
    public abstract class BaseStartup
    {
        internal IServiceCollection Configure(string[] args = default)
        {
            args = args ?? new string[0];
            var services = new ServiceCollection();
            var configurationBuilder = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", optional: true);
            ConfigureAppConfiguration(configurationBuilder);
            var configuration = configurationBuilder.Build();
            services.AddSingleton<IConfigurationRoot>(configurationBuilder.Build());
            ConfigureServices(services, configuration);
            return services;
        }
        public virtual void ConfigureAppConfiguration(IConfigurationBuilder configuration)
        {
        }
        public abstract void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration);
    }
}