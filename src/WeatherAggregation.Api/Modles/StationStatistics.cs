namespace WeatherAggregation.Api.Models;

public sealed class StationStats
{
    public decimal Min { get; private set; }
    public decimal Max { get; private set; }
    public decimal Sum { get; private set; }
    public long Count { get; private set; }

    public StationStats(decimal temperature)
    {
        Min = temperature;
        Max = temperature;
        Sum = temperature;
        Count = 1;
    }

    public void Add(decimal temperature)
    {
        if (temperature < Min) Min = temperature;
        if (temperature > Max) Max = temperature;

        Sum += temperature;
        Count++;
    }

    public decimal Mean =>
        Math.Round(Sum / Count, 1,
            MidpointRounding.AwayFromZero);
}