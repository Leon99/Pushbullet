using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Pushbullet.UI.Win81.ViewModel;

namespace Pushbullet.UI.Win81.Views
{
	public sealed partial class SignInPage
	{
		public SignInPage()
		{
			InitializeComponent();
		}

		public SignInViewModel VM
		{
			get { return (SignInViewModel) DataContext; }
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			Frame.BackStack.Clear();
		}

		private void LoginPage_OnLoaded(object sender, RoutedEventArgs e)
		{
			WaitLogginStackPanel.Height = LoginStackPanel.ActualHeight + LoginStackPanel.Margin.Bottom +
			                              LoginStackPanel.Margin.Top;

			if (VM.IsApiTokenValid)
			{
				VM.SignInCommand.Execute(null);
			}
			else
			{
				LoginTextBox.Focus(FocusState.Programmatic);
			}
		}
	}
}