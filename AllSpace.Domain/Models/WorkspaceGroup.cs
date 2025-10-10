using System.Collections.Immutable;

namespace AllSpace.Domain.Models;

public record WorkspaceGroup(
	string Id,
	string Name,
	string Color,
	ImmutableHashSet<string> WorkspaceIds,
	int Order = 0)
{
	public static WorkspaceGroup Empty(string id, string name, string color)
		=> new(id, name, color, ImmutableHashSet<string>.Empty);

	public WorkspaceGroup AddWorkspace(string workspaceId)
		=> this with { WorkspaceIds = WorkspaceIds.Add(workspaceId) };

	public WorkspaceGroup RemoveWorkspace(string workspaceId)
		=> this with { WorkspaceIds = WorkspaceIds.Remove(workspaceId) };

	public bool ContainsWorkspace(string workspaceId)
		=> WorkspaceIds.Contains(workspaceId);
}