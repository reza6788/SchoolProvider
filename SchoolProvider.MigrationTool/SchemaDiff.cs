namespace SchoolProvider.MigrationTool;

public class SchemaDiff
{
    public List<string> AddedFields { get; set; } = new();
    public List<string> RemovedFields { get; set; } = new();
}

public static class SchemaComparer
{
    public static SchemaDiff Compare(HashSet<string> mongoFields, IEnumerable<string> entityFields)
    {
        var entityFieldSet = entityFields.ToHashSet();
        return new SchemaDiff
        {
            AddedFields = entityFieldSet.Except(mongoFields).ToList(),
            RemovedFields = mongoFields.Except(entityFieldSet).ToList()
        };
    }
}