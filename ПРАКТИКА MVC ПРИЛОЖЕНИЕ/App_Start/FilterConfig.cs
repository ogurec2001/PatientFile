using System.Web;
using System.Web.Mvc;

namespace ПРАКТИКА_MVC_ПРИЛОЖЕНИЕ
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
