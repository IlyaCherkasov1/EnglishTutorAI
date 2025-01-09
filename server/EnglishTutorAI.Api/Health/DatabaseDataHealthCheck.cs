using EnglishTutorAI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EnglishTutorAI.Api.Health;

public class DatabaseDataHealthCheck : IHealthCheck
{
    private readonly ApplicationDbContext _context;

    public DatabaseDataHealthCheck(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = new())
    {
        try
        {
            var hasData = await _context.Translates.AnyAsync(cancellationToken);

            return hasData
                ? HealthCheckResult.Healthy("Critical data is available in the database.")
                : HealthCheckResult.Degraded("Database is reachable but no data found in 'Translates' table.");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("An error occurred while checking database data.", ex);
        }
    }
}