# KOR.ColorConversions
KOR.ColorConversions is simple color conversation and recognition library for .NET.


### Color Conversions

####Universal Converter:

```csharp
var xcolor = Conversions.ConvertColors(oject color);
```
**color** can be Color(_System.Windows.Media_) or string(hex or rgb or argb), ConvertColors detects automatically and returns,

```csharp
public string Hex { get; set; }
public string Rgb { get; set; }
public string Argb { get; set; }

public string InverseColorHex_1 { get; set; } // hsv based
public string InverseColorHex_2 { get; set; } // rgb based
```


####Single Converters:

```csharp
var xcolor = Conversions.HexString2Color(string color);
var xcolor = Conversions.RgbString2Color(string color);
var xcolor = Conversions.ArgbString2Color(string color);
```
put the string color and retruns **Color**

####Type Converters:

Between _System.Drawing.Color_ and _System.Windows.Media.Color_

```csharp
var xcolor = Conversions.DrawingColortoMediaColor(System.Drawing.Color color);
var xcolor = Conversions.MediaColortoDrawingColor(System.Windows.Media.Color color);
```

**Tip:** this project _System.Windows.Media.Color_ based working.

####Color Inversing

We need color iversing for more readable on different color of backgrounds. So there is twho way,

First, basic dark or light calculating ((r+g+b) / 128);

```csharp
var xcolor = Conversions.InverseColor(object color, bool blackandwhite = false, byte maxblack = 230, byte minwhite = 30);
```

returns object type, you can use like this (Color|string)xcolor.
if your **color** variable is Color, returns string value and at the same time the opposite;

Second way is HSV based color contrast;

```csharp
var xcolor = Conversions.InvertColor(Color color, bool blackandwhite, byte maxblack = 230, byte minwhite = 30)
```

returns Color.

### Find Color Types

Current color types,

```csharp
public enum ColorType
{
    Undefined, Hex, Rgb, Argb
}
```

```csharp
var xcolorMatch = Detector.FindColorType(object color);
```

returns 

```csharp
public class MatchColor
{
    public object Color { get; set; }
    public ColorType Type { get; set; }
}
```

####Match String Color

Like string color validation (with regex),

```csharp
var xcolorType = Detector.MatchColorfromString(string color)
```

####Pase and Extract

Parser (with regex) color and extract given string content,

```csharp
var xcolorType = Detector.ParseExtract(string color)
```

returns MatchColor.