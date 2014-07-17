using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Pushbullet.Api;
using Pushbullet.Api.Model;

namespace Pushbullet.UI.Win81.ViewModel
{
	public class ItemViewModelFactory
	{
		public static ItemViewModel FromDevice(PushbulletDevice device)
		{
			return new ItemViewModel
			{
				Caption = device.Name,
				Content = device.Manufacturer + " " + device.Model,
				IconData = GetIconData(device.Type),
				IconBackground = (Brush) Application.Current.Resources["DeviceIconBackground"],
			};
		}


		private static string GetIconData(PushbulletDeviceType type)
		{
			return (string) Application.Current.Resources[type + "IconData"];
		}

		public static ItemViewModel FromPush(ActivePush push)
		{
			var viewModel = new ItemViewModel
			{
				IconBackground = GetIconBackground(push.Type),
				IconData = GetIconData(push.Type),
				PushType = push.Type,
				CreatedOn = push.Created.ToLocalTime().ToString("M"),
			};
			var captionedPush = push as ICaptionedPush;
			if (captionedPush != null && !string.IsNullOrEmpty(captionedPush.Caption))
			{
				viewModel.Caption = captionedPush.Caption;
			}
			else
			{
				viewModel.Caption = push.Type.ToString();
			}
			switch (push.Type)
			{
				case PushbulletPushType.Note:
					viewModel.Content = ((NotePush) push).Body;
					break;
				case PushbulletPushType.Link:
					Uri uri;
					var uriString = ((LinkPush) push).Url;
					if (!Uri.TryCreate(uriString, UriKind.Absolute, out uri))
					{
						uriString = "http://" + uriString;
						Uri.TryCreate(uriString, UriKind.Absolute, out uri);
					}
					viewModel.Content = uri;
					break;
				case PushbulletPushType.List:
					viewModel.Content = ((ListPush) push).Items;
					break;
				case PushbulletPushType.Address:
					var addressPush = ((AddressPush) push);
					viewModel.Content = addressPush.Address;
					break;
				case PushbulletPushType.File:
					var filePush = ((FilePush) push);
					viewModel.Content = new Uri(filePush.FileUrl);
					break;
			}
			return viewModel;
		}

		private static Brush GetIconBackground(PushbulletPushType type)
		{
			return (Brush) Application.Current.Resources[type + "IconBackground"];
		}

		private static string GetIconData(PushbulletPushType type)
		{
			return (string)Application.Current.Resources[type + "IconData"];
		}
	}


}