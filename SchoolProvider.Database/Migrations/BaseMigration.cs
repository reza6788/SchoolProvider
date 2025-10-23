using MongoDB.Bson;

namespace SchoolProvider.Database.Migrations;

public abstract class BaseMigration<T>
{
    internal string CollectionName => nameof(T);
}