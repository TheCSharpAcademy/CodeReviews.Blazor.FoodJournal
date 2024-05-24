using FoodJournal.Components;
using FoodJournal.Data;
using FoodJournal.Repositories;
using FoodJournal.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.Components.Tooltip;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient();
builder.Services.AddFluentUIComponents();
builder.Services.AddDataGridEntityFrameworkAdapter();

builder.Services.AddSingleton<ErrorHandler>();
var connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<FoodJournalDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<ITooltipService, TooltipService>();
builder.Services.AddScoped<IFoodRepository, FoodRepository>();
builder.Services.AddScoped<FoodService>();
builder.Services.AddScoped<IMealRepository, MealRepository>();
builder.Services.AddScoped<MealService>();
builder.Services.AddScoped<Seeder>();
builder.Services.AddScoped<StatsService>();
builder.Services.AddWMBSC();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{

    var services = scope.ServiceProvider;
    try
    {
        var seeder = services.GetRequiredService<Seeder>();
        await seeder.SeedDb();
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine(ex.Message);
    }
}





// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
