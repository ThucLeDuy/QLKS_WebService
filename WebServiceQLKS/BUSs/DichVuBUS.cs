using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3;
using WebApplication3.BUSs;

namespace WebApplication3
{
    public class DichVuBUS
    {
        QLKSDataContext db = new QLKSDataContext();
        public List<DichVu> HienThiDichVu()
        {
            return db.DichVus.ToList();
        }
        public List<DichVuWeb> LayDanhSachDVWeb()
        {
            List<DichVuWeb> dsDV = new List<DichVuWeb>();
            foreach(DichVu dv in db.DichVus.ToList())
            {
                dsDV.Add(new DichVuWeb(dv.MaDV, dv.MoTaDV, dv.GiaDV));
            }
            return dsDV;
        }
        
        public DichVu LayDichVuByMaDV(string maDichVu)
        {
            foreach(DichVu dichVu in db.DichVus.ToList())
            {
                if(dichVu.MaDV == maDichVu)
                {
                    return dichVu;
                }
            }
            return null;
        }
        
        public bool KiemTraTrungMaDV(string maDichVu)
        {
            foreach (DichVu kh in HienThiDichVu())
            {
                if (kh.MaDV == maDichVu) return true;
            }
            return false;
        }
        public Decimal TinhTienTungDV(string maDichVu, int soLuong)
        {           
            return Convert.ToDecimal(LayDichVuByMaDV(maDichVu).GiaDV * soLuong);
        }
        public bool XoaDichVu(string maDichVu)
        {
            DichVu dichVU = db.DichVus.Single(x => x.MaDV == maDichVu);
            db.DichVus.DeleteOnSubmit(dichVU);
            db.SubmitChanges();
            return true;
        }
        public bool ThemDichVu(DichVu dichVu)
        {
            db.DichVus.InsertOnSubmit(dichVu);
            db.SubmitChanges();
            return true;
        }
        public bool CapNhatDichVu(DichVu dichVu)
        {
            XoaDichVu(dichVu.MaDV);
            ThemDichVu(dichVu);
            return true;
        }
    }
}
