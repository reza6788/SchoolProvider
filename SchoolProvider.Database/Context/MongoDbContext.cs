using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace SchoolProvider.Database.Context;

public class MongoDbContext : IMongoDbContext
{
    private readonly IMongoDatabase _database;
    public string ConnectionString { get;}
    public string DatabaseName { get;}
    public MongoDbContext(IConfiguration configuration)
    {
        ConnectionString = configuration["MongoDb_ConnectionString"];
        DatabaseName = configuration["MongoDb_DatabaseName"];

        var client = new MongoClient(ConnectionString);
        _database = client.GetDatabase(DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string name) => _database.GetCollection<T>(name);
}