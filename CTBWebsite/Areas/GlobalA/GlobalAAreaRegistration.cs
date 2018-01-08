using System.Web.Mvc;

namespace CTBWebsite.Areas.GlobalA
{
    public class GlobalAAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GlobalA";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "GlobalA_default",
                "GlobalA/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}