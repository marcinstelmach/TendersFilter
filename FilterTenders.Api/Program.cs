using System.Text.Json.Serialization;
using FilterTenders.Api;
using FilterTenders.Api.ViewModels;
using FilterTenders.Application;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfiguredHttpClient(builder.Configuration);
builder.Services.AddDependencies();
builder.Services.Configure<JsonOptions>(options => options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();
app.UseHttpsRedirection();

app.MapGet("api/tenders", async ([AsParameters] GetTendersRequest request, ITendersService tendersService) =>
{
    return await tendersService.GetTendersAsync();
}).AddEndpointFilter<TendersRequestFilter>();

app.Run();