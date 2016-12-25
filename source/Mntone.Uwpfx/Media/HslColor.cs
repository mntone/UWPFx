using System.Windows.Media;

namespace Mntone.Uwpfx.Media
{
	public struct HslColor
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
		/// The lightness is 0-1 range
		/// </summary>
		public float L;

		/// <summary>
		/// The alpha/opacity is 0-1 range
		/// </summary>
		public float A;

		public Color ToColor()
			=> ColorHelper.FromHsl(H, S, L, A);
	}
}