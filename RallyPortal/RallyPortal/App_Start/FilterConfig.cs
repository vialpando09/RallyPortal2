using System.Web;
using System.Web.Mvc;
using RallyPortal.Filters;

namespace RallyPortal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LayoutSelectorActionFilterAttribute());
            filters.Add(new MessageActionFilterAttribute());
        }
    }
}