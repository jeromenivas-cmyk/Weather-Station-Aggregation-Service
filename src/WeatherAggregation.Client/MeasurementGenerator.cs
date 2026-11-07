using System.Text.Json;

namespace WeatherAggregation.Client;

public static class MeasurementGenerator
{
    private static readonly string[] Stations =
    {
        "Hamburg",
        "Bulawayo",
        "Palembang",
        "New York",
        "St. John's",
        "Rio De Janeiro"
    };

    private static readonly Random Random = new();

    public static IEnumerable<string> Generate(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var measurement = new
            {
                stationName =
                    Stations[
                        Random.Next(Stations.Length)],

                temperature =
                    Math.Round(
                        (decimal)(Random.NextDouble() * 199.8 - 99.9),
                        1)
            };

            yield return JsonSerializer.Serialize(
                measurement);
        }
    }
}