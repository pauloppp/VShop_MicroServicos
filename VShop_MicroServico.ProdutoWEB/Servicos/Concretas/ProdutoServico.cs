using System.Text;
using System.Text.Json;
using VShop_MicroServico.ProdutoWEB.Models;
using VShop_MicroServico.ProdutoWEB.Servicos.Interfaces;

namespace VShop_MicroServico.ProdutoWEB.Servicos.Concretas
{
    public class ProdutoServico : IProdutoServico
    {
        private const string apiEndPoint = "/api/produtos";
        private ProdutoViewModel? produtoViewModel;
        private IEnumerable<ProdutoViewModel>? listaProdutosViewModel;
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _clientFactory;

        public ProdutoServico(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<ProdutoViewModel>> GetAllProdutos()
        {
            var client = _clientFactory.CreateClient("ProdutoAPI");

            using (var response = await client.GetAsync(apiEndPoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    listaProdutosViewModel = await JsonSerializer.DeserializeAsync<IEnumerable<ProdutoViewModel>>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            };
            return listaProdutosViewModel;
        }

        public async Task<ProdutoViewModel> FindProdutoById(int id)
        {
            var client = _clientFactory.CreateClient("ProdutoAPI");

            using (var response = await client.GetAsync(apiEndPoint + "/" + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    produtoViewModel = await JsonSerializer.DeserializeAsync<ProdutoViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            };
            return produtoViewModel;
        }

        public async Task<ProdutoViewModel> CreateProduto(ProdutoViewModel produtoViewModel)
        {
            var client = _clientFactory.CreateClient("ProdutoAPI");

            StringContent content = new StringContent(JsonSerializer.Serialize(produtoViewModel), Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiEndPoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    produtoViewModel = await JsonSerializer.DeserializeAsync<ProdutoViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            };
            return produtoViewModel;
        }

        public async Task<ProdutoViewModel> UpdateProduto(ProdutoViewModel produtoViewModel)
        {
            var client = _clientFactory.CreateClient("ProdutoAPI");

            ProdutoViewModel produtoAtualizado = new ProdutoViewModel();

            using (var response = await client.PutAsJsonAsync(apiEndPoint, produtoViewModel))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    produtoAtualizado = await JsonSerializer.DeserializeAsync<ProdutoViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            };
            return produtoAtualizado;
        }


        public async Task<bool> DeleteProdutoById(int id)
        {
            var client = _clientFactory.CreateClient("ProdutoAPI");
            
            using (var response = await client.DeleteAsync(apiEndPoint + "/" + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                
            };
            return false;
        }
    }
}
