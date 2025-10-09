using AllSpace.Domain.Models;

namespace AllSpace.Domain.Interfaces;

public interface IProfileService
{
	Task<IEnumerable<Profile>> GetAllAsync();
	Task<Profile> GetByIdAsync(string id);
	Task<Profile> CreateAsync(Profile profile);
	Task<Profile> UpdateAsync(Profile profile);
	Task<bool> DeleteAsync(string id);
	Task<Profile> GetByTypeAsync(string type);
	Task<bool> ValidateCredentialsAsync(string profileId);
}