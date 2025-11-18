namespace AllSpace.Domain.Models;

public record NotificationSettings(
	Guid WorkspaceId,
	bool Enabled,
	bool PlaySound,
	string? CustomSound = null,
	bool ShowBadge = true,
	bool ShowDesktopNotification = true);