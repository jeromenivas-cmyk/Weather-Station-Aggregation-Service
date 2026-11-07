using System.Text;

namespace WeatherAggregation.Api.Helpers;

public static class StationNameHelper
{
    public static string GetKey(string stationName)
    {
        return stationName.Trim().ToUpperInvariant();
    }

    public static string Normalize(string stationName)
    {
        if (string.IsNullOrWhiteSpace(stationName))
            return stationName;

        var sb = new StringBuilder();
        bool startWord = true;

        foreach (char c in stationName)
        {
            if (char.IsLetter(c))
            {
                sb.Append(startWord
                    ? char.ToUpperInvariant(c)
                    : char.ToLowerInvariant(c));

                startWord = false;
            }
            else
            {
                sb.Append(c);

                if (char.IsWhiteSpace(c) ||
                    c == '-' ||
                    c == '.' ||
                    c == '\'')
                {
                    startWord = true;
                }
            }
        }

        return sb.ToString();
    }
}