// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IndentityServerEcossistema
{
    public static class Config
    {
        //### Configuracao para funcionar requisicoes vindas de APIs
        public static IEnumerable<ApiResource> ApiResources =>
         new List<ApiResource>
         {
                new ApiResource("doughnutapi")
                {                    
                    Scopes = { "doughnutapi", "console-cliente", "swagger-client" }
                }
         };


        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResources.Email(),
                new IdentityResource("roles", "User role(s)", new List<string> { "role" }),                
            };


        /// <summary>
        /// Escopos suportados: Utilizei para que Cliente ReactWeb funcione.
        /// É necessario que os escopos que os cliente configurem estejam nesta lista
        /// </summary>
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("scope1"),
                new ApiScope("scope2"),

                //###
                new ApiScope("doughnutapi", "Doughnut API"),
                new ApiScope("console-cliente"),
                
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {

                #region Clients

                // m2m client credentials flow client
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "scope1" }
                },

                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "https://localhost:44300/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "scope2" }
                },

                
                // React client
                new Client
                {
                    ClientId = "wewantdoughnuts",
                    ClientName = "We Want Doughnuts",
                    ClientUri = "http://localhost:3000",

                    AllowedGrantTypes = GrantTypes.Code,

                    RequireClientSecret = false,

                    RedirectUris =
                    {
                        "http://localhost:3000/signin-oidc",
                    },

                    PostLogoutRedirectUris = { "http://localhost:3000/signout-oidc" },                    
                    AllowedCorsOrigins = { "http://localhost:3000" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "doughnutapi",
                        "roles"
                    },

                    AllowAccessTokensViaBrowser = true, 
                    
                    //Exibir tela de consetimento para o usuário
                    RequireConsent = true,
                    
                },

                



              

               // interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    // where to redirect to after login
                    RedirectUris = { "https://localhost:44340/signin-oidc" },
                    //RedirectUris = { "https://localhost:44340/Home/Privacy" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:44340/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles"
                    },
                    AllowAccessTokensViaBrowser = true,
                    AllowOfflineAccess = true,
                    AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                },




                
                #endregion Clients



                #region Apresentacao PUC


                // Aplicação Web - React PUC 
                new Client
                {
                    ClientId = "react-puc",
                    ClientName = "Cliente PUC",
                    ClientUri = "http://localhost:4200/",

                    AllowedGrantTypes = GrantTypes.Code,

                    RequireClientSecret = false,

                    RedirectUris =
                    {
                        "http://localhost:4200/signin-callback.html",
                    },

                    PostLogoutRedirectUris = { "http://localhost:4200/signout-oidc" },
                    AllowedCorsOrigins = { "http://localhost:4200" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "roles",
                        "doughnutapi",
                    },

                    //Permitir refresh token
                    AllowAccessTokensViaBrowser = true, 
                    AllowOfflineAccess = true,
                    
                    //Exibir tela de consetimento para o usuário
                    RequireConsent = false,
                },


               // API Protegida (Donuts)
               new Client
               {
                    ClientId = "swagger-client",
                    ClientName = "Swagger UI for demo_api",
                    ClientSecrets = {new Secret("swagger-client".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RedirectUris = {"https://localhost:44356/swagger/oauth2-redirect.html"},


                    AllowedCorsOrigins = new List<string>(){
                        "https://localhost:44356",
                        "https://localhost:4200"
                    },

                    AllowedScopes = { "doughnutapi" }
                },


                // Console application cliente
                new Client
                {
                    ClientId = "console-cliente",
                    ClientName = "Client Credentials Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("console-cliente".Sha256()) },
                    AllowedScopes = { "console-cliente" }
                },

               #endregion Apresentacao PUC
            };
    }
}