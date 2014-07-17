using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Pushbullet.Api.Model;
using Pushbullet.UI.Win81.ViewModel;

namespace Pushbullet.UI.Win81.Model
{
	public interface IDataService
	{
		Task<PushbulletUser> SignIn(string apiToken);
		
		ObservableCollection<ItemViewModel> GetDevices();
		
		ObservableCollection<ItemViewModel> GetPushes();
	}
}