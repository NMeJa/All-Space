using System.Collections.Immutable;
using AllSpace.Domain.Interfaces;

namespace AllSpace.Domain.Models;

public class WorkspaceFolder(
	string id,
	string name,
	WorkspaceFolder.FolderType type,
	ImmutableList<AppInfo> apps) : ISidebarItem
{
	public string Id { get; set; } = id;
	public string Name { get; set; } = name;
	public FolderType Type { get; set; } = type;
	public ImmutableList<AppInfo> Apps { get; set; } = apps;
	public int Order { get; set; } = Random.Shared.Next(0, 100);

	public static WorkspaceFolder Create(string id, string name, FolderType type = FolderType.DROPDOWN, params AppInfo[] apps)
		=> new(id, name, type, apps?.ToImmutableList() ?? ImmutableList<AppInfo>.Empty);

	public static WorkspaceFolder Empty(string id, string name, FolderType type = FolderType.DROPDOWN)
		=> new(id, name, type, ImmutableList<AppInfo>.Empty);

	public WorkspaceFolder AddApp(AppInfo app)
	{
		Apps = Apps.Add(app);
		return this;
	}

	public WorkspaceFolder RemoveApp(string appId)
	{
		Apps = Apps.RemoveAll(a => a.Id == appId);
		return this;
	}

	public WorkspaceFolder UpdateApp(string appId, Func<AppInfo, AppInfo> updater)
	{
		var index = Apps.FindIndex(a => a.Id == appId);
		if (index >= 0)
		{
			Apps = Apps.SetItem(index, updater(Apps[index]));
		}

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