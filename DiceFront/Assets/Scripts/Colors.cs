using UnityEngine;

public static class Colors
{
    public static readonly Color BG = Hex("#f19070");

    public static readonly Color Blue = Hex("#00c1ef");
    public static readonly Color BlueSelected = Hex("#162a31");
    public static readonly Color BlueBlink = Hex("#45d4f6");

    public static readonly Color Red = Hex("#fe5e56");
    public static readonly Color RedSelected = Hex("#321714");
    public static readonly Color RedBlink = Hex("#fe9691");

    public static readonly Color Grey = Hex("#D8D8D8");
    public static readonly Color GreySelected = Hex("#414850");
    public static readonly Color GreyBlink = Hex("#6b7178");

    public static readonly Color DicePips = Hex("#374957");

    public static readonly Color GreyBG = Hex("#414850");
    public static readonly Color LightGreyBoundary = Hex("#59616b");
    public static readonly Color DiceSide = Hex("#999b9a");

    public static readonly Color White = Hex("#ffffff");
    public static readonly Color Black = Hex("#000000");

    static Color Hex(string hex)
    {
        ColorUtility.TryParseHtmlString(hex, out var c);
        return c;
    }
}
