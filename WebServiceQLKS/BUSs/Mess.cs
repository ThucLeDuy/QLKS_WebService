using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.BUSs
{
    public class Mess
    {
        public string soPhong { get; set; }
        public string tenPhong { get; set; }
        public Mess(string soPhong, string tenPhong)
        {
            this.soPhong = soPhong;
            this.tenPhong = tenPhong;
        }
        public Mess()
        {

        }
    }
}