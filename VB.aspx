<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VB.aspx.vb" Inherits="VB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
  <div>
      <asp:Button ID="Button1" runat="server" Text="InsertDoc" OnClick="InsertDoc_Click" />
        <asp:Button ID="Button2" runat="server" Text="InsertXls" OnClick="InsertXls_Click" />
        <asp:Button ID="Button3" runat="server" Text="InsertImage" OnClick="InsertImage_Click" />
        <asp:Button ID="Button4" runat="server" Text="InsertPdf" OnClick="InsertPdf_Click" /><br /><br />
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick = "Retreive_Doc">Download Doc</asp:LinkButton>
        <asp:LinkButton ID="LinkButton2" runat="server" OnClick = "Retreive_Xls">Download xls</asp:LinkButton>
        <asp:LinkButton ID="LinkButton3" runat="server" OnClick = "Retreive_Image">Download Image</asp:LinkButton>
        <asp:LinkButton ID="LinkButton4" runat="server" OnClick = "Retreive_Pdf">Download Pdf</asp:LinkButton>
    </div>
    </form>
</body>
</html>
