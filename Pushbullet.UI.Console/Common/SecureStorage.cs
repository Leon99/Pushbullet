using Pushbullet.UI.Console.Properties;

namespace Pushbullet.UI.Console.Common
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
			Settings.Default.ApiKey = token.Protect();
			Settings.Default.Save();
		}

		public static string LoadToken()
		{
			if (string.IsNullOrEmpty(Settings.Default.ApiKey))
			{
				return null;
			}
			return Settings.Default.ApiKey.Unprotect();
		}
	}
}
