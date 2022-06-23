using Microsoft.AspNetCore.Identity;

namespace VShop_MicroServico.IdentityServer.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string PrimeiroNome { get; set; } = string.Empty;
        public string UltimoNome { get; set; } = string.Empty;
    }
}
