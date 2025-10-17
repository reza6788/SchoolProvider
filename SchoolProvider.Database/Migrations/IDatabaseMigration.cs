using MongoDB.Driver;

namespace SchoolProvider.Database.Migrations;

public interface IDatabaseMigration
{
    string Version { get; }
    Task UpAsync(IMongoDatabase database);
    Task DownAsync(IMongoDatabase database);
}