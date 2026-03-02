using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.Routing;

namespace App.Web
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
      routes.IgnoreRoute("{resource}.aspx/{*pathInfo}");
      routes.IgnoreRoute("CrystalImageHandler.aspx");
      routes.IgnoreRoute("{*allaspx}", new { allaspx = @".*(CrystalImageHandler).*" });
            //routes.Add(new Route("{*url}", new CustomRouteHandler()));
            routes.MapRoute(
          name: "Default",
          url: "{controller}/{action}/{id}",
          defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
          //namespaces: new[] { string.Format("{0}.Controllers", BuildManager.GetGlobalAsaxType().BaseType.Assembly.GetName().Name) }
      );
    }
  }
}
