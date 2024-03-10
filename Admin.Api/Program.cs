using Admin.Api.Extensions;
using Admin.Shared;
using Raven.Client.Documents;
using Raven.DependencyInjection;
using Raven.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
          policy =>
          {
              policy.WithOrigins("https://localhost:7084",
                                  "https://localhost:7112",
                                  "http://localhost:5093");
          });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddRavenDbDocStore() // Create our IDocumentStore singleton using the database settings in appsettings.json
    .AddRavenDbAsyncSession() // Create an Raven IAsyncDocumentSession for every request.
    .AddIdentity<User, IdentityRole>() // Tell ASP.NET to use identity framework.
    .AddRavenDbIdentityStores<User, IdentityRole>(); // Use Raven as the Identity store for user users and roles.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();
// Required for Raven Identity to work with authorization and authentication.
app.UseAuthentication();
app.UseAuthorization();

// Create the database if it doesn't exist.
// Also, create our roles if they don't exist. Needed because we're doing some role-based auth in this demo.
var docStore = app.Services.GetRequiredService<IDocumentStore>();
docStore.EnsureExists();
docStore.EnsureRolesExist(new List<string> { User.AdminRole, User.MemberRole });

app.MapControllers();

app.Run();
