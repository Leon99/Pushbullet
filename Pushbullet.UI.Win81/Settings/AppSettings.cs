using Windows.Storage;

namespace Pushbullet.UI.Win81.Settings
{
	public class AppSettings
	{
		private const string SignInSettingsContainerName = "SignInSettings";
		public static SignInSettings SignIn;

		static AppSettings()
		{
			ApplicationDataContainer settingsContainer;
			if (
				!ApplicationData.Current.RoamingSettings.Containers.TryGetValue(SignInSettingsContainerName, out settingsContainer))
			{
				settingsContainer = ApplicationData.Current.RoamingSettings.CreateContainer(SignInSettingsContainerName,
					ApplicationDataCreateDisposition.Always);
			}
			SignIn = new SignInSettings(settingsContainer.Values);
		}
	}
}