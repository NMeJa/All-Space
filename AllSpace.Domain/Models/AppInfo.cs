namespace AllSpace.Domain.Models;

public record AppInfo(
	string Id,
	string Name,
	string Url,
	string Color,
	string? CustomIconPath = null,
	string? FaviconPath = null,
	string? DefaultIconPath = null,
	string? Category = null)
{
	// Get the best available icon with priority
	public IconInfo GetIcon()
	{
		if (!string.IsNullOrEmpty(CustomIconPath) && File.Exists(CustomIconPath))
			return new IconInfo(CustomIconPath, IconType.Custom);

		if (!string.IsNullOrEmpty(FaviconPath) && File.Exists(FaviconPath))
			return new IconInfo(FaviconPath, IconType.Favicon);

		if (!string.IsNullOrEmpty(DefaultIconPath) && File.Exists(DefaultIconPath))
			return new IconInfo(DefaultIconPath, IconType.Default);

		return new IconInfo(IconConstants.DefaultAppIcon, IconType.SystemDefault);
	}


	// For apps that need authentication
	public record Authenticated(
		string Id,
		string Name,
		string Url,
		string Color,
		string ProfileId,
		string? CustomIconPath = null,
		string? FaviconPath = null,
		string? DefaultIconPath = null,
		string? Category = null
		) : AppInfo(Id, Name, Url, Color, CustomIconPath, FaviconPath, DefaultIconPath, Category);
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