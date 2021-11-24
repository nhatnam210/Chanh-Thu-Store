using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ChanhThu_Store.Models;
using ChanhThu_Store.Models.DAO;
using Microsoft.AspNet.Identity;


namespace ChanhThu_Store.Controllers
{
    public class CartController : Controller
    {
        ChanhThuStoreContext db = new ChanhThuStoreContext();
        private const string CartSession = "CartSession";
        DateTime thisDay = DateTime.Today;

        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if(cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];
            foreach(var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Sanpham.MaSanPham == item.Sanpham.MaSanPham);
                if(jsonItem != null)
                {
                    item.Soluong = jsonItem.Soluong;
                }
                
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Delete(string id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Sanpham.MaSanPham == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public ActionResult AddItem(string masanpham, int soluong)
        {
            var sanpham = new SanPhamModel().ViewDetail(masanpham);
            var cart = Session[CartSession];
            if(cart != null)
            {
                var list = (List<CartItem>)cart;
                if(list.Exists(x => x.Sanpham.MaSanPham == masanpham))
                {
                    foreach (var item in list)
                    {
                        if (item.Sanpham.MaSanPham == masanpham)
                        {
                            item.Soluong += soluong;
                        }
                        
                    }
                }
                else
                {
                    CartItem item = new CartItem();
                    item.Sanpham = sanpham;
                    item.Soluong = soluong;
                    list.Add(item);
                }
                Session[CartSession] = list;
            }
            else
            {
                CartItem item = new CartItem();
                item.Sanpham = sanpham;
                item.Soluong = soluong;
                List<CartItem> list = new List<CartItem>();
                list.Add(item);
                Session[CartSession] = list;
            }
            //return new HttpStatusCodeResult(204);
            return Redirect(Request.UrlReferrer.ToString());
        }
        [Authorize]
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Payment(string name, string email, string phone,string address ,string ship = "0", string mavoucher = null)
        {
            var userID = User.Identity.GetUserId();
            var giatrigiam = 0;

            if (userID != null)
            {
                HoaDon order = new HoaDon();

                order.NgayLap = DateTime.Now.Date;
                order.Ten = name;
                order.SDT = phone;
                order.Email = email;
                order.DiaChi = address;
                order.MaVoucher = mavoucher;

                if (db.ChiTietVouchers.Any(p => p.MaKhachHang == userID && p.MaVoucher == mavoucher))
                {
                    order.MaVoucher = mavoucher;
                    Voucher dungvoucher = db.Vouchers.Find(mavoucher);
                        giatrigiam = dungvoucher.GiaTriGiam;
                }
                order.MaKhachHang = userID;
                var shipFee = Convert.ToInt32(ship);
                order.Ship = shipFee;
                var total = 0.0;
                try
                {
                    var cart = (List<CartItem>)Session[CartSession];
                    if(cart.Count() > 0)
                    {
                        foreach (var item in cart)
                        {
                            total += item.Sanpham.Gia * item.Soluong;
                        }
                        /*Lấy voucher*/
                        total -= total * Convert.ToDouble(giatrigiam) / 100;
                        order.TongTien = Convert.ToInt32(total);

                        var id = new HoaDonDAO().Insert(order);

                        ChitietHoaDonDAO detailDao = new ChitietHoaDonDAO();
                        foreach (var item in cart)
                        {
                            /*Thay đổi số lượng tồn kho*/
                            SanPham sanphamDB = db.SanPhams.Find(item.Sanpham.MaSanPham);

                            sanphamDB.SoLuongDaBan += item.Soluong;
                            var tonkhomoi = sanphamDB.SoLuongTonKho - item.Soluong;
                            if (tonkhomoi < 0)
                            {
                                tonkhomoi = 0;
                            }
                            sanphamDB.SoLuongTonKho = tonkhomoi;

                            /*Thay đổi điểm tích lũy*/
                            AspNetUser user = db.AspNetUsers.Find(userID);
                            user.DiemTichLuy += item.Sanpham.Diem * item.Soluong;

                            /*Thêm vào chitiethoadon*/
                            ChiTietHoaDon orderDetail = new ChiTietHoaDon();
                            orderDetail.MaSanPham = item.Sanpham.MaSanPham;
                            orderDetail.MaHoaDon = id;
                            orderDetail.DonGia = item.Sanpham.Gia;
                            orderDetail.Soluong = item.Soluong;
                            detailDao.Insert(orderDetail);
                        }

                        /*Cập nhật số lượng voucher của User*/
                        ChiTietVoucher ctvcUser = db.ChiTietVouchers.SingleOrDefault(p => p.MaKhachHang == userID && p.MaVoucher == mavoucher);
                        if (ctvcUser != null)
                        {
                            ctvcUser.SoLuong--;
                            if (ctvcUser.SoLuong == 0)
                            {
                                db.ChiTietVouchers.Remove(ctvcUser);
                            }
                        }
                    }
                }
                catch
                {
                    return Redirect("/loi-thanh-toan");
                }

                db.SaveChanges();
                return Redirect("/hoan-thanh");
            }
                return null;
        }
        public ActionResult Success()
        {
            Session.Clear();
            return View();
        }

        public ActionResult VoucherTheoUser()
        {
            var userID = User.Identity.GetUserId();
            IQueryable<Voucher> listVoucher = null;
            IQueryable<ChiTietVoucher> listChiTietVoucher = null;
            if (userID!=null)
            {
                /*hiện voucher sở hữu + còn hạn*/
                listVoucher = (from vc in db.Vouchers
                               join ctvc in db.ChiTietVouchers on vc.MaVoucher equals ctvc.MaVoucher
                               where vc.MaVoucher == ctvc.MaVoucher && ctvc.MaKhachHang == userID && vc.HanSuDung > thisDay
                               select vc).Distinct();

                /*Cập nhật tình trạng voucher trong chietvoucher*/
                listChiTietVoucher = from ctvc in db.ChiTietVouchers
                                     join vc in db.Vouchers on ctvc.MaVoucher equals vc.MaVoucher
                                     where ctvc.MaKhachHang == userID && vc.HanSuDung < thisDay
                                     select ctvc;

                foreach (var item in listChiTietVoucher)
                {
                    item.TinhTrang = false;
                    ////if ((item.Voucher.HanSuDung - thisDay) > 30) { };
                    //db.ChiTietVouchers.Remove(item);
                }
            }

            db.SaveChanges();
            return PartialView("VoucherTheoUser", listVoucher);
        }

        public ActionResult ThongTinKhachDatHang()
        {
            var userID = User.Identity.GetUserId();
            IQueryable<AspNetUser> thongtin = null;
            if (userID != null)
            {
                thongtin = (from tt in db.AspNetUsers
                            where tt.Id == userID
                            select tt).Distinct();
            }

            return PartialView("ThongTinKhachDatHang", thongtin);
        }
    }
}