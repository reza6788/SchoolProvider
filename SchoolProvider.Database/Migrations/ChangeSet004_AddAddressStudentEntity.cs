using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBMigrations;
using Version = MongoDBMigrations.Version;

namespace SchoolProvider.Database.Migrations;

public class ChangeSet002_AddAddressStudentEntity :IMigration
{
    public Version Version => new(0, 0, 4);
    public string Name => "Add Address to Student Entity";
    
    public void Up(IMongoDatabase database)
    {
        var collectionName = "StudentEntity";
        var collection = database.GetCollection<BsonDocument>(collectionName);

        var filter = Builders<BsonDocument>.Filter.Or(
            Builders<BsonDocument>.Filter.Exists("Address", false)
        );

        var update = Builders<BsonDocument>.Update
            .Set("Address", 0);

        var result = collection.UpdateMany(filter, update);
        Console.WriteLine($"Updated {result.ModifiedCount} student documents.");
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