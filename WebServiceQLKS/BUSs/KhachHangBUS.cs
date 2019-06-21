using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3;
using System.Data;
using System.Data.SqlClient;
using WebApplication3.BUSs;

namespace WebApplication3
{
    public class KhachHangBUS
    {
        QLKSDataContext db = new QLKSDataContext();
        private static HoaDonDichVuBUS hdDichVuBUS = new HoaDonDichVuBUS();
        private static DichVuBUS dichVuBUS = new DichVuBUS();
        private static DatPhongTaiChoBUS datPhongTCBUS = new DatPhongTaiChoBUS();
        //private static DatPhongTruocBUS datPhongTruocBUS = new DatPhongTruocBUS();

        public List<HoaDonDV> LayDanhSachDichVuKH(string maKhachHang)
        {
            List<HoaDonDV> dsCacDV = new List<HoaDonDV>();
            foreach (HoaDonDV hdDV in hdDichVuBUS.HienThiHoaDonDV())
            {
                if(hdDV.MaKH == maKhachHang)
                {
                    dsCacDV.Add(hdDV);
                }
            }
            return dsCacDV;
        }
        public bool KiemTraTrungMaKH(string maKhachHang)
        {
            foreach (KhachHang kh in HienThiKhachHang())
            {
                if (kh.MaKH == maKhachHang) return true;
            }
            return false;
        }
        public Decimal TongTienDichVu(string maKhachHang)
        {
            decimal tongTienDV = 0;
            List<HoaDonDV> dsCacDV = LayDanhSachDichVuKH(maKhachHang);
            int doDaiDS = dsCacDV.Count;
            if(doDaiDS != 0)
            {
                foreach (HoaDonDV hoaDonDV in dsCacDV)
                {
                    tongTienDV += dichVuBUS.TinhTienTungDV(hoaDonDV.MaDV, Convert.ToInt16(hoaDonDV.SoLuongDV));
                }
            }
            return tongTienDV;
        }
        public List<KhachHang> HienThiKhachHang()
        {
            return db.KhachHangs.ToList();
        }
        public String LayTenKhachHangByMaKH(string maKH)
        {
            foreach(KhachHang khachHang in db.KhachHangs.ToList())
            {
                if (khachHang.MaKH == maKH) return khachHang.HoTenKH;
            }
            return null;
        }

        //Note: Khi XoaKhachHang se xoa ca DatPhongTaiCho (neu co) va DatPhongTruoc (neu co)
        public bool XoaKhachHang(string maKhachHang)
        {
            KhachHang khachHang = db.KhachHangs.Single(x => x.MaKH == maKhachHang);
            datPhongTCBUS.XoaDatPhongTaiCho(maKhachHang);
            //datPhongTruocBUS.XoaDatPhongTruoc(maKhachHang);
            db.KhachHangs.DeleteOnSubmit(khachHang);
            db.SubmitChanges();
            return true;
        }
        public bool ThemKhachHang(KhachHang khachHang)
        {
            if(KiemTraTrungMaKH(khachHang.MaKH) == true) return false;
            db.KhachHangs.InsertOnSubmit(khachHang);
            db.SubmitChanges();
            return true;
        }
        public bool CapNhatKhachHang(KhachHang khachHang)
        {
            SqlConnection con = new SqlConnection(db.Connection.ConnectionString);
            SqlCommand command = new SqlCommand("update KhachHang set HoTenKH = @hoTen, DiaChi = @diaChi, Sodt = @soDt, QuocTich = @quocTich, CMND = @CMND WHERE MaKH = @maKH", con);
            command.Parameters.AddWithValue("@hoTen", khachHang.HoTenKH);
            command.Parameters.AddWithValue("@diaChi", khachHang.DiaChi);
            command.Parameters.AddWithValue("@soDt", khachHang.Sodt);
            command.Parameters.AddWithValue("@quocTich", khachHang.QuocTich);
            command.Parameters.AddWithValue("@CMND", khachHang.CMND);
            command.Parameters.AddWithValue("@maKH", khachHang.MaKH);
            con.Open();
            int result = command.ExecuteNonQuery();
            con.Close();
            if (result <= 0) return false;
            else return true;
            
        }
        public String LayMaKhachHangQuaSoPhong(Phong phong)
        {
            DatPhongTaiChoBUS datPhongTcBUS = new DatPhongTaiChoBUS();
            //DatPhongTruocBUS datPhongTruocBUS = new DatPhongTruocBUS();
            string maKhachHang = datPhongTcBUS.LayMaKhachHangQuaSoP(phong.SoPhong);
            if (maKhachHang != null)
            {
                return maKhachHang;
            }
            else
            {
                //maKhachHang = datPhongTruocBUS.LayMaKhachHangQuaSoP(phong.SoPhong);
                if (maKhachHang != null) return maKhachHang;
            }
            return null;
        }
        public KhachHangWeb LayKhachHangQuaMaKH(string maKH)
        {
            KhachHang kh = db.KhachHangs.Single(x => x.MaKH == maKH);
            if (kh != null)
            {
                return new KhachHangWeb(kh.MaKH, kh.HoTenKH, kh.DiaChi, kh.Sodt, kh.QuocTich, kh.CMND);
            }
            return null;
        }

       
    }
}
