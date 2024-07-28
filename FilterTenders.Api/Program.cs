using FilterTenders.Api;
using FilterTenders.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfiguredHttpClient(builder.Configuration);
builder.Services.AddDependencies();

var app = builder.Build();


app.UseHttpsRedirection();


app.MapGet("api/tenders", async (ITendersService tendersService, IHttpClientFactory factory) =>
{
    return await tendersService.GetTendersAsync();
});

app.Run();