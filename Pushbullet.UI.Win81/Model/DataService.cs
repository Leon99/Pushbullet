using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Pushbullet.Api;
using Pushbullet.Api.Model;
using Pushbullet.UI.Win81.ViewModel;

namespace Pushbullet.UI.Win81.Model
{
	public class DataService : IDataService
	{
		private PushbulletClient _client;

		public Task<PushbulletUser> SignIn(string apiToken)
		{
			_client = new PushbulletClient(apiToken);

			return _client.GetCurrentUser();
		}

		public ObservableCollection<ItemViewModel> GetDevices()
		{
			EnsureClient();
			var devicesModel = new ObservableCollection<ItemViewModel>();
			PushbulletDevices devices = _client.GetDevices();
			foreach (PushbulletDevice device in devices.Devices)
			{
				devicesModel.Add(ItemViewModelFactory.FromDevice(device));
			}
			return devicesModel;
		}

		public ObservableCollection<ItemViewModel> GetPushes()
		{
			EnsureClient();
			var pushesModel = new ObservableCollection<ItemViewModel>();
			PushbulletPushes pushes = _client.GetPushes();
			foreach (ActivePush push in pushes.Pushes.OfType<ActivePush>().OrderByDescending(push => push.CreatedRaw))
			{
				pushesModel.Add(ItemViewModelFactory.FromPush(push));
			}
			return pushesModel;
		}

		private void EnsureClient()
		{
			if (_client == null)
			{
				throw new InvalidOperationException("Client is not initialized. SignIn should be called first.");
			}
		}
	}
}