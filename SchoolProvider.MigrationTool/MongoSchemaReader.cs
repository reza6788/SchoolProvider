using MongoDB.Bson;
using MongoDB.Driver;

namespace SchoolProvider.MigrationTool;

public static class MongoSchemaReader
{
    public static async Task<HashSet<string>> GetMongoFieldsAsync(IMongoDatabase db, string collectionName)
    {
        var collection = db.GetCollection<BsonDocument>(collectionName);
        var sample = await collection.Find(FilterDefinition<BsonDocument>.Empty).Limit(1).FirstOrDefaultAsync();
        return sample?.Names.ToHashSet() ?? new HashSet<string>();
    }
}