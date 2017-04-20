<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Order.aspx.cs" Inherits="Order" %>
<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="headContent" ContentPlaceHolderID="headPlaceHolder" Runat="Server">  
    <link href="Styles/Order.css" rel="stylesheet" />    
</asp:Content>

<asp:Content ID="formContent" ContentPlaceHolderID="formPlaceHolder" Runat="Server">
    <label>Please select a product&nbsp;</label>
    <asp:DropDownList ID="ddlProducts" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource" DataTextField="Name" DataValueField="ProductID">
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString='<%$ ConnectionStrings:HalloweenDatabase %>' SelectCommand="SELECT [ProductID], [Name], [ShortDescription], [LongDescription], [ImageFile], [UnitPrice] FROM [Products] ORDER BY [Name]"></asp:SqlDataSource>
    <div id="productData">   
        <asp:Label ID="lblName" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="lblShortDescription" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="lblLongDescription" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="lblUnitPrice" runat="server" Text="Label"></asp:Label>
        <label id="lblQuantity">Quantity&nbsp;</label>
        <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>   
        <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ErrorMessage="Quantity is required." ControlToValidate="txtQuantity" CssClass="validator" Display="Dynamic"></asp:RequiredFieldValidator>       
        <asp:RangeValidator ID="rgvQuantity" runat="server" ErrorMessage="Must be between 1 and 500." ControlToValidate="txtQuantity" CssClass="validator" Display="Dynamic" MaximumValue="500" MinimumValue="1" Type="Integer"></asp:RangeValidator>
        <br />
        <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add to Cart" CssClass="button" />
        <asp:Button ID="btnCart" runat="server" CausesValidation="False" PostBackUrl="~/Cart.aspx" Text="Go to Cart" CssClass="button" />
    </div>
    <asp:Image ID="imgProduct" runat="server" /> 
</asp:Content>

