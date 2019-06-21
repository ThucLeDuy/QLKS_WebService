using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.BUSs
{
    public class PhongWeb
    {
        public string SoPhong { get; set; }

        public string TenPhong { get; set; }

        public string MaLoaiP { get; set; }

        public decimal? GiaTrenNgay { get; set; }

        public int? SoTang { get; set; }

        public int? TinhTrang { get; set; }

        public int? SucChua { get; set; }

        public decimal? GiaQuaDem { get; set; }
        public PhongWeb(string _SoPhong, string _TenPhong, string _MaLoaiP,
            decimal? _GiaTrenNgay, int? _SoTang, int? _TinhTrang, int? _SucChua, decimal? _GiaQuaDem)
        {
            this.SoPhong = _SoPhong;
            this.TenPhong = _TenPhong;
            this.MaLoaiP = _MaLoaiP;
            this.GiaTrenNgay = _GiaTrenNgay;
            this.SoTang = _SoTang;
            this.TinhTrang = _TinhTrang;
            this.SucChua = _SucChua;
            this.GiaQuaDem = _GiaQuaDem;

        }

        

        public PhongWeb()
        {

        }

        public static implicit operator PhongWeb(Phong v)
        {
            throw new NotImplementedException();
        }
    }
}