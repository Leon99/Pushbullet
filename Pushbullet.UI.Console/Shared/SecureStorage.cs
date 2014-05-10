using System;
using System.Security.Cryptography;
using System.Text;
using Pushbullet.UI.Console.Properties;

namespace Pushbullet.UI.Console.Shared
{
	class SecureStorage
	{
		static SecureStorage()
		{
			if (Settings.Default.UpgradeRequired)
			{
				Settings.Default.Upgrade();
				Settings.Default.UpgradeRequired = false;
				Settings.Default.Save();
			}
		}
		public static void SaveToken(string token)
		{
			byte[] plaintextBytes = Encoding.ASCII.GetBytes(token);
			byte[] encodedBytes = ProtectedData.Protect(plaintextBytes, null, DataProtectionScope.CurrentUser);
			Settings.Default.ApiKey = Convert.ToBase64String(encodedBytes);
			Settings.Default.Save();
		}

		public static string LoadToken()
		{
			if (string.IsNullOrEmpty(Settings.Default.ApiKey))
			{
				return null;
			}
			byte[] encodedBytes = Convert.FromBase64String(Settings.Default.ApiKey);
			byte[] decodedBytes = ProtectedData.Unprotect(encodedBytes, null, DataProtectionScope.CurrentUser);

			return Encoding.ASCII.GetString(decodedBytes);
		}
	}
}
