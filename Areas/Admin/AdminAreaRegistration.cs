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
                    "Them nha san xuat admin",
                    "admin/nha-san-xuat/them-nha-san-xuat",
                    new { controller = "NhaSanXuats", action = "Create", id = UrlParameter.Optional }
                );
            context.MapRoute(
                    "Sua nha san xuat admin",
                    "admin/nha-san-xuat/sua-nha-san-xuat",
                    new { controller = "NhaSanXuats", action = "Edit", id = UrlParameter.Optional }
                );
            context.MapRoute(
                    "Xoa nha san xuat admin",
                    "admin/nha-san-xuat/xoa-nha-san-xuat",
                    new { controller = "NhaSanXuats", action = "Delete", id = UrlParameter.Optional }
                );
            context.MapRoute(
                "Danh muc admin",
                "admin/danh-muc",
                new { controller = "DanhMucsAdmin", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Danh muc con admin",
                "admin/danh-muc-con",
                new { controller = "DanhMucConsAdmin", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Hoa Don admin",
                "admin/hoa-don",
                new { controller = "HoaDonsAdmin", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "San Pham admin",
                "admin/san-pham",
                new { controller = "SanPhamsAdmin", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Khach hang admin",
                "admin/khach-hang",
                new { controller = "AspNetUsersAdmin", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
               "chi tiet HD admin",
               "admin/chi-tiet-hoa-don",
               new { controller = "ChiTietHoaDonsAdmin", action = "Index", id = UrlParameter.Optional }
           );
            context.MapRoute(
             "Xuat hoa don admin",
             "admin/xuat-hoa-don",
             new { controller = "ChiTietHoaDonsAdmin", action = "InHoaDon", id = UrlParameter.Optional }
         );
            context.MapRoute(
               "Slider header admin",
               "admin/slider-header",
               new { controller = "SlideHeadersAdmin", action = "Index", id = UrlParameter.Optional }
           );
            context.MapRoute(
               "Banner admin",
               "admin/banner",
               new { controller = "BannersAdmin", action = "Index", id = UrlParameter.Optional }
           );
            context.MapRoute(
               "Slider footer admin",
               "admin/slider-footer",
               new { controller = "SlideFootersAdmin", action = "Index", id = UrlParameter.Optional }
           );
            context.MapRoute(
               "Voucher admin",
               "admin/voucher",
               new { controller = "VouchersAdmin", action = "Index", id = UrlParameter.Optional }
           );
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}