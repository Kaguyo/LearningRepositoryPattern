using FGOxSTR_Server.Data;
using FGOxSTR_Server.Domain;
using Microsoft.EntityFrameworkCore;
using FGOxSTR_Server.UseCases;
using FGOxSTR_Server.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder => builder
            .WithOrigins("http://localhost:5173")
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<CreateUserUseCaseTest>();
builder.Services.AddScoped<CreateUserUseCase>();

var app = builder.Build();

app.MapPost("/users", async (CreateUserUseCaseTest createUserUseCase, User user) =>
{
    try
    {   
        var createdUser = await createUserUseCase.Execute(user.Username, user.Email, user.Password);
        return Results.Created($"/users/{createdUser.Email}", createdUser);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
        return Results.BadRequest(new { ex.Message });
    }
});


app.Run();