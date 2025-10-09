using AllSpace.Domain.Models;

namespace AllSpace.Domain.Interfaces;

public interface IProfileRepository
{
	Task<IEnumerable<Profile>> GetAllAsync();
	Task<Profile> GetByIdAsync(string id);
	Task<Profile> InsertAsync(Profile profile);
	Task<bool> UpdateAsync(Profile profile);
	Task<bool> DeleteAsync(string id);
	Task<Profile> GetByTypeAsync(string type);
}