using System.Web;
using System.Web.Mvc;

namespace DBE.Net.Facel.WebFacel
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
