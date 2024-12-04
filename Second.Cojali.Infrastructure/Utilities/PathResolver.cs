namespace Second.Cojali.Infrastructure.Utilities;

public class PathResolver
{
    protected PathResolver()
    {
        
    }
    public static string ResolveAndValidatePath(string baseDirectory, string relativePath, string errorMessage)
    {
        if (string.IsNullOrEmpty(relativePath))
        {
            throw new ArgumentException("The relative path is not configured.", nameof(relativePath));
        }

        var absolutePath = Path.GetFullPath(Path.Combine(baseDirectory, relativePath));

        if (!File.Exists(absolutePath))
        {
            throw new FileNotFoundException(errorMessage, absolutePath);
        }

        return absolutePath;
    }
}
