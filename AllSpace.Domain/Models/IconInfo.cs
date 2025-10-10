namespace AllSpace.Domain.Models;

public record struct IconInfo(
	string Path, // Local file path
	IconType Type,
	byte[]? Data = null // Optional: icon data if needed
	);

public enum IconType
{
	Custom,  // User-selected custom icon
	Favicon, // Auto-fetched from the website
	Default  // Random from default icon pack
}