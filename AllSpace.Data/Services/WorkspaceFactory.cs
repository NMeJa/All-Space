using System.Collections.Immutable;
using AllSpace.Domain.Models;

namespace AllSpace.Data.Services;

public class WorkspaceFactory(IIconService iconService)
{
	public async Task<AppInfo> CreateAppAsync(string id, string name, string url, string color)
	{
		// Try to get favicon
		var faviconPath = await iconService.GetFaviconPathAsync(url);

		// Get a default icon as a fallback
		var defaultIconPath = iconService.GetDefaultIconForService(name);

		return new AppInfo(id, name, url, color,
						   CustomIconPath: null,
						   FaviconPath: faviconPath,
						   DefaultIconPath: defaultIconPath);
	}

	public Workspace CreateWorkspace(string id, string name, params AppInfo[] apps)
	{
		// Assign a random default icon for multi-app workspaces
		string? defaultIconPath = null;
		if (apps.Length > 1)
		{
			defaultIconPath = iconService.GetRandomDefaultIcon();
		}

		return new Workspace(id, name, apps.ToImmutableList(),
							 defaultIconPath: defaultIconPath);
	}

	public WorkspaceFolder CreateFolder(string id, string name,
										WorkspaceFolder.FolderType type, params Workspace[] workspaces)
	{
		// Assign a random default icon for folders
		var defaultIconPath = iconService.GetRandomDefaultIcon();

		return new WorkspaceFolder(id, name, type,
								   workspaces.ToImmutableList(),
								   ImmutableList<WorkspaceFolder>.Empty,
								   defaultIconPath: defaultIconPath);
	}
}