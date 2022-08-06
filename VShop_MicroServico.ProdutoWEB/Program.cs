using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using VShop_MicroServico.ProdutoWEB.PontoFlutuante;
using VShop_MicroServico.ProdutoWEB.Servicos.Concretas;
using VShop_MicroServico.ProdutoWEB.Servicos.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews(options =>
{
    options.ModelBinderProviders.Insert(0, new CustomFloatingPointModelBinderProvider());
    // [...]
});

// HttpClientFactory... resolve
builder.Services.AddHttpClient<IProdutoServico, ProdutoServico>("ProdutoAPI", c =>
{
    c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServicoURI:ProdutoAPI")); //Pega do arquivo appsettings.json
    //c.BaseAddress = new Uri("https://localhost:7143"); //Funciona também
});

// HttpClientFactory... resolve
builder.Services.AddHttpClient<ICarrinhoServico, CarrinhoServico>("CarrinhoAPI", c =>
{
    //c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServicoURI:CarrinhoAPI")); //Pega do arquivo appsettings.json
    c.BaseAddress = new Uri("https://localhost:7226"); //Funciona também
});


// Resolve Injeção de Dependência entre interfaces e classes concretas
builder.Services.AddScoped<IProdutoServico, ProdutoServico>();
builder.Services.AddScoped<ICategoriaServico, CategoriaServico>();
builder.Services.AddScoped<ICarrinhoServico, CarrinhoServico>();


// Adiciona Autenticação
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("Cookies", c =>
    {
        c.ExpireTimeSpan = TimeSpan.FromMinutes(10);
        c.Events = new CookieAuthenticationEvents()
        {
            OnRedirectToAccessDenied = (context) =>
            {
                context.HttpContext.Response.Redirect(builder.Configuration["ServicoURI:IdentityServer"] + "/Account/AccessDenied");
                return Task.CompletedTask;
            }
        };
    })
    .AddOpenIdConnect("oidc", options =>
    {
        options.Events.OnRemoteFailure = context =>
        {
            context.Response.Redirect("/");
            context.HandleResponse();

            return Task.FromResult(0);
        };
        options.Authority = "https://localhost:7078"; //builder.Configuration["ServicoURI:IdentityServer"];
        options.GetClaimsFromUserInfoEndpoint = true;
        options.ClientId = "vshop";
        options.ClientSecret = builder.Configuration["Client:Secret"];
        options.ResponseType = "code";
        options.ClaimActions.MapJsonKey("role", "role", "role");
        options.ClaimActions.MapJsonKey("sub", "sub", "sub");
        options.TokenValidationParameters.NameClaimType = "name";
        options.TokenValidationParameters.RoleClaimType = "role";
        options.Scope.Add("vshop");
        options.SaveTokens = true;
    }
);


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

app.UseAuthentication(); //Seguir ordem: 1 (Primeiro-Autentica).
app.UseAuthorization();  //Seguir Ordem: 2 (Depois-Autoriza).

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
