﻿<%@ Page Title="Please Login" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="LoginPage.aspx.cs" Inherits="LoginPage" %>
<%@ MasterType VirtualPath="~/Site.master" %>


<asp:Content ID="headContent" ContentPlaceHolderID="headPlaceHolder" Runat="Server">  
    <%--<link href="Styles/Order.css" rel="stylesheet" />--%>    
</asp:Content>

<asp:Content ID="formContent" ContentPlaceHolderID="formPlaceHolder" Runat="Server">
    <aside>

   </aside>
    <main>
    UserName: <asp:TextBox ID="LoginUserNameBox" runat="server"></asp:TextBox>
        <br />
    Password: <asp:TextBox ID="LoginPasswordBox" runat="server"></asp:TextBox>
        <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
<asp:Button ID="LoginButton" runat="server" Text="Login" OnClick="LoginButton_Click" />
    <br />
    Register: 
    <br />    
    User Name:<asp:TextBox ID="RegisterUserNameBox" runat="server"></asp:TextBox>
        <br />
    Password: <asp:TextBox ID="RegisterPasswordBox" runat="server"></asp:TextBox>
        <br />
    <asp:Button ID="RegisterButton" runat="server" Text="Register" />
    </main>
</asp:Content>

