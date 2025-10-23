using MongoDB.Driver;
using MongoDBMigrations;
using Version = MongoDBMigrations.Version;

namespace SchoolProvider.Database.Migrations;

public class ChangeSet006_InsertCourseEntity : IMigration
{
    public Version Version => new(0, 0, 6);
    public string Name => "Insert Course Entity";
    
    public void Up(IMongoDatabase database)
    {
        var collectionName = "CourseEntity";
        
        var existingCollections = database.ListCollectionNames().ToList();
        if (!existingCollections.Contains(collectionName))
        {
            database.CreateCollection(collectionName);
        }
    }

    public void Down(IMongoDatabase database)
    {
        try
        {
            var collectionName = "CourseEntity";
            database.DropCollection(collectionName);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

   
}