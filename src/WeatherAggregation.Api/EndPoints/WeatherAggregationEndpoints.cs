using System.Text.Json;
using WeatherAggregation.Api.Helpers;
using WeatherAggregation.Api.Models;

namespace WeatherAggregation.Api.Endpoints;

public static class WeatherEndpoints
{
    public static async Task Aggregate(HttpContext context)
    {
        var stations =
            new Dictionary<string, StationStats>();

        using var reader =
            new StreamReader(context.Request.Body);

        string? line;

        while ((line = await reader.ReadLineAsync()) != null)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            Console.WriteLine($"Received: {line}");
            
            var measurement =
                JsonSerializer.Deserialize<Measurement>(
                    line,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            if (measurement == null)
                continue;

            var key =
                StationNameHelper.GetKey(
                    measurement.StationName);

            if (stations.TryGetValue(key, out var stats))
            {
                stats.Add(measurement.Temperature);
            }
            else
            {
                stations[key] =
                    new StationStats(
                        measurement.Temperature);
            }
        }

        context.Response.ContentType =
            "application/x-ndjson";

        foreach (var station in stations
                     .OrderBy(x => x.Key))
        {
            var result =
                new AggregationResult
                {
                    StationName =
                        StationNameHelper.Normalize(
                            station.Key),

                    Min = station.Value.Min,
                    Mean = station.Value.Mean,
                    Max = station.Value.Max
                };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(result));

            await context.Response.WriteAsync("\n");
        }
    }
}