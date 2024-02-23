using Admin.Shared;
using Raven.DependencyInjection;
using Raven.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services
    .AddRavenDbDocStore() // Create our IDocumentStore singleton using the database settings in appsettings.json
    .AddRavenDbAsyncSession() // Create an Raven IAsyncDocumentSession for every request.
    .AddIdentity<User, IdentityRole>() // Tell ASP.NET to use identity framework.
    .AddRavenDbIdentityStores<User, IdentityRole>(); // Use Raven as the Identity store for user users and roles.

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
