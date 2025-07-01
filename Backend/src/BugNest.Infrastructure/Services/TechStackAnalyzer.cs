using BugNest.Application.Interfaces;

public class TechStackAnalyzer : ITechStackAnalyzer
{
    public async Task<(string? mainLanguage, string? techStack)> AnalyzeAsync(string extractedPath)
    {
        var fileExtensions = Directory.GetFiles(extractedPath, "*.*", SearchOption.AllDirectories)
                                      .Select(f => Path.GetExtension(f).ToLower())
                                      .GroupBy(ext => ext)
                                      .OrderByDescending(g => g.Count())
                                      .ToList();

        var mainExt = fileExtensions.FirstOrDefault()?.Key;

        var lang = mainExt switch
        {
            ".ts" or ".tsx" => "TypeScript",
            ".js" or ".jsx" => "JavaScript",
            ".cs" => "C#",
            ".py" => "Python",
            ".java" => "Java",
            _ => null
        };

        var techStack = new List<string>();
        if (Directory.Exists(Path.Combine(extractedPath, "node_modules")) || File.Exists(Path.Combine(extractedPath, "package.json")))
            techStack.Add("Node.js");

        if (File.Exists(Path.Combine(extractedPath, "tsconfig.json")))
            techStack.Add("TypeScript");

        if (Directory.EnumerateFiles(extractedPath, "*.csproj", SearchOption.AllDirectories).Any())
            techStack.Add(".NET");

        if (File.Exists(Path.Combine(extractedPath, "Dockerfile")))
            techStack.Add("Docker");

        return (lang, string.Join(", ", techStack));
    }
}
