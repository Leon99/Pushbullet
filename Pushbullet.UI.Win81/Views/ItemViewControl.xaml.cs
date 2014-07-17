using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using NUnit.Framework;

namespace Pushbullet.UI.Win81.Views
{
	public sealed partial class ItemViewControl
	{
		public ItemViewControl()
		{
			InitializeComponent();
			DataContextChanged += ItemViewControl_DataContextChanged;
		}

		void ItemViewControl_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
		{
			ContentPresenter.ContentTemplate = ((DataTemplateSelector) Resources["PushTypeSelector"]).SelectTemplate(DataContext);
		}
	}
}