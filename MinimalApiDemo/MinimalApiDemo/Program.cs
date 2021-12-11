using Microsoft.EntityFrameworkCore;
using MinimalApiDemo.Models;

#region Configs
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PizzaDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
#endregion
app.MapGet("/pizza", async (PizzaDb db) =>
    await db.Todos.ToListAsync());

app.MapGet("/pizza/promo", async (PizzaDb db) =>
    await db.Todos.Where(t => t.IsPromo).ToListAsync());

app.MapGet("/pizza/{id}", async (int id, PizzaDb db) =>
    await db.Todos.FindAsync(id)
        is Pizza todo
            ? Results.Ok(todo)
            : Results.NotFound());

app.MapPost("/pizza", async (Pizza todo, PizzaDb db) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/pizza/{todo.Id}", todo);
});

app.MapPut("/pizza/{id}", async (int id, Pizza inputTodo, PizzaDb db) =>
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Name = inputTodo.Name;
    todo.Price = inputTodo.Price;
    todo.Rating = inputTodo.Rating;
    todo.Ingredients = inputTodo.Ingredients;
    todo.IsPromo = inputTodo.IsPromo;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/pizza/{id}", async (int id, PizzaDb db) =>
{
    if (await db.Todos.FindAsync(id) is Pizza todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.Ok(todo);
    }

    return Results.NotFound();
});

app.Run();