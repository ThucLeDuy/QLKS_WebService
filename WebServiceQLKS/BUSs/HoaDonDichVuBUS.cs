using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3;
using System.Data.SqlClient;
using WebApplication3.BUSs;

namespace WebApplication3
{
    public class HoaDonDichVuBUS
    {
        QLKSDataContext db = new QLKSDataContext();
        private static KhachHangBUS khBUS = new KhachHangBUS();
        public List<HoaDonDV> HienThiHoaDonDV()
        {   
            return db.HoaDonDVs.ToList();
        }
        public List<HoaDonDVWeb> LayDanhSachHoaDonDVCua1KHWeb(string maKH)
        {
            List<HoaDonDVWeb> dsHD = new List<HoaDonDVWeb>();
            foreach (HoaDonDV hdDVW in LayDanhSachHoaDonDVCua1KH(maKH))
            {
                dsHD.Add(new HoaDonDVWeb(hdDVW.MaKH, hdDVW.MaDV, hdDVW.SoLuongDV, hdDVW.NgaySuDungDV));
            }
            return dsHD;
        }
        public List<HoaDonDV> LayDanhSachHoaDonDVCua1KH(string maKH)
        {
            List<HoaDonDV> dsHD = new List<HoaDonDV>();
            foreach(HoaDonDV hd in db.HoaDonDVs.ToList())
            {
                if(hd.MaKH == maKH)
                {
                    dsHD.Add(hd);
                }
            }
            return dsHD;
        }
        public HoaDonDV TimHoaDonByMaKhachHang(string maKH)
        {
            foreach (HoaDonDV hdDV in db.HoaDonDVs.ToList())
            {
                if (maKH == hdDV.MaKH) return hdDV;
            }
            return null;
        }
        public bool XoaHoaDonDVTheoNgay(string maKH, DateTime ngaySuDung)
        {
            SqlConnection con = new SqlConnection(db.Connection.ConnectionString);
            SqlCommand command = new SqlCommand("delete from HoaDonDV where HoaDonDV.MaKH = '" + maKH + "' and HoaDonDV.NgaySuDungDV = '" + ngaySuDung + "'", con);
            con.Open();
            int result = command.ExecuteNonQuery();
            con.Close();
            if (result <= 0) return false;
            else return true;
            
        }

        public bool XoaMotDichVuKH(string maKhachHang, string maDV, DateTime ngaySuDungDV)
        {
            SqlConnection con = new SqlConnection(db.Connection.ConnectionString);
            SqlCommand command = new SqlCommand("Delete from HoaDonDV where MaKH = @maKH and MaDV = @maDV and NgaySuDungDV= @ngaySuDungDV", con);
            command.Parameters.AddWithValue("@maKH", maKhachHang);
            command.Parameters.AddWithValue("@maDV", maDV);
            command.Parameters.AddWithValue("@ngaySuDungDV", ngaySuDungDV);

            con.Open();
            int result = command.ExecuteNonQuery();
            con.Close();
            if (result <= 0) return false;
            else return true;

        }
        public bool XoaTatCaDichVuCuaKH(string maKhachHang)
        {
            SqlConnection con = new SqlConnection(db.Connection.ConnectionString);
            SqlCommand command = new SqlCommand("Delete from HoaDonDV where MaKH = '" + maKhachHang + "'", con);
            con.Open();
            int result = command.ExecuteNonQuery();
            con.Close();
            if (result <= 0) return false;
            else return true;

            //db.ExecuteQuery<HoaDonDV>("Delete from HoaDonDV where MaKH = '" + maKhachHang+ "'");
            //db.SubmitChanges();
            
            //return true;
        }
        public bool ThemHoaDonDV(HoaDonDV hoaDonDV)
        {
            db.HoaDonDVs.InsertOnSubmit(hoaDonDV);
            db.SubmitChanges();
            return true;
        }
        public bool ThemHoaDonDVWeb(HoaDonDVWeb hdDVWeb)
        {
            HoaDonDV hd = new HoaDonDV();
            hd.MaDV = hdDVWeb.MaDV;
            hd.MaKH = hdDVWeb.MaKH;
            hd.NgaySuDungDV = hdDVWeb.NgaySuDungDV;
            hd.SoLuongDV = hdDVWeb.SoLuongDV;
            db.HoaDonDVs.InsertOnSubmit(hd);
            db.SubmitChanges();
            return true;
        }
        public bool ThemHoaDonDV(string maKhachHang, string maDV, int soLuongDV, DateTime ngaySuDung)
        {
            string ngaySuDungFormatted = ngaySuDung.ToString("MM-dd-yyyy");
            db.ExecuteQuery<HoaDonDV>("Insert into HoaDonDV values('" + maKhachHang + "','" + maDV + "'," + soLuongDV + ",'" + ngaySuDungFormatted + "')");
            db.SubmitChanges();
            return true;
        }
        public bool CapNhatSoLuongDVHoaDonDV(HoaDonDV hoaDonDV)
        {
            (db.HoaDonDVs.Single(x => x.MaDV == hoaDonDV.MaDV && x.MaKH == hoaDonDV.MaKH && x.NgaySuDungDV == hoaDonDV.NgaySuDungDV)).SoLuongDV = hoaDonDV.SoLuongDV;
            db.SubmitChanges();
            return true;
        }
        public bool CapNhatHoaDonDV(HoaDonDV hoaDonDV)
        {
            XoaMotDichVuKH(hoaDonDV.MaKH, hoaDonDV.MaKH, hoaDonDV.NgaySuDungDV);
            ThemHoaDonDV(hoaDonDV.MaKH, hoaDonDV.MaDV, Convert.ToInt32(hoaDonDV.SoLuongDV), Convert.ToDateTime(hoaDonDV.NgaySuDungDV));
            return true;
        }
    }
}
