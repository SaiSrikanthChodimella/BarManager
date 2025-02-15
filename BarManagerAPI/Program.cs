using BarManagerAPI.Models;
using BarManagerAPI.Repositories;
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

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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

            app.MapGet("/MenuItems", async (IUnitOfWork unitOfWork) =>
            {
                var items = await unitOfWork.MenuItemRepository.GetAllAsync();
                return Results.Ok(items);
            });

            app.MapPost("/MenuItems", async (MenuItem menuItem, IUnitOfWork unitOfWork) =>
            {
                await unitOfWork.MenuItemRepository.AddAsync(menuItem);
                await unitOfWork.SaveAsync();

                return Results.Created($"/MenuItems/{menuItem.Id}", menuItem);
            });

            app.MapPut("/MenuItems/{id}", async (int id, MenuItem updatedMenuItem, IUnitOfWork unitOfWork) =>
            {
                var menuItem = await unitOfWork.MenuItemRepository.GetByIdAsync(id);

                if (menuItem is null)
                {
                    return Results.NotFound();
                }

                menuItem.Name = updatedMenuItem.Name;
                menuItem.Description = updatedMenuItem.Description;
                menuItem.Price = updatedMenuItem.Price;
                menuItem.Image = updatedMenuItem.Image;

                unitOfWork.MenuItemRepository.Update(menuItem);
                await unitOfWork.SaveAsync();

                return Results.NoContent();
            });

            app.MapDelete("/MenuItems/{id}", async (int id, IUnitOfWork unitOfWork) =>
            {
                var menuItem = await unitOfWork.MenuItemRepository.GetByIdAsync(id);

                if (menuItem is null)
                {
                    return Results.NotFound();
                }
                unitOfWork.MenuItemRepository.Delete(menuItem);

                await unitOfWork.SaveAsync();
                return Results.Ok(menuItem);
            });

            //Menu Category 
            app.MapGet("/MenuCategory", async (IUnitOfWork unitOfWork) =>
            {
                var items = await unitOfWork.MenuCategoryRepository.GetAllAsync();
                return Results.Ok(items);
            });

            app.MapPost("/MenuCategory", async (MenuCategory menuCategoryItem, IUnitOfWork unitOfWork) =>
            {
                await unitOfWork.MenuCategoryRepository.AddAsync(menuCategoryItem);
                await unitOfWork.SaveAsync();

                return Results.Created($"/MenuItems/{menuCategoryItem.Id}", menuCategoryItem);
            });

            app.MapPut("/MenuCategory/{id}", async (int id, MenuCategory updatedMenuCategoryItem, IUnitOfWork unitOfWork) =>
            {
                var menuCategory = await unitOfWork.MenuCategoryRepository.GetByIdAsync(id);

                if (menuCategory is null)
                {
                    return Results.NotFound();
                }

                menuCategory.Id = updatedMenuCategoryItem.Id;
                menuCategory.Name = updatedMenuCategoryItem.Name;

                unitOfWork.MenuCategoryRepository.Update(menuCategory);
                await unitOfWork.SaveAsync();

                return Results.NoContent();
            });

            app.MapDelete("/MenuCategory/{id}", async (int id, IUnitOfWork unitOfWork) =>
            {
                var menuCategory = await unitOfWork.MenuCategoryRepository.GetByIdAsync(id);

                if (menuCategory is null)
                {
                    return Results.NotFound();
                }
                unitOfWork.MenuCategoryRepository.Delete(menuCategory);

                await unitOfWork.SaveAsync();

                return Results.Ok(menuCategory);
            });

            // Team Members 
            app.MapGet("/TeamMembers", async (IUnitOfWork unitOfWork) =>
            {
                var items = await unitOfWork.TeamMembersRepository.GetAllAsync();
                return Results.Ok(items);
            });

            app.MapPost("/TeamMembers", async (TeamMembers member, IUnitOfWork unitOfWork) =>
            {
                await unitOfWork.TeamMembersRepository.AddAsync(member);
                await unitOfWork.SaveAsync();

                return Results.Created($"/TeamMembers/{member.Id}", member);
            });

            app.MapPut("/TeamMembers/{id}", async (int id, TeamMembers updatedTeamMember, IUnitOfWork unitOfWork) =>
            {
                var TeamMember = await unitOfWork.TeamMembersRepository.GetByIdAsync(id);

                if (TeamMember is null)
                {
                    return Results.NotFound();
                }

                TeamMember.Name = updatedTeamMember.Name;
                TeamMember.Description = updatedTeamMember.Description;
                TeamMember.UserQuote = updatedTeamMember.UserQuote;
                TeamMember.Image = updatedTeamMember.Image;

                unitOfWork.TeamMembersRepository.Update(TeamMember);

                await unitOfWork.SaveAsync();

                return Results.NoContent();
            });

            app.MapDelete("/TeamMembers/{id}", async (int id, IUnitOfWork unitOfWork) =>
            {
                var teamMember = await unitOfWork.TeamMembersRepository.GetByIdAsync(id);

                if (teamMember is null)
                {
                    return Results.NotFound();
                }

                unitOfWork.TeamMembersRepository.Delete(teamMember);

                await unitOfWork.SaveAsync();

                return Results.NotFound();
            });

            //Events
            app.MapGet("/Events", async (IUnitOfWork unitOfWork) =>
            {
                var items = await unitOfWork.EventItemsRepository.GetAllAsync();
                return Results.Ok(items);
            });

            app.MapPost("/Events", async (EventItems events, IUnitOfWork unitOfWork) =>
            {
                await unitOfWork.EventItemsRepository.AddAsync(events);
                await unitOfWork.SaveAsync();

                return Results.Created($"/TeamMembers/{events.Id}", events);
            });

            app.MapPut("/Events/{id}", async (int id, EventItems updatedeventItems, IUnitOfWork unitOfWork) =>
            {
                var eventitem = await unitOfWork.EventItemsRepository.GetByIdAsync(id);

                if (eventitem is null)
                {
                    return Results.NotFound();
                }

                eventitem.Id = updatedeventItems.Id;
                eventitem.Start = updatedeventItems.Start;
                eventitem.End = updatedeventItems.End;
                eventitem.Text = updatedeventItems.Text;

                unitOfWork.EventItemsRepository.Update(eventitem);

                await unitOfWork.SaveAsync();

                return Results.NoContent();
            });

            app.MapDelete("/Events/{id}", async (int id, IUnitOfWork unitOfWork) =>
            {
                var eventItem = await unitOfWork.EventItemsRepository.GetByIdAsync(id);

                if (eventItem is null)
                {
                    return Results.NotFound();
                }

                unitOfWork.EventItemsRepository.Delete(eventItem);

                await unitOfWork.SaveAsync();

                return Results.NotFound();
            });

            app.Run();
        }
    }
}
