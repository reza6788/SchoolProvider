using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBMigrations;
using SchoolProvider.Database.Entities;

namespace SchoolProvider.Database.Migrations;

public class ChangeSet001_InsertTeacherTable : IMigration
{
    public MongoDBMigrations.Version Version => new(0, 0, 1);
    public string Name => "Insert Teacher Table";
    public void Up(IMongoDatabase database)
    {
        var collectionName = "TeacherEntity";
        
        var existingCollections = database.ListCollectionNames().ToList();
        if (!existingCollections.Contains(collectionName))
        {
            database.CreateCollection(collectionName);
        }
    }

    public void Down(IMongoDatabase database)
    {
        var collectionName = "TeacherEntity";
        database.DropCollection(collectionName);
    }

    
}