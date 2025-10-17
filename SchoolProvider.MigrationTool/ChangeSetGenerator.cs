using System.Text;

namespace SchoolProvider.MigrationTool;

public class ChangeSetGenerator
{
    public static string GenerateChangeSetCode(string version, string entityName, SchemaDiff diff)
    {
        var sb = new StringBuilder();
        sb.AppendLine("using MongoDB.Bson;");
        sb.AppendLine("using MongoDB.Driver;");
        sb.AppendLine("using SchoolProvider.Database.Migrations;");
        sb.AppendLine();
        sb.AppendLine($"public class ChangeSet{version.Replace(".", "")}_{entityName} : IDatabaseMigration");
        sb.AppendLine("{");
        sb.AppendLine($"    public string Version => \"{version}\";");
        sb.AppendLine($"    public async Task ApplyAsync(IMongoDatabase database)");
        sb.AppendLine("    {");
        sb.AppendLine($"        var collection = database.GetCollection<BsonDocument>(\"{entityName}\");");
        sb.AppendLine();

        // ADD
        if (diff.AddedFields.Any())
        {
            sb.AppendLine("        #region Add");
            foreach (var field in diff.AddedFields)
            {
                sb.AppendLine($"        await collection.UpdateManyAsync(FilterDefinition<BsonDocument>.Empty, Builders<BsonDocument>.Update.Set(\"{field}\", BsonNull.Value));");
            }
            sb.AppendLine("        #endregion");
        }

        // REMOVE
        if (diff.RemovedFields.Any())
        {
            sb.AppendLine("        #region Remove");
            foreach (var field in diff.RemovedFields)
            {
                sb.AppendLine($"        await collection.UpdateManyAsync(FilterDefinition<BsonDocument>.Empty, Builders<BsonDocument>.Update.Unset(\"{field}\"));");
            }
            sb.AppendLine("        #endregion");
        }

        sb.AppendLine("    }");
        sb.AppendLine("}");
        return sb.ToString();
    }
}