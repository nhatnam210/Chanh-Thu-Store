using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChanhThuStorePoCo;

namespace ChanhThu_Store.Models.BUS
{
    public class UserBUS
    {
        public static IEnumerable<Voucher> DoiVoucher (String id) 
        {
            var db = new ChanhThuStorePoCoDB();
            return db.Query<Voucher>("select * from Voucher as v, AspNetUsers as u where u.DiemTichLuy <= v.DiemDoi  and u.Id = '" + id + "'");
        }
    }
}