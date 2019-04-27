public static class Utils
{
    public static string RemoveSpaceFromString(string str)
    {
        str = str.Replace(" ", "");
        return str;
    }

    public static float RemoveDecimals(float number, int amountOfDecimals)
    {
        float newNumber = float.Parse(number.ToString("f" + amountOfDecimals.ToString()));
        return newNumber;
    }
}