using AllSpace.Domain.Interfaces;
using AllSpace.Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AllSpace.UI.Components;

public partial class ASSidebar
{
	[Parameter] public bool IsHorizontal { get; set; } = false;
	[Parameter] public bool IsReversed { get; set; } = false;

	private string GetSidebarClass() => "ma-0 pa-0 " + (IsHorizontal ? "sidebar-horizontal" : "sidebar-vertical");
	private string GetScrollContainerClass() => IsHorizontal ? "sidebar-scroll-horizontal" : "sidebar-scroll-vertical";
	private string GetBottomClass() => IsHorizontal ? "sidebar-end pa-2 me-2" : "sidebar-bottom pa-2 me-2";

	[Parameter] public string SidebarId { get; set; } = string.Empty;
	[Parameter] public SidebarState State { get; set; } = default!;

	[Parameter] public WorkspaceFolder Folder { get; set; } = default!;
	[Parameter] public string ActiveWorkspaceId { get; set; } = string.Empty;
	[Parameter] public int NestingOrder { get; set; }

	[Parameter] public EventCallback<WorkspaceFolder> OnSidebarFolderOpen { get; set; }
	[Parameter] public EventCallback<Workspace> OnWorkspaceClick { get; set; }
	[Parameter] public EventCallback OnAddWorkspace { get; set; }
	[Parameter] public EventCallback<SidebarState> OnStateChanged { get; set; }

	private List<WorkspaceFolder> Folders { get; set; } = new();
	private List<Workspace> Workspaces { get; set; } = new();
	private IOrderedEnumerable<ISidebarItem> items = Enumerable.Empty<ISidebarItem>().OrderBy(e => e.Order);


	protected override void OnParametersSet()
	{
		base.OnParametersSet();
		Folders = Folder.SubFolders.ToList();
		Workspaces = Folder.Workspaces.ToList();
		items = Folders.Concat<ISidebarItem>(Workspaces)
					   .OrderByDescending(e => e.Order);
	}

	private async Task HandleSidebarOpen(WorkspaceFolder? folder)
	{
		await OnSidebarFolderOpen.InvokeAsync(folder);
	}

	private async Task AddWorkspace()
	{
		await OnAddWorkspace.InvokeAsync();
	}

	private async Task HandleFolderToggle(WorkspaceFolder folder)
	{
		var stateAfterCollapse = CollapseCollapsable();
		var newState = stateAfterCollapse.ToggleFolder(folder.Id);
		await OnStateChanged.InvokeAsync(newState);
		await HandleSidebarOpen(folder);
	}

	private async Task HandleWorkspaceClick(Workspace workspace)
	{
		var allFolders = new List<WorkspaceFolder> { Folder };
		allFolders.AddRange(Folders);
		var folder = NestingOrder != 0 ? allFolders.FirstOrDefault(f => f.Workspaces.Any(w => w.Id.Equals(workspace.Id))) : null;

		var isFromCollapsable = Folders
								.Where(f => f.Type == WorkspaceFolder.FolderType.DROPDOWN_COLLAPSABLE)
								.Where(f => State.IsFolderExpanded(f.Id))
								.Any(f => f.Workspaces.Any(w => w.Id.Equals(workspace.Id)));

		if (!isFromCollapsable)
		{
			var newState = CollapseCollapsable();
			await OnStateChanged.InvokeAsync(newState);
		}

		await OnWorkspaceClick.InvokeAsync(workspace);
		await HandleSidebarOpen(folder);
	}

	private SidebarState CollapseCollapsable()
	{
		// ReSharper disable once InvertIf
		if (State.ExpandedFolderIds.Any())
		{
			var newExpandedIds = State.ExpandedFolderIds
									  .Where(id => !Folders.Any(f => f.Id == id && f.Type == WorkspaceFolder.FolderType.DROPDOWN_COLLAPSABLE))
									  .ToList();

			if (newExpandedIds.Count != State.ExpandedFolderIds.Count)
			{
				return State with { ExpandedFolderIds = newExpandedIds };
			}
		}

		return State; //if no change
	}

	private IJSObjectReference? module;

	private ElementReference scrollContainer;
	private DotNetObjectReference<ASSidebar>? dotNetRef;
	private CancellationTokenSource? scrollCancellationTokenSource;

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			dotNetRef = DotNetObjectReference.Create(this);

			module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./Components/ASSidebar.razor.js");

			// Restore scroll position
			if (State.ScrollPosition > 0)
			{
				await module.InvokeVoidAsync("scrollToPosition", scrollContainer, State.ScrollPosition);
			}

			// Set up scroll listener
			await module.InvokeVoidAsync("addScrollListener", scrollContainer, dotNetRef);
		}
	}

	[JSInvokable]
	public void OnScroll(double scrollPosition)
	{
		// Cancel previous debounce
		scrollCancellationTokenSource?.Cancel();
		scrollCancellationTokenSource?.Dispose();
		scrollCancellationTokenSource = new CancellationTokenSource();

		var token = scrollCancellationTokenSource.Token;

		_ = Task.Delay(100, token).ContinueWith(async _ =>
			{
				if (!token.IsCancellationRequested)
				{
					try
					{
						await InvokeAsync(async () =>
							{
								var newState = State with { ScrollPosition = scrollPosition };
								await OnStateChanged.InvokeAsync(newState);
							});
					}
					catch (ObjectDisposedException)
					{
						/* Component disposed */
					}
				}
			}, token, TaskContinuationOptions.NotOnCanceled, TaskScheduler.Default);
	}

	public async ValueTask DisposeAsync()
	{
		if (scrollCancellationTokenSource is not null)
		{
			await scrollCancellationTokenSource.CancelAsync();
			scrollCancellationTokenSource.Dispose();
		}

		dotNetRef?.Dispose();
		if (module is not null) await module.DisposeAsync();
		GC.SuppressFinalize(this);
	}
}