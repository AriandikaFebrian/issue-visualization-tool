// 📁 BugNest.Application/Interfaces/ITechStackAnalyzer.cs
using BugNest.Domain.Entities;

namespace BugNest.Application.Interfaces;

public interface ITechStackAnalyzer
{
    Task<(string? mainLanguage, string? techStack)> AnalyzeAsync(string extractedPath);
}
