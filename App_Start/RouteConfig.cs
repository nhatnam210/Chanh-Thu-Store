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
                  name: "gioi thieu",
                  url: "gioi-thieu",
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
                  defaults: new { controller = "DanhMucCons", action = "Details", id = UrlParameter.Optional }
          );
            routes.MapRoute(
                   name: "chi tiet san pham",
                   url: "cua-hang/san-pham",
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
                 name: "Thong tin ca nhan",
                 url: "nguoi-dung",
                 defaults: new { controller = "Account", action = "Info", id = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "yeu thich",
                url: "nguoi-dung/yeu-thich",
                defaults: new { controller = "Account", action = "YeuThich", id = UrlParameter.Optional }
         );
            //routes.MapRoute(
            //   name: "chi tiet thong tin ca nhan",
            //   url: "nguoi-dung/thong-tin-ca-nhan",
            //   defaults: new { controller = "Account", action = "ThongTinCaNhan", id = UrlParameter.Optional }
            //);
            routes.MapRoute(
               name: "chinh sua thong tin ca nhan",
               url: "nguoi-dung/thong-tin-ca-nhan",
               defaults: new { controller = "Account", action = "SuaThongTinCaNhan", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "lich su mua hang",
               url: "nguoi-dung/lich-su-mua-hang",
               defaults: new { controller = "Account", action = "LichSuMuaHang", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "chi tiet hoa don",
                url: "nguoi-dung/lich-su-mua-hang/hoa-don",
                defaults: new { controller = "ChitietHoaDon", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "tim kiem",
                url: "tim-kiem",
                defaults: new { controller = "TimKiem", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "voucher",
                url: "voucher",
                defaults: new { controller = "Vouchers", action = "DanhSachVoucher", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
