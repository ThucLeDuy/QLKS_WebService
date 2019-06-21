using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.BUSs
{
    public class HoaDonDVWeb
    {
        public string MaKH { get; set; }

        public string MaDV { get; set; }

        public int? SoLuongDV { get; set; }

        public DateTime NgaySuDungDV { get; set; }

        public HoaDonDVWeb(string _MaKH, string _MaDV, int? _SoLuongDV, DateTime _NgaySuDungDV)
        {
            this.MaDV = _MaDV;
            this.MaKH = _MaKH;
            this.SoLuongDV = _SoLuongDV;
            this.NgaySuDungDV = _NgaySuDungDV;
        }
        public HoaDonDVWeb() { }
    }
}