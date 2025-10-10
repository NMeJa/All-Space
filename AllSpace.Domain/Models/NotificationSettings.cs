namespace AllSpace.Domain.Models;

public record NotificationSettings(
	string WorkspaceId,
	bool Enabled,
	bool PlaySound,
	string? CustomSound = null,
	bool ShowBadge = true,
	bool ShowDesktopNotification = true);