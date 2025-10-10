namespace AllSpace.Domain.Models;

public record WorkspaceInfo(
	string Id,
	string Name,
	string Url,
	string BackgroundColor,
	string? Icon = null,
	string? ImageUrl = null,
	string? ProfileId = null)
{
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