using System;
using System.Linq;
using System.Windows.Media;

namespace KOR.ColorConversions
{
	/// <summary>
	/// Color Conversaions
	/// </summary>
	public class Conversions
	{
		/// <summary>
		/// Universal color converter, to all
		/// </summary>
		/// <param name="fromColor"></param>
		/// <returns></returns>
		public static Colors ConvertColors(object fromColor)
		{
			var color = Detector.FindColorType(fromColor);
			Color clr = new Color();

			if (color.Type != ColorType.Undefined)
			{
				if (color.Color is string)
				{
					clr = String2Color((string)color.Color, color.Type);
				}
				else if (color.Color is Color)
				{
					clr = (Color)color.Color;
				}

				var colors = new Colors
				{
					Hex = (clr).ToString(),
					Rgb = (clr).R + "," + (clr).G + "," + (clr).B,
					Argb = (clr).A + "," + (clr).R + "," + (clr).G + "," + (clr).B
				};

				if (InverseColor(color.Color) is string)
				{
					colors.InverseColorHex_1 = Color2String(ArgbString2Color((string)InverseColor(color.Color)), ColorType.Hex);
					colors.InverseColorHex_2 = Color2String(ArgbString2Color((string)InverseColor(color.Color, true)), ColorType.Hex);
				}
				else if (InverseColor(color.Color) is Color)
				{
					colors.InverseColorHex_1 = Color2String((Color)InverseColor(color.Color), ColorType.Hex);
					colors.InverseColorHex_2 = Color2String((Color)InverseColor(color.Color, true), ColorType.Hex);
				}
				return colors;
			}

			return new Colors();
		}

		/// <summary>
		/// To any type color converter
		/// </summary>
		/// <param name="fromColor"></param>
		/// <param name="toColor"></param>
		/// <returns></returns>
		public static object ConvertColor(object fromColor, ColorType toColor)
		{
			var color = Detector.FindColorType(fromColor);

			// if is undefined returns false
			if (color.Type == ColorType.Undefined)
			{
				return false;
			}
			// hex to hex
			else if (color.Type == ColorType.Hex && color.Type == toColor)
			{
				if (color.Color is string)
				{
					return (Color)ColorConverter.ConvertFromString((string)color.Color);
				}
				else if (color.Color is Color)
				{
					return ((Color)color.Color).ToString();
				}
			}
			// hex to rgb
			else if (color.Type == ColorType.Hex && toColor == ColorType.Rgb)
			{
				if (color.Color is string)
				{
					return (Color)ColorConverter.ConvertFromString((string)color.Color);
				}
				else if (color.Color is Color)
				{
					return ((Color)color.Color).R + "," + ((Color)color.Color).G + "," + ((Color)color.Color).B;
				}
			}
			// hex to argb
			else if (color.Type == ColorType.Hex && toColor == ColorType.Argb)
			{
				if (color.Color is string)
				{
					return (Color)ColorConverter.ConvertFromString((string)color.Color);
				}
				else if (color.Color is Color)
				{
					return ((Color)color.Color).A + "," + ((Color)color.Color).R + "," + ((Color)color.Color).G + "," + ((Color)color.Color).B;
				}
			}
			// rgb to hex
			else if (color.Type == ColorType.Rgb && toColor == ColorType.Hex)
			{
				if (color.Color is string)
				{
					return RgbString2Color((string)color.Color);
				}
				else if (color.Color is Color)
				{
					return ((Color)color.Color).R + "," + ((Color)color.Color).G + "," + ((Color)color.Color).B;
				}
			}
			// argb to hex
			else if (color.Type == ColorType.Argb && toColor == ColorType.Hex)
			{
				if (color.Color is string)
				{
					return ArgbString2Color((string)color.Color);
				}
				else if (color.Color is Color)
				{
					return ((Color)color.Color).A + "," + ((Color)color.Color).R + "," + ((Color)color.Color).G + "," + ((Color)color.Color).B;
				}
			}

			return false;
		}

		/// <summary>
		/// Color inverter for reading
		/// </summary>
		/// <param name="color"></param>
		/// <param name="blackandwhite"></param>
		/// <param name="maxblack"></param>
		/// <param name="minwhite"></param>
		/// <returns></returns>
		public static object InverseColor(object color, bool blackandwhite = false, byte maxblack = 230, byte minwhite = 30)
		{
			var clr = Detector.FindColorType(color);

			// hex inversation
			if (clr.Type == ColorType.Hex)
			{
				if (clr.Color is string)
				{
					return InvertColor((Color)ColorConverter.ConvertFromString((string)clr.Color), blackandwhite, maxblack, minwhite);
				}
				else if (clr.Color is Color)
				{
					return InvertColor((Color)clr.Color, blackandwhite, maxblack, minwhite).ToString();
				}
			}
			// rgb inversation
			else if (clr.Type == ColorType.Rgb)
			{
				if (clr.Color is string)
				{
					return InvertColor(RgbString2Color((string)clr.Color), blackandwhite, maxblack, minwhite);
				}
				else if (clr.Color is Color)
				{
					var invcolor = InvertColor((Color)clr.Color, blackandwhite, maxblack, minwhite);
					return invcolor.R + "," + invcolor.G + "," + invcolor.B;
				}
			}
			// argb inversation
			else if (clr.Type == ColorType.Argb)
			{
				if (clr.Color is string)
				{
					return InvertColor(ArgbString2Color((string)clr.Color), blackandwhite, maxblack, minwhite);
				}
				else if (clr.Color is Color)
				{
					var invcolor = InvertColor((Color)clr.Color, blackandwhite, maxblack, minwhite);
					return invcolor.A + "," + invcolor.R + "," + invcolor.G + "," + invcolor.B;
				}
			}

