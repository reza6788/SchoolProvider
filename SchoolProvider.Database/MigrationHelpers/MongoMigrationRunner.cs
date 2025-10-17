using System.Reflection;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using SchoolProvider.Database.Migrations;

namespace SchoolProvider.Database.MigrationHelpers;

public static class MongoMigrationRunner
{
    private static IMongoDatabase? _database;
    private static IMongoCollection<BsonDocument>? _changelog;

    public static async Task RunMigrationsAsync(ConfigurationManager configuration, bool rollback = false)
    {
        var client = new MongoClient(configuration["MongoDb_ConnectionString"]);
        _database = client.GetDatabase(configuration["MongoDb_DatabaseName"]);
        _changelog = _database.GetCollection<BsonDocument>("__MigrationHistory");

        var migrations = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(IDatabaseMigration).IsAssignableFrom(t) && !t.IsInterface)
            .Select(t => (IDatabaseMigration)Activator.CreateInstance(t)!)
            .OrderBy(m => m.Version)
            .ToList();

        if (migrations.Count == 0)
        {
            Console.WriteLine("No migrations found.");
            return;
        }

        if (rollback)
        {
            await RollbackLastMigrationAsync(migrations);
        }
        else
        {
            await ApplyPendingMigrationsAsync(migrations);
        }
        // Console.WriteLine($"Found {migrations.Count} migrations.");
        //
        // foreach (var migration in migrations)
        // {
        //     if (!await IsAppliedAsync(migration.Version))
        //     {
        //         Console.WriteLine($"Applying migration {migration.Version}...");
        //         await migration.ApplyAsync(Database);
        //         await MarkAsAppliedAsync(migration.Version);
        //     }
        // }
    }


    private static async Task ApplyPendingMigrationsAsync(List<IDatabaseMigration> migrations)
    {
        Console.WriteLine($"Found {migrations.Count} migrations...");

        foreach (var migration in migrations)
        {
            if (!await IsAppliedAsync(migration.Version))
            {
                Console.WriteLine($"Applying migration {migration.Version} ...");
                try
                {
                    await migration.UpAsync(_database!);
                    await MarkAsAppliedAsync(migration.Version);
                    Console.WriteLine($"Migration {migration.Version} applied successfully.");
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Migration {migration.Version} failed: {ex.Message}");
                    Console.ResetColor();
                    throw; // stop on error to prevent partial execution
                }
            }
            else
            {
                Console.WriteLine($"⏭️  Skipping migration {migration.Version} (already applied).");
            }
        }

        Console.WriteLine("All migrations are up to date.");
    }

    private static async Task RollbackLastMigrationAsync(List<IDatabaseMigration> migrations)
    {
        var lastApplied = await _changelog!.Find(FilterDefinition<BsonDocument>.Empty)
            .Sort(Builders<BsonDocument>.Sort.Descending("AppliedOn"))
            .Limit(1)
            .FirstOrDefaultAsync();

        if (lastApplied == null)
        {
            Console.WriteLine("No applied migrations to rollback.");
            return;
        }

        var version = lastApplied["Version"].AsString;
        var migration = migrations.FirstOrDefault(m => m.Version == version);

        if (migration == null)
        {
            Console.WriteLine($"Could not find migration class for version {version}.");
            return;
        }

        Console.WriteLine($"Rolling back migration {version} ...");

        try
        {
            await migration.DownAsync(_database!);
            await RemoveFromChangelogAsync(version);
            Console.WriteLine($"Migration {version} rolled back successfully.");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Rollback of {version} failed: {ex.Message}");
            Console.ResetColor();
            throw;
        }
    }

    private static async Task<bool> IsAppliedAsync(string version)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("Version", version);
        return await _changelog!.Find(filter).AnyAsync();
    }

    private static async Task MarkAsAppliedAsync(string version)
    {
        var doc = new BsonDocument
        {
            { "Version", version },
            { "AppliedOn", DateTime.UtcNow }
        };
        await _changelog!.InsertOneAsync(doc);
    }

    private static async Task RemoveFromChangelogAsync(string version)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("Version", version);
        await _changelog!.DeleteOneAsync(filter);
    }
}