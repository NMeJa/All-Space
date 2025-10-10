namespace AllSpace.Domain.Models;

public readonly record struct WorkspaceState(
	string? ActiveWorkspaceId,
	bool IsExpanded,
	DateTime LastAccessed);