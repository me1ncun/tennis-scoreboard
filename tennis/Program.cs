using frontend.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using tennis;
using tennis_scoreboard.Models;
using tennis.Database.Repositories.Implementation;
using tennis.Database.Services;
using tennis.Extensions;
using tennis.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<MatchesUtil>();
builder.Services.AddTransient<MatchService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddTransient<IPlayerRepository, PlayerRepository>();
builder.Services.AddTransient<IPlayerService, PlayerService>();
builder.Services.AddTransient<IMatchesRepository, MatchesRepository>();

builder.Services.AddDistributedMemoryCache();//To Store session in Memory, This is default implementation of IDistributedCache    
builder.Services.AddSession();  

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.ApplyMigrations();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
