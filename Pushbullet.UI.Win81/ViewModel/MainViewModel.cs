using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Pushbullet.Api;
using Pushbullet.UI.Win81.Model;

namespace Pushbullet.UI.Win81.ViewModel
{
	public class MainViewModel : ViewModelBase
	{
		private readonly IDataService _dataService;

		/// <summary>
		///     Initializes a new instance of the MainViewModel class.
		/// </summary>
		public MainViewModel(IDataService dataService)
		{
			_dataService = dataService;
			
			_closeRightPaneCommand = new Lazy<RelayCommand>(() => new RelayCommand(CloseRightPane));
			_openNewPushPaneCommand = new Lazy<RelayCommand>(() => new RelayCommand(NewPush, () => SelectedDevice != null));
			_sendPushCommand = new Lazy<RelayCommand>(() => new RelayCommand(SendPush, () => SelectedDevice != null));

			Initialize();

			RightPaneVisibility = IsInDesignModeStatic ? Visibility.Visible : Visibility.Collapsed;
			if (IsInDesignModeStatic)
			{
				SelectedDevice = Devices[0];
			}
		}

		#region Properties

		public ObservableCollection<ItemViewModel> Devices
		{
			get { return _dataService.GetDevices(); }
		}

		public ObservableCollection<ItemViewModel> Pushes
		{
			get { return _dataService.GetPushes(); }
		}


		private Visibility _rightPaneVisibility;

		public Visibility RightPaneVisibility
		{
			get { return _rightPaneVisibility; }
			set { Set(ref _rightPaneVisibility, value); }
		}

		private string _newPushTitle;

		public string NewPushTitle
		{
			get { return _newPushTitle; }
			set { Set(ref _newPushTitle, value); }
		}

		private string _newPushContent;

		public string NewPushContent
		{
			get { return _newPushContent; }
			set { Set(ref _newPushContent, value); }
		}

		#endregion

		#region Commands

		private readonly Lazy<RelayCommand> _openNewPushPaneCommand;

		public ICommand OpenNewPushPaneCommand
		{
			get { return _openNewPushPaneCommand.Value; }
		}

		private void NewPush()
		{
			RightPaneVisibility = Visibility.Visible;
		}


		private readonly Lazy<RelayCommand> _closeRightPaneCommand;

		public ICommand CloseRightPaneCommand
		{
			get { return _closeRightPaneCommand.Value; }
		}

		private ItemViewModel _selectedDevice;

		public ItemViewModel SelectedDevice
		{
			get { return _selectedDevice; }
			set
			{
				var oldDevice = _selectedDevice;
				Set(ref _selectedDevice, value);
				if ((oldDevice == null && value != null) || (oldDevice != null && value == null))
				{
					SendPushCommand.RaiseCanExecuteChanged();
				}
				RaisePropertyChanged(() => IsBottomAppBarOpen);				
			}
		}

		private ItemViewModel _selectedPush;

		public ItemViewModel SelectedPush
		{
			get { return _selectedPush; }
			set
			{
				Set(ref _selectedPush, value);
				RaisePropertyChanged(() => IsBottomAppBarOpen);
			}
		}

		private readonly Lazy<RelayCommand> _sendPushCommand;

		public RelayCommand SendPushCommand
		{
			get { return _sendPushCommand.Value; }
		}

		public bool IsBottomAppBarOpen
		{
			get { return SelectedDevice != null || SelectedPush != null; }
		}

		private void SendPush()
		{
			_dataService.SendPush(SelectedDevice.ItemId, PushbulletPushType.Note, NewPushTitle, NewPushContent);
		}

		private void CloseRightPane()
		{
			RightPaneVisibility = Visibility.Collapsed;
		}
		#endregion

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