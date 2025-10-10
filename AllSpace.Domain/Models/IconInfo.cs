namespace AllSpace.Domain.Models;

public record struct IconInfo(
	string Path, // Local file path
	IconType Type,
	byte[]? Data = null // Optional: icon data if needed
	);

public enum IconType
{
	Custom,       // User-selected custom icon
	Favicon,      // Auto-fetched from the website
	Default,      // Random from default icon pack
	SystemDefault // System default icon
}

public static class IconConstants
{
	public const string DefaultQuestionMarkIcon = "images/default-question-mark.svg";
	public const string DefaultFolderIcon = DefaultQuestionMarkIcon;
	public const string DefaultWorkspaceIcon = DefaultQuestionMarkIcon;
	public const string DefaultAppIcon = DefaultQuestionMarkIcon;
}