using System.Web;
using System.Web.Mvc;

namespace GateGold
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // Eklediğimiz hata filtresini buraya kaydediyoruz:
            filters.Add(new GateGold.Filters.CustomExceptionFilter());
        }
    }
}
