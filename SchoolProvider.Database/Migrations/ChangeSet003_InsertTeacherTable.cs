using MongoDB.Bson;
using MongoDB.Driver;

namespace SchoolProvider.Database.Migrations;

public class ChangeSet003_InsertTeacherTable : IDatabaseMigration
{
    public string Version => "0.0.3";
    public async Task UpAsync(IMongoDatabase database)
    {
        const string collectionName = "TeacherEntity";

        var existingCollections = await database.ListCollectionNames().ToListAsync();
        if (existingCollections.Contains(collectionName))
        {
            Console.WriteLine($"Collection '{collectionName}' already exists. Skipping creation.");
            return;
        }

        await database.CreateCollectionAsync(collectionName);

        var teachers = database.GetCollection<BsonDocument>(collectionName);

        // create indexes
        var indexModels = new List<CreateIndexModel<BsonDocument>>
        {
            
            new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Ascending("Email"),
                new CreateIndexOptions { Unique = true, Name = "IX_Teachers_Email" }
            ),
          
            new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Ascending("LastName"),
                new CreateIndexOptions { Name = "IX_Teachers_LastName" }
            )
        };

        await teachers.Indexes.CreateManyAsync(indexModels);

        Console.WriteLine($"Collection '{collectionName}' created with indexes.");
    }

    public async Task DownAsync(IMongoDatabase database)
    {
        const string collectionName = "TeacherEntity";

        var existingCollections = await database.ListCollectionNames().ToListAsync();
        if (!existingCollections.Contains(collectionName))
        {
            Console.WriteLine($"Collection '{collectionName}' does not exist. Nothing to drop.");
            return;
        }

        await database.DropCollectionAsync(collectionName);
        Console.WriteLine($"Collection '{collectionName}' dropped successfully.");
    }
}