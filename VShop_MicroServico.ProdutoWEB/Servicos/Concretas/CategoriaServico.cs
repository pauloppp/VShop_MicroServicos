using System.Net.Http.Headers;
using System.Text.Json;
using VShop_MicroServico.ProdutoWEB.Models;
using VShop_MicroServico.ProdutoWEB.Servicos.Interfaces;

namespace VShop_MicroServico.ProdutoWEB.Servicos.Concretas
{
    public class CategoriaServico : ICategoriaServico
    {
        private const string apiEndPoint = "/api/categorias";
        private CategoriaViewModel? categoriaViewModel;
        private IEnumerable<CategoriaViewModel>? listaCategoriaViewModel;
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _clientFactory;

        public CategoriaServico(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<CategoriaViewModel>> GetAllCategorias(string token)
        {
            var client = _clientFactory.CreateClient("ProdutoAPI");

            // Incluindo o Token no cabeçalho da requisição (RequestHeaders).
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (var response = await client.GetAsync(apiEndPoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    listaCategoriaViewModel = await JsonSerializer.DeserializeAsync<IEnumerable<CategoriaViewModel>>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            };
            return listaCategoriaViewModel;
        }
    }
}
