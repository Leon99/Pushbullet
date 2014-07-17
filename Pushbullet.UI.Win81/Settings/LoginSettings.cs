using Windows.Foundation.Collections;

namespace Pushbullet.UI.Win81.Settings
{
	public class LoginSettings : SettingsBase
	{
		public LoginSettings(IPropertySet values) : base(values)
		{
		}

		public string ApiToken
		{
			get { return GetValue<string>(); }
			set { SetValue(value); }
		}
	}
}