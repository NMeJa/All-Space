using System.Collections.Immutable;
using AllSpace.Data.Services;
using AllSpace.Domain.Models;
using Microsoft.AspNetCore.Components;

namespace AllSpace.UI.Layout;

public partial class MainLayout : LayoutComponentBase
{
	private List<WorkspaceFolder> Folders = new();
	private List<Workspace> Workspaces = new();
	private string ActiveWorkspaceId = "1";
	private Workspace ActiveWorkspace = null;

	protected override void OnInitialized()
	{
		base.OnInitialized();
		Folders = MockDataGenerator.GenerateFolders(10);
		Workspaces = MockDataGenerator.GenerateRandomWorkspaces(5);
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