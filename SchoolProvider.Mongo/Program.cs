using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Mongo.Migration;
using Mongo.Migration.Startup;
using Mongo.Migration.Startup.DotNetCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MongoDb_ConnectionString");
var databaseName = builder.Configuration["MongoDb_DatabaseName"] ?? "MyDatabase";

var migrationSettings = new MongoMigrationSettings
{
    ConnectionString = connectionString,
    Database = databaseName,
    VersionFieldName = "_migrationVersion",
};

var migration = new MongoMigration(migrationSettings, typeof(Program).Assembly);

// 3. Run migrations
migration.Run();

Console.WriteLine("MongoDB migrations applied successfully.");