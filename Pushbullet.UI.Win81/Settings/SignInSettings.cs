using Windows.Foundation.Collections;

namespace Pushbullet.UI.Win81.Settings
{
	public class SignInSettings : SettingsBase
	{
		public SignInSettings(IPropertySet values) : base(values)
		{
		}

		public string ApiToken
		{
			get { return GetValue<string>(); }
			set { SetValue(value); }
		}
	}
}