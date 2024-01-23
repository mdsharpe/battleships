using Application;
using Infrastructure;
using Server.Endpoints;
using Server.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    // TODO - Adds multiple times to JoinGame endpoint.
    config.OperationFilter<PlayerHeaderFilter>();
});

builder.Services
    .AddInfrastructure()
    .AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGameEndpoints();

app.Run();
