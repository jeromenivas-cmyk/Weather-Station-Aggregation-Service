namespace WeatherAggregation.Api.Middleware;

public sealed class ApiDispatcher
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public ApiDispatcher(
        RequestDelegate next,
        IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(
                "X-Api-Key",
                out var suppliedKey))
        {
            context.Response.StatusCode = 401;
            return;
        }

        var expectedKey =
            _configuration["WeatherApi:ApiKey"];

        // Console.WriteLine($"Expected: [{expectedKey}]");
        // Console.WriteLine($"Supplied: [{suppliedKey}]");

        if (!string.Equals(
                suppliedKey,
                expectedKey,
                StringComparison.Ordinal))
        {
            context.Response.StatusCode = 401;
            return;
        }

        await _next(context);
    }
}