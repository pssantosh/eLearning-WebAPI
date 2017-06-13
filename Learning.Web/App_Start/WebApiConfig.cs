using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Learning.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Courses",
                routeTemplate: "api/courses/{id}",
                defaults: new { Controller = "courses", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Students",
                routeTemplate: "api/students/{id}",
                defaults: new { Controller = "students", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name:           "Enrollments",
                routeTemplate:  "api/courses/{courseId}/students/{userName}",
                defaults:       new { Controller = "Enrollment", userName = RouteParameter.Optional }
            );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
