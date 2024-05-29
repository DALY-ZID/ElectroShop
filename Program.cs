using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniProjet.Net.Models.Repositories;

using MiniProjet.Net.Models;
using MiniProjet.Net.Repositories.CategorieRepositories;
using MiniProjet.Net.Repositories.OrderItemsRepositories;
using MiniProjet.Net.Repositories.OrderRepositories;
using MiniProjet.Net.Repositories.ProductRepositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICommandeRepository, CommandeRepository>();
builder.Services.AddScoped<IPanierRepository, PanierRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemsRepository, OrderItemsRepository>();
builder.Services.AddScoped<ICategorieRepository, CategorieRepository>();
builder.Services.AddScoped<IContenuPanierRepository, ContenuPanierRepository>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ProjectDBConnection")));


builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
