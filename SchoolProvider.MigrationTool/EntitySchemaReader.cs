using System.Reflection;

namespace SchoolProvider.MigrationTool;

public class EntitySchemaReader
{
    public static Dictionary<string, Type> GetEntitySchema<T>()
    {
        return typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .ToDictionary(p => p.Name, p => p.PropertyType);
    }
}