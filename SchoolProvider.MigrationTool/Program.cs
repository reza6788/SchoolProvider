using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SchoolProvider.Database.Entities;
using SchoolProvider.Database.MigrationHelpers;
using SchoolProvider.MigrationTool;


// Build configuration manually
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Bind MongoDb settings
var mongoSettings = new MongoDbSettings();
configuration.GetSection("MongoDb").Bind(mongoSettings);

var services = new ServiceCollection();
services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = new MongoClient(mongoSettings.ConnectionString);
    return client.GetDatabase(mongoSettings.DatabaseName);
});

// Build the service provider
var provider = services.BuildServiceProvider();

// Get database
var db = provider.GetRequiredService<IMongoDatabase>();

var databaseAssembly = typeof(SchoolProvider.Database.Entities.StudentEntity).Assembly;
var assemblyLocation = databaseAssembly.Location;

// Go up from bin/Debug/net9.0 to reach the project folder
var databaseProjectFolder = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(assemblyLocation)!, "..", "..", ".."));

// Now get the Migrations folder path
var migrationsFolder = Path.Combine(databaseProjectFolder, "Migrations");


var entityTypes = typeof(StudentEntity).Assembly
    .GetTypes()
    .Where(t => t.IsClass && t.Namespace == "SchoolProvider.Database.Entities" && !t.IsAbstract)
    .ToList();


// Run your migration generator
var generator = new MigrationGenerator(db);
foreach (var entityType in entityTypes)
{
    // Dynamically pick next version
    var version = MigrationVersionHelper.GetNextVersion(migrationsFolder);

    Console.WriteLine($"Generating migration {version} for {entityType.Name}...");

    var method = typeof(MigrationGenerator)
        .GetMethod("GenerateForEntityAsync")!
        .MakeGenericMethod(entityType);

    await (Task)method.Invoke(generator, new object[] { version, migrationsFolder })!;
}
// await generator.GenerateForEntityAsync<StudentEntity>("0.0.4", migrationsPath);

// Or apply all migrations
var runner = new MongoMigrationRunner(db);
await runner.RunMigrationsAsync();

Console.WriteLine("✅ Migrations complete.");