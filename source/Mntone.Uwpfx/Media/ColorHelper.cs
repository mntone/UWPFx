using System;
using System.Linq;
using System.Reflection;
using System.Windows.Media;

namespace Mntone.Uwpfx.Media
{
	public static partial class ColorHelper
	{
		public static Color ToColor(this string colorString)
		{
			if (string.IsNullOrEmpty(colorString)) throw new ArgumentException(nameof(colorString));

			if (colorString[0] == '#')
			{
				switch (colorString.Length)
				{
					case 9:
						{
							var cuint = Convert.ToUInt32(colorString.Substring(1), 16);
							var a = (byte)(cuint >> 24);
							var r = (byte)((cuint >> 16) & 0xff);
							var g = (byte)((cuint >> 8) & 0xff);
							var b = (byte)(cuint & 0xff);
							return Color.FromArgb(a, r, g, b);
						}

					case 7:
						{
							var cuint = Convert.ToUInt32(colorString.Substring(1), 16);
							var r = (byte)((cuint >> 16) & 0xff);
							var g = (byte)((cuint >> 8) & 0xff);
							var b = (byte)(cuint & 0xff);
							return Color.FromArgb(255, r, g, b);
						}

					case 5:
						{
							var cuint = Convert.ToUInt16(colorString.Substring(1), 16);
							var a = (byte)(cuint >> 12);
							var r = (byte)((cuint >> 8) & 0xf);
							var g = (byte)((cuint >> 4) & 0xf);
							var b = (byte)(cuint & 0xf);
							a = (byte)(a << 4 | a);
							r = (byte)(r << 4 | r);
							g = (byte)(g << 4 | g);
							b = (byte)(b << 4 | b);
							return Color.FromArgb(a, r, g, b);
						}

					case 4:
						{
							var cuint = Convert.ToUInt16(colorString.Substring(1), 16);
							var r = (byte)((cuint >> 8) & 0xf);
							var g = (byte)((cuint >> 4) & 0xf);
							var b = (byte)(cuint & 0xf);
							r = (byte)(r << 4 | r);
							g = (byte)(g << 4 | g);
							b = (byte)(b << 4 | b);
							return Color.FromArgb(255, r, g, b);
						}

					default:
						throw new FormatException(string.Format("The {0} string passed in the colorString argument is not a recognized Color format.", colorString));
				}
			}

			if (colorString.Length >= 10 && colorString.StartsWith("rgb(") && colorString[colorString.Length - 1] == ')')
			{
				var colorString2 = colorString.Substring(4, colorString.Length - 5);
				if (colorString2.Contains('%'))
				{
					var values = colorString2.Split(',').Select(v => v.Trim()).ToArray();
					if (values.Length == 3 && values.All(v => v[v.Length - 1] == '%'))
					{
						var r = byte.Parse(values[0].Substring(0, values[0].Length - 1));
						if (r <= 100)
						{
							var g = byte.Parse(values[1].Substring(0, values[1].Length - 1));
							if (g <= 100)
							{
								var b = byte.Parse(values[2].Substring(0, values[2].Length - 1));
								if (b <= 100)
								{
									const double toDouble = 1.0 / 100.0;
									return Color.FromScRgb(1.0f, (float)(toDouble * r), (float)(toDouble * g), (float)(toDouble * b));
								}
							}
						}
					}
					throw new FormatException(string.Format("The {0} string passed in the colorString argument is not a recognized Color format (rgb(R%,G%,B%)).", colorString));
				}
				else
				{
					var values = colorString2.Split(',');
					if (values.Length == 3)
					{
						var r = byte.Parse(values[0]);
						var g = byte.Parse(values[1]);
						var b = byte.Parse(values[2]);
						return Color.FromArgb(255, r, g, b);
					}
					throw new FormatException(string.Format("The {0} string passed in the colorString argument is not a recognized Color format (rgb(R,G,B)).", colorString));
				}
			}

			if (colorString.Length >= 13 && colorString.StartsWith("rgba(") && colorString[colorString.Length - 1] == ')')
			{
				var colorString2 = colorString.Substring(5, colorString.Length - 6);
				if (colorString2.Contains('%'))
				{
					var values = colorString2.Split(',').Select(v => v.Trim()).ToArray();
					if (values.Length == 4 && values.Take(3).All(v => v[v.Length - 1] == '%'))
					{
						var r = byte.Parse(values[0].Substring(0, values[0].Length - 1));
						if (r <= 100)
						{
							var g = byte.Parse(values[1].Substring(0, values[1].Length - 1));
							if (g <= 100)
							{
								var b = byte.Parse(values[2].Substring(0, values[2].Length - 1));
								if (b <= 100)
								{
									var a = double.Parse(values[3]);
									const double toDouble = 1.0 / 100.0;
									return Color.FromScRgb((float)Math.Min(a, 1.0), (float)(toDouble * r), (float)(toDouble * g), (float)(toDouble * b));
								}
							}
						}
					}
					throw new FormatException(string.Format("The {0} string passed in the colorString argument is not a recognized Color format (rgba(R%,G%,B%,A)).", colorString));
				}
				else
				{
					var values = colorString2.Split(',');
					if (values.Length == 4)
					{
						var r = byte.Parse(values[0]);
						var g = byte.Parse(values[1]);
						var b = byte.Parse(values[2]);
						var a = double.Parse(values[3]);
						const double toDouble = 1.0 / 255.0;
						return Color.FromScRgb((float)Math.Min(a, 1.0), (float)(toDouble * r), (float)(toDouble * g), (float)(toDouble * b));
					}
					throw new FormatException(string.Format("The {0} string passed in the colorString argument is not a recognized Color format (rgba(R,G,B,A)).", colorString));
				}
			}

			if (colorString.Length >= 12 && colorString.StartsWith("hsl(") && colorString[colorString.Length - 1] == ')')
			{
				var values = colorString.Substring(4, colorString.Length - 5).Split(',').Select(v => v.Trim()).ToArray();
				if (values.Length == 3 && values.Skip(1).All(v => v[v.Length - 1] == '%'))
				{
					var h = ushort.Parse(values[0]);
					if (h <= 360)
					{
						var s = byte.Parse(values[1].Substring(0, values[1].Length - 1));
						if (s <= 100)
						{
							var l = byte.Parse(values[2].Substring(0, values[2].Length - 1));
							if (l <= 100)
							{
								const double toDouble = 1.0 / 100.0;
								return FromHsl(h, (float)(toDouble * s), (float)(toDouble * l));
							}
						}
					}
				}
				throw new FormatException(string.Format("The {0} string passed in the colorString argument is not a recognized Color format (hsv(H,S%,V%)).", colorString));
			}

			if (colorString.Length >= 15 && colorString.StartsWith("hsla(") && colorString[colorString.Length - 1] == ')')
			{
				var values = colorString.Substring(5, colorString.Length - 6).Split(',').Select(v => v.Trim()).ToArray();
				if (values.Length == 4 && values.Skip(1).Take(2).All(v => v[v.Length - 1] == '%'))
				{
					var h = ushort.Parse(values[0]);
					if (h <= 360)
					{
						var s = byte.Parse(values[1].Substring(0, values[1].Length - 1));
						if (s <= 100)
						{
							var l = byte.Parse(values[2].Substring(0, values[2].Length - 1));
							if (l <= 100)
							{
								var a = double.Parse(values[3]);
								const double toDouble = 1.0 / 100.0;
								return FromHsl(h, (float)(toDouble * s), (float)(toDouble * l), Math.Min(a, 1.0));
							}
						}
					}
				}
				throw new FormatException(string.Format("The {0} string passed in the colorString argument is not a recognized Color format (hsva(H,S%,V%,A)).", colorString));
			}

			if (colorString.StartsWith("sc#"))
			{
				var values = colorString.Substring(3).Split(',');
				if (values.Length == 4)
				{
					var scA = double.Parse(values[0]);
					var scR = double.Parse(values[1]);
					var scG = double.Parse(values[2]);
					var scB = double.Parse(values[3]);
					return Color.FromScRgb((float)scA, (float)scR, (float)scG, (float)scB);
				}
				if (values.Length == 3)
				{
					var scR = double.Parse(values[0]);
					var scG = double.Parse(values[1]);
					var scB = double.Parse(values[2]);
					return Color.FromScRgb(1.0f, (float)scR, (float)scG, (float)scB);
				}
				throw new FormatException(string.Format("The {0} string passed in the colorString argument is not a recognized Color format (sc#[scA,]scR,scG,scB).", colorString));
			}

			var prop = typeof(Colors).GetTypeInfo().GetDeclaredProperty(colorString);
			if (prop != null) return (Color)prop.GetValue(null);

			throw new FormatException(string.Format("The {0} string passed in the colorString argument is not a recognized Color.", colorString));
		}

		public static string ToHex6(this Color color)
		{
			return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
		}

		public static string ToHex8(this Color color)
		{
			return $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
		}
	}
}