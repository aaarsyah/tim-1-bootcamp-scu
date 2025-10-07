using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MyApp.WebAPI.Extensions;

/// <summary>
/// Extension methods untuk database operations seperti auto-migration
/// </summary>
public static class DatabaseExtensions
{
    public static async Task<MigrationResult> MigrateAllDatabasesAsync(
        this WebApplication app, 
        bool continueOnError = false)
    {
        var result = new MigrationResult();
        
        try
        {
            using var scope = app.Services.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            
            // Discover all DbContext types from current assembly
            var dbContextTypes = DiscoverDbContextTypes(logger);
            result.TotalContexts = dbContextTypes.Count;
            
            if (dbContextTypes.Count == 0)
            {
                logger.LogWarning("⚠️ No DbContext types found in the assembly");
                return result;
            }
            
            logger.LogInformation("🗄️ Starting database migration for {Count} DbContext(s)...", dbContextTypes.Count);
            
            // Process each DbContext
            foreach (var contextType in dbContextTypes)
            {
                try
                {
                    await MigrateDbContextAsync(scope.ServiceProvider, contextType, logger, result);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "❌ Critical error migrating {ContextName}", contextType.Name);
                    result.Errors.Add(new MigrationError
                    {
                        ContextName = contextType.Name,
                        Exception = ex,
                        IsCritical = true
                    });
                    
                    if (!continueOnError)
                    {
                        logger.LogError("🛑 Stopping migration process due to error (continueOnError=false)");
                        throw;
                    }
                }
            }
            
            // Log summary
            LogMigrationSummary(logger, result);
            
            return result;
        }
        catch (Exception ex)
        {
            var logger = app.Services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "💥 Fatal error during migration process");
            result.HasCriticalError = true;
            throw;
        }
    }
    
    /// <summary>
    /// Discover all DbContext types in the executing assembly
    /// </summary>
    private static List<Type> DiscoverDbContextTypes(ILogger logger)
    {
        try
        {
            var assembly = Assembly.GetExecutingAssembly();
            var dbContextTypes = assembly.GetTypes()
                .Where(t => 
                    t.IsClass && 
                    !t.IsAbstract && 
                    t.IsSubclassOf(typeof(DbContext)))
                .OrderBy(t => t.Name) // Predictable order
                .ToList();
            
            logger.LogDebug("🔍 Discovered {Count} DbContext type(s): {Types}", 
                dbContextTypes.Count, 
                string.Join(", ", dbContextTypes.Select(t => t.Name)));
            
            return dbContextTypes;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error discovering DbContext types");
            return new List<Type>();
        }
    }
    
    /// <summary>
    /// Migrate a specific DbContext
    /// </summary>
    private static async Task MigrateDbContextAsync(
        IServiceProvider serviceProvider, 
        Type contextType, 
        ILogger logger,
        MigrationResult result)
    {
        var contextName = contextType.Name;
        logger.LogInformation("🔄 Processing {ContextName}...", contextName);
        
        // Get DbContext instance from DI
        var context = serviceProvider.GetService(contextType) as DbContext;
        
        if (context == null)
        {
            logger.LogWarning("⚠️ {ContextName} not registered in DI container, skipping...", contextName);
            result.SkippedContexts.Add(contextName);
            return;
        }
        
        try
        {
            // Check database connection
            var canConnect = await context.Database.CanConnectAsync();
            if (!canConnect)
            {
                logger.LogError("❌ Cannot connect to database for {ContextName}", contextName);
                result.Errors.Add(new MigrationError
                {
                    ContextName = contextName,
                    ErrorMessage = "Cannot connect to database"
                });
                return;
            }
            
            // Get migration info
            var pendingMigrations = (await context.Database.GetPendingMigrationsAsync()).ToList();
            var appliedMigrations = (await context.Database.GetAppliedMigrationsAsync()).ToList();
            
            logger.LogInformation("📊 {ContextName}: {Applied} applied, {Pending} pending migration(s)", 
                contextName, appliedMigrations.Count, pendingMigrations.Count);
            
            if (pendingMigrations.Any())
            {
                logger.LogInformation("📋 Applying {Count} migration(s) to {ContextName}:", 
                    pendingMigrations.Count, contextName);
                
                foreach (var migration in pendingMigrations)
                {
                    logger.LogInformation("  → {MigrationName}", migration);
                }
                
                // Apply migrations
                await context.Database.MigrateAsync();
                
                logger.LogInformation("✅ {ContextName} migrated successfully!", contextName);
                result.MigratedContexts.Add(contextName);
                result.TotalMigrationsApplied += pendingMigrations.Count;
            }
            else
            {
                logger.LogInformation("✅ {ContextName} is already up to date", contextName);
                result.UpToDateContexts.Add(contextName);
            }
            
            // Log database statistics (optional)
            await LogDatabaseStatistics(context, contextName, logger);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error migrating {ContextName}: {Message}", contextName, ex.Message);
            result.Errors.Add(new MigrationError
            {
                ContextName = contextName,
                Exception = ex,
                ErrorMessage = ex.Message
            });
        }
    }
    
    /// <summary>
    /// Log database statistics for a context
    /// </summary>
    private static Task LogDatabaseStatistics(DbContext context, string contextName, ILogger logger)
    {
        try
        {
            // Get entity types
            var entityTypes = context.Model.GetEntityTypes();
            var tableNames = entityTypes
                .Select(t => t.GetTableName())
                .Where(n => !string.IsNullOrEmpty(n))
                .Distinct()
                .ToList();
            
            logger.LogDebug("📊 {ContextName} manages {Count} table(s): {Tables}", 
                contextName, 
                tableNames.Count, 
                string.Join(", ", tableNames));
        }
        catch (Exception ex)
        {
            logger.LogDebug(ex, "Unable to retrieve statistics for {ContextName}", contextName);
        }
        
        return Task.CompletedTask;
    }
    
    /// <summary>
    /// Log migration summary
    /// </summary>
    private static void LogMigrationSummary(ILogger logger, MigrationResult result)
    {
        logger.LogInformation("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        logger.LogInformation("📊 MIGRATION SUMMARY");
        logger.LogInformation("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        logger.LogInformation("Total DbContexts found: {Total}", result.TotalContexts);
        logger.LogInformation("✅ Migrated: {Count} ({Names})", 
            result.MigratedContexts.Count, 
            string.Join(", ", result.MigratedContexts));
        logger.LogInformation("✅ Up to date: {Count} ({Names})", 
            result.UpToDateContexts.Count, 
            string.Join(", ", result.UpToDateContexts));
        logger.LogInformation("⚠️ Skipped: {Count} ({Names})", 
            result.SkippedContexts.Count, 
            string.Join(", ", result.SkippedContexts));
        logger.LogInformation("❌ Errors: {Count}", result.Errors.Count);
        logger.LogInformation("📈 Total migrations applied: {Count}", result.TotalMigrationsApplied);
        
        if (result.Errors.Any())
        {
            logger.LogWarning("⚠️ Migration completed with {Count} error(s):", result.Errors.Count);
            foreach (var error in result.Errors)
            {
                logger.LogWarning("  → {ContextName}: {Message}", 
                    error.ContextName, 
                    error.ErrorMessage ?? error.Exception?.Message ?? "Unknown error");
            }
        }
        else
        {
            logger.LogInformation("✨ All migrations completed successfully!");
        }
        
        logger.LogInformation("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
    }
}

/// <summary>
/// Result of migration operation
/// </summary>
public class MigrationResult
{
    /// <summary>
    /// Total number of DbContext types discovered
    /// </summary>
    public int TotalContexts { get; set; }
    
    /// <summary>
    /// Total number of migrations applied across all contexts
    /// </summary>
    public int TotalMigrationsApplied { get; set; }
    
    /// <summary>
    /// List of contexts that had migrations applied
    /// </summary>
    public List<string> MigratedContexts { get; set; } = new();
    
    /// <summary>
    /// List of contexts that were already up to date
    /// </summary>
    public List<string> UpToDateContexts { get; set; } = new();
    
    /// <summary>
    /// List of contexts that were skipped (not registered in DI)
    /// </summary>
    public List<string> SkippedContexts { get; set; } = new();
    
    /// <summary>
    /// List of errors encountered during migration
    /// </summary>
    public List<MigrationError> Errors { get; set; } = new();
    
    /// <summary>
    /// Indicates if a critical error occurred that stopped the migration process
    /// </summary>
    public bool HasCriticalError { get; set; }
    
    /// <summary>
    /// Returns true if migration completed without any errors
    /// </summary>
    public bool IsSuccessful => !Errors.Any() && !HasCriticalError;
}

/// <summary>
/// Migration error details
/// </summary>
public class MigrationError
{
    /// <summary>
    /// Name of the DbContext that failed
    /// </summary>
    public string ContextName { get; set; } = string.Empty;
    
    /// <summary>
    /// Error message describing the failure
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// The exception that was thrown, if any
    /// </summary>
    public Exception? Exception { get; set; }
    
    /// <summary>
    /// Indicates if this error is critical and stopped the migration process
    /// </summary>
    public bool IsCritical { get; set; }
}
