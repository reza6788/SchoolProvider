using Kot.MongoDB.Migrations;
using Microsoft.Extensions.Logging;
using SchoolProvider.Database.Migrations;



var options = new MigrationOptions("School3");

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .AddConsole()
        .SetMinimumLevel(LogLevel.Debug);
});

var migrator = MigratorBuilder.FromConnectionString("mongodb://softpark:softpark@localhost:27017", options)
        .LoadMigrationsFromAssembly(typeof(CS001_InsertTeacherEntity).Assembly)
        .WithLogger(loggerFactory) 
    .Build();

MigrationResult result = await migrator.MigrateAsync();

var aa=result;