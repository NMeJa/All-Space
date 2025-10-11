using System.Collections.Immutable;
using AllSpace.Domain.Interfaces;

namespace AllSpace.Domain.Models;

public class WorkspaceFolder(
	string id,
	string name,
	WorkspaceFolder.FolderType type,
	ImmutableList<Workspace> workspaces,
	ImmutableList<WorkspaceFolder> subFolders,
	string? customIconPath = null,
	string? defaultIconPath = null)
	: ISidebarItem
{
	public string Id { get; set; } = id;
	public string Name { get; set; } = name;
	public FolderType Type { get; set; } = type;
	public ImmutableList<Workspace> Workspaces { get; set; } = workspaces;
	public ImmutableList<WorkspaceFolder> SubFolders { get; set; } = subFolders;
	public string? CustomIconPath { get; set; } = customIconPath;
	public string? DefaultIconPath { get; set; } = defaultIconPath;
	public int Order { get; set; } = Random.Shared.Next(0, 100);

	// Get icon with priority: Custom > Default > null (show grid)
	public IconInfo GetIcon()
	{
		if (!string.IsNullOrEmpty(CustomIconPath) && File.Exists(CustomIconPath))
			return new IconInfo(CustomIconPath, IconType.Custom);

		if (!string.IsNullOrEmpty(DefaultIconPath) && File.Exists(DefaultIconPath))
			return new IconInfo(DefaultIconPath, IconType.Default);

		return new IconInfo(IconConstants.DefaultFolderIcon, IconType.SystemDefault);
	}

	public bool TryGetWorkspace(string workspaceId, out Workspace? workspace)
	{
		workspace = Workspaces.FirstOrDefault(w => w.Id == workspaceId);
		return workspace is not null;
	}

	public static WorkspaceFolder Create(string id, string name, FolderType type = FolderType.DROPDOWN,
										 string? customIconPath = null,
										 string? defaultIconPath = null,
										 params ISidebarItem[]? items)
	{
		var workspaces = items?.OfType<Workspace>().ToImmutableList();
		var folders = items?.OfType<WorkspaceFolder>().ToImmutableList();
		return new(id, name, type, workspaces ?? ImmutableList<Workspace>.Empty,
				   folders ?? ImmutableList<WorkspaceFolder>.Empty,
				   customIconPath, defaultIconPath);
	}

	public static WorkspaceFolder Empty(string id, string name, FolderType type = FolderType.DROPDOWN)
		=> new(id, name, type, ImmutableList<Workspace>.Empty, ImmutableList<WorkspaceFolder>.Empty);

	public WorkspaceFolder AddWorkspace(Workspace workspace)
	{
		Workspaces = Workspaces.Add(workspace);
		return this;
	}

	public WorkspaceFolder RemoveWorkspace(string workspaceId)
	{
		Workspaces = Workspaces.RemoveAll(w => w.Id == workspaceId);
		return this;
	}

	public enum FolderType
	{
		DROPDOWN,
		DROPDOWN_COLLAPSABLE,
		SIDEBAR
	}
}


/*
 *	var productivityFolder = WorkspaceFolder.Create(
 *      "folder-1",
 *      "Productivity",
 *      gmailApp,
 *      outlookApp,
 *      new AppInfo("slack-1", "Slack", "https://slack.com", "#4a154b"),
 *		new AppInfo("trello-1", "Trello", "https://trello.com", "#0079bf")
 *	    );
 *
 *	var updatedFolder = productivityFolder.AddApp(
 *	    new AppInfo("notion-1", "Notion", "https://notion.so", "#000000")
 *	    );
 */