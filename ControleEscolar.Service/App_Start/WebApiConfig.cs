using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;
using Newtonsoft.Json.Converters;

namespace ControleEscolar.Service
{
    public static class WebApiConfig
    {

        public class MyJsonDateTimeConverter : IsoDateTimeConverter
        {
            public MyJsonDateTimeConverter()
            {
                base.DateTimeFormat = "dd/MM/yyyy";
            }
        }

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            var cors = new EnableCorsAttribute("*", "*", "*");            
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                 name: "CustomApi",
                 routeTemplate: "api/{controller}/{action}/{id}",
                 defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // JSON FORMATTER
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            json.Indent = true;

            //json.SerializerSettings.Converters.Add(new MyJsonDateTimeConverter());

            // remover XMLFormatter
            config.Formatters.Clear();
            config.Formatters.Add(json);
        }
    }
}
