using AllSpace.Domain.Interfaces;
using System.Collections.Immutable;

namespace AllSpace.Domain.Models;

public class Workspace(
	string id,
	string name,
	ImmutableList<AppInfo> apps,
	string? profileId = null,
	string? customIconPath = null,
	string? defaultIconPath = null)
	: ISidebarItem
{
	public string Id { get; set; } = id;
	public string Name { get; set; } = name;
	public ImmutableList<AppInfo> Apps { get; set; } = apps;
	public string? ProfileId { get; set; } = profileId;
	public string? CustomIconPath { get; set; } = customIconPath;
	public string? DefaultIconPath { get; set; } = defaultIconPath;
	public int Order { get; set; } = Random.Shared.Next(0, 100);

	public string BackgroundColor => Apps.FirstOrDefault()?.Color ?? "#5865f2";
	public bool RequiresAuth => !string.IsNullOrEmpty(ProfileId);

	// Get icon with priority: Custom > Single App's icon > Default
	public IconInfo GetIcon()
	{
		// Priority 1: Custom icon
		if (!string.IsNullOrEmpty(CustomIconPath) && File.Exists(CustomIconPath))
			return new IconInfo(CustomIconPath, IconType.Custom);

		// Priority 2: If a single app, use its icon
		if (Apps.Count == 1) return Apps[0].GetIcon();

		// Priority 3: Default random icon
		if (!string.IsNullOrEmpty(DefaultIconPath) && File.Exists(DefaultIconPath))
			return new IconInfo(DefaultIconPath, IconType.Default);
		//TODO Return Default Icon Question mark
		return default;
	}

	// Factory methods
	public static Workspace CreateSingle(string id, AppInfo app, string? customIconPath = null)
		=> new(id, app.Name, ImmutableList.Create(app),
			   app is AppInfo.Authenticated auth ? auth.ProfileId : null,
			   customIconPath);

	public static Workspace CreateMulti(string id, string name, string? customIconPath = null,
										string? defaultIconPath = null, params AppInfo[] apps)
		=> new(id, name, apps.ToImmutableList(), null, customIconPath, defaultIconPath);
}


/*
 *	var workspace = new WorkspaceInfo(
 *	    Id: "ws-1",
 *      Name: "Gmail",
 *      Url: "https://mail.google.com",
 *      BackgroundColor: "#ea4335",
 *      Icon: Icons.Material.Filled.Email,
 *      ProfileId: "google-profile-1"
 *		);
 *
 *		// Pattern matching with records
 *		var description = workspace switch
 *		{
 *			{ RequiresAuth: true } => $"{workspace.Name} (authenticated)",
 *			_ => workspace.Name
 *		};
 */