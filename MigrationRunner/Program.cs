using MongoDB.Driver;
using MongoDBMigrations;
using SchoolProvider.Database.Context;
using SchoolProvider.Database.Migrations;


var client = new MongoClient("mongodb://softpark:softpark@localhost:27017");
var runner = new MigrationEngine()
    .UseDatabase(client,"School2")
    .UseAssembly(typeof(MongoDbContext).Assembly)
    .UseSchemeValidation(false);

runner.Run();

