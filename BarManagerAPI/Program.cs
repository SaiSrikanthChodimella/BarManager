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
            builder.AddServiceDefaults();
            builder.Services.AddAuthorization();
            builder.Services.AddControllers();

            builder.Services.AddDbContext<BarManagerDBContext>(options => options.UseInMemoryDatabase("MenuItems"));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddProblemDetails();
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            app.MapDefaultEndpoints();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseExceptionHandler();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}