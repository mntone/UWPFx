using System.Windows;

namespace Mntone.Uwpfx.Sample
{
	public partial class App
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			Resources.Add("FontWeightSemiBold", FontWeight.FromOpenTypeWeight(350));

			base.OnStartup(e);
		}
	}
}