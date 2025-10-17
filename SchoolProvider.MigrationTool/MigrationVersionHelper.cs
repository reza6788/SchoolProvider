using System.Text.RegularExpressions;

namespace SchoolProvider.MigrationTool;

public static  class MigrationVersionHelper
{
    public static string GetNextVersion(string migrationsPath)
    {
        if (!Directory.Exists(migrationsPath))
            Directory.CreateDirectory(migrationsPath);

        var files = Directory.GetFiles(migrationsPath, "ChangeSet*.cs");

        if (!files.Any())
            return "0.0.1"; // first migration

        // Extract numeric version from filenames like ChangeSet001_StudentEntity.cs
        var versionNumbers = files
            .Select(f =>
            {
                var name = Path.GetFileNameWithoutExtension(f); // ChangeSet001_StudentEntity
                var match = Regex.Match(name, @"ChangeSet(\d+)_");
                return match.Success ? int.Parse(match.Groups[1].Value) : 0;
            })
            .OrderBy(v => v)
            .ToList();

        var next = versionNumbers.Last() + 1;
        return $"0.0.{next:D1}"; // format as 0.0.X
    }
}