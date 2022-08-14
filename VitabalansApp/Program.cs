using DataAccess.DbAccess;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using VitabalansApp.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMemoryCache();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<AuthenticationStateProvider, UserAuthStateProvider>();
builder.Services.AddAuthorizationCore();

builder.Services.AddSingleton<ISqlAccess, SqlAccess>();
builder.Services.AddSingleton<IArticlesData, ArticlesData>();
builder.Services.AddSingleton<ICustomersData, CustomersData>();
builder.Services.AddSingleton<IOrdersData, OrdersData>();
builder.Services.AddSingleton<IPriceGroupsData, PriceGroupsData>();
builder.Services.AddSingleton<IPricesData, PricesData>();
builder.Services.AddSingleton<IDatabaseData, DatabaseData>();
builder.Services.AddScoped<IUsersData, UsersData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
