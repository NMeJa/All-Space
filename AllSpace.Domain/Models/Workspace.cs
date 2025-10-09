namespace AllSpace.Domain.Models;

public class Workspace
{
	public string Id { get; set; } = Guid.NewGuid().ToString();
	public string Name { get; set; }
	public string Url { get; set; }
	public string Icon { get; set; }
	public string ProfileId { get; set; }
	public int Order { get; set; }
	public string GroupName { get; set; }
	public bool IsActive { get; set; }
	public string UserDataFolder { get; set; }
	public Dictionary<string, object> Settings { get; set; } = new();
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime LastAccessedAt { get; set; }
}