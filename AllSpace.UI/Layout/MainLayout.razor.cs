using System.Collections.Immutable;
using System.Reflection;
using AllSpace.Data.Services;
using AllSpace.Domain.Models;
using Microsoft.AspNetCore.Components;

namespace AllSpace.UI.Layout;

public partial class MainLayout : LayoutComponentBase
{
	private List<WorkspaceFolder> folders = new();
	private List<Workspace> workspaces = new();
	private string activeWorkspaceId = "1";
	private Workspace activeWorkspace = default!;

	private WorkspaceFolder[] defaultFolder = default!;
	private Stack<WorkspaceFolder> sidebars = default!;

	protected override void OnInitialized()
	{
		base.OnInitialized();
		workspaces = MockDataGenerator.GenerateRandomWorkspaces(5);
		folders = MockDataGenerator.GenerateFolders(10);
		defaultFolder = new[]
		{
			new WorkspaceFolder("default", "Default", WorkspaceFolder.FolderType.SIDEBAR,
								workspaces.ToImmutableList(), folders.ToImmutableList())
		};
		sidebars = new(defaultFolder);
	}

	private void ChangeWorkspace(Workspace workspace)
	{
		activeWorkspace = workspace;
		activeWorkspaceId = workspace.Id;
	}

	private void AddWorkspace() { }

	private void AddSidebar(WorkspaceFolder folder)
	{
		if (folder.Type != WorkspaceFolder.FolderType.SIDEBAR) throw new AmbiguousMatchException("Not a sidebar folder. This should never happen.");
		sidebars.Push(folder);
	}

	private void RemoveSidebar()
	{
		if (sidebars.Count == 1) throw new InvalidOperationException("Cannot remove the only sidebar. This should never happen.");
		sidebars.Pop();
	}
}