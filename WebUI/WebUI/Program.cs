using Admin.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Raven.DependencyInjection;
using Raven.Identity;
using WebUI.Components;
using WebUI.Components.Account;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

builder.Services
    .AddRavenDbDocStore() // Create our IDocumentStore singleton using the database settings in appsettings.json
    .AddRavenDbAsyncSession() // Create an Raven IAsyncDocumentSession for every request.
    .AddIdentity<User, Raven.Identity.IdentityRole>() // Tell ASP.NET to use identity framework.
    .AddRavenDbIdentityStores<User, Raven.Identity.IdentityRole>()
    .AddDefaultTokenProviders();// Use Raven as the Identity store for user users and roles.

builder.Services.AddSingleton<IEmailSender<User>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(WebUI.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
