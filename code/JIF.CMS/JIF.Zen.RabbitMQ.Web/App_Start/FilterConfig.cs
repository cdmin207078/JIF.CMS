using System.Web;
using System.Web.Mvc;

namespace JIF.Zen.RabbitMQ.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}