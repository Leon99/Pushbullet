using System;
using Windows.UI.Xaml.Media;
using GalaSoft.MvvmLight;
using Pushbullet.Api;

namespace Pushbullet.UI.Win81.ViewModel
{
	public class ItemViewModel : ObservableObject
	{
		public PushbulletPushType? PushType { get; set; }

		public string Caption { get; set; }

		public Brush IconBackground { get; set; }

		public string IconData { get; set; }

		public object Content { get; set; }

		public string CreatedOn { get; set; }
	}
}