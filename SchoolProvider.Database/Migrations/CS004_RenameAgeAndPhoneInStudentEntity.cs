using Kot.MongoDB.Migrations;
using MongoDB.Bson;
using MongoDB.Driver;
using SchoolProvider.Database.Entities;

namespace SchoolProvider.Database.Migrations;

public class CS002_RenameAgeAndPhoneInStudentEntity : IMongoMigration
{
    public DatabaseVersion Version => new(0, 0, 4);
    public string Name => "Rename Age and Phone in Student Entity";
    
    public async Task UpAsync(IMongoDatabase db, IClientSessionHandle session, CancellationToken cancellationToken)
    {
        var collectionName = "StudentEntity";
        var existingCollections = db.ListCollectionNames().ToList();
        if (!existingCollections.Contains(collectionName))
        {
            db.CreateCollection(collectionName);
        }
        
        var collection = db.GetCollection<BsonDocument>(collectionName);
        
        // Rename "Age" → "AgeInYears"
        var filterAge = Builders<BsonDocument>.Filter.Exists("Age", true);
        var updateAge = Builders<BsonDocument>.Update.Rename("Age", "AgeInYears");
        var resultAge = await collection.UpdateManyAsync(filterAge, updateAge);
        Console.WriteLine($"Renamed {resultAge.ModifiedCount} documents (Age → AgeInYears).");

        // Rename "PhoneNumber" → "Phone"
        var filterPhone = Builders<BsonDocument>.Filter.Exists("PhoneNumber", true);
        var updatePhone = Builders<BsonDocument>.Update.Rename("PhoneNumber", "Phone");
        var resultPhone = await collection.UpdateManyAsync(filterPhone, updatePhone);
        Console.WriteLine($"Renamed {resultPhone.ModifiedCount} documents (PhoneNumber → Phone).");
    }

    public async Task DownAsync(IMongoDatabase db, IClientSessionHandle session, CancellationToken cancellationToken)
    {
        var collection = db.GetCollection<BsonDocument>("StudentEntity");

        // Revert renames
        var filterAge = Builders<BsonDocument>.Filter.Exists("AgeInYears", true);
        var updateAge = Builders<BsonDocument>.Update.Rename("AgeInYears", "Age");
        var resultAge = await collection.UpdateManyAsync(filterAge, updateAge);
        Console.WriteLine($"[Rollback] Reverted {resultAge.ModifiedCount} documents (AgeInYears → Age).");

        var filterPhone = Builders<BsonDocument>.Filter.Exists("Phone", true);
        var updatePhone = Builders<BsonDocument>.Update.Rename("Phone", "PhoneNumber");
        var resultPhone = await collection.UpdateManyAsync(filterPhone, updatePhone);
        Console.WriteLine($"[Rollback] Reverted {resultPhone.ModifiedCount} documents (Phone → PhoneNumber).");
    }

   
}