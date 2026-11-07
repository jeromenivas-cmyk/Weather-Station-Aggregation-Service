namespace WeatherAggregation.Api.Models;

public sealed record Measurement(
    string StationName,
    decimal Temperature);