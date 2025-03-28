using Server.Domain;
using Server.UseCases;
using Server.UserRepository;
using Server.Service;

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

builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<UserUseCase>();

var app = builder.Build();

app.MapPost("/users", (UserUseCase userUseCase, User user) =>
{
    try
    {   
        userUseCase.CreateUser(user.Username, user.Email, user.Password);
        return Results.Created($"/users/{user.Email}", user);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro {ex.Message}");
        return Results.BadRequest(new { ex.Message });
    }
});

app.MapGet("/users/{email}", (UserUseCase userUseCase, string email) =>
{
    var user = userUseCase.GetUserByEmail(email);
    return user is not null ? Results.Json(user) : Results.NotFound(new { Message = "User not found." });
});

app.MapPut("/users", (UserUseCase userUseCase, User user) =>
{
    try
    {
        userUseCase.UpdateUser(user);
        return Results.Ok(new { Message = "User updated successfully." });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { ex.Message });
    }
});

app.MapDelete("/users/{email}", (UserUseCase userUseCase, string email) =>
{
    try
    {
        userUseCase.DeleteUser(email);
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { ex.Message });
    }
});

app.Run();
