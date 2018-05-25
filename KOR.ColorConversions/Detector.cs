using System.Text.RegularExpressions;
using System.Windows.Media;

namespace KOR.ColorConversions
{
	/// <summary>
	/// Color detector class
	/// </summary>
    public static class Detector
    {
		/// <summary>
		/// Find color type with color parse and extract
		/// </summary>
        public static bool Extract { get; set; }

		/// <summary>
		/// Color parser # (hash) option
		/// </summary>
        public static bool Hash { get; set; }

		/// <summary>
		/// Alpha channel selection for color parser
		/// </summary>
        public static bool Alpha { get; set; }

		/// <summary>
		/// Color Type finder
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
        public static MatchColor FindColorType(object color)
        {
            if (color != null)
            {
                if (color is string)
                {
                    if (Extract)
                    {
                        return ParseExtract((string)color);
                    }

                    return new MatchColor()
                    {
                        Color = (string)color,
                        Type = MatchColorfromString((string)color)
                    };

                }
                else if (color is Color)
                {
                    return new MatchColor()
                    {
                        Color = (Color)color,
                        Type = ColorType.Argb
                    };
                }
            }

            return new MatchColor()
            {
                Color = null,
                Type = ColorType.Undefined
            };
        }

		/// <summary>
		/// Color match from string
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
        public static ColorType MatchColorfromString(string color)
        {
            #region Hex Control

            string hexpettern = @"^#(?:[0-9a-fA-F]{3,4}){1,2}$";

            var match = new Regex(hexpettern).Match(color);
            if (match.Success)
            {
                return ColorType.Hex;
            }

            #endregion

            #region Rgb Control

            string rgbpettern = @"^\(?([01]?\d\d?|2[0-4]\d|25[0-5])(\W+)([01]?\d\d?|2[0-4]\d|25[0-5])\W+(([01]?\d\d?|2[0-4]\d|25[0-5])\)?)$";
            match = new Regex(rgbpettern).Match(color);
            if (match.Success)
            {
                return ColorType.Rgb;
            }

            #endregion

            #region Argb Control

            string argbpettern = @"^\(?([01]?\d\d?|2[0-4]\d|25[0-5])(\W+)([01]?\d\d?|2[0-4]\d|25[0-5])(\W+)([01]?\d\d?|2[0-4]\d|25[0-5])\W+(([01]?\d\d?|2[0-4]\d|25[0-5])\)?)$";
            match = new Regex(argbpettern).Match(color);
            if (match.Success)
            {
                return ColorType.Argb;
            }

            #endregion

            return ColorType.Undefined;
        }

		/// <summary>
		/// Content parser for color
		/// </summary>
		/// <param name="content"></param>
		/// <returns></returns>
        public static MatchColor ParseExtract(string content)
        {
            #region Hex Control

            string hexpettern = Hash ? "#" : "";
            hexpettern += "(?:[0-9a-fA-F]{3" + (Alpha ? ",4" : "") + "}){1,2}";

            var match = new Regex(hexpettern).Match(content);
            if (match.Success)
            {
                return new MatchColor()
                {
                    Color = match.Value,
                    Type = ColorType.Hex
                };
            }

            #endregion

            #region Rgb Control

            string rgbpettern = @"\(?([01]?\d\d?|2[0-4]\d|25[0-5])(\W+)([01]?\d\d?|2[0-4]\d|25[0-5])\W+(([01]?\d\d?|2[0-4]\d|25[0-5])\)?)";
            match = new Regex(rgbpettern).Match(content);
            if (match.Success)
            {
                return new MatchColor()
                {
                    Color = match.Value,
                    Type = ColorType.Rgb
                };
            }

            #endregion

            #region Argb Control

            string argbpettern = @"\(?([01]?\d\d?|2[0-4]\d|25[0-5])(\W+)([01]?\d\d?|2[0-4]\d|25[0-5])(\W+)([01]?\d\d?|2[0-4]\d|25[0-5])\W+(([01]?\d\d?|2[0-4]\d|25[0-5])\)?)";
            match = new Regex(argbpettern).Match(content);
            if (match.Success)
            {
                return new MatchColor()
                {
                    Color = match.Value,
                    Type = ColorType.Argb
                };
            }

            #endregion

            return new MatchColor()
            {
                Color = string.Empty,
                Type = ColorType.Hex
            };
        }
    }
}
