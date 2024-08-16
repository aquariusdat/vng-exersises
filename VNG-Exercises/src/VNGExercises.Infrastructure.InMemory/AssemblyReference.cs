using System.Reflection;

namespace VNGExercises.Infrastructure.InMemory;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
