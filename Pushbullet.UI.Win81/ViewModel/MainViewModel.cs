using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Pushbullet.UI.Win81.Common;
using Pushbullet.UI.Win81.Model;

namespace Pushbullet.UI.Win81.ViewModel
{
	public class MainViewModel : ViewModelBase
	{
		private readonly IDataService _dataService;
		private readonly INavigationService _navigationService;

		private RelayCommand _navigateCommand;


		/// <summary>
		///     Initializes a new instance of the MainViewModel class.
		/// </summary>
		public MainViewModel(
			IDataService dataService,
			INavigationService navigationService)
		{
			_dataService = dataService;
			_navigationService = navigationService;
			Initialize();
		}

		public ObservableCollection<ItemViewModel> Devices
		{
			get { return _dataService.GetDevices(); }
		}

		public ObservableCollection<ItemViewModel> Pushes
		{
			get { return _dataService.GetPushes(); }
		}

		public int PreviewColumnWidth { get; set; }


		public Visibility PreviewColumnVisibility
		{
			get { return PreviewColumnWidth > 0 ? Visibility.Visible : Visibility.Collapsed; }
			set { PreviewColumnWidth = value == Visibility.Visible ? 400 : 0; }
		}

		////public override void Cleanup()
		////{
		////    // Clean up if needed

		////    base.Cleanup();
		////}
		public void Load(DateTime lastVisit)
		{
			if (lastVisit > DateTime.MinValue)
			{
			}
		}

		private async Task Initialize()
		{
			try
			{
			}
			catch (Exception ex)
			{
				// Report error here
			}
		}
	}
}