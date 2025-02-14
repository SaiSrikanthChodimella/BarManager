using BarManagerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BarManagerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddDbContext<BarManagerDBContext>(options => options.UseInMemoryDatabase("MenuItems"));

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("/MenuItems", async (BarManagerDBContext db) => await db.MenuItem.ToListAsync());

            app.MapPost("/MenuItems", async (MenuItem menuItem, BarManagerDBContext db) =>
            {
                db.MenuItem.Add(menuItem);
                await db.SaveChangesAsync();

                return Results.Created($"/MenuItems/{menuItem.Id}", menuItem);
            });

            app.MapPut("/Menuitems/{id}", async (int id, MenuItem updatedMenuItem, BarManagerDBContext db) =>
            {
                var menuItem = await db.MenuItem.FindAsync(id);

                if (menuItem is null) return Results.NotFound();

                menuItem.Name = updatedMenuItem.Name;
                menuItem.Description = updatedMenuItem.Description;
                menuItem.Price = updatedMenuItem.Price;
                menuItem.Image = updatedMenuItem.Image;

                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            app.MapDelete("/MenuItems/{id}", async (int id, BarManagerDBContext db) =>
            {
                if (await db.MenuItem.FindAsync(id) is MenuItem menuItem)
                {
                    db.MenuItem.Remove(menuItem);
                    await db.SaveChangesAsync();
                    return Results.Ok(menuItem);
                }

                return Results.NotFound();
            });

            app.Run();
        }
    }
}
