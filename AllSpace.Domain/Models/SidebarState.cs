namespace AllSpace.Domain.Models;

public readonly record struct SidebarState(
	bool IsCollapsed,
	string? ExpandedFolderId,
	string? HoveredItemId);

/*
 *	var sidebarState = new SidebarState(
 *		IsCollapsed: false,
 *		ExpandedFolderId: "folder-1",
 *		HoveredItemId: null
 *		);
 */