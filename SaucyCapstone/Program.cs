using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Static;
using SaucyCapstone.Data;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Stores.MaxLengthForKeys = 128;
}).AddEntityFrameworkStores<ApplicationDbContext>();

services.AddAuthorization();

services.AddDatabaseDeveloperPageExceptionFilter();
services.AddRazorPages()
    .AddRazorRuntimeCompilation();

services.AddControllersWithViews();


var app = builder.Build();

//Seed the data to the database
await app.Services.SeedDataAsync();

var mvcBuilder = builder.Services.AddRazorPages();
// Configure the HTTP request pipeline. AKA middleware
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseHttpsRedirection();
    app.UseDeveloperExceptionPage();
    mvcBuilder.AddRazorRuntimeCompilation();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseStaticFiles();

app.UseRouting();

//Handles headers forwarded from nginx
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();