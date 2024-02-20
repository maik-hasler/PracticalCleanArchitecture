using Api;
using Application;
using Application.Auctions.Queries.GetAuctions;
using Infrastructure;
using Mediator;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddApiServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.UseSwaggerUi(settings =>
{
    settings.Path = "/api";
    settings.DocumentPath = "/api/specification.json";
});

app.MapGet("/auctions", async (
    IMediator mediator) =>
{
    var query = new GetAuctionsQuery();

    var result = await mediator.Send(query);

    return result.IsSuccess
        ? Results.Ok(result.Value)
        : Results.BadRequest();
});

app.Run();
