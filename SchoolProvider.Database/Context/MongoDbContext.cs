using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace SchoolProvider.Database.Context;

public class MongoDbContext: IMongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration["MongoDb_ConnectionString"];
        var dbName = configuration["MongoDb_DatabaseName"];
    
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(dbName);
    }
    
    public IMongoCollection<T> GetCollection<T>(string name) => _database.GetCollection<T>(name);
}