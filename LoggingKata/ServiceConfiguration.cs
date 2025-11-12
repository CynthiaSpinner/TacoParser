using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using LoggingKata.Services;

namespace LoggingKata
{
    //centralized service configuration
    //follows single responsibility principle - one place to configure all services
    public static class ServiceConfiguration
    {
        //configures and returns the service provider with all registered services
        public static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            
            //registering logging with Serilog as the provider
            services.AddLogging(builder => builder.AddSerilog(Serilog.Log.Logger));
            
            //registering file reading service
            services.AddTransient<CsvFileReaderService>();
            
            //registering parser - TacoParser implements ILocationParser
            services.AddTransient<ILocationParser, TacoParser>();
            
            //registering location parsing service (uses ILocationParser)
            services.AddTransient<LocationParserService>();
            
            //registering distance calculation service
            services.AddTransient<DistanceCalculatorService>();
            
            //registering distance converter service
            services.AddTransient<DistanceConverterService>();
            
            //registering result formatter service
            services.AddTransient<ResultFormatterService>();
            
            //building and returning the service provider (DI container)
            return services.BuildServiceProvider();
        }
    }
}

