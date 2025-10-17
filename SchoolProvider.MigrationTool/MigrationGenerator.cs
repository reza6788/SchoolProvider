using MongoDB.Driver;

namespace SchoolProvider.MigrationTool;

public class MigrationGenerator
{
    private readonly IMongoDatabase _database;

    public MigrationGenerator(IMongoDatabase database)
    {
        _database = database;
    }

    public async Task GenerateForEntityAsync<T>(string version, string outputPath)
    {
        var entityName = typeof(T).Name;
        var entityFields = EntitySchemaReader.GetEntitySchema<T>().Keys;
        var mongoFields = await MongoSchemaReader.GetMongoFieldsAsync(_database, entityName);

        var diff = SchemaComparer.Compare(mongoFields, entityFields);

        if (!diff.AddedFields.Any() && !diff.RemovedFields.Any())
        {
            Console.WriteLine("✅ No changes detected.");
            return;
        }

        var code = ChangeSetGenerator.GenerateChangeSetCode(version, entityName, diff);
        var filePath = Path.Combine(outputPath, $"ChangeSet{version.Replace(".", "")}_{entityName}.cs");
        if(!Directory.Exists(outputPath))
            Directory.CreateDirectory(outputPath);
        await File.WriteAllTextAsync(filePath, code);

        Console.WriteLine($"📝 Created migration: {filePath}");
    }
}