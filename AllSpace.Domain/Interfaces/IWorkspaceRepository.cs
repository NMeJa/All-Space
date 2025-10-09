using System.Linq.Expressions;
using AllSpace.Domain.Models;

namespace AllSpace.Domain.Interfaces;

public interface IWorkspaceRepository
{
	Task<IEnumerable<Workspace>> GetAllAsync();
	Task<Workspace> GetByIdAsync(string id);
	Task<Workspace> InsertAsync(Workspace workspace);
	Task<bool> UpdateAsync(Workspace workspace);
	Task<bool> DeleteAsync(string id);
	Task<IEnumerable<Workspace>> FindAsync(Expression<Func<Workspace, bool>> predicate);
}