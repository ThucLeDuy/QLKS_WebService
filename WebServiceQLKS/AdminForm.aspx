<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminForm.aspx.cs" Inherits="WebApplication3.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quản lý khách hàng</title>
</head>
<body>
    <h1 style="text-align:center; color:gray">Quản lý nhân viên</h1>
    <%--<h1 id="userName">name</h1>--%>
    <h1 id="res">false to insert - no connection</h1>
<script src="https://www.gstatic.com/firebasejs/4.9.1/firebase.js"></script>
<script src="https://code.jquery.com/jquery-migrate-1.3.0.js"></script>
<script src = "https://code.jquery.com/jquery-1.3.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<style>
    .textbox1{
        font-size:12pt;

    }
</style>
<script type="text/javascript">
    function CapNhatKHOnclick() {
        var maKH = document.getElementById('<%=txtMaKH.ClientID %>').value;
        var tenKH = document.getElementById('<%=txtTenKH.ClientID %>').value;
        var diaChi = document.getElementById('<%=txtDiaChi.ClientID %>').value;
        var soDt = document.getElementById('<%=txtSoDt.ClientID %>').value;
        var quocTich = document.getElementById('<%=txtQuocTich.ClientID %>').value;
        var CMND = document.getElementById('<%=txtCMND.ClientID %>').value;
        //PageMethods.CapNhatKhachHang(maKH, tenKH, diaChi, soDt, quocTich, CMND);
    }
    <%--function ThemKhOnclick() {
        var maKH = document.getElementById('<%=txtMaKH.ClientID %>').value;
        var tenKH = document.getElementById('<%=txtTenKH.ClientID %>').value;
        var diaChi = document.getElementById('<%=txtDiaChi.ClientID %>').value;
        var soDt = document.getElementById('<%=txtSoDt.ClientID %>').value;
        var quocTich = document.getElementById('<%=txtQuocTich.ClientID %>').value;
        var CMND = document.getElementById('<%=txtCMND.ClientID %>').value;
        PageMethods.ThemKhachHang(maKH, tenKH, diaChi, soDt, quocTich, CMND);
           
        <%  //if (txtMaKH.ClientID != null || txtMaKH.ClientID != "" || checkMAKH(txtMaKH.ClientID))
//{
//    ThemKhachHang(txtMaKH.ClientID, txtTenKH.ClientID, txtDiaChi.ClientID, txtSoDt.ClientID, txtQuocTich.ClientID, txtCMND.ClientID);
//}%>
        <%//Server.TransferRequest(Request.Url.AbsolutePath, false);%>
        <%--  
            
            TableRow rowKHmoi = new TableRow();
            TableCell cellMaKH = new TableCell();
            cellMaKH.Text = txtMaKH.ClientID;
            TableCell cellHoTenKH = new TableCell();
            cellHoTenKH.Text = txtTenKH.ClientID;
            TableCell cellDiaChi = new TableCell();
            cellDiaChi.Text = txtDiaChi.ClientID;
            TableCell cellSoDt = new TableCell();
            cellSoDt.Text = txtSoDt.ClientID;
            TableCell cellQuocTich = new TableCell();
            cellQuocTich.Text = txtQuocTich.ClientID;
            TableCell cellCMND = new TableCell();
            cellCMND.Text = txtCMND.ClientID;
            rowKHmoi.Cells.Add(cellMaKH);
            rowKHmoi.Cells.Add(cellHoTenKH);
            rowKHmoi.Cells.Add(cellDiaChi);
            rowKHmoi.Cells.Add(cellSoDt);
            rowKHmoi.Cells.Add(cellCMND);
            rowKHmoi.Cells.Add(cellQuocTich);
            
            
            Table1.Rows.Add(rowKHmoi);
        
    }--%>

    function AddUser(name) {
        document.getElementById('result').innerHTML = 'false to insert - no connection';
        var res = PageMethods.AddUser(name, 1, "qwd");
        //PageMethods.refresh();
        if(res)
            window.location.reload(false);
        else {
            document.getElementById('result').innerHTML = 'false to insert';
        }
        function onSucess(result) {
            alert(result);
        }
        function onError(result) {
            alert('Something wrong.');
        }
    }


        <%--function HandleIT() {
            var name = document.getElementById('<%=txtname.ClientID %>').value;
            var address = document.getElementById('<%=txtaddress.ClientID %>').value;
            PageMethods.ProcessIT(name, address, onSucess, onError); 
            function onSucess(result) {
                alert(result);
            }
            function onError(result) {
                alert('Something wrong.');
            }}--%>
        
    </script>
