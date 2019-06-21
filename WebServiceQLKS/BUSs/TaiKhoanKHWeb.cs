using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.BUSs
{
    public class TaiKhoanKHWeb
    {
        public string _MaKH;

        public string _Password;

        public int? _TinhTrang;

        private DateTime? _NgayTao;

        public TaiKhoanKHWeb(string _MaKH, string _Password, int? _TinhTrang, DateTime? _NgayTao)
        {
            this._MaKH = _MaKH;
            this._NgayTao = _NgayTao;
            this._TinhTrang = _TinhTrang;
            this._Password = _Password;
        }
        public TaiKhoanKHWeb() { }
    }
}