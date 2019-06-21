using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using QuanLyKhachSan.DAO;
using WebApplication3;


namespace WebApplication3
{
    public class DatPhongTaiChoBUS
    {
        QLKSDataContext db = new QLKSDataContext();
        KhachHangBUS khBUS = new KhachHangBUS();
        public List<DatPhongTaiCho> HienThiDatPhongTaiCho()
        {
            return db.DatPhongTaiChos.ToList();
        }
        public DatPhongTaiCho LayPhieuDatPhongByMaKH(string maKhachHang)
        {
            foreach(DatPhongTaiCho datPhongTaiCho in HienThiDatPhongTaiCho())
            {
                if(datPhongTaiCho.MaKH == maKhachHang)
                {
                    return datPhongTaiCho;
                }
            }
            return null;         
        }
        public bool KiemTraTrungMaKH(string maKhachHang)
        {
            foreach (DatPhongTaiCho datPgTC in HienThiDatPhongTaiCho())
            {
                if (datPgTC.MaKH == maKhachHang) return true;
            }
            return false;
        }
        public bool XoaDatPhongTaiCho(string maKH)
        {
            //Kiem tra neu CO' KH trong DatPhongTC moi duoc xoa
            if (!KiemTraTrungMaKH(maKH)) return false;
            DatPhongTaiCho datPhongTaiCho = db.DatPhongTaiChos.Single(x => x.MaKH == maKH);
            db.DatPhongTaiChos.DeleteOnSubmit(datPhongTaiCho);
            db.SubmitChanges();
            return true;
        }
        public bool ThemDatPhongTaiCho(DatPhongTaiCho datPhongTaiCho)
        {
            //Kiem tra neu KO co KH trong DatPhongTC moi duoc them
            if (KiemTraTrungMaKH(datPhongTaiCho.MaKH)) return false;
            db.DatPhongTaiChos.InsertOnSubmit(datPhongTaiCho);
            db.SubmitChanges();
            return true;
        }
        public bool CapNhatDatPhongTaiCho(DatPhongTaiCho datPTaiCho)
        {
            XoaDatPhongTaiCho(datPTaiCho.MaKH);
            ThemDatPhongTaiCho(datPTaiCho);
            return true;
        }
        public String LayMaKhachHangQuaSoP(string soPhong)
        {
            foreach(DatPhongTaiCho datPTaiCho in db.DatPhongTaiChos.ToList())
            {
                if(datPTaiCho.SoPhong == soPhong)
                {
                    return datPTaiCho.KhachHang.MaKH;
                }
            }
            Console.WriteLine("Phat hien null datphongtc");
            return null;
        }
    }
}
