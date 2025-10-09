using AllSpace.Domain.Models;

namespace AllSpace.Domain.Interfaces;

public interface IWorkspaceService
{
	Task<IEnumerable<Workspace>> GetAllAsync();
	Task<Workspace> GetByIdAsync(string id);
	Task<Workspace> CreateAsync(Workspace workspace);
	Task<Workspace> UpdateAsync(Workspace workspace);
	Task<bool> DeleteAsync(string id);
	Task<IEnumerable<Workspace>> GetByGroupAsync(string groupName);
	Task ReorderAsync(string id, int newOrder);
}