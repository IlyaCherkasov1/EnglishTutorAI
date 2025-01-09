using EnglishTutorAI.Infrastructure;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EnglishTutorAI.Api.Health;

public class DatabaseConnectivityHealthCheck : IHealthCheck
{
    private readonly ApplicationDbContext _context;

    public DatabaseConnectivityHealthCheck(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var canConnect = await _context.Database.CanConnectAsync(cancellationToken);

            return canConnect
                ? HealthCheckResult.Healthy("Successfully connected to the database.")
                : HealthCheckResult.Unhealthy("Unable to connect to the database.");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("An error occurred while checking database connectivity.", ex);
        }
    }
}