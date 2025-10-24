using Kot.MongoDB.Migrations;
using MongoDB.Bson;
using MongoDB.Driver;
using SchoolProvider.Database.Entities;

namespace SchoolProvider.Database.Migrations;

public class CS001_InsertTeacherEntity : IMongoMigration
{
    public DatabaseVersion Version => new(0, 0, 1);
    public string Name => "Insert Teacher Entity";
    
    public Task UpAsync(IMongoDatabase db, IClientSessionHandle session, CancellationToken cancellationToken)
    {
       
        var collectionName = "TeacherEntity";
        var existingCollections = db.ListCollectionNames().ToList();
        if (!existingCollections.Contains(collectionName))
        {
            db.CreateCollection(collectionName);
        }
        
        var collection = db.GetCollection<BsonDocument>(collectionName);
        
        var filter = Builders<BsonDocument>.Filter.Or(
            Builders<BsonDocument>.Filter.Exists("Name", false)
        );
        
        var update = Builders<BsonDocument>.Update.Set("Name", 0);
        
        var result = collection.UpdateMany(filter, update);
        
        Console.WriteLine($"Updated {result.ModifiedCount} NotificationChannels documents.");
        return Task.CompletedTask;
    }

    public Task DownAsync(IMongoDatabase db, IClientSessionHandle session, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

   
}