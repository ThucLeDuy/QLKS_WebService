using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3;
using WebApplication3.BUSs;

namespace WebApplication3
{
    public class PhongBUS
    {
        QLKSDataContext db = new QLKSDataContext();
        
        public List<Phong> HienThiPhong()
        {
            return db.Phongs.ToList();
        }
        public List<PhongWeb> LayDanhSachPhongWeb()
        {
            List<PhongWeb> dsPhongWeb = new List<PhongWeb>();
            foreach (Phong p in db.Phongs.ToList())
            {
                PhongWeb pw = new PhongWeb(p.SoPhong, p.TenPhong, p.MaLoaiP, p.GiaTrenNgay, p.SoTang, p.TinhTrang, p.SucChua, p.GiaQuaDem);
                dsPhongWeb.Add(pw);
            }
            return dsPhongWeb;        
        }
        public bool KiemTraTrungPhong(string soPhong)
        {
            foreach (Phong phong in HienThiPhong())
            {
                if (phong.SoPhong == soPhong) return true;
            }
            return false;
        }
        public Phong LayPhongQuaSoPhong(string soPhong)
        {
            foreach(Phong phong in HienThiPhong())
            {
                if (phong.SoPhong == soPhong) return phong;
            }
            return null;
        }
        public bool XoaPhong(string soPhong)
        {
            Phong Phong = db.Phongs.Single(x => x.SoPhong == soPhong);
            db.Phongs.DeleteOnSubmit(Phong);
            db.SubmitChanges();
            return true;
        }
        public bool ThemPhong(Phong Phong)
        {
            db.Phongs.InsertOnSubmit(Phong);
            db.SubmitChanges();
            return true;
        }
        public bool CapNhatPhong(Phong phong)
        {
            XoaPhong(phong.SoPhong);
            ThemPhong(phong);
            return true;
        }
    }
}
