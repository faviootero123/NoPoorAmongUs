using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Static;
using SaucyCapstone.Data;
using System.Configuration;
using Microsoft.AspNetCore.Identity.UI.Services;
using SaucyCapstone.Services;
using Data;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var services = builder.Services;

// Add services to the container.
services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
});

services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
    options.User.RequireUniqueEmail = true;
    options.Stores.MaxLengthForKeys = 128;
}).AddEntityFrameworkStores<ApplicationDbContext>();

services.AddAuthorization();

services.AddDatabaseDeveloperPageExceptionFilter();
services.AddRazorPages()
    .AddRazorRuntimeCompilation();

services.AddControllersWithViews();
services.Configure<EmailConfiguration>(config.GetSection("EmailConfiguration"));

services.AddScoped<IEmailSender, EmailSender>();
services.AddSession();

var app = builder.Build();

//Seed the data to the database
if (app.Configuration.GetValue<bool>("SeedData"))
{
    await app.Services.SeedDataAsync();
}
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

app.MapRazorPages();

app.Run();