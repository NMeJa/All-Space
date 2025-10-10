namespace AllSpace.Data.Services;

public interface IAssetPathResolver
{
	string GetAssetPath(string relativePath);
}

public class AssetPathResolver : IAssetPathResolver
{
	private readonly string _wwwrootPath;

	public AssetPathResolver()
	{
		// Get the application's base directory
		var baseDir = AppContext.BaseDirectory;
		_wwwrootPath = Path.Combine(baseDir, "wwwroot");
	}

	public string GetAssetPath(string relativePath)
	{
		// Remove the leading slash if present
		relativePath = relativePath.TrimStart('/');

		var fullPath = Path.Combine(_wwwrootPath, relativePath);
		return File.Exists(fullPath) ? fullPath : string.Empty;
	}
}