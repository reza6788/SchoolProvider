using MongoDB.Driver;

namespace SchoolProvider.Database.Context;

public interface IMongoDbContext
{
    IMongoCollection<T> GetCollection<T>(string name);
}