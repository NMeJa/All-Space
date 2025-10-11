namespace AllSpace.Domain.Models;

public record SidebarState(
	string SidebarId,               // Unique identifier for this sidebar
	List<string> ExpandedFolderIds, // List of expanded folder IDs
	double ScrollPosition = 0)      // Scroll position tracking
{
	public static SidebarState Create(string sidebarId)
		=> new(sidebarId, new List<string>(), 0);

	// Helper methods for managing expanded folders
	public SidebarState ToggleFolder(string folderId)
	{
		var newExpandedIds = new List<string>(ExpandedFolderIds);
		if (!newExpandedIds.Remove(folderId))
			newExpandedIds.Add(folderId);
		return this with { ExpandedFolderIds = newExpandedIds };
	}

	public SidebarState ExpandFolder(string folderId)
	{
		if (ExpandedFolderIds.Contains(folderId))
			return this;

		var newExpandedIds = new List<string>(ExpandedFolderIds) { folderId };
		return this with { ExpandedFolderIds = newExpandedIds };
	}

	public SidebarState CollapseFolder(string folderId)
	{
		if (!ExpandedFolderIds.Contains(folderId))
			return this;

		var newExpandedIds = new List<string>(ExpandedFolderIds);
		newExpandedIds.Remove(folderId);
		return this with { ExpandedFolderIds = newExpandedIds };
	}

	public bool IsFolderExpanded(string folderId)
		=> ExpandedFolderIds.Contains(folderId);
}