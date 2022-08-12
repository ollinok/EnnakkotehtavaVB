using Blazored.LocalStorage;
using DataAccess.DbAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMemoryCache();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidAudience = "ennakkotehtava-olli-nokkonen",
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("super salainen avain"))
    };
});

// Video 20
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("employee", policy =>
    {
        policy.RequireClaim("role", "employee");
    });
});

builder.Services.AddSingleton<ISqlAccess, SqlAccess>();
builder.Services.AddSingleton<IArticlesData, ArticlesData>();
builder.Services.AddSingleton<ICustomersData, CustomersData>();
builder.Services.AddSingleton<IOrdersData, OrdersData>();
builder.Services.AddSingleton<IPriceGroupsData, PriceGroupsData>();
builder.Services.AddSingleton<IPricesData, PricesData>();
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

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