			return false;
		}

		/// <summary>
		/// Color to string (to selected type)
		/// </summary>
		/// <param name="color"></param>
		/// <param name="toColor"></param>
		/// <returns></returns>
		public static string Color2String(Color color, ColorType toColor)
		{
			// hex string
			if (toColor == ColorType.Hex)
			{
				return color.ToString();
			}
			// rgb string
			else if (toColor == ColorType.Rgb)
			{
				return color.R + "," + color.G + "," + color.B;
			}
			// argb string
			else if (toColor == ColorType.Argb)
			{
				return color.A + "," + color.R + "," + color.G + "," + color.B;
			}

			return string.Empty;
		}

		/// <summary>
		/// String to Color (tp selected Type)
		/// </summary>
		/// <param name="color"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static Color String2Color(string color, ColorType type)
		{
			if (type == ColorType.Hex)
			{
				return HexString2Color(color);
			}
			else if (type == ColorType.Rgb)
			{
				return RgbString2Color(color);
			}
			else if (type == ColorType.Argb)
			{
				return ArgbString2Color(color);
			}

			return new Color();
		}

		/// <summary>
		/// Hex string to Color
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public static Color HexString2Color(string color)
		{
			return (Color)ColorConverter.ConvertFromString(color);
		}

		/// <summary>
		/// RGB string to Color
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public static Color RgbString2Color(string color)
		{
			var frames = color.Split(',').Select(f => byte.Parse(f)).ToArray();

			return Color.FromRgb(frames[0], frames[1], frames[2]);
		}

		/// <summary>
		/// ARGB string to Color
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public static Color ArgbString2Color(string color)
		{
			var frames = color.Split(',').Select(f => byte.Parse(f)).ToArray();

			return Color.FromArgb(frames[0], frames[1], frames[2], frames[3]);
		}

		/// <summary>
		/// Alternative color inverter (HSV based)
		/// </summary>
		/// <param name="color"></param>
		/// <param name="blackandwhite"></param>
		/// <param name="maxblack"></param>
		/// <param name="minwhite"></param>
		/// <returns></returns>
		public static Color InvertColor(Color color, bool blackandwhite, byte maxblack = 230, byte minwhite = 30)
		{
			if (blackandwhite)
			{
				Color invertcolor = new Color();

				if (color.R + color.G + color.B / 3 <= 128)
				{
					invertcolor.A = 255;
					invertcolor.R = maxblack;
					invertcolor.G = maxblack;
					invertcolor.B = maxblack;
					return invertcolor;
				}
				else
				{
					invertcolor.A = 255;
					invertcolor.R = minwhite;
					invertcolor.G = minwhite;
					invertcolor.B = minwhite;
					return invertcolor;
				}
			}

			return DrawingColortoMediaColor(ColorToHSV(MediaColortoDrawingColor(color)));
		}

		/// <summary>
		/// System.Drawing.Color to System.Windows.Media.Color converter
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public static Color DrawingColortoMediaColor(System.Drawing.Color color)
		{
			return Color.FromArgb(color.A, color.R, color.G, color.B);
		}

		/// <summary>
		/// System.Windows.Media.Color to System.Drawing.Color converter
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public static System.Drawing.Color MediaColortoDrawingColor(Color color)
		{
			return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
		}

		/// <summary>
		/// Color to HSV converter
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public static System.Drawing.Color ColorToHSV(System.Drawing.Color color)
		{
			int max = Math.Max(color.R, Math.Max(color.G, color.B));
			int min = Math.Min(color.R, Math.Min(color.G, color.B));

			var hue = (color.GetHue() + 180) % 360;
			var saturation = (max == 0) ? 0 : 1d - (1d * min / max);
			var value = max / 255d;

			return HSVToRGB(hue, saturation, value);
		}

		/// <summary>
		/// HSV to RGB Converter
		/// </summary>
		/// <param name="hue"></param>
		/// <param name="saturation"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static System.Drawing.Color HSVToRGB(float hue, double saturation, double value)
		{
			var range = Convert.ToInt32(Math.Floor(hue / 60.0)) % 6;
			var f = hue / 60.0 - Math.Floor(hue / 60.0);

			var v = value * 255.0;
			var p = v * (1 - saturation);
			var q = v * (1 - f * saturation);
			var t = v * (1 - (1 - f) * saturation);

			switch (range)
			{
				case 0:
					return System.Drawing.Color.FromArgb(255, (int)v, (int)t, (int)p);
				case 1:
					return System.Drawing.Color.FromArgb(255, (int)q, (int)v, (int)p);
				case 2:
					return System.Drawing.Color.FromArgb(255, (int)p, (int)v, (int)t);
				case 3:
					return System.Drawing.Color.FromArgb(255, (int)p, (int)q, (int)v);
				case 4:
					return System.Drawing.Color.FromArgb(255, (int)t, (int)p, (int)v);
			}
			return System.Drawing.Color.FromArgb(255, (int)v, (int)p, (int)q);
		}
	}
}