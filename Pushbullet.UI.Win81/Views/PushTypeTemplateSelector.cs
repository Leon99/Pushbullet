using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Pushbullet.Api;
using Pushbullet.UI.Win81.Model;
using Pushbullet.UI.Win81.ViewModel;

namespace Pushbullet.UI.Win81.Views
{
	internal class PushTypeTemplateSelector : DataTemplateSelector
	{
		public DataTemplate NoteTemplate { get; set; }

		public DataTemplate LinkTemplate { get; set; }

		public DataTemplate ListTemplate { get; set; }

		public DataTemplate AddressTemplate { get; set; }

		public DataTemplate FileTemplate { get; set; }

		protected override DataTemplate SelectTemplateCore(object item)
		{
			var viewModel = (ItemViewModel)item;
			if (viewModel.PushType == null)
			{
				return NoteTemplate;
			}
			switch (viewModel.PushType)
			{
				case PushbulletPushType.Note:
				case PushbulletPushType.Address:
					return NoteTemplate;
				case PushbulletPushType.Link:
					return LinkTemplate;
				case PushbulletPushType.File:
					return FileTemplate;
				case PushbulletPushType.List:
					return ListTemplate;
				default:
					return NoteTemplate;
			}
		}
	}
}