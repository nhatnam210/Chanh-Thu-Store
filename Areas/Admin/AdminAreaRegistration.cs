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
            /*Nha san xuat*/
            context.MapRoute(
                    "Nha san xuat admin",
                    "admin/nha-san-xuat",
                    new { controller = "NhaSanXuats", action = "Index", id = UrlParameter.Optional }
                );
            
            context.MapRoute(
                    "Them nha san xuat admin",
                    "admin/nha-san-xuat/them",
                    new { controller = "NhaSanXuats", action = "Create", id = UrlParameter.Optional }
                );
            context.MapRoute(
                    "Sua nha san xuat admin",
                    "admin/nha-san-xuat/chinh-sua",
                    new { controller = "NhaSanXuats", action = "Edit", id = UrlParameter.Optional }
                );
            context.MapRoute(
                    "Xoa nha san xuat admin",
                    "admin/nha-san-xuat/xoa",
                    new { controller = "NhaSanXuats", action = "Delete", id = UrlParameter.Optional }
                );

            /*Danh muc*/
            context.MapRoute(
                    "Danh muc admin",
                    "admin/danh-muc",
                    new { controller = "DanhMucsAdmin", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                    "Them danh muc admin",
                    "admin/danh-muc/them",
                    new { controller = "DanhMucsAdmin", action = "Create", id = UrlParameter.Optional }
                );
            context.MapRoute(
                    "Sua danh muc admin",
                    "admin/danh-muc/chinh-sua",
                    new { controller = "DanhMucsAdmin", action = "Edit", id = UrlParameter.Optional }
                );
            context.MapRoute(
                    "Xoa danh muc admin",
                    "admin/danh-muc/xoa",
                    new { controller = "DanhMucsAdmin", action = "Delete", id = UrlParameter.Optional }
                );

            /*Loai san pham*/
            context.MapRoute(
                    "Loai san pham admin",
                    "admin/loai-san-pham",
                    new { controller = "DanhMucConsAdmin", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                    "Them loai san pham admin",
                    "admin/loai-san-pham/them",
                    new { controller = "DanhMucConsAdmin", action = "Create", id = UrlParameter.Optional }
                );
            context.MapRoute(
                    "Sua loai san pham admin",
                    "admin/loai-san-pham/chinh-sua",
                    new { controller = "DanhMucConsAdmin", action = "Edit", id = UrlParameter.Optional }
                );
            context.MapRoute(
                    "Xoa loai san pham admin",
                    "admin/loai-san-pham/xoa",
                    new { controller = "DanhMucConsAdmin", action = "Delete", id = UrlParameter.Optional }
                );

            /*San pham*/
            context.MapRoute(
                    "San Pham admin",
                    "admin/san-pham",
                    new { controller = "SanPhamsAdmin", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                  "chi tiet san pham admin",
                  "admin/san-pham/chi-tiet",
                  new { controller = "SanPhamsAdmin", action = "Details", id = UrlParameter.Optional }
              );
            context.MapRoute(
                   "Them san pham admin",
                   "admin/san-pham/them",
                   new { controller = "SanPhamsAdmin", action = "Create", id = UrlParameter.Optional }
               );
            context.MapRoute(
                    "Sua san pham admin",
                    "admin/san-pham/chinh-sua",
                    new { controller = "SanPhamsAdmin", action = "Edit", id = UrlParameter.Optional }
                );
            context.MapRoute(
                    "Xoa san pham admin",
                    "admin/san-pham/xoa",
                    new { controller = "SanPhamsAdmin", action = "Delete", id = UrlParameter.Optional }
                );

            /*Hoa don*/
            context.MapRoute(
                    "Hoa Don admin",
                    "admin/hoa-don",
                    new { controller = "HoaDonsAdmin", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                   "chi tiet HD admin",
                   "admin/chi-tiet-hoa-don",
                   new { controller = "ChiTietHoaDonsAdmin", action = "Index", id = UrlParameter.Optional }
           );
            context.MapRoute(
                     "Xuat hoa don admin",
                     "admin/in-hoa-don",
                     new { controller = "HoaDonsAdmin", action = "InHoaDon", id = UrlParameter.Optional }
         );
            context.MapRoute(
                   "xoa HD admin",
                   "admin/hoadon/xoa",
                   new { controller = "HoaDonsAdmin", action = "Delete", id = UrlParameter.Optional }
           );

            /*Khach hang*/
            context.MapRoute(
                    "Khach hang admin",
                    "admin/khach-hang",
                    new { controller = "AspNetUsersAdmin", action = "Index", id = UrlParameter.Optional }
            );

            /*Voucher*/
            context.MapRoute(
                   "Voucher admin",
                   "admin/voucher",
                   new { controller = "VouchersAdmin", action = "Index", id = UrlParameter.Optional }
           );
            context.MapRoute(
                 "Them Voucher admin",
                 "admin/voucher/them",
                 new { controller = "VouchersAdmin", action = "Create", id = UrlParameter.Optional }
            );
            context.MapRoute(
                    "Sua Voucher admin",
                    "admin/voucher/chinh-sua",
                    new { controller = "VouchersAdmin", action = "Edit", id = UrlParameter.Optional }
                );
            context.MapRoute(
                    "Xoa Voucher admin",
                    "admin/voucher/xoa",
                    new { controller = "VouchersAdmin", action = "Delete", id = UrlParameter.Optional }
                );

            /*Thong tin cua hang*/
            context.MapRoute(
                   "Thong tin cua hang admin",
                   "admin/thong-tin-cua-hang",
                   new { controller = "ThongTinCuaHangsAdmin", action = "Index", id = UrlParameter.Optional }
           );
            context.MapRoute(
                  "Chinh sua thong tin cua hang admin",
                  "admin/thong-tin-cua-hang/chinh-sua",
                  new { controller = "ThongTinCuaHangsAdmin", action = "Edit", id = UrlParameter.Optional }
          );
            context.MapRoute(
                 "Chi tiet thong tin cua hang admin",
                 "admin/thong-tin-cua-hang/chi-tiet",
                 new { controller = "ThongTinCuaHangsAdmin", action = "Details", id = UrlParameter.Optional }
         );

            /*slider header*/
            context.MapRoute(
                   "Slider header admin",
                   "admin/chay-anh-dau-trang",
                   new { controller = "SlideHeadersAdmin", action = "Index", id = UrlParameter.Optional }
           );
            context.MapRoute(
                  "Them slider header admin",
                  "admin/chay-anh-dau-trang/them",
                  new { controller = "SlideHeadersAdmin", action = "Create", id = UrlParameter.Optional }
              );
            context.MapRoute(
                    "Sua slider header admin",
                    "admin/chay-anh-dau-trang/chinh-sua",
                    new { controller = "SlideHeadersAdmin", action = "Edit", id = UrlParameter.Optional }
                );
            context.MapRoute(
                    "Xoa slider header admin",
                    "admin/chay-anh-dau-trang/xoa",
                    new { controller = "SlideHeadersAdmin", action = "Delete", id = UrlParameter.Optional }
                );

            /*Slider footer*/
            context.MapRoute(
                   "Slider footer admin",
                   "admin/chay-anh-cuoi-trang",
                   new { controller = "SlideFootersAdmin", action = "Index", id = UrlParameter.Optional }
           );
            context.MapRoute(
                     "Them slider footer admin",
                     "admin/chay-anh-cuoi-trang/them",
                     new { controller = "SlideFootersAdmin", action = "Create", id = UrlParameter.Optional }
             );
            context.MapRoute(
                    "Sua slider footer admin",
                    "admin/chay-anh-cuoi-trang/chinh-sua",
                    new { controller = "SlideFootersAdmin", action = "Edit", id = UrlParameter.Optional }
                );
            context.MapRoute(
                    "Xoa slider footer admin",
                    "admin/chay-anh-cuoi-trang/xoa",
                    new { controller = "SlideFootersAdmin", action = "Delete", id = UrlParameter.Optional }
                );

            /*Banner*/
            context.MapRoute(
                   "Quang cao admin",
                   "admin/quang-cao",
                   new { controller = "BannersAdmin", action = "Index", id = UrlParameter.Optional }
           );
            context.MapRoute(
                    "Them quang cao admin",
                    "admin/quang-cao/them",
                    new { controller = "BannersAdmin", action = "Create", id = UrlParameter.Optional }
            );
            context.MapRoute(
                    "Sua quang cao admin",
                    "admin/quang-cao/chinh-sua",
                    new { controller = "BannersAdmin", action = "Edit", id = UrlParameter.Optional }
                );
            context.MapRoute(
                    "Xoa quang cao admin",
                    "admin/quang-cao/xoa",
                    new { controller = "BannersAdmin", action = "Delete", id = UrlParameter.Optional }
                );

            

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}