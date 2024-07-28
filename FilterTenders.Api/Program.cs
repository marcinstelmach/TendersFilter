using System.Text.Json.Serialization;
using FilterTenders.Api;
using FilterTenders.Api.Filters;
using FilterTenders.Application;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfiguredHttpClient(builder.Configuration);
builder.Services.AddDependencies();
builder.Services.Configure<JsonOptions>(options => options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();
app.UseHttpsRedirection();

app.MapGet("api/tenders", async ([AsParameters] GetTendersQuery request, ITendersService tendersService) =>
{
    // wrap response in view model
    // return Ok
    return await tendersService.GetTendersAsync(request);
}).AddEndpointFilter<GetTendersQueryFilter>();

app.Run();