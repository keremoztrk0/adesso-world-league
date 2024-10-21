using AdessoWorldLeague.Application;
using AdessoWorldLeague.Application.DTOs;
using AdessoWorldLeague.Application.Features.Commands;
using AdessoWorldLeague.Application.Features.Queries;
using AdessoWorldLeague.Infrastructure;
using AdessoWorldLeague.WebAPI.Middlewares;
using FluentResults;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();
app.UseMiddleware<ExceptionHandlerMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/api/draw-groups", async (DrawGroupRequestModel model, ISender sender) =>
{
    Result<Guid> result = await sender.Send(new DrawGroupsCommand(model.NumberOfGroups, model.FirstNameOfPerson, model.LastNameOfPerson));
    return result.IsSuccess ? TypedResults.CreatedAtRoute("GetDrawResult", new {id = result.Value}) : Results.BadRequest(result.Errors);
});

app.MapGet("/api/draw-result/{id:guid}", async (Guid id,ISender sender) =>
    {
        var result = await sender.Send(new GetDrawResultQuery {Id = id});
        return result is not null ? Results.Ok(result) : Results.NotFound();
    })
    .Produces<DrawResultDto>()
    .WithName("GetDrawResult");


app.UseHttpsRedirection();

app.Run();

internal record DrawGroupRequestModel(int NumberOfGroups, string FirstNameOfPerson, string LastNameOfPerson);