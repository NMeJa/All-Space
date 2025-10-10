namespace AllSpace.Domain.Models;

public record WorkspaceConfig(
	string Id,
	WorkspaceInfo Info,
	NotificationSettings Notifications,
	IReadOnlyDictionary<string, string> CustomSettings,
	DateTime CreatedAt,
	DateTime ModifiedAt)
{
	public static WorkspaceConfig Create(WorkspaceInfo info)
		=> new(Guid.NewGuid().ToString(),
			   info,
			   new NotificationSettings(info.Id, true, true),
			   new Dictionary<string, string>().AsReadOnly(),
			   DateTime.UtcNow,
			   DateTime.UtcNow);

	public WorkspaceConfig UpdateSettings(Dictionary<string, string> settings)
		=> this with
		{
			CustomSettings = new Dictionary<string, string>(settings).AsReadOnly(),
			ModifiedAt = DateTime.UtcNow
		};
}