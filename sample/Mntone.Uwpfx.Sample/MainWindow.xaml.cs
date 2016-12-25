using Mntone.Uwpfx.Media;
using System.Windows;
using System.Windows.Media;

namespace Mntone.Uwpfx.Sample
{
	public partial class MainWindow : Window
	{
		public SolidColorBrush BackgroundColor { get; }
		public SolidColorBrush BackgroundColor2 { get; }

		public MainWindow()
		{
			InitializeComponent();

			BackgroundColor = new SolidColorBrush("hsla(120, 40%, 60%, 0.2)".ToColor());
			BackgroundColor2 = new SolidColorBrush("rgb(24%, 32%, 100%)".ToColor());
			DataContext = this;
		}
	}
}