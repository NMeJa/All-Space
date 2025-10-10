using System.Collections.Immutable;
using AllSpace.Data.Services;
using AllSpace.Domain.Models;
using Microsoft.AspNetCore.Components;

namespace AllSpace.UI.Layout;

public partial class MainLayout : LayoutComponentBase
{
	private ImmutableList<WorkspaceFolder> Folders = ImmutableList<WorkspaceFolder>.Empty;
	private ImmutableList<Workspace> Workspaces = ImmutableList<Workspace>.Empty;
	private string ActiveWorkspaceId = "1";
	private Workspace ActiveWorkspace = null;

	protected override void OnInitialized()
	{
		base.OnInitialized();
		Folders = MockDataGenerator.GenerateFolders(10);
		Workspaces = MockDataGenerator.GetMockWorkspaces();
	}

	private void HandleWorkspaceClick(Workspace workspace)
	{
		ActiveWorkspace = workspace;
		ActiveWorkspaceId = workspace.Id;
	}

	private void HandleFolderClick(WorkspaceFolder folder)
	{
		// Expand folder logic
	}
}