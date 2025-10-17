using MongoDB.Bson;
using MongoDB.Driver;
using SchoolProvider.Database.MigrationHelpers;

namespace SchoolProvider.Database.Migrations;

public class ChangeSet002_AddEmailAndPhone : IDatabaseMigration
{
    public string Version => "0.0.2";
    public async Task UpAsync(IMongoDatabase database)
    {
        var students = database.GetCollection<BsonDocument>("StudentEntity");

        // Add
        var addEmail = Builders<BsonDocument>.Update.Set("Email", "");
        await students.UpdateManyAsync(FilterDefinition<BsonDocument>.Empty, addEmail);
        
        var addPhone = Builders<BsonDocument>.Update.Set("Phone", "");
        await students.UpdateManyAsync(FilterDefinition<BsonDocument>.Empty, addPhone);

    }

    public async Task DownAsync(IMongoDatabase database)
    {
        var students = database.GetCollection<BsonDocument>("StudentEntity");
        
        // Remove
        var RemoveEmail = Builders<BsonDocument>.Update.Unset("Email");
        await students.UpdateManyAsync(new BsonDocument(), RemoveEmail);
        
        var RemovePhone = Builders<BsonDocument>.Update.Unset("Phone");
        await students.UpdateManyAsync(new BsonDocument(), RemovePhone);
    }

}