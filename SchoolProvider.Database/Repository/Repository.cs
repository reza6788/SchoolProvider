using System.Linq.Expressions;
using MongoDB.Driver;
using SchoolProvider.Database.Context;
using SchoolProvider.Database.Entities;

namespace SchoolProvider.Database.Repository;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly IMongoCollection<T> _collection;

    public Repository(IMongoDbContext context)
    {
        _collection = context.GetCollection<T>(typeof(T).Name);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var filter = Builders<T>.Filter.Eq("IsDeleted", false);
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<T?> GetByIdAsync(string id)
    {
        var filter = Builders<T>.Filter.And(
            Builders<T>.Filter.Eq("Id", id),
            Builders<T>.Filter.Eq("IsDeleted", false)
        );
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> GetByFilterAsync(Expression<Func<T, bool>> filter)
    {
        return await _collection.Find(Builders<T>.Filter.Where(filter)
                                      & Builders<T>.Filter.Eq("IsDeleted", false))
            .ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        entity.CreateDateTime = DateTime.Now;
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task UpdateAsync(string id, T entity)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        entity.LastChangeDateTime = DateTime.Now;

        await _collection.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(string id, T entity)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        entity.IsDeleted = true;
        entity.DeleteDateTime = DateTime.Now;

        await _collection.ReplaceOneAsync(filter, entity);
    }
}