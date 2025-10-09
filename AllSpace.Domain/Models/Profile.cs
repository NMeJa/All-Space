namespace AllSpace.Domain.Models;

public class Profile
{
	public string Id { get; set; } = Guid.NewGuid().ToString();
	public string Name { get; set; }
	public string Type { get; set; } // Google, Microsoft, Generic
	public string Email { get; set; }
	public byte[] EncryptedCredentials { get; set; }
	public Dictionary<string, string> AdditionalData { get; set; } = new();
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime LastUsed { get; set; }
	public bool IsDefault { get; set; }
}