using System.Collections.Immutable;

namespace AllSpace.Domain.Models;

public record AppCollectionState(
	ImmutableList<WorkspaceFolder> Folders,
	ImmutableList<WorkspaceInfo> Workspaces,
	ImmutableList<WorkspaceGroup> Groups,
	ImmutableDictionary<string, AuthProfile> Profiles)
{
	public static AppCollectionState Empty => new(
												  ImmutableList<WorkspaceFolder>.Empty,
												  ImmutableList<WorkspaceInfo>.Empty,
												  ImmutableList<WorkspaceGroup>.Empty,
												  ImmutableDictionary<string, AuthProfile>.Empty
												 );

	public AppCollectionState AddFolder(WorkspaceFolder folder)
		=> this with { Folders = Folders.Add(folder) };

	public AppCollectionState UpdateFolder(string folderId, Func<WorkspaceFolder, WorkspaceFolder> updater)
	{
		var index = Folders.FindIndex(f => f.Id == folderId);
		return index >= 0
				   ? this with { Folders = Folders.SetItem(index, updater(Folders[index])) }
				   : this;
	}

	public AppCollectionState AddWorkspace(WorkspaceInfo workspace)
		=> this with { Workspaces = Workspaces.Add(workspace) };

	public AppCollectionState SetProfile(AuthProfile profile)
		=> this with { Profiles = Profiles.SetItem(profile.Id, profile) };

	public AppCollectionState RemoveProfile(string profileId)
		=> this with { Profiles = Profiles.Remove(profileId) };
}