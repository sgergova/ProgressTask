using Service;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Progress
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
