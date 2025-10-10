namespace AllSpace.Domain.Models;

public record AppInfo(
	string Id,
	string Name,
	string Url,
	string Color,
	string? Icon = null,
	string? Category = null)
{
	// For apps that need authentication
	public record Authenticated(
		string Id,
		string Name,
		string Url,
		string Color,
		string ProfileId,
		string? Icon = null,
		string? Category = null
		) : AppInfo(Id, Name, Url, Color, Icon, Category);
}

/*
 *  var gmailApp = new AppInfo(
 *      Id: "gmail-1",
 *      Name: "Gmail",
 *      Url: "https://mail.google.com",
 *      Color: "#ea4335",
 *      Icon: Icons.Material.Filled.Email
 *	    );
 *
 *	var outlookApp = new AppInfo.Authenticated(
 *      Id: "outlook-1",
 *      Name: "Outlook",
 *      Url: "https://outlook.live.com",
 *      Color: "#0078d4",
 *      ProfileId: "microsoft-profile-1",
 *      Icon: Icons.Material.Filled.Mail
 *	    );
 */