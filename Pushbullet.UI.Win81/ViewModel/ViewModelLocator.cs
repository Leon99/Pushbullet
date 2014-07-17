/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="using:Pushbullet.UI.Win81.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using System.Diagnostics.CodeAnalysis;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Pushbullet.UI.Win81.Common;
using Pushbullet.UI.Win81.Design;
using Pushbullet.UI.Win81.Model;

namespace Pushbullet.UI.Win81.ViewModel
{
	public class ViewModelLocator
	{
		#region Properties

		[SuppressMessage("Microsoft.Performance",
			"CA1822:MarkMembersAsStatic",
			Justification = "This non-static member is needed for data binding purposes.")]
		public MainViewModel Main
		{
			get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
		}

		[SuppressMessage("Microsoft.Performance",
			"CA1822:MarkMembersAsStatic",
			Justification = "This non-static member is needed for data binding purposes.")]
		public SignInViewModel SignIn
		{
			get { return ServiceLocator.Current.GetInstance<SignInViewModel>(); }
		}

		#endregion

		static ViewModelLocator()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			if (ViewModelBase.IsInDesignModeStatic)
			{
				SimpleIoc.Default.Register<IDataService, DesignDataService>();
			}
			else
			{
				SimpleIoc.Default.Register<IDataService, DataService>();
			}

			SimpleIoc.Default.Register<IDialogService, DialogService>();
			SimpleIoc.Default.Register<INavigationService, NavigationService>();
			SimpleIoc.Default.Register<MainViewModel>();
			SimpleIoc.Default.Register<SignInViewModel>();
		}

		/// <summary>
		///     Cleans up all the resources.
		/// </summary>
		public static void Cleanup()
		{
		}
	}
}