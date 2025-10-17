using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBMigrations;
using Version = MongoDBMigrations.Version;

namespace SchoolProvider.Database.Migrations;

public class ChangeSet002_AddAgePhoneStudentEntity :IMigration
{
    public string Name => "Add Age and Phone to Student Entity";
    public Version Version => new(0, 0, 2);
    
    public void Up(IMongoDatabase database)
    {
        var collectionName = "StudentEntity";
        var collection = database.GetCollection<BsonDocument>(collectionName);

        var filter = Builders<BsonDocument>.Filter.Or(
            Builders<BsonDocument>.Filter.Exists("Age", false),
            Builders<BsonDocument>.Filter.Exists("Phone", false)
        );

        var update = Builders<BsonDocument>.Update
            .Set("Age", 0)
            .Set("Phone", "");

        var result = collection.UpdateMany(filter, update);
        Console.WriteLine($"Updated {result.ModifiedCount} student documents.");
    }

    public void Down(IMongoDatabase database)
    {
        var collectionName = "StudentEntity";
        var collection = database.GetCollection<BsonDocument>(collectionName);

        var update = Builders<BsonDocument>.Update
            .Unset("Age")
            .Unset("Phone");

        collection.UpdateMany(FilterDefinition<BsonDocument>.Empty, update);
    }

}