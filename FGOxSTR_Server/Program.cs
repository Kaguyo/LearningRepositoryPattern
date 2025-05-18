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
builder.Services.AddScoped<CreateUserUseCaseInMemory>();
builder.Services.AddScoped<CreateUserUseCase>();
builder.Services.AddScoped<LoginUserUseCaseInMemory>();
builder.Services.AddScoped<LoginUserUseCase>();

var app = builder.Build();

app.MapPost("/users", async (CreateUserUseCaseInMemory createUserUseCase, User user) =>
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

app.MapPost("/auth/login", async (LoginUserUseCaseInMemory loginUseCase, User user) =>
{
    try
    {
        var authenticatedUser = loginUseCase.Execute(user.Email, user.Password);
        if (authenticatedUser == null)
        {
            return Results.Json(new { message = "Credenciais inválidas. Verifique seu email e senha." }, statusCode: 401);
        }

        return Results.Ok(new { message = "Login bem-sucedido", user = authenticatedUser });
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
        return Results.BadRequest(new { ex.Message });
    }
});


app.Run();