using BuildingBlocks.Infrastructure;
using Projects.Infrastructure;
using Projects.Presentation;
using TaskFlow.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddBuildingBlocks();

builder.Services.AddProjectsInfrastructure(
    builder.Configuration.GetConnectionString("DefaultConnection")!);

builder.Services
    .AddControllers()
    .AddApplicationPart(
        typeof(AssemblyReference).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseGlobalExceptionHandler();

app.MapControllers();

app.Run();