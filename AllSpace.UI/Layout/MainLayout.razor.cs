using System.Collections.Immutable;
using AllSpace.Domain.Models;
using Microsoft.AspNetCore.Components;

namespace AllSpace.UI.Layout;

public partial class MainLayout : LayoutComponentBase
{
	private ImmutableList<WorkspaceFolder> Folders = ImmutableList<WorkspaceFolder>.Empty;
	private ImmutableList<WorkspaceInfo> Workspaces = ImmutableList<WorkspaceInfo>.Empty;
	private string ActiveWorkspaceId = "1";
	private WorkspaceInfo ActiveWorkspace = null;

	protected override void OnInitialized()
	{
		base.OnInitialized();
		var list = new List<WorkspaceFolder>
		{
			WorkspaceFolder.Create("1", "test", new AppInfo("1", "test", "test", "test")),
			WorkspaceFolder.Create("1", "test", new AppInfo("1", "test", "test", "test")),
			WorkspaceFolder.Create("1", "test", new AppInfo("1", "test", "test", "test")),
			WorkspaceFolder.Create("1", "test", new AppInfo("1", "test", "test", "test")),
			WorkspaceFolder.Create("1", "test", new AppInfo("1", "test", "test", "test")),
			WorkspaceFolder.Create("1", "test", new AppInfo("1", "test", "test", "test")),
		};
		Folders
	}

	private void HandleWorkspaceClick(WorkspaceInfo workspace)
	{
		ActiveWorkspace = workspace;
		ActiveWorkspaceId = workspace.Id;
	}

	private void HandleFolderClick(WorkspaceFolder folder)
	{
		// Expand folder logic
	}
}