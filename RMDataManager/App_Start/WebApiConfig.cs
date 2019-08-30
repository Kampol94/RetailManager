using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace RMDataManager
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Konfiguracja i usługi składnika Web API
            // Skonfiguruj składnik Web API, aby korzystał tylko z uwierzytelniania za pomocą tokenów bearer.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Trasy składnika Web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
