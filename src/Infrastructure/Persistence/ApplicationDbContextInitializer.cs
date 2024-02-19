using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;

internal sealed class ApplicationDbContextInitializer(
    ILogger<ApplicationDbContextInitializer> logger,
    ApplicationDbContext dbContext)
{
    private readonly ILogger<ApplicationDbContextInitializer> _logger = logger;

    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task InitializeAsync()
    {
        try
        {
            await _dbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing the database.");

            throw;
        }
    }
}
