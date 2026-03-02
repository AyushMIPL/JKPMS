using System.Web;
using System.Web.Mvc;

namespace App.Web
{
  public class FilterConfig
  {
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
      filters.Add(new HandleErrorAttribute
      {
        ExceptionType = typeof(System.Data.DataException),
        View = "DatabaseError"
      });

      filters.Add(new HandleErrorAttribute(),2);
    }
  }
}
