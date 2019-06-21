using WebApplication3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.BUSs;

namespace WebApplication3
{
    class TaiKhoanTaiKhoanKHBUS
    {
        QLKSDataContext db = new QLKSDataContext();
        
        public bool KiemTraTrungMaKH(string maKH)
        {
            foreach (TaiKhoanKH taiKhoanKH in HienThiTaiKhoanKH())
            {
                if (taiKhoanKH.MaKH == maKH) return true;
            }
            return false;
        }
        
        public List<TaiKhoanKH> HienThiTaiKhoanKH()
        {
            return db.TaiKhoanKHs.ToList();
        }
        public bool ThayDoiMatKhauWeb(string maKH, string oldPassword, string newPassword)
        {
            if(KiemTraKHDangNhapWeb(maKH, oldPassword))
            {
                db.TaiKhoanKHs.Single(x => x.MaKH == maKH && x.Password == oldPassword).Password = newPassword;
                db.SubmitChanges();
                return true;
            }
            return false;
        }
        public bool KiemTraKHDangNhapWeb(string maKH, string password)
        {
            try
            {
                db.TaiKhoanKHs.Single(x => x.MaKH == maKH && x.Password == password);
            }
            catch (ArgumentNullException)
            {
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            return true;
        }
        public List<TaiKhoanKHWeb> LayDanhSachTaiKhoanWeb()
        {
            List<TaiKhoanKHWeb> dsTKW = new List<TaiKhoanKHWeb>();
            foreach (TaiKhoanKH tk in db.TaiKhoanKHs.ToList())
            {
                dsTKW.Add(new TaiKhoanKHWeb(tk.MaKH, tk.Password, tk.TinhTrang, tk.NgayTao));
            }
            return dsTKW;
        }
        public bool XoaTaiKhoanKH(string maKH)
        {
            TaiKhoanKH taiKhoanKH = db.TaiKhoanKHs.Single(x => x.MaKH == maKH);
            db.TaiKhoanKHs.DeleteOnSubmit(taiKhoanKH);
            db.SubmitChanges();
            return true;
        }
        public bool ThemTaiKhoanKH(TaiKhoanKH taiKhoanKH)
        {
            db.TaiKhoanKHs.InsertOnSubmit(taiKhoanKH);
            db.SubmitChanges();
            return true;
        }
        public bool CapNhatTaiKhoanKH(TaiKhoanKH taiKhoanKH)
        {
            XoaTaiKhoanKH(taiKhoanKH.MaKH);
            ThemTaiKhoanKH(taiKhoanKH);
            return true;
        }
        
    }
}
