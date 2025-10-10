using System.Collections.Immutable;

namespace AllSpace.Domain.Models;

public record AuthProfile(
	string Id,
	string Name,
	string Provider,
	ImmutableDictionary<string, string> EncryptedCredentials,
	DateTime CreatedAt,
	DateTime? LastUsedAt = null)
{
	public static AuthProfile Create(
		string id,
		string name,
		string provider,
		Dictionary<string, string> credentials)
	{
		return new AuthProfile(
							   id,
							   name,
							   provider,
							   credentials?.ToImmutableDictionary() ?? ImmutableDictionary<string, string>.Empty,
							   DateTime.UtcNow);
	}

	public AuthProfile UpdateCredential(string key, string value)
		=> this with { EncryptedCredentials = EncryptedCredentials.SetItem(key, value) };

	public AuthProfile RemoveCredential(string key)
		=> this with { EncryptedCredentials = EncryptedCredentials.Remove(key) };
}

/*
 *	var googleProfile = AuthProfile.Create(
 *		"google-1",
 *		"Work Google Account",
 *		AuthProfile.Providers.Google,
 *		new Dictionary<string, string>
 *			{
 *			["email"] = "encrypted_email",
 *			["token"] = "encrypted_token"
 *			}
 *		);
 */