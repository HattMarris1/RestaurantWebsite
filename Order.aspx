<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Order.aspx.cs" Inherits="Order" %>
<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="headContent" ContentPlaceHolderID="headPlaceHolder" Runat="Server">  
    <link href="Styles/Order.css" rel="stylesheet" />    
</asp:Content>

<asp:Content ID="formContent" ContentPlaceHolderID="formPlaceHolder" Runat="Server">
    <aside>

   </aside>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Name], [IsVeg], [Image], [IsNew] FROM [MenuItem]"></asp:SqlDataSource>
    <asp:Panel ID="MenuItemContainer" runat="server" DataSourceID="SqlDataSource1">
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="btnAdd_Click" />

    </asp:Panel>
</asp:Content>

