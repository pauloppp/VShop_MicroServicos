using Duende.IdentityServer.Test;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VShop_MicroServico.IdentityServer.Configuration;
using VShop_MicroServico.IdentityServer.Data;
using VShop_MicroServico.IdentityServer.SeedDataBase.Concretas;
using VShop_MicroServico.IdentityServer.SeedDataBase.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Acesso aos dados
var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<IdentidadeAppDbContext>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

// Registro de servi�o do Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<IdentidadeAppDbContext>().AddDefaultTokenProviders();

// Configura��es do IdentityServer
var builderIdentityServer = builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;

}).AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
  .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
  .AddInMemoryClients(IdentityConfiguration.Clients)
  .AddAspNetIdentity<ApplicationUser>();
builderIdentityServer.AddDeveloperSigningCredential();

// Resolve Inje��o de Depend�ncia para a inicializa��o dos Usu�rios.
builder.Services.AddScoped<IDataBaseIdentityServerInitializer, DataBaseIdentityServerInitializer>();

// Inclus�o da lista de usu�rios para inicializa��o (Evita erro "Exception" inicial de AccountController)
// builderIdentityServer.AddTestUsers(new List<TestUser>());

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

app.UseIdentityServer();

app.UseAuthorization();

// Chamando m�todos de inicializa��o dos Usu�rios. 
SeedDataBaseIdentityServer(app);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();





void SeedDataBaseIdentityServer(IApplicationBuilder app)
{
    using (var serviceScope = app.ApplicationServices.CreateScope())
    {
        var initRoleUsers = serviceScope.ServiceProvider.GetService<IDataBaseIdentityServerInitializer>();
        initRoleUsers.InitializeSeedRoles();
        initRoleUsers.InitializeSeedUsers();
    }
}
