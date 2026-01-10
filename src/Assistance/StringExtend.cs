namespace Assistance;

public static class StringExtend {
    public static string CenterAlign(this string str, int width) => str.PadLeft(((width - str.Length) / 2) + str.Length).PadRight(width);
}
