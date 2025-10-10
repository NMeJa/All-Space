using System.Collections.Immutable;

namespace AllSpace.Domain.Models;

public record WorkspaceFolder(
	string Id,
	string Name,
	ImmutableList<AppInfo> Apps)
{
	public static WorkspaceFolder Create(string id, string name, params AppInfo[] apps)
		=> new(id, name, apps?.ToImmutableList() ?? ImmutableList<AppInfo>.Empty);

	public static WorkspaceFolder Empty(string id, string name)
		=> new(id, name, ImmutableList<AppInfo>.Empty);

	public WorkspaceFolder AddApp(AppInfo app)
		=> this with { Apps = Apps.Add(app) };

	public WorkspaceFolder RemoveApp(string appId)
		=> this with { Apps = Apps.RemoveAll(a => a.Id == appId) };

	public WorkspaceFolder UpdateApp(string appId, Func<AppInfo, AppInfo> updater)
	{
		var index = Apps.FindIndex(a => a.Id == appId);
		return index >= 0
				   ? this with { Apps = Apps.SetItem(index, updater(Apps[index])) }
				   : this;
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