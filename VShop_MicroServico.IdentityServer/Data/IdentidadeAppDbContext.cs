using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VShop_MicroServico.IdentityServer.Data
{
    public class IdentidadeAppDbContext : IdentityDbContext<ApplicationUser> //Tipada com Usuário
    {

        public IdentidadeAppDbContext(DbContextOptions<IdentidadeAppDbContext> options) : base(options)
        {

        }
    }
}
