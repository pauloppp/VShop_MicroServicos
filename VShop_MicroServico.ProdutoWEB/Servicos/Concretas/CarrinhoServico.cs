using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using VShop_MicroServico.ProdutoWEB.Models;
using VShop_MicroServico.ProdutoWEB.Servicos.Interfaces;

namespace VShop_MicroServico.ProdutoWEB.Servicos.Concretas
{
    public class CarrinhoServico : ICarrinhoServico
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions? _options;
        private const string apiEndpoint = "/api/carrinho";
        private CarrinhoViewModel carrinhoVM = new CarrinhoViewModel();

        public CarrinhoServico(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<CarrinhoViewModel> GetCarrinhoByUserIdAsync(string userId, string token)
        {
            var client = _clientFactory.CreateClient("CarrinhoAPI");
            PutTokenInHeaderAuthorization(token, client);

            using (var response = await client.GetAsync($"{apiEndpoint}/getcarrinho/{userId}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    carrinhoVM = await JsonSerializer.DeserializeAsync<CarrinhoViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return carrinhoVM;
        }

        public async Task<CarrinhoViewModel> AddItemToCarrinhoAsync(CarrinhoViewModel carrinhoVM, string token)
        {
            var client = _clientFactory.CreateClient("CarrinhoAPI");
            PutTokenInHeaderAuthorization(token, client);

            StringContent content = new StringContent(JsonSerializer.Serialize(carrinhoVM), Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync($"{apiEndpoint}/addcarrinho/", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    carrinhoVM = await JsonSerializer.DeserializeAsync<CarrinhoViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return carrinhoVM;
        }

        public async Task<CarrinhoViewModel> UpdateCarrinhoAsync(CarrinhoViewModel carrinhoVM, string token)
        {
            var client = _clientFactory.CreateClient("CarrinhoAPI");
            PutTokenInHeaderAuthorization(token, client);

            CarrinhoViewModel carrinhoUpdated = new CarrinhoViewModel();

            using (var response = await client.PutAsJsonAsync($"{apiEndpoint}/updatecarrinho/", carrinhoVM))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    carrinhoUpdated = await JsonSerializer.DeserializeAsync<CarrinhoViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return carrinhoUpdated;
        }

        public async Task<bool> RemoveItemFromCarrinhoAsync(int carrinhoId, string token)
        {
            var client = _clientFactory.CreateClient("CarrinhoAPI");
            PutTokenInHeaderAuthorization(token, client);

            using (var response = await client.DeleteAsync($"{apiEndpoint}/deletecarrinho/" + carrinhoId))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

        private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        public Task<bool> ApplyCouponAsync(CarrinhoViewModel carrinhoVM, string couponCode, string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveCouponAsync(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ClearCarrinhoAsync(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public Task<CarrinhoViewModel> CheckoutAsync(CarrinhoCabecViewModel carrinhoCabec, string token)
        {
            throw new NotImplementedException();
        }
    }
}
