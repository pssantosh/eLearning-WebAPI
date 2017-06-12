﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        }
    }
}
