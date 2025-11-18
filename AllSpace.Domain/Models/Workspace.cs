using AllSpace.Domain.Interfaces;
using System.Collections.Immutable;

namespace AllSpace.Domain.Models;

public class Workspace(Guid id, string name) : ISidebarItem
{
	public static Guid ActiveWorkspaceId { get; set; } = Guid.Empty;
	public Guid Id { get; } = id;
	public string Name { get; } = name;
	public int Order { get; set; }
	public IconInfo? Icon { get; set; }
	public List<AppInfo> Apps { get; } = new();
	public string? ProfileId { get; set; }
	public bool IsActive => id.Equals(ActiveWorkspaceId);
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