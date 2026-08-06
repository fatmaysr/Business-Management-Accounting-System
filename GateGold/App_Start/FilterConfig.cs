using System.Web;
using System.Web.Mvc;

namespace GateGold
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // Artık hiçbir controller/action, [AllowAnonymous] ile işaretlenmediği
            // sürece login olmadan erişilemez. Önceden bu zorunlu değildi.
            filters.Add(new AuthorizeAttribute());
        }
    }
}
