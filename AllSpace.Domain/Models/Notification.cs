namespace AllSpace.Domain.Models;

public class Notification
{
	public string Id { get; set; } = Guid.NewGuid().ToString();
	public string WorkspaceId { get; set; }
	public string Title { get; set; }
	public string Body { get; set; }
	public string Icon { get; set; }
	public NotificationType Type { get; set; }
	public bool IsRead { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public Dictionary<string, object> Data { get; set; } = new();
}

public enum NotificationType
{
	Info,
	Message,
	Alert,
	Error
}