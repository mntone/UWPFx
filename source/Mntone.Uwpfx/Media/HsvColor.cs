using System.Windows.Media;

namespace Mntone.Uwpfx.Media
{
	public struct HsvColor
	{
		/// <summary>
		/// The hue is 0-360 range
		/// </summary>
		public float H;

		/// <summary>
		/// The saturation is 0-1 range
		/// </summary>
		public float S;

		/// <summary>
		/// The value is 0-1 range
		/// </summary>
		public float V;

		/// <summary>
		/// The alpha/opacity is 0-1 range
		/// </summary>
		public float A;

		public Color ToColor()
			=> ColorHelper.FromHsv(H, S, V, A);
	}
}