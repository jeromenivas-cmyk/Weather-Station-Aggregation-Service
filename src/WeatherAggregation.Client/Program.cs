using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using WeatherAggregation.Client;

var configuration =
    new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

string apiUrl = configuration["ApiUrl"]!;
string apiKey = configuration["ApiKey"]!;
int count = int.Parse(configuration["MeasurementCount"]!);

using var client = new HttpClient();

client.DefaultRequestHeaders.Add(
    "X-Api-Key",
    apiKey);

var stream = new MemoryStream();

await using (var writer = new StreamWriter(
    stream,
    Encoding.UTF8,
    leaveOpen: true))
{
    foreach (var item in
             MeasurementGenerator.Generate(count))
    {
        await writer.WriteLineAsync(item);            
    }

    await writer.FlushAsync();
}

stream.Position = 0;

var content = new StreamContent(stream);
content.Headers.ContentType =
    new System.Net.Http.Headers.MediaTypeHeaderValue(
        "application/x-ndjson");

var response =
    await client.PostAsync(apiUrl, content);

response.EnsureSuccessStatusCode();

var result =
    await response.Content.ReadAsStringAsync();

Console.WriteLine(result);