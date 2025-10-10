using AllSpace.Domain.Interfaces;

namespace AllSpace.Domain.Models;

public class Workspace(
	string id,
	string name,
	string url,
	string backgroundColor,
	string? icon = null,
	string? imageUrl = null,
	string? profileId = null) : ISidebarItem
{
	public string Id { get; set; } = id;
	public string Name { get; set; } = name;
	public string Url { get; set; } = url;
	public string BackgroundColor { get; set; } = backgroundColor;
	public string? Icon { get; set; } = icon;
	public string? ImageUrl { get; set; } = imageUrl;
	public string? ProfileId { get; set; } = profileId;
	public int Order { get; set; } = Random.Shared.Next(0, 100);

	// Check if the workspace requires authentication
	public bool RequiresAuth => !string.IsNullOrEmpty(ProfileId);
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