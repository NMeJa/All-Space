namespace AllSpace.Domain.Interfaces;

public interface IEncryptionService
{
	byte[] Encrypt(string plainText, string password = "");
	string Decrypt(byte[] cipherText, string password = "");
	string HashPassword(string password);
	bool VerifyPassword(string password, string hash);
	byte[] GenerateKey();
	string GenerateSalt();
}