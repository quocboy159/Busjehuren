using Busjehuren.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Busjehuren.FE.Models;

namespace Busjehuren.FE.App_Start
{
    public class PageRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            IContentService contentService = NinjectExt.Default.Get<IContentService>();

            //these would usually come from a database
            var ignorePages = GlobalStatic.IgnoreRoutes.Split(';');
            var pages = contentService.GetAllStaticPages().Where(x => !ignorePages.Contains(x.Alias));

            if (values[parameterName] == null)
                return false;

            var alias = values[parameterName].ToString();

            return pages.Any(x => x.Alias == alias.ToLower());
        }
    }
}