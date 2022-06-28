using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

// Adiciona opções para o SWAGGER aceitar JWT para testes.
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "VShop_MicroServico.ProductAPI", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"'Bearer' [space] seu token", //Informar no Swagger, a palavra Bearer + espaço + seu TOKEN.
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
         {
            new OpenApiSecurityScheme
            {
               Reference = new OpenApiReference
               {
                  Type = ReferenceType.SecurityScheme,
                  Id = "Bearer"
               },
               Scheme = "oauth2",
               Name = "Bearer",
               In= ParameterLocation.Header
            },
            new List<string> ()
         }
    });
});

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

// Adiciona Autenticação - Jwt_Bearer
builder.Services.AddAuthentication("Bearer")
       .AddJwtBearer("Bearer", options =>
       {
           options.Authority =
             builder.Configuration["VShop.IdentityServer:ApplicationUrl"];

           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateAudience = false
           };
       });

// Adiciona Autorização - Scopo_VSHOP
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "vshop");
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Ordem: 1 (Primeiro Autentica).  
app.UseAuthorization();  // Ordem: 2 (Depois Autoriza).

app.MapControllers();

app.Run();
