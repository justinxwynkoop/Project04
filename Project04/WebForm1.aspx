<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Project04.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblMedia" runat="server" Text="Media Type: "></asp:Label>
            1&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="ddlMedia" runat="server">
            </asp:DropDownList>
            <br />
            <br />
            <asp:TextBox ID="tbxSearch" runat="server"></asp:TextBox>
                
        &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
               
            <br />
            <br />
            <asp:Panel ID="pnlTable" runat="server">
            </asp:Panel>
               
        </div>
    </form>
</body>
</html>
