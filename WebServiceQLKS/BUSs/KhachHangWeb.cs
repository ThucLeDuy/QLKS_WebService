using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.BUSs
{
    public class KhachHangWeb
    {
        public string _MaKH { get; set; }

        public string _HoTenKH { get; set; }

        public string _DiaChi { get; set; }

        public string _Sodt { get; set; }

        public string _QuocTich { get; set; }

        public string _CMND { get; set; }

        public KhachHangWeb(string _MaKH, string _HoTenKH, string _DiaChi, string _Sodt, string _QuocTich, string _CMND)
        {
            this._MaKH = _MaKH;
            this._HoTenKH = _HoTenKH;
            this._DiaChi = _DiaChi;
            this._Sodt = _Sodt;
            this._QuocTich = _QuocTich;
            this._CMND = _CMND;          
        }
        public KhachHangWeb()
        {

        }
        public static implicit operator KhachHangWeb(KhachHang v)
        {
            throw new NotImplementedException();
        }
    }
}