using AllSpace.Data.Services;
using AllSpace.Domain.Interfaces;
using AllSpace.Domain.Models;
using Microsoft.AspNetCore.Components;

namespace AllSpace.UI.Components.Sidebar;

public partial class DeckSidebar : ComponentBase
{
	[Parameter] public bool IsHorizontal { get; set; }


	private List<WorkspaceFolder> Folders { get; set; } = new();

	private List<Workspace> Workspaces { get; set; } = MockDataGenerator.GetMockWorkspaces().ToList();
	public Task SelectWorkspace(Workspace workspace) => Task.CompletedTask;

	private IOrderedEnumerable<ISidebarItem> items = Enumerable.Empty<ISidebarItem>().OrderBy(e => e.Order);

	private EventCallback DoSomething()
	{
		return EventCallback.Factory.Create(this, () => { });
	}
}