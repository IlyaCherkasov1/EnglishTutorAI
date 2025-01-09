using EnglishTutorAI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EnglishTutorAI.Api.Health;

public class DatabaseHealthCheck : IHealthCheck
{
    private readonly ApplicationDbContext _context;

    public DatabaseHealthCheck(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = new())
    {
        try
        {
            var canConnect = await _context.Database.CanConnectAsync(cancellationToken);
            if (!canConnect)
            {
                return HealthCheckResult.Unhealthy("Unable to connect to the database.");
            }

            var hasData = await _context.Translates.AnyAsync(cancellationToken);
            if (!hasData)
            {
                return HealthCheckResult.Degraded("Database is reachable but no data found in 'Translates' table.");
            }

            return HealthCheckResult.Healthy("Database is reachable and contains data.");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("An error occurred while checking the database health.", ex);
        }
    }
}