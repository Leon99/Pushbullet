using System.Runtime.CompilerServices;
using Windows.Foundation.Collections;

namespace Pushbullet.UI.Win81.Settings
{
	public class SettingsBase
	{
		private readonly IPropertySet _values;

		public SettingsBase(IPropertySet values)
		{
			_values = values;
		}


		protected T GetValue<T>([CallerMemberName] string propertyName = null)
		{
			return (T) _values[propertyName];
		}


		protected void SetValue(object value, [CallerMemberName] string propertyName = null)
		{
			_values[propertyName] = value;
		}
	}
}