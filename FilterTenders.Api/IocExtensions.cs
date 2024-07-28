using Filters.Tenders.Core;
using FilterTenders.Application;
using FilterTenders.Application.Queries;
using FilterTenders.Infrastructure;

namespace FilterTenders.Api;

public static class IocExtensions
{
    public static IServiceCollection AddConfiguredHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        var tendersApiUrl = configuration.GetValue<string>("TendersGuruApiBaseUrl");
        if (string.IsNullOrWhiteSpace(tendersApiUrl))
        {
            throw new Exception("TendersGuruApiBaseUrl not provided in configuration");
        }

        services.AddHttpClient("TendersApi", config =>
        {
            config.BaseAddress = new Uri(tendersApiUrl);
        });

        return services;
    }

    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<ITendersRepository, TendersRepository>();
        services.AddTransient<ITendersService, TendersService>();
        services.AddTransient<ITendersSpecificationBuilder, TendersSpecificationBuilder>();

        return services;
    }
}