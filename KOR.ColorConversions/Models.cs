namespace KOR.ColorConversions
{
    public class Colors
    {
        public string Hex { get; set; }
        public string Rgb { get; set; }
        public string Argb { get; set; }

		public string InverseColorHex_1 { get; set; }
        public string InverseColorHex_2 { get; set; }
    }

    public enum ColorType
    {
        Undefined, Hex, Rgb, Argb
    }

    public class MatchColor
    {
        public object Color { get; set; }
        public ColorType Type { get; set; }
    }
}
