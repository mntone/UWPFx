using Mntone.Uwpfx.Helper;
using System;
using System.Windows.Media;

namespace Mntone.Uwpfx.Media
{
	public static partial class ColorHelper
	{
		internal static HslColor ToHslOutMax(this Color color, out double max)
		{
			const double toDouble = 1.0 / 255.0;
			var r = toDouble * color.R;
			var g = toDouble * color.G;
			var b = toDouble * color.B;
			var min = MathHelper.Min(r, g, b);
			max = MathHelper.Max(r, g, b);
			var chroma = max - min;
			var isChromaZero = MathHelper.IsZero(chroma);

			double h1;
			if (isChromaZero)
			{
				h1 = 0.0;
			}
			else if (MathHelper.Equals(max, r))
			{
				h1 = ((g - b) / chroma) % 6;
			}
			else if (MathHelper.Equals(max, g))
			{
				h1 = 2.0 + (b - r) / chroma;
			}
			else
			{
				h1 = 4.0 + (r - g) / chroma;
			}

			double hue = 60.0 * h1;
			double lightness = 0.5 * (max - min);
			double saturation = isChromaZero ? 0.0 : chroma / (1.0 - Math.Abs(2 * lightness) - 1.0);
			double alpha = toDouble * color.A;

			HslColor ret;
			ret.H = (float)hue;
			ret.S = (float)saturation;
			ret.L = (float)lightness;
			ret.A = (float)alpha;
			return ret;
		}

		public static HslColor ToHsl(this Color color)
		{
			double max;
			return color.ToHslOutMax(out max);
		}

		public static HsvColor ToHsv(this Color color)
		{
			double max;
			var hsl = color.ToHslOutMax(out max);

			HsvColor ret;
			ret.H = hsl.H;
			ret.S = hsl.S;
			ret.V = (float)max;
			ret.A = hsl.A;
			return ret;
		}

		public static Color FromHsl(double hue, double saturation, double lightness, double alpha = 1.0)
		{
			if (hue < 0.0 || hue > 360.0) throw new ArgumentOutOfRangeException(nameof(hue));

			double chroma = (1.0 - Math.Abs((2 * lightness) - 1)) * saturation;
			double h1 = hue / 60.0;
			double x = chroma * (1.0 - Math.Abs((h1 % 2) - 1));
			double m = lightness - 0.5 * chroma;
			return FromHcx(h1, (float)chroma, (float)x, alpha);
		}

		public static Color FromHsv(double hue, double saturation, double value, double alpha = 1.0)
		{
			if (hue < 0.0 || hue > 360.0) throw new ArgumentOutOfRangeException(nameof(hue));

			double chroma = value * saturation;
			double h1 = hue / 60.0;
			double x = chroma * (1.0 - Math.Abs((h1 % 2) - 1));
			double m = value - chroma;
			return FromHcx(h1, (float)chroma, (float)x, alpha);
		}

		private static Color FromHcx(double h1, float chroma, float x, double alpha)
		{
			float r1, g1, b1;
			if (h1 < 1.0)
			{
				r1 = chroma;
				g1 = x;
				b1 = 0.0f;
			}
			else if (h1 < 2.0)
			{
				r1 = x;
				g1 = chroma;
				b1 = 0.0f;
			}
			else if (h1 < 3.0)
			{
				r1 = 0.0f;
				g1 = chroma;
				b1 = x;
			}
			else if (h1 < 4.0)
			{
				r1 = 0.0f;
				g1 = x;
				b1 = chroma;
			}
			else if (h1 < 5.0)
			{
				r1 = x;
				g1 = 0.0f;
				b1 = chroma;
			}
			else
			{
				r1 = chroma;
				g1 = 0.0f;
				b1 = x;
			}
			return Color.FromScRgb((float)alpha, r1, g1, b1);
		}

	}
}