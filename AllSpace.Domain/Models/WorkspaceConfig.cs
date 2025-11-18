namespace AllSpace.Domain.Models;

public record WorkspaceConfig(
	string Id,
	Workspace Item,
	NotificationSettings Notifications,
	IReadOnlyDictionary<string, string> CustomSettings,
	DateTime CreatedAt,
	DateTime ModifiedAt)
{
	// public static WorkspaceConfig Create(Workspace item)
	// 	=> new(Guid.NewGuid().ToString(),
	// 		   item,
	// 		   new NotificationSettings(item.Id, true, true),
	// 		   new Dictionary<string, string>().AsReadOnly(),
	// 		   DateTime.UtcNow,
	// 		   DateTime.UtcNow);

	public WorkspaceConfig UpdateSettings(Dictionary<string, string> settings)
		=> this with
		{
			CustomSettings = new Dictionary<string, string>(settings).AsReadOnly(),
			ModifiedAt = DateTime.UtcNow
		};
}