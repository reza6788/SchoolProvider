using MongoDB.Bson;
using MongoDB.Driver;
using SchoolProvider.Database.MigrationHelpers;

namespace SchoolProvider.Database.Migrations;

public class ChangeSet001_AddDateOfBirthAndRenameAge : IDatabaseMigration
{
    public string Version => "0.0.1";
    public async Task UpAsync(IMongoDatabase database)
    {
        var students = database.GetCollection<BsonDocument>("StudentEntity");

        // Rename
        var renameAge = Builders<BsonDocument>.Update.Rename("Age", "AgeInYears");
        await students.UpdateManyAsync(FilterDefinition<BsonDocument>.Empty, renameAge);
        
        
        // Add
        var addDateOfBirth = Builders<BsonDocument>.Update.Set("DateOfBirth", DateTime.MinValue);
        await students.UpdateManyAsync(FilterDefinition<BsonDocument>.Empty, addDateOfBirth);
        
    }

    public async Task DownAsync(IMongoDatabase database)
    {
        var students = database.GetCollection<BsonDocument>("StudentEntity");
        // Rename
        var renameAge = Builders<BsonDocument>.Update.Rename("AgeInYears", "Age");
        await students.UpdateManyAsync(FilterDefinition<BsonDocument>.Empty, renameAge);
        
        // remove
        var removeDateOfBirth = Builders<BsonDocument>.Update.Unset("DateOfBirth");
        await students.UpdateManyAsync(new BsonDocument(), removeDateOfBirth);
    }

   
}