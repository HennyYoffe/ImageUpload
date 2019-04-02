using System.Web;
using System.Web.Mvc;

namespace Lesson55_Upload_April1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
