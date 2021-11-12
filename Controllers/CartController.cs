using System;
using System.Collections.Generic;
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
        private const string CartSession = "CartSession";

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
                    var item = new CartItem();
                    item.Sanpham = sanpham;
                    item.Soluong = soluong;
                    list.Add(item);
                }
                Session[CartSession] = list;
            }
            else
            {
                var item = new CartItem();
                item.Sanpham = sanpham;
                item.Soluong = soluong;
                var list = new List<CartItem>();
                list.Add(item);
                Session[CartSession] = list;
            }
            return new HttpStatusCodeResult(204);
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
        public ActionResult Payment(string name , string phone , string email , string address ,string shipping)
        {
            var userID = User.Identity.GetUserId();
            if(userID != null)
            {
                var order = new HoaDon();
                order.NgayLap = DateTime.Now.Date;
                order.Ten = name;
                order.SDT = phone;
                order.Email = email;
                order.DiaChi = address;
                order.MaKhachHang = userID;
                order.Ship = Convert.ToInt32(shipping);
                var total = 0;
                try
                {
                    var cart = (List<CartItem>)Session[CartSession];
                    foreach (var item in cart)
                    {
                        total += item.Sanpham.Gia * item.Soluong + int.Parse(shipping);
                    }
                    order.TongTien = total;
                    var id = new HoaDonDAO().Insert(order);

                    var detailDao = new ChitietHoaDonDAO();

                    foreach (var item in cart)
                    {
                        var orderDetail = new ChiTietHoaDon();
                        orderDetail.MaSanPham = item.Sanpham.MaSanPham;
                        orderDetail.MaHoaDon = id;
                        orderDetail.DonGia = item.Sanpham.Gia;
                        orderDetail.Soluong = item.Soluong;
                        detailDao.Insert(orderDetail);
                    }

                }
                catch (Exception Ex)
                {
                    return Redirect("/loi-thanh-toan");
                }

                return Redirect("/hoan-thanh");
            }
            else
            {
                return null;
            }
          
        }
        public ActionResult Success()
        {
            Session.Clear();
            
            
            return View();
        }


    }
}