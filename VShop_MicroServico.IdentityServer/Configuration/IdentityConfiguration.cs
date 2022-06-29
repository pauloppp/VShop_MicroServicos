﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace VShop_MicroServico.IdentityServer.Configuration
{
    public class IdentityConfiguration
    {
        public const string Admin = "Admin";
        public const string Client = "Client";

        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
      {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
      };

        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
      {
                // vshop é aplicação web que vai acessar
                // o IdentityServer para obter o token
                new ApiScope("vshop", "VShop Server"),
                new ApiScope(name: "read", "Read data."),
                new ApiScope(name: "write", "Write data."),
                new ApiScope(name: "delete", "Delete data."),
      };

        public static IEnumerable<Client> Clients => new List<Client>
       {
               //cliente genérico
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("tutancamom@horus_#$%".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials, //precisa das credenciais do usuário
                    AllowedScopes = {"read", "write", "profile" }
                },
                new Client
                {
                    ClientId = "vshop",
                    ClientSecrets = { new Secret("tutancamom@horus_#$%".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code, //via codigo
                    
                    // O parâmetro RedirectUris abaixo deve receber a URI do projeto "WEB, MVC ou MVC-CORE" que está tentando se logar.
                    // ================================================================================================================================
                    RedirectUris = {"https://localhost:7073/signin-oidc", "http://localhost:17980/signin-oidc"}, //login
                    PostLogoutRedirectUris = {"https://localhost:7073/signout-callback-oidc", "http://localhost:17980/signout-callback-oidc"}, //logout
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "vshop"
                    }
                    // ================================================================================================================================
                }
       };

    }
}
