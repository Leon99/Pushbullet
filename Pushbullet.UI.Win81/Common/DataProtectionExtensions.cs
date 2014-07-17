using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage.Streams;

namespace Pushbullet.UI.Win81.Common
{
	public static class DataProtectionExtensions
	{
		public static async Task<string> ProtectAsync(this string clearText, string scope = "LOCAL=user")
		{
			Contract.Requires(clearText != null);
			Contract.Requires(scope != null);

			IBuffer clearBuffer = CryptographicBuffer.ConvertStringToBinary(clearText, BinaryStringEncoding.Utf8);
			var provider = new DataProtectionProvider(scope);
			IBuffer encryptedBuffer = await provider.ProtectAsync(clearBuffer);
			return CryptographicBuffer.EncodeToBase64String(encryptedBuffer);
		}

		public static async Task<string> UnprotectAsync(this string encryptedText)
		{
			Contract.Requires(encryptedText != null);

			IBuffer encryptedBuffer = CryptographicBuffer.DecodeFromBase64String(encryptedText);
			var provider = new DataProtectionProvider();
			IBuffer clearBuffer = await provider.UnprotectAsync(encryptedBuffer);
			return CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf8, clearBuffer);
		}
	}
}