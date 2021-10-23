using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ChanhThu_Store
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "lien he",
              url: "lien-he",
              defaults: new { controller = "ContactUs", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "cua hang",
              url: "cua-hang",
              defaults: new { controller = "SanPhams", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "thong tin",
              url: "thong-tin",
              defaults: new { controller = "AboutUs", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "trang chu",
              url: "trang-chu",
              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "bo suu tap",
              url: "bo-suu-tap",
              defaults: new { controller = "DanhMucCons", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "san pham theo danh muc con",
              url: "cua-hang/danh-muc-con",
              defaults : new { controller = "DanhMucCons", action = "Details", id = UrlParameter.Optional }
          );
            routes.MapRoute(
               name: "chi tiet san pham",
               url: "san-pham/chi-tiet",
               defaults: new { controller = "SanPhams", action = "Details", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "dang ky",
               url: "dang-ky",
               defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "dang nhap",
               url: "dang-nhap",
               defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
              name: "them vao gio",
              url: "them-vao-gio",
              defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "gio hang",
              url: "gio-hang",
              defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "thanh toan",
              url: "thanh-toan",
              defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional }
           );
            routes.MapRoute(
             name: "Thanh toan thanh cong",
             url: "hoan-thanh",
             defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