<script>
    
  // Initialize Firebase
      var config = {
        apiKey: "AIzaSyBbQ8oiOkaG-R-vsAv20waQBDmI_nk91P0",
        authDomain: "qlks-42d18.firebaseapp.com",
        databaseURL: "https://qlks-42d18.firebaseio.com",
        projectId: "qlks-42d18",
        storageBucket: "qlks-42d18.appspot.com",
        messagingSenderId: "285038581962"
      };
      firebase.initializeApp(config);
      
      var userName = document.getElementById('userName');
      var res = document.getElementById('result');
      var dbRef = firebase.database().ref().child('UserName');
      //var dbRef = firebase.database().ref().child('KhachHang');
    //dbRef.on('value', snap => userName.innerText = snap.val());
      //dbRef.on('value', snap => PageMethods.refresh());
      //dbRef.on('value', snap => AddUser('a'));
      dbRef.on('value', snap => userName.innerText = snap.val());
      dbRef.on('value', snap => AddUser(snap.val()));
      
      function themKhachHangFirebase() {

          firebase.database().ref().set({
              UserName: 'ht',
          });
          //firebase.database().ref('KhachHang/' + "y1").set({
          //    tenKH: 'ht',
          //    diaChi: 'diaChiKH',
          //    soDt: 'soDtKH',
          //    quocTich: 'quocTichKH',
          //    CMND: 'CMNDkh',
          //});
      }
      

</script>
    <script type="text/javascript">
        function alertMe() {
            alert('Hello');
        }
    </script>
    <form id="form1" runat="server">
        <script type="text/javascript">
    //function DeleteKartItems(name) {
            function DeleteKartItems() {
                var res = document.getElementById('result');
                var userName = document.getElementById('userName');
                
                try{
                    $.ajax({
                        type: "POST",
                        url: 'WebFormMaster.aspx/InsertData',
                        data: '{"name":"whatever"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            alert("");
                            //userName.innerHTML = "sucssecs";
                            //res.innerHTML = data.name;
                        },
                        error: function (e) {
                            alert(e);
                        }
                    });
                } catch (err) { alert(e);}

            }
   
       </script>
    <div>
        
        
        
        <input type="button" id="btnTest" onclick="DeleteKartItems()" value="Test" />
        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <%--<b style="font-size:14px;font-style:oblique ">Mã khách hàng</b>--%>
        <asp:TextBox CssClass="textbox1" ID="txtMaKH" Font-Size="12" placeholder="Mã khách hàng" runat="server" ></asp:TextBox>
        <br />
        <asp:TextBox CssClass="textbox1" ID="txtTenKH" placeholder="Họ tên khách hàng" runat="server" ></asp:TextBox>
        <br />
        <asp:TextBox CssClass="textbox1" ID="txtDiaChi" placeholder="Địa chỉ" runat="server"></asp:TextBox>
        <br />
        <asp:TextBox CssClass="textbox1" ID="txtSoDt" placeholder="Số điện thoại" runat="server"></asp:TextBox>
        <br />
        <asp:TextBox CssClass="textbox1" ID="txtQuocTich" placeholder="Quốc tịch" runat="server"></asp:TextBox>
        <br />
        <asp:TextBox CssClass="textbox1" ID="txtCMND" placeholder="CMND" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="btnThemKH" runat="server" Text="Thêm khách hàng mới" OnClientClick="ThemKhOnclick();" />
        <br />
        <asp:Button ID="btnCapNhatKH" runat="server" Text="Cập nhật khách hàng" OnClientClick="CapNhatKHOnclick();" />
        <br />
        <%--<asp:Button ID="Button1" runat="server" Text="Firebase Test" OnClientClick="TestInsertFirebase()" />
        <br />--%>
        <%--<asp:Button ID="btnCreateAccount" runat="server" Text="Signup" OnClientClick="HandleIT();" />
        <br />--%>
        <asp:Table id="Table1" GridLines="Both" HorizontalAlign="Left" Font-Names="Verdana" Font-Size="9pt" CellPadding="15" CellSpacing="0" Runat="server"/>
        <br />
        

    </div>
    </form>
   <script>
               <% //ThemKhachHang(txtMaKH.ClientID, txtTenKH.ClientID, txtDiaChi.ClientID, txtSoDt.ClientID, txtQuocTich.ClientID, txtCMND.ClientID); %>
   </script>

</body>
</html>