// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace MultiShop.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>
        {
            new ApiResource("ResourceCatalog")
            {
                Scopes = {"CatalogFullPermission", "CatalogReadPermission"}
            },
            new ApiResource("ResourceOrder")
            {
                Scopes = {"OrderFullPermission"}        
            },
            new ApiResource("ResourceDiscount")
            {
                Scopes = {"DiscountFullPermission"}
            },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
        {
            new ApiScope("CatalogFullPermission","Full access for catalog operations."),
            new ApiScope("CatalogReadPermission","Read access for catalog operations."),
            new ApiScope("OrderFullPermission","Full access for order operations."),
            new ApiScope("DiscountFullPermission","Full access for discount operations."),
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)

        };
        public static IEnumerable<Client> Clients => new List<Client>
        {
            //User
            new Client
            {
                ClientId="MultiShopVisitorId",
                ClientName="MultiShop Visitor User",
                AllowedGrantTypes= GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("multishopsecret".Sha256())},
                AllowedScopes = { "CatalogReadPermission" }
            },

            //Manager
            new Client
            {
                ClientId="MultiShopManagerId",
                ClientName="MultiShop Manager User",
                AllowedGrantTypes= GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("multishopsecret".Sha256())},
                AllowedScopes = { "CatalogFullPermission" }
            },

            //Admin
            new Client
            {
                ClientId="MultiShopAdminId",
                ClientName="MultiShop Admin User",
                AllowedGrantTypes= GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("multishopsecret".Sha256())},
                AllowedScopes = { 
                    "CatalogFullPermission", 
                    "CatalogReadPermission",
                    "OrderFullPermission", 
                    "DiscountFullPermission",
                    IdentityServerConstants.LocalApi.ScopeName,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                },
                AccessTokenLifetime = 600
            }
        };

    }
}