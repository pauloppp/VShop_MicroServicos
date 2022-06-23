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

builder.Services.AddHttpClient("ProdutoAPI", c =>
{
    c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServicoURI:ProdutoAPI")); //Pega do arquivo appsettings.json
    //c.BaseAddress = new Uri("https://localhost:7143"); //Funciona também
});

// Resolve Injeção de Dependência entre interfaces e classes concretas
builder.Services.AddScoped<IProdutoServico, ProdutoServico>();
builder.Services.AddScoped<ICategoriaServico, CategoriaServico>();

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
