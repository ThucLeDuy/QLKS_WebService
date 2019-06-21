using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.BUSs
{
    public class DichVuWeb
    {
        public string _MaDV { get; set; }

        public string _MoTaDV { get; set; }

        public decimal? _GiaDV { get; set; }
        public DichVuWeb(string _MaDV, string _MoTaDV, decimal? _GiaDV)
        {
            this._MaDV = _MaDV;
            this._MoTaDV = _MoTaDV;
            this._GiaDV = _GiaDV;
        }
        public DichVuWeb() { }
    }
}