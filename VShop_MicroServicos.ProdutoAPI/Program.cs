using Microsoft.EntityFrameworkCore;
using VShop_MicroServicos.ProdutoAPI.Contexto;
using VShop_MicroServicos.ProdutoAPI.Repositorios.Concretas;
using VShop_MicroServicos.ProdutoAPI.Repositorios.Interfaces;
using VShop_MicroServicos.ProdutoAPI.Servicos.Concretas;
using VShop_MicroServicos.ProdutoAPI.Servicos.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// AddJsonOptions ==> Ignora a referência cícilica entre os objetos Produto_x_Categoria
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Acesso aos dados
var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ProdutoAppDbContext>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

// Mapeamento
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Registro/Inclusão dos serviços no container DI para resolver Injeção de Dependências
builder.Services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
builder.Services.AddScoped<ICategoriaServico, CategoriaServico>();
builder.Services.AddScoped<IProdutoServico, ProdutoServico>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
