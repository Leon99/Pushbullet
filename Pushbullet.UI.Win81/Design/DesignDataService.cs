using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Pushbullet.Api;
using Pushbullet.Api.Model;
using Pushbullet.UI.Win81.Model;
using Pushbullet.UI.Win81.ViewModel;

namespace Pushbullet.UI.Win81.Design
{
	public class DesignDataService : IDataService
	{
		public Task<PushbulletUser> SignIn(string apiToken)
		{
			return new Task<PushbulletUser>(() => new PushbulletUser
			{
				Name = "Signed in user",
				Email = "users@email.com"
			});
		}

		public ObservableCollection<ItemViewModel> GetDevices()
		{
			return new ObservableCollection<ItemViewModel>
			{
				ItemViewModelFactory.FromDevice(new PushbulletDevice
				{
					Nickname = "Device 1",
					Type = PushbulletDeviceType.Windows,
					Model = "Windows device"
				}),
				ItemViewModelFactory.FromDevice(new PushbulletDevice
				{
					Nickname = "Device 2",
					Type = PushbulletDeviceType.Android,
					Model = "Android device"
				}),
				ItemViewModelFactory.FromDevice(new PushbulletDevice
				{
					Nickname = "Device 3",
					Type = PushbulletDeviceType.Chrome,
					Model = "Chrome device"
				}),
				ItemViewModelFactory.FromDevice(new PushbulletDevice
				{
					Nickname = "Device 4",
					Type = PushbulletDeviceType.Firefox,
					Model = "Firefox device"
				}),
			};
		}

		public ObservableCollection<ItemViewModel> GetPushes()
		{
			return new ObservableCollection<ItemViewModel>
			{
				ItemViewModelFactory.FromPush(new NotePush
				{
					Title = "Push 1", 
					Body = "Push text", 
					Type = PushbulletPushType.Note
				}),
				ItemViewModelFactory.FromPush(new LinkPush
				{
					Title = "Push 2", 
					Url = "www.pushbullet.com",
				}),
				ItemViewModelFactory.FromPush(new ListPush()),
				ItemViewModelFactory.FromPush(new AddressPush
				{
					Address = "Content",
				}),
				ItemViewModelFactory.FromPush(new FilePush
				{
					FileName = "Push 5", 
					FileUrl = "DL link",
				}),
			};
		}
	}
}