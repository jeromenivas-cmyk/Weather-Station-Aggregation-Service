namespace WeatherAggregation.Api.Models;

public class Measurement
{
    public string StationName { get; set; } = string.Empty;
    public decimal Temperature { get; set; }
}