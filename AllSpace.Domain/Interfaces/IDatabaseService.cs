namespace AllSpace.Domain.Interfaces;

public interface IDatabaseService
{
	Task<T> GetCollectionAsync<T>(string collectionName) where T : class;
	Task BackupAsync(string backupPath);
	Task RestoreAsync(string backupPath);
	Task CompactAsync();
	void Dispose();
}