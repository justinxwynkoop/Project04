﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Project04.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Style.css">
    <style type="text/css">
        .auto-style1 {
            width: 131px;
            height: 130px;
        }
    </style>
</head>
<body>
    <form class="box" action="WebForm1.aspx" method="post" runat="server">
        <div class="stage">
            <img alt="" class="auto-style1" src="ITunesLogo.png" />
            <h1>iTunes Lookup</h1>
            <h4>Select Media Type</h4>
            <asp:DropDownList ID="ddlMedia" runat="server">
            </asp:DropDownList>
            <br />
            <br />
            <asp:TextBox ID="tbxSearch" runat="server"></asp:TextBox>
                
        &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            </div>
            <div>
            <br />
            <br />
            <asp:Panel ID="pnlTable" runat="server">
            </asp:Panel>
               
        </div>
    </form>

</body>
</html>
