using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grocery_Store.Models
{
    public class GioHang
    {
        public SANPHAM sanPham { get; set; }
        public int soLuong { get; set; }
        public GioHang(SANPHAM sanPham, int soLuong)
        {
            this.sanPham = sanPham;
            this.soLuong = soLuong;
        }
        public int tongCong
        {
            get
            {
                return ((int)(soLuong * sanPham.GiaKM ?? sanPham.GiaBan));
            }
        }
        public static List<GioHang> GetGioHang()
        {
            List<GioHang> gioHangs = new List<GioHang>();
            if (!HttpContext.Current.Request.Cookies.AllKeys.Contains("GioHang")) return gioHangs;
            string cookie = HttpContext.Current.Request.Cookies["GioHang"].Value;
            if (String.IsNullOrEmpty(cookie) || cookie.Equals("")) return gioHangs;
            string[] id_sl = cookie.Split(',');
            foreach (string i in id_sl)
            {
                string[] gh = i.Split('|');
                GroceryStoreDB db = new GroceryStoreDB();
                try
                {
                    SANPHAM SANPHAM = db.SANPHAMs.ToList().Where(x => x.ID == Convert.ToInt32(gh[0])).First();
                    GioHang gioHang = new GioHang(SANPHAM, Convert.ToInt32(gh[1]));
                    gioHangs.Add(gioHang);
                }
                catch
                {
                    continue;
                }

            }
            return gioHangs;
        }

        public static double TongHang()
        {
            List<GioHang> gioHangs = GioHang.GetGioHang();
            int tc = 0;
            foreach (GioHang gh in gioHangs)
            {
                tc += gh.soLuong * (gh.sanPham.GiaKM ?? gh.sanPham.GiaBan);
            }
            return tc;
        }
    }
}