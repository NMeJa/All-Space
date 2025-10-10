using System.Reflection;

namespace AllSpace.Data.Services;

public interface IIconService
{
	Task<string?> GetFaviconPathAsync(string websiteUrl);
	string GetRandomDefaultIcon();
	string GetDefaultIconForService(string serviceName);
	List<string> GetAllDefaultIcons();
}

public class IconService : IIconService
{
	private readonly HttpClient _httpClient;
	private readonly string _iconCachePath;
	private readonly string _defaultIconsPath;
	private readonly List<string> _defaultIcons;
	private readonly Random _random = new();

	public IconService(HttpClient httpClient)
	{
		_httpClient = httpClient;

		var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		_iconCachePath = Path.Combine(appDataPath, "AllSpace", "IconCache");
		_defaultIconsPath = Path.Combine(appDataPath, "AllSpace", "DefaultIcons");

		Directory.CreateDirectory(_iconCachePath);
		Directory.CreateDirectory(_defaultIconsPath);

		// Load default icons
		_defaultIcons = LoadDefaultIcons();
	}

	private List<string> LoadDefaultIcons()
	{
		// These would be bundled with your app
		var iconFiles = Directory.GetFiles(_defaultIconsPath, "*.png")
								 .Concat(Directory.GetFiles(_defaultIconsPath, "*.svg"))
								 .ToList();

		// If no icons found, copy from embedded resources
		if (!iconFiles.Any())
		{
			CopyDefaultIconsFromResources();
			iconFiles = Directory.GetFiles(_defaultIconsPath, "*.*").ToList();
		}

		return iconFiles;
	}

	private void CopyDefaultIconsFromResources()
	{
		// Copy embedded default icons to local folder
		var assembly = Assembly.GetExecutingAssembly();
		var resourceNames = assembly.GetManifestResourceNames()
									.Where(name => name.Contains("DefaultIcons"));

		foreach (var resourceName in resourceNames)
		{
			var fileName = Path.GetFileName(resourceName);
			var outputPath = Path.Combine(_defaultIconsPath, fileName);

			using var stream = assembly.GetManifestResourceStream(resourceName);
			using var fileStream = File.Create(outputPath);
			stream?.CopyTo(fileStream);
		}
	}

	public string GetRandomDefaultIcon()
	{
		if (!_defaultIcons.Any())
			return string.Empty;

		var randomIndex = _random.Next(_defaultIcons.Count);
		return _defaultIcons[randomIndex];
	}

	public string GetDefaultIconForService(string serviceName)
	{
		// Try to find a matching default icon by name
		var matchingIcon = _defaultIcons.FirstOrDefault(icon =>
															Path.GetFileNameWithoutExtension(icon)
																.Contains(serviceName, StringComparison.OrdinalIgnoreCase));

		return matchingIcon ?? GetRandomDefaultIcon();
	}

	public async Task<string?> GetFaviconPathAsync(string websiteUrl)
	{
		try
		{
			var uri = new Uri(websiteUrl);
			var fileName = $"favicon_{uri.Host.Replace(".", "_")}.png";
			var localPath = Path.Combine(_iconCachePath, fileName);

			// Return cached version if exists
			if (File.Exists(localPath))
				return localPath;

			// Try to download favicon
			var faviconUrl = $"https://www.google.com/s2/favicons?domain={uri.Host}&sz=64";
			var imageBytes = await _httpClient.GetByteArrayAsync(faviconUrl);

			// Save to cache
			await File.WriteAllBytesAsync(localPath, imageBytes);
			return localPath;
		}
		catch
		{
			return null;
		}
	}

	public List<string> GetAllDefaultIcons() => _defaultIcons.ToList();
}