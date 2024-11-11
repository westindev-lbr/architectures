using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using movie_flow_api.Domain.Interface;
using movie_flow_api.Infrastructure.Persistence.Repository;
using Npgsql;

namespace movie_flow_api.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDbConnection>(sp => new NpgsqlConnection(configuration.GetConnectionString("PGSQL")));
        services.AddScoped<IMovieRepository, MovieRepositoryImpl>();
        return services;
    }
}
