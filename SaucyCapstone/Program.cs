using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Static;
using SaucyCapstone.Data;
using System.Configuration;
using Microsoft.AspNetCore.Identity.UI.Services;
using SaucyCapstone.Services;
using Data;
using Serilog;
using Serilog.Sinks.Grafana.Loki;
using Microsoft.Net.Http.Headers;
using SaucyCapstone.Constants;

var builder = WebApplication.CreateBuilder(args);



var config = builder.Configuration;
var services = builder.Services;

builder.Host.UseSerilog((ctx, lc) =>
{
    var logctx = lc.Enrich.WithProperty("Application", ctx.HostingEnvironment.ApplicationName)
    .Enrich.WithProperty("Environment", ctx.HostingEnvironment.EnvironmentName)
    .WriteTo.Console();
    if (builder.Environment.IsProduction())
    {
        logctx.WriteTo.GrafanaLoki("http://waifu:3100");
    }
});


services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
    options.User.RequireUniqueEmail = true;
    options.Stores.MaxLengthForKeys = 128;
}).AddDefaultTokenProviders()
.AddEntityFrameworkStores<ApplicationDbContext>();

services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", p => p.RequireRole(Roles.Admin));
});

services.AddDatabaseDeveloperPageExceptionFilter();
services.AddRazorPages(options => 
{ 
    options.Conventions.AuthorizeFolder("/Admin", "Admin"); 
}).AddRazorRuntimeCompilation();
services.AddHttpContextAccessor();

services.AddScoped<IEmailSender, EmailSender>();

services.AddSession();

var app = builder.Build();
app.UseSerilogRequestLogging();

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
    app.UseStaticFiles();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseStaticFiles(new StaticFileOptions{
    OnPrepareResponse = ctx => {
        const int durationInSeconds = 60 * 60 * 24;
        ctx.Context.Request.Headers[HeaderNames.CacheControl] = $"public,max-age={durationInSeconds}";
    }
});
}



app.UseRouting();
app.UseSession();

//Handles headers forwarded from nginx
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();