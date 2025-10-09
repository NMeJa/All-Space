namespace AllSpace.Domain.Models;

public class Credential
{
	public string Id { get; set; } = Guid.NewGuid().ToString();
	public string ProfileId { get; set; }
	public byte[] EncryptedUsername { get; set; }
	public byte[] EncryptedPassword { get; set; }
	public byte[] Salt { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime LastModified { get; set; }
}