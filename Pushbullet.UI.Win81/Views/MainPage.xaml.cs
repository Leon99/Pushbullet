using System;
using Pushbullet.UI.Win81.ViewModel;

namespace Pushbullet.UI.Win81.Views
{
	public sealed partial class MainPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		/// <summary>
		///     Gets the view's ViewModel.
		/// </summary>
		public MainViewModel Vm
		{
			get { return (MainViewModel) DataContext; }
		}

		protected override void LoadState(object state)
		{
			var casted = state as MainPageState;

			if (casted != null)
			{
				Vm.Load(casted.LastVisit);
			}
		}

		protected override object SaveState()
		{
			return new MainPageState
			{
				LastVisit = DateTime.Now
			};
		}
	}

	public class MainPageState
	{
		public DateTime LastVisit { get; set; }
	}
}