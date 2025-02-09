using fullstack_repository_pattern;
using fullstack_repository_pattern.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();

var useCase = new CarsUseCase();

app.MapGet("/garage", () =>
{
    return useCase.GetGarageCars();
});

app.MapPost("/garage", (Car car) =>
{
    
    if (!useCase.AddCar(car, out var errors))
    {
        return Results.BadRequest(new { Errors = errors });
    }
    
    return Results.Created($"/garage/{car.Make}-{car.Model}", car);
});

app.Run();