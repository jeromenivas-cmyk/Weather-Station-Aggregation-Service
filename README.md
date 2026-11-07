Weather Station Aggregation Service

ASP.NET Core service for aggregating weather station measurements supplied as a JSONL stream.

Features

- Streaming processing
- Handles very large files
- API Key authentication
- Case-insensitive station aggregation
- Camel Case station normalization
- JSONL input/output
- Console test client

Running

API
cd src/WeatherAggregation.Api
dotnet run

Client
cd src/WeatherAggregation.Client
dotnet run

API Request

POST /api/weather/aggregate

Header:

X-Api-Key: my-secret-key

Example body:

{"stationName":"Hamburg","temperature":12.0}
{"stationName":"Hamburg","temperature":34.2}
Example response:

{"stationName":"Hamburg","min":12.0,"mean":23.1,"max":34.2}
