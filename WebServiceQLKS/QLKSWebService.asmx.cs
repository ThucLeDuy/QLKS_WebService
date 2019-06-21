
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using WebApplication3;
using WebApplication3.BUSs;

namespace WebApplication3
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        FirebaseClient firebase = new FirebaseClient("https://qlks-2faa6.firebaseio.com/");
        QLKSDataContext db;
        KhachHangBUS khachHangBUS = new KhachHangBUS();
        DichVuBUS dichVuBUS = new DichVuBUS();
        HoaDonDichVuBUS hoaDonDVBUS = new HoaDonDichVuBUS();
        DatPhongTaiChoBUS datPhongTaiChoBUS = new DatPhongTaiChoBUS();
        //DatPhongTruocBUS datPhongTruocBUS = new DatPhongTruocBUS();
        TaiKhoanTaiKhoanKHBUS taiKhoanKHBUS = new TaiKhoanTaiKhoanKHBUS();
        PhongBUS phongBUS = new PhongBUS();
        public WebService1()
        {
            db = new QLKSDataContext();
            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
            //Task task = new Task(RunSync2);
            //task.Start();
            //task.Wait();
        }
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        
        [WebMethod]
        public bool XoaKhachHang(string maKH)
        {
            if (khachHangBUS.XoaKhachHang(maKH))
            {
                XoaKhachHangFirebase(maKH);
                return true;
            }
            return false;
        }
        private async Task XoaKhachHangFirebase(string maKH)
        {

            await firebase
              .Child("KhachHang")
              .Child(maKH)
              .DeleteAsync();
        }
        [WebMethod]
        public bool ThemKhachHangMoiWeb(string maKH, string tenKH, string diaChi, string soDT, string quocTich, string CMND)
        {
            if (maKH == null || maKH == "")
            {
                return false;
            }
            KhachHang kh = new KhachHang();
            kh.MaKH = maKH;
            kh.HoTenKH = tenKH;
            kh.DiaChi = diaChi;
            kh.Sodt = soDT;
            kh.QuocTich = quocTich;
            kh.CMND = CMND;

            if (khachHangBUS.ThemKhachHang(kh) == true)//Đã thực hiện thêm và kiểm tra thêm thành công hay ko
            {
                ThemKhachHangFirebase(kh);
                return true;
            }
            return false;

        }
        [WebMethod]
        public bool ThayDoiMatKhauKHWeb(string maKH, string oldPassword, string newPassword)
        {
            return taiKhoanKHBUS.ThayDoiMatKhauWeb(maKH, oldPassword, newPassword);
        }
        [WebMethod]
        public bool KiemTraKHDangNhapWeb(string maKH, string password)
        {
            return taiKhoanKHBUS.KiemTraKHDangNhapWeb(maKH, password);
        }
        [WebMethod]
        public List<TaiKhoanKHWeb> LayDanhSachTaiKhoanWeb()
        {
            return taiKhoanKHBUS.LayDanhSachTaiKhoanWeb();
        }
        [WebMethod]
        public List<KhachHangWeb> LayKhachHangQuaMaKH(string maKH)
        {
            List<KhachHangWeb> ds = new List<KhachHangWeb>();
            //KhachHangWeb kh = new KhachHangWeb("BAOO1", "LDvm", "TDT", "9009", "VN", "08394823");
            ds.Add(khachHangBUS.LayKhachHangQuaMaKH(maKH));
            return ds;//khachHangBUS.LayKhachHangQuaMaKH(maHK);
        }
        [WebMethod]
        public List<DichVuWeb> LayDanhSachDVWeb()
        {
            return dichVuBUS.LayDanhSachDVWeb();
        }
        [WebMethod]
        public List<HoaDonDVWeb> LayDanhSachDVCua1KH_Web(string maKH)
        {
            return hoaDonDVBUS.LayDanhSachHoaDonDVCua1KHWeb(maKH);
        }
        [WebMethod]
        public bool ThemHoaDonDichVu_Web(string maKH, string maDV, int soLuongDV, string NgaySuDungDV)
        {
            DateTime ngaySuDungFormat = Convert.ToDateTime(NgaySuDungDV);//DateTime.ParseExact(NgaySuDungDV, "dd//mm//yy HH:mm:ss", CultureInfo.InvariantCulture);
            HoaDonDVWeb hdDV = new HoaDonDVWeb();
            hdDV.MaKH = maKH;
            hdDV.MaDV = maDV;
            hdDV.SoLuongDV = soLuongDV;
            hdDV.NgaySuDungDV = ngaySuDungFormat;
            //if (hoaDonDVBUS.ThemHoaDonDVWeb(hdDV))
            {
                ThemHoaDonDichVuFirebase(hdDV);
                return true;
            }
            //return false;
        }
         
        private async Task ThemHoaDonDichVuFirebase(HoaDonDVWeb hdDV)
        {
            Random rd = new Random();
            string keyDV = hdDV.MaKH + hdDV.MaDV + rd.Next(999);
            await firebase.Child("HoaDonDV")
                            .Child(keyDV)//hdDV.MaKH + hdDV.MaDV + hdDV.NgaySuDungDV.ToString())
                            .PutAsync(hdDV);// PostAsync(new NhanVien("D"));//.PutAsync(new Dinosaur());

        }
        [WebMethod]
        public List<BUSs.Mess> LayPhongTest()
        {
            List<BUSs.Mess> dsMess = new List<BUSs.Mess>();
            dsMess.Add(new BUSs.Mess("A01", "Phong Doi"));
            dsMess.Add(new BUSs.Mess("A02", "Phong Don"));
            return dsMess;
        }
        //[WebMethod]
        //public int DatPhongTruoc_Web(string maKH, string soPhong, string ngayDat, string ngayDen
        //    , decimal tienDatTruoc, string soThe, int soNguoi, int soNgayThue)
        //{
        //    try
        //    {
        //        DatPhongTruoc phieuDatPhTruoc = new DatPhongTruoc();
        //        phieuDatPhTruoc.MaKH = maKH;
        //        phieuDatPhTruoc.SoPhong = soPhong;
        //        //phieuDatPhTruoc.NgayDat = DateTime.ParseExact(ngayDat, "MM/dd/yy") Convert.ToDateTime(ngayDat, 
        //        //phieuDatPhTruoc.TienDatTruoc = tienDatTruoc;
        //    }
        //    catch (Exception)
        //    {
        //        return -1;
        //    }
        //    return 1;//phongBUS.HienThiPhong();
        //}
        [WebMethod]
        public List<Phong> LayDanhSachPhongTypePhong()
        {

            return phongBUS.HienThiPhong();
        }
        [WebMethod]
        public List<PhongWeb> LayDanhSachPhongTypePhongWeb()
        {
            return phongBUS.LayDanhSachPhongWeb();           
        }
        private async Task ThemKhachHangFirebase(KhachHang kh)
        {

            await firebase.Child("KhachHang")
                            .Child("KH" + kh.MaKH)
                            .PutAsync(kh);// PostAsync(new NhanVien("D"));//.PutAsync(new Dinosaur());
            //return kh.MaKH;
        }
        private async Task XoaHoaDonDVFirebase(string maKH, string maDV, DateTime ngaySuDungDV_YYMMDD, int soLuongDV)
        {
            
            await firebase.Child("HoaDonDV")
                            .Child("KH" + maKH)
                            .PutAsync(maDV);// PostAsync(new NhanVien("D"));//.PutAsync(new Dinosaur());
            //return kh.MaKH;
        }
        [WebMethod]
        public void CapNhatKhachHang(string maKH, string tenKH, string diaChi, string soDT, string quocTich, string CMND)
        {
            if (maKH == null || maKH == "")
            {
                return;
            }
            KhachHang kh = new KhachHang();
            kh.MaKH = maKH;
            kh.HoTenKH = tenKH;
            kh.DiaChi = diaChi;
            kh.Sodt = soDT;
            kh.QuocTich = quocTich;
            kh.CMND = CMND;

            khachHangBUS.CapNhatKhachHang(kh);
            //GenerateRowsCells();
            //return res;
            //if (res) Page.Server.TransferRequest(Request.Url.AbsolutePath, false);
        }
        [WebMethod]
        public Boolean AddUser(string userID, string userName, int userPermitSt, string userPwd)
        {
            KhachHang kh = new KhachHang();
            kh.MaKH = userID;
            kh.HoTenKH = userName;
            kh.Sodt = userPermitSt.ToString();
            kh.QuocTich = "VN";
            kh.CMND = userPwd;
            kh.DiaChi = "HCM";

            bool res = khachHangBUS.ThemKhachHang(kh);
            return res;
        }
        [WebMethod]
        public Boolean ThemHoaDonDichVuChoKH(string maKH, string maDV, DateTime ngaySuDungDV_YYMMDD, int soLuongDV)
        {
            HoaDonDV hdDV = new HoaDonDV();
            hdDV.MaDV = maDV;
            hdDV.MaKH = maKH;
            hdDV.NgaySuDungDV = ngaySuDungDV_YYMMDD;
            hdDV.SoLuongDV = soLuongDV;
            bool res = hoaDonDVBUS. ThemHoaDonDV(hdDV);
            if (res)
            {
                //XoaHoaDonDVFirebase( maKH,  maDV,  ngaySuDungDV_YYMMDD,  soLuongDV);
                return res;
            }
            return res;
            //
        }
        [WebMethod]
        public Boolean CapNhatSoLuongDichVuChoKH(string maKH, string maDV, DateTime ngaySuDungDV_YYMMDD, int soLuongDV)
        {
            HoaDonDV hdDV = new HoaDonDV();
            hdDV.MaDV = maDV;
            hdDV.MaKH = maKH;
            hdDV.NgaySuDungDV = ngaySuDungDV_YYMMDD;
            hdDV.SoLuongDV = soLuongDV;
            bool res = hoaDonDVBUS.CapNhatSoLuongDVHoaDonDV(hdDV);
            return res;
        }
        [WebMethod]
        public Boolean XoaMotHoaDonDichVu(string maKhachHang, string maDV, DateTime ngaySuDungDV)
        {
            bool res = hoaDonDVBUS.XoaMotDichVuKH(maKhachHang, maDV, ngaySuDungDV);
            return res;
        }
        [WebMethod]
        public Boolean CapNhatHoaDonDV(string maKH, string maDV, DateTime ngaySuDungDV_YYMMDD, int soLuongDV)
        {
            HoaDonDV hdDV = new HoaDonDV();
            hdDV.MaKH = maKH;
            hdDV.MaDV = maDV;
            hdDV.NgaySuDungDV = ngaySuDungDV_YYMMDD;
            hdDV.SoLuongDV = soLuongDV;
            bool res = hoaDonDVBUS.CapNhatHoaDonDV(hdDV);
            return res;
        }
        [WebMethod]
        public bool ThemDatPhongTaiCho(string maKH, DateTime ngayDat_YYMMDD, int soNguoi, string soPhong)
        {
            DatPhongTaiCho datPhongTC = new DatPhongTaiCho();
            datPhongTC.MaKH = maKH;
            datPhongTC.NgayDat = ngayDat_YYMMDD;
            datPhongTC.SoNguoi = soNguoi;
            datPhongTC.SoPhong = soPhong;
            bool res = datPhongTaiChoBUS.ThemDatPhongTaiCho(datPhongTC);
            
            return res;
        }

        //[WebMethod]
        //public List<> getListUser()
        //{
        //    List<UserInfo> listUser = db.UserInfos.ToList();
        //    return listUser;
        //}
        //[WebMethod]
        //public String getUserNameByID(string id)
        //{
        //    foreach (UserInfo user in getListUser())
        //    {
        //        if (id == user.ID) return user.Name;
        //    }
        //    return "Ko tim thay";
        //}
        //public Boolean checkIdExist(String userID)
        //{
        //    //kiem tra neu co tin tai ID tra ve true
        //    foreach (UserInfo user in getListUser())
        //    {
        //        if (userID == user.ID) return true;
        //    }
        //    //neu ko tra ve false;
        //    return false;
        //}
        //[WebMethod]
        //public Boolean AddUserWithType(UserInfo user)
        //{
        //    if (!checkIdExist(user.ID))
        //    {
        //        db.UserInfos.InsertOnSubmit(user);
        //        db.SubmitChanges();
        //        return true;
        //    }
        //    return false;
        //}
        //[WebMethod]
        //public Boolean AddUser(string userID, string userName, int userPermitSt, string userPwd)
        //{
        //    if (!checkIdExist(userID))
        //    {
        //        UserInfo u = new UserInfo();
        //        u.ID = userID;
        //        u.Name = userName;
        //        u.Password = userPwd;
        //        u.PermitStatus = userPermitSt;
        //        db.UserInfos.InsertOnSubmit(u);
        //        db.SubmitChanges();
        //        return true;
        //    }
        //    return false;
        //}
        //[WebMethod]
        //public Boolean updateUser(string userID, string userName, int userPermitSt, string userPwd)
        //{
        //    if (checkIdExist(userID))
        //    {
        //        deleteUser(userID);
        //        AddUser(userID, userName, userPermitSt, userPwd);
        //        return true;
        //    }
        //    return false;
        //}
        //[WebMethod]
        //public UserInfo getUserByID(string id)
        //{
        //    foreach (UserInfo user in getListUser())
        //    {
        //        if (id == user.ID) return user;
        //    }
        //    return null;
        //}
        //[WebMethod]
        //public Boolean deleteUser(string id)
        //{
        //    try
        //    {
        //        UserInfo userInfo = getUserByID(id);
        //        db.UserInfos.DeleteOnSubmit(userInfo);
        //        db.SubmitChanges();
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
    }
}
