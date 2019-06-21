using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        static WebService1 service = new WebService1();
        KhachHangBUS khBUS = new KhachHangBUS();
        private string maKH;
        private string tenKH;
        private string diaChi;
        private string soDt;
        private string quocTich;
        private string CMND;
        //static Q db;
        protected void Page_Load(object sender, EventArgs e)
        {
            maKH = txtMaKH.Text;
            tenKH = txtTenKH.Text;
            diaChi = txtDiaChi.Text;
            soDt = txtSoDt.Text;
            quocTich = txtQuocTich.Text;
            CMND = txtCMND.Text;
            //db = new UserDataContext();
            GenerateRowsCells();
            btnThemKH.Click += new EventHandler(this.ThemKhOnclick);
            btnCapNhatKH.Click += new EventHandler(this.CapNhatKhachHangOnClick);
            //Button1.Click += new EventHandler(this.TestInsertFirebase);
            //if (!ClientScript.IsStartupScriptRegistered("alert"))
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(),
            //        "alert", "alertMe();", true);
            //}
        }
        [WebMethod]
        public void refresh()
        {
            Page.Server.TransferRequest(Request.Url.AbsolutePath, false);
        }
        public void TestInsertFirebase(object sender, EventArgs e)
        {
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "themKhachHangFirebase12", "themKhachHangFirebase();", true);
            //Page.Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        [WebMethod]
        public static Boolean AddUser(string userName, int userPermitSt, string userPwd)
        {
            if (userName == "httj5") return false;
            Random rd = new Random();            
            service.AddUser(rd.Next(100).ToString(), userName, userPermitSt, userPwd);
            return true;
        }
        private void CapNhatKhachHangOnClick(object sender, EventArgs e)
        {
            
            if (maKH == null || maKH == "" || maKH == "txtMaKH")
            {
                return;
            }
            KhachHang kh = new KhachHang();
            kh.MaKH = maKH;
            kh.HoTenKH = tenKH;
            kh.DiaChi = diaChi;
            kh.Sodt = soDt;
            kh.QuocTich = quocTich;
            kh.CMND = CMND;

            bool res = khBUS.CapNhatKhachHang(kh);
            
            Page.Server.TransferRequest(Request.Url.AbsolutePath, false);
        }
        public void ThemKhOnclick(object sender, EventArgs e)
        {

            if (maKH == null || maKH == "" || maKH == "txtMaKH")
            {
                return;
            }
            KhachHang kh = new KhachHang();
            kh.MaKH = maKH;
            kh.HoTenKH = tenKH;
            kh.DiaChi = diaChi;
            kh.Sodt = soDt;
            kh.QuocTich = quocTich;
            kh.CMND = CMND;

            //bool res = khBUS.ThemKhachHang(kh);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "themKhachHangFirebase1", "themKhachHangFirebase();", true);
            //ScriptManager.RegisterStartupScript(btnThemKH, btnThemKH.GetType(), "themKhachHangFirebase1", "themKhachHangFirebase();", true);
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "themKhachHangFirebase1", "themKhachHangFirebase();", true);
            //Page.Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        public void GenerateRowsCells()
        {
            TableRow r1 = new TableRow();
            TableCell cellMaKH = new TableCell();
            cellMaKH.Text = "Mã khách hàng";
            TableCell cellHoTenKH = new TableCell();
            cellHoTenKH.Text = "Họ tên";
            TableCell cellDiaChi = new TableCell();
            cellDiaChi.Text = "Địa chỉ";
            TableCell cellSoDt = new TableCell();
            cellSoDt.Text = "Số điện thoại";
            TableCell cellQuocTich = new TableCell();
            cellQuocTich.Text = "Quốc tịch";
            TableCell cellCMND = new TableCell();
            cellCMND.Text = "CMND";
            TableCell cellDelKhachHang = new TableCell();
            r1.Cells.Add(cellMaKH);
            r1.Cells.Add(cellHoTenKH);
            r1.Cells.Add(cellDiaChi);
            r1.Cells.Add(cellSoDt);           
            r1.Cells.Add(cellQuocTich);
            r1.Cells.Add(cellCMND);
            r1.Cells.Add(cellDelKhachHang);
            r1.Font.Bold = true;
            r1.Font.Size = 12;
            Table1.Rows.Add(r1);
            foreach (KhachHang kh in khBUS.HienThiKhachHang())
            {
                r1 = new TableRow();

                cellMaKH = new TableCell();
                cellMaKH.Controls.Add(new LiteralControl(kh.MaKH));
                cellHoTenKH = new TableCell();
                cellHoTenKH.Text = kh.HoTenKH;               
                cellDiaChi = new TableCell();
                cellDiaChi.Text = kh.DiaChi;
                cellSoDt = new TableCell();
                cellSoDt.Text = kh.Sodt;
                cellQuocTich = new TableCell();
                cellQuocTich.Text = kh.QuocTich;
                cellCMND = new TableCell();
                cellCMND.Text = kh.CMND;


                cellDelKhachHang = new TableCell();
                Button b1 = new Button();
                b1.Text = "Xóa";
                //gan id cho button cung la id cua KH
                b1.ID = kh.MaKH;
                b1.Click += new EventHandler(btnDelete);
                cellDelKhachHang.Controls.Add(b1);//id cua kh


                r1.Cells.Add(cellMaKH);
                r1.Cells.Add(cellHoTenKH);
                r1.Cells.Add(cellDiaChi);
                r1.Cells.Add(cellSoDt);
                r1.Cells.Add(cellQuocTich);
                r1.Cells.Add(cellCMND);
                r1.Cells.Add(cellDelKhachHang);
                
                Table1.Rows.Add(r1);
            }

        }
        protected void btnDelete(object sender, EventArgs e)
        {
            string maKH = ((Control)sender).ID;
            bool res = khBUS.XoaKhachHang(maKH);
            if (res) Response.Redirect(Request.RawUrl);
            // Hoac su dung -> Server.TransferRequest(Request.Url.AbsolutePath, false); -- to refresh page with less process
        }
    }
}