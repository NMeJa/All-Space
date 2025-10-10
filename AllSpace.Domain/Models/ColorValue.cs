namespace AllSpace.Domain.Models;

public readonly record struct ColorValue(byte R, byte G, byte B, byte A = 255)
{
	public string ToHex() => $"#{R:X2}{G:X2}{B:X2}";

	public static ColorValue FromHex(string hex)
	{
		hex = hex.TrimStart('#');
		return new ColorValue(Convert.ToByte(hex[0..2], 16),
							  Convert.ToByte(hex[2..4], 16),
							  Convert.ToByte(hex[4..6], 16),
							  hex.Length >= 8 ? Convert.ToByte(hex[6..8], 16) : (byte)255
							 );
	}
}