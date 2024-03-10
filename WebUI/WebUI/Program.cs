using Admin.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Raven.DependencyInjection;
using Raven.Identity;
using WebUI.Client.BackendServices;
using WebUI.Components;
using WebUI.Components.Account;
using WebUI.Frontend;
using WebUI.Services;


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

builder.Services.AddControllers(options =>
{
    options.UseNamespaceRouteToken();
});

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["BackendUrl"]!)
});

builder.Services.AddScoped<IEmailSender<User>, IdentityNoOpEmailSender>();
builder.Services.AddScoped<IBackendService, BackendService>();
builder.Services.AddScoped<IUserService, UserServerService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseStaticFiles();
app.UseAntiforgery();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(WebUI.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
