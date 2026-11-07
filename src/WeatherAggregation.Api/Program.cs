using WeatherAggregation.Api.Endpoints;
using WeatherAggregation.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseMiddleware<ApiDispatcher>();

// app.MapGet("/testkey", (IConfiguration config) =>
// {
//     return Results.Ok(config["WeatherApi:ApiKey"]);
// });

app.MapPost(
    "/api/weather/aggregate",
    WeatherEndpoints.Aggregate);

app.Run();