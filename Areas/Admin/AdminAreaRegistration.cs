using System.Web.Mvc;

namespace ChanhThu_Store.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Home admin",
                "admin",
                new { controller = "MainAdmin", action = "Index", id = UrlParameter.Optional }
            );
        context.MapRoute(
                "Nha san xuat admin",
                "admin/nha-san-xuat",
                new { controller = "NhaSanXuats", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}