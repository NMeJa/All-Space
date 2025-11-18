using System.Collections.Immutable;
using AllSpace.Data.Services;
using AllSpace.Domain.Models;
using Microsoft.AspNetCore.Components;

namespace AllSpace.UI.Layout;

public partial class MainLayout : LayoutComponentBase
{
	private List<WorkspaceFolder> folders = new();
	private List<Workspace> workspaces = new();

	private Workspace activeWorkspace = default!;
	private string activeWorkspaceId = "1";

	private WorkspaceFolder[] defaultFolder = default!;
	private Stack<WorkspaceFolder> sidebars = default!;
	private readonly Dictionary<string, SidebarState> sidebarStates = new();

	protected override void OnInitialized()
	{
		base.OnInitialized();
		workspaces = MockDataGenerator.GetMockWorkspaces();
		folders = MockDataGenerator.GenerateFolders(0);
		defaultFolder = new[]
		{
			new WorkspaceFolder("default", "Default", WorkspaceFolder.FolderType.SIDEBAR,
								workspaces.ToImmutableList(), folders.ToImmutableList())
		};
		sidebars = new(defaultFolder);
	}

	/// <summary>
	/// Create a unique ID based on the folder and its position in the stack
	/// </summary>
	private static string GetSidebarId(WorkspaceFolder folder, int index) => $"{folder.Id}_{index}";

	private SidebarState GetOrCreateSidebarState(string sidebarId)
	{
		if (sidebarStates.TryGetValue(sidebarId, out var state)) return state;
		state = SidebarState.Create(sidebarId);
		sidebarStates[sidebarId] = state;
		return state;
	}

	private void UpdateSidebarState(string sidebarId, SidebarState newState)
	{
		sidebarStates[sidebarId] = newState;
	}

	private void ChangeWorkspace(Workspace workspace)
	{
		activeWorkspace = workspace;
		// activeWorkspaceId = workspace.Id;
		StateHasChanged();
	}

	private void AddWorkspace()
	{
		var newWorkspace = MockDataGenerator.GenerateRandomWorkspace();
		if (sidebars.Count == 0) return;
		var defaultSidebar = sidebars.First();
		defaultSidebar.Workspaces = defaultSidebar.Workspaces.Add(newWorkspace);
		StateHasChanged();
	}

	private void AddSidebar(WorkspaceFolder? folder)
	{
		if (folder is null)
		{
			EnsureSidebarContainsFolder(folders.First());
			return;
		}

		if (folder.Id == "default") throw new InvalidOperationException("Cannot add the default folder as a sidebar.");

		EnsureSidebarContainsFolder(folder);

		if (folder.Type != WorkspaceFolder.FolderType.SIDEBAR) return;

		sidebars.Push(folder);
		StateHasChanged();
	}

	private void EnsureSidebarContainsFolder(WorkspaceFolder newFolder)
	{
		while (sidebars.Count > 0)
		{
			if (!sidebars.TryPeek(out var topSidebar)) continue;
			// Check if this sidebar contains the new folder
			var containsFolder = topSidebar.SubFolders.Any(f => f.Id == newFolder.Id);

			if (containsFolder) break;

			// This sidebar doesn't contain the folder, remove it and check next
			RemoveSidebar();
		}

		StateHasChanged();
	}

	private void RemoveSidebar()
	{
		if (sidebars.Count <= 1) throw new InvalidOperationException("Cannot remove the default sidebar. This should never happen.");

		sidebars.Pop();
		StateHasChanged();
	}

	public void Dispose()
	{
		// Save states to local storage if needed
	}
}