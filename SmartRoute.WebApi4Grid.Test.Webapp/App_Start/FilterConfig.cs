using System.Web;
using System.Web.Mvc;

namespace SmartRoute.WebApi4Grid.Test.Webapp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}