using BarManager.ApiClients;
using BarManager.Components;
using BarManager.Components.Pages.Events;
using Radzen;

namespace BarManager;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddServiceDefaults();
        // Add services to the container.
        builder.Services.AddRazorComponents().AddInteractiveServerComponents();

        builder.Services.AddRadzenComponents();

        builder.Services.AddScoped<DialogService>();
        builder.Services.AddScoped<EventsService>();

        builder.Services.AddHttpClient<TeamMemberApiClient>(client =>
        {
            client.BaseAddress = new("https+http://BarManagerAPI/");
        });

        builder.Services.AddHttpClient<MenuCategoryApiClient>(client =>
        {
            client.BaseAddress = new("https+http://BarManagerAPI/");
        });

        builder.Services.AddHttpClient<MenuItemsApiClient>(client =>
        {
            client.BaseAddress = new("https+http://BarManagerAPI/");
        });

        builder.Services.AddHttpClient<EventsApiClient>(client =>
        {
            client.BaseAddress = new("https+http://BarManagerAPI/");
        });

        var app = builder.Build();

        app.MapDefaultEndpoints();
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

        app.Run();
    }
}
