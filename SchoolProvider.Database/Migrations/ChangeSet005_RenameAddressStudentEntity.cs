using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBMigrations;
using Version = MongoDBMigrations.Version;

namespace SchoolProvider.Database.Migrations;

public class ChangeSet005_RenameAddressStudentEntity :IMigration
{
    public Version Version => new(0, 0, 5);
    public string Name => "Rename Address in Student Entity";
    
    public void Up(IMongoDatabase database)
    {
        var collectionName = "StudentEntity";
        var collection = database.GetCollection<BsonDocument>(collectionName);

        // Rename the field "address" -> "Address" for all documents that have it
        var filter = Builders<BsonDocument>.Filter.Exists("Address");
        var update = Builders<BsonDocument>.Update.Rename("Address", "Address Home");

        var result = collection.UpdateMany(filter, update);
        Console.WriteLine($"[UP] Updated {result.ModifiedCount} documents (Address -> Address Home).");
    }

    public void Down(IMongoDatabase database)
    {
        var collectionName = "StudentEntity";
        var collection = database.GetCollection<BsonDocument>(collectionName);

        var update = Builders<BsonDocument>.Update
            .Unset("Address");

        collection.UpdateMany(FilterDefinition<BsonDocument>.Empty, update);
    }

}