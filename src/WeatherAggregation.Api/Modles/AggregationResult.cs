namespace WeatherAggregation.Api.Models;

public sealed class AggregationResult
{
    public string StationName { get; set; } = string.Empty;

    public decimal Min { get; set; }

    public decimal Mean { get; set; }

    public decimal Max { get; set; }
}