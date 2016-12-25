using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Uwpfx.Helper
{
	internal static class MathHelper
	{
		public static double Min(float a, float b, float c)
			=> Math.Min(Math.Min(a, b), c);

		public static double Min(double a, double b, double c)
			=> Math.Min(Math.Min(a, b), c);

		public static double Max(float a, float b, float c)
			=> Math.Max(Math.Max(a, b), c);

		public static double Max(double a, double b, double c)
			=> Math.Max(Math.Max(a, b), c);

		public static bool Equals(float a, float b)
			=> Math.Abs(a - b) < float.Epsilon;

		public static bool Equals(double a, double b)
			=> Math.Abs(a - b) < double.Epsilon;

		public static bool IsZero(this float v)
			=> Math.Abs(v) < float.Epsilon;

		public static bool IsZero(this double v)
			=> Math.Abs(v) < double.Epsilon;
	}
}