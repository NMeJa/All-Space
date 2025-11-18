namespace AllSpace.Domain.Models;

public record struct IconInfo(string PathOrName, IconType Type = IconType.SvgOrFont);

public enum IconType
{
	SvgOrFont,
	Image,
}

public static class IconConstants
{
	public const string DefaultQuestionMarkIcon = "images/default-question-mark.svg";
	public const string DefaultFolderIcon = DefaultQuestionMarkIcon;
	public const string DefaultWorkspaceIcon = DefaultQuestionMarkIcon;
	public const string DefaultAppIcon = DefaultQuestionMarkIcon;
}