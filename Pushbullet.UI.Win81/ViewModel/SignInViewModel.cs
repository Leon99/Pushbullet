using System;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Pushbullet.Api.Model;
using Pushbullet.UI.Win81.Common;
using Pushbullet.UI.Win81.Model;
using Pushbullet.UI.Win81.Settings;
using Pushbullet.UI.Win81.Views;

namespace Pushbullet.UI.Win81.ViewModel
{
	public class SignInViewModel : ViewModelBase
	{
		private readonly IDataService _dataService;
		private readonly IDialogService _dialogService;
		private readonly INavigationService _navigationService;


		#region Model Properties

		#region ApiToken

		private string _apiToken;

		private bool _isApiTokenValid;

		public string ApiToken
		{
			get { return _apiToken; }
			set { Set(ref _apiToken, value); }
		}

		public bool IsApiTokenValid
		{
			get { return _isApiTokenValid; }
			set
			{
				Set(ref _isApiTokenValid, value);
				SignInCommand.RaiseCanExecuteChanged();
			}
		}

		#endregion

		#region SignInInProgress

		private Boolean _signInInProgress;

		public Boolean SignInInProgress
		{
			get { return _signInInProgress; }
			set { Set(ref _signInInProgress, value); }
		}

		#endregion

		#region LoginStackPanelVisibility

		private Visibility _loginStackPanelVisibility;

		public Visibility LoginStackPanelVisibility
		{
			get { return _loginStackPanelVisibility; }
			set { Set(ref _loginStackPanelVisibility, value); }
		}

		#endregion

		#region ProgressRingStackPanelVisibility

		private Visibility _progressRingStackPanelVisibility;

		public Visibility ProgressRingStackPanelVisibility
		{
			get { return _progressRingStackPanelVisibility; }
			set { Set(ref _progressRingStackPanelVisibility, value); }
		}

		#endregion

		#endregion

		/// <summary>
		///     Initializes a new instance of the SignInViewModel class.
		/// </summary>
		public SignInViewModel(
			IDataService dataService,
			INavigationService navigationService,
			IDialogService dialogService)
		{
			_dataService = dataService;
			_navigationService = navigationService;
			_dialogService = dialogService;

			_signInCommand = new Lazy<RelayCommand>(() => new RelayCommand(
				SignIn, () => IsApiTokenValid));

			LoginStackPanelVisibility = Visibility.Visible;
			ProgressRingStackPanelVisibility = Visibility.Collapsed;

			if (!string.IsNullOrEmpty(AppSettings.SignIn.ApiToken))
			{
				ApiToken = AppSettings.SignIn.ApiToken.UnprotectAsync().Result;
			}
		}

		#region Commands
		
		#region Sign In

		private readonly Lazy<RelayCommand> _signInCommand;

		public RelayCommand SignInCommand
		{
			get { return _signInCommand.Value; }
		}

		private async void SignIn()
		{
			SignInInProgress = true;
			LoginStackPanelVisibility = Visibility.Collapsed;
			ProgressRingStackPanelVisibility = Visibility.Visible;

			PushbulletUser user = null;
			try
			{
				user = _dataService.SignIn(ApiToken);
				AppSettings.SignIn.ApiToken = await ApiToken.ProtectAsync();
			}
			catch (Exception ex)
			{
				_dialogService.ShowError(Resources.UnableToSignIn + Environment.NewLine + ex.Message, Package.Current.DisplayName,
					"OK", null);
			}

			if (user != null)
			{
				_navigationService.Navigate(typeof (MainPage));
			}
			else
			{
				SignInInProgress = false;
				LoginStackPanelVisibility = Visibility.Visible;
				ProgressRingStackPanelVisibility = Visibility.Collapsed;
			}
		}
		
		#endregion

		#endregion
	}
}