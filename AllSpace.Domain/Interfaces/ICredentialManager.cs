using AllSpace.Domain.Models;

namespace AllSpace.Domain.Interfaces;

public interface ICredentialManager
{
	Task<Credential> StoreCredentialAsync(string profileId, string username, string password);
	Task<Credential> GetCredentialAsync(string profileId);
	Task<bool> DeleteCredentialAsync(string profileId);
	Task<bool> ValidateMasterPasswordAsync(string password);
	Task SetMasterPasswordAsync(string password);
	bool IsMasterPasswordSet();
}