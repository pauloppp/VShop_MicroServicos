using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using VShop_MicroServico.IdentityServer.Configuration;
using VShop_MicroServico.IdentityServer.Data;
using VShop_MicroServico.IdentityServer.SeedDataBase.Interfaces;

namespace VShop_MicroServico.IdentityServer.SeedDataBase.Concretas
{
    public class DataBaseIdentityServerInitializer : IDataBaseIdentityServerInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataBaseIdentityServerInitializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void InitializeSeedRoles()
        {
            //se o usuario admin não existir cria o usuario , define a senha e atribui ao perfil
            if (_userManager.FindByEmailAsync("admin1@com.br").Result == null)
            {
                //define os dados do usuário admin
                ApplicationUser admin = new ApplicationUser()
                {
                    UserName = "admin1",
                    NormalizedUserName = "ADMIN1",
                    Email = "admin1@com.br",
                    NormalizedEmail = "ADMIN1@COM.BR",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumber = "+55 (11) 2345-6789",
                    PrimeiroNome = "Usuario",
                    UltimoNome = "Admin1",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                //cria o usuário Admin e atribui a senha
                IdentityResult resultAdmin = _userManager.CreateAsync(admin, "Numsei@1234").Result;
                if (resultAdmin.Succeeded)
                {
                    //inclui o usuário admin ao perfil admin
                    _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).Wait();

                    //inclui as claims do usuário admin
                    var adminClaims = _userManager.AddClaimsAsync(admin, new Claim[]
                    {
                    new Claim(JwtClaimTypes.Name, $"{admin.PrimeiroNome} {admin.UltimoNome}"),
                    new Claim(JwtClaimTypes.GivenName, admin.PrimeiroNome),
                    new Claim(JwtClaimTypes.FamilyName, admin.UltimoNome),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
                    }).Result;
                }
            }

            //se o usuario client não existir cria o usuario , define a senha e atribui ao perfil
            if (_userManager.FindByEmailAsync("client1@com.br").Result == null)
            {
                //define os dados do usuário client
                ApplicationUser client = new ApplicationUser()
                {
                    UserName = "client1",
                    NormalizedUserName = "CLIENT1",
                    Email = "client1@com.br",
                    NormalizedEmail = "CLIENT1@COM.BR",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumber = "+55 (11) 2345-6789",
                    PrimeiroNome = "Usuario",
                    UltimoNome = "Client1",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                //cria o usuário Client e atribui a senha
                IdentityResult resultClient = _userManager.CreateAsync(client, "Numsei@1234").Result;
                //inclui o usuário Client ao perfil Client
                if (resultClient.Succeeded)
                {
                    _userManager.AddToRoleAsync(client, IdentityConfiguration.Client).Wait();

                    //adiciona as claims do usuário Client
                    var clientClaims = _userManager.AddClaimsAsync(client, new Claim[]
                    {
                    new Claim(JwtClaimTypes.Name, $"{client.PrimeiroNome} {client.UltimoNome}"),
                    new Claim(JwtClaimTypes.GivenName, client.PrimeiroNome),
                    new Claim(JwtClaimTypes.FamilyName, client.UltimoNome),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
                    }).Result;
                }
            }
        }

        public void InitializeSeedUsers()
        {
            //Se o usuario Admin não existir então cria o usuario e a senha e o atribui ao perfil
            if (_roleManager.FindByNameAsync(IdentityConfiguration.Admin).Result == null)
            {
                //cria o perfil Admin
                IdentityRole roleAdmin = new IdentityRole();
                roleAdmin.Name = IdentityConfiguration.Admin;
                roleAdmin.NormalizedName = IdentityConfiguration.Admin.ToUpper();
                _roleManager.CreateAsync(roleAdmin).Wait();

                //define os dados do usuário admin
                ApplicationUser admin = new ApplicationUser()
                {
                    UserName = "paulo-admin",
                    NormalizedUserName = "PAULO-ADMIN",
                    Email = "paulo_admin@com.br",
                    NormalizedEmail = "PAULO_ADMIN@COM.BR",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumber = "+55 (11) 2345-6789",
                    PrimeiroNome = "Paulo",
                    UltimoNome = "Admin",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                //cria o usuário Admin e atribui a senha
                IdentityResult resultAdmin = _userManager.CreateAsync(admin, "Numsei@1234").Result;
                if (resultAdmin.Succeeded)
                {
                    //inclui o usuário admin ao perfil admin
                    _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).Wait();

                    //inclui as claims do usuário admin
                    var adminClaims = _userManager.AddClaimsAsync(admin, new Claim[]
                    {
                    new Claim(JwtClaimTypes.Name, $"{admin.PrimeiroNome} {admin.UltimoNome}"),
                    new Claim(JwtClaimTypes.GivenName, admin.PrimeiroNome),
                    new Claim(JwtClaimTypes.FamilyName, admin.UltimoNome),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
                    }).Result;
                }
            }

            // se o perfil Client não existir então cria o perfil, cria o usuario e atribui ao perfil
            if (_roleManager.FindByNameAsync(IdentityConfiguration.Client).Result == null)
            {
                //cria o perfil Client
                IdentityRole roleClient = new IdentityRole();
                roleClient.Name = IdentityConfiguration.Client;
                roleClient.NormalizedName = IdentityConfiguration.Client.ToUpper();
                _roleManager.CreateAsync(roleClient).Wait();

                //define os dados do usuário client
                ApplicationUser client = new ApplicationUser()
                {
                    UserName = "paulo-client",
                    NormalizedUserName = "PAULO-CLIENT",
                    Email = "paulo_client@com.br",
                    NormalizedEmail = "PAULO_CLIENT@COM.BR",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumber = "+55 (11) 2345-6789",
                    PrimeiroNome = "Paulo",
                    UltimoNome = "Client",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                //cria o usuário Client e atribui a senha
                IdentityResult resultClient = _userManager.CreateAsync(client, "Numsei@1234").Result;
                //inclui o usuário Client ao perfil Client
                if (resultClient.Succeeded)
                {
                    _userManager.AddToRoleAsync(client, IdentityConfiguration.Client).Wait();

                    //adiciona as claims do usuário Client
                    var clientClaims = _userManager.AddClaimsAsync(client, new Claim[]
                    {
                    new Claim(JwtClaimTypes.Name, $"{client.PrimeiroNome} {client.UltimoNome}"),
                    new Claim(JwtClaimTypes.GivenName, client.PrimeiroNome),
                    new Claim(JwtClaimTypes.FamilyName, client.UltimoNome),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
                    }).Result;
                }
            }
        }
    }
}
