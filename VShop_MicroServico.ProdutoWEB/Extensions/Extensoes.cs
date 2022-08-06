using System.Net.Http.Headers;

namespace VShop_MicroServico.ProdutoWEB.Extensions
{
    public static class Extensoes
    {
        public static void PutTokenInHeaderAuthorization(this HttpClient client, string token)
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

    }
}
