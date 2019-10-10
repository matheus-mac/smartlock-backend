using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace smartlock_backend
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Serviços e configuração da API da Web

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ActionsApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            //var corsAttribute = new EnableCorsAttribute("http://localhost:3000", "*","*");
            var corsAttributeProduction = new EnableCorsAttribute("https://modest-stallman-a69b78.netlify.com", "*", "*");
            //config.EnableCors(corsAttribute);
            config.EnableCors(corsAttributeProduction);
        }
    }
}
