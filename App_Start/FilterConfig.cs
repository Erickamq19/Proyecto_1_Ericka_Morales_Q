using System.Web;
using System.Web.Mvc;

namespace Proyecto_1_Ericka_Morales_Q
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
