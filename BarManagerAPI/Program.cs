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

            //Menu Category 
            app.MapGet("/MenuCategory", async (BarManagerDBContext db) => await db.MenuCategory.ToListAsync());

            app.MapPost("/MenuCategory", async (MenuCategory menuCategoryItem, BarManagerDBContext db) =>
            {
                db.MenuCategory.Add(menuCategoryItem);
                await db.SaveChangesAsync();

                return Results.Created($"/MenuItems/{menuCategoryItem.Id}", menuCategoryItem);
            });

            app.MapPut("/MenuCategory/{id}", async (int id, MenuCategory updatedMenuCategoryItem, BarManagerDBContext db) =>
            {
                var menuCategory = await db.MenuCategory.FindAsync(id);

                if (menuCategory is null) return Results.NotFound();

                menuCategory.Id = updatedMenuCategoryItem.Id;
                menuCategory.Name = updatedMenuCategoryItem.Name;

                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            app.MapDelete("/MenuCategory/{id}", async (int id, BarManagerDBContext db) =>
            {
                if (await db.MenuCategory.FindAsync(id) is MenuCategory menuCategoryItem)
                {
                    db.MenuCategory.Remove(menuCategoryItem);
                    await db.SaveChangesAsync();
                    return Results.Ok(menuCategoryItem);
                }

                return Results.NotFound();
            });

            // Team Members 
            app.MapGet("/TeamMembers", async (BarManagerDBContext db) => await db.TeamMembers.AsNoTracking().ToListAsync());

            app.MapPost("/TeamMembers", async (TeamMembers member, BarManagerDBContext db) =>
            {
                db.TeamMembers.Add(member);
                await db.SaveChangesAsync();

                return Results.Created($"/TeamMembers/{member.Id}", member);
            });

            app.MapPut("/TeamMembers/{id}", async (int id, TeamMembers updatedTeamMember, BarManagerDBContext db) =>
            {
                var TeamMember = await db.TeamMembers.FindAsync(id);

                if (TeamMember is null) return Results.NotFound();

                TeamMember.Name = updatedTeamMember.Name;
                TeamMember.Description = updatedTeamMember.Description;
                TeamMember.UserQuote = updatedTeamMember.UserQuote;
                TeamMember.Image = updatedTeamMember.Image;

                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            app.MapDelete("/TeamMembers/{id}", async (int id, BarManagerDBContext db) =>
            {
                if (await db.TeamMembers.FindAsync(id) is TeamMembers teamMember)
                {
                    db.TeamMembers.Remove(teamMember);
                    await db.SaveChangesAsync();
                    return Results.Ok(teamMember);
                }

                return Results.NotFound();
            });

            app.Run();
        }
    }
}
