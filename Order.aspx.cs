using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Order : System.Web.UI.Page
{
    private Product selectedProduct;

    protected void Page_Load(object sender, EventArgs e)
    {
        //bind dropdown on first load; get and show product data on every load        
        if (!IsPostBack)
        {
           DataTable itemDetails= getAllMenuItems();
            foreach (DataRow row in itemDetails.Rows)
            {
                Panel p = new Panel();
                Label nameLabel = new Label();
                nameLabel.Text = row["Name"].ToString();

                Image img = new Image();
                img.ImageUrl = "Images/MenuItems/" + row["Image"];
                img.Width = 100;
                img.Height = 100;
                p.Controls.Add(img);
                p.Controls.Add(nameLabel);
                this.Controls.Add(p);
               
            }
        };
       /* selectedProduct = this.GetSelectedProduct();
        lblName.Text = selectedProduct.Name;
        lblShortDescription.Text = selectedProduct.ShortDescription;
        lblLongDescription.Text = selectedProduct.LongDescription;
        lblUnitPrice.Text = selectedProduct.UnitPrice.ToString("c") + " each";
        imgProduct.ImageUrl = "Images/Products/" + selectedProduct.ImageFile;*/
    }

    private DataTable getAllMenuItems()
    {
        SqlConnection conn = new SqlConnection();
        string connstring = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\\RestaurantDB.mdf; Integrated Security = True";
        conn.ConnectionString = connstring;
        conn.Open();
        string itemGet = "SELECT * FROM MenuItem";
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = new SqlCommand(itemGet, conn);
        DataTable dt = new DataTable();
        adapter.Fill(dt);
        return dt;
    }

   /* private Product GetSelectedProduct()
    {
        //get row from SqlDataSource based on value in dropdownlist
        DataView productsTable = (DataView)
            SqlDataSource.Select(DataSourceSelectArguments.Empty);
        productsTable.RowFilter = string.Format("ProductID = '{0}'",
            ddlProducts.SelectedValue);
        DataRowView row = (DataRowView)productsTable[0];

        //create a new product object and load with data from row
        Product p = new Product();
        p.ProductID = row["ProductID"].ToString();
        p.Name = row["Name"].ToString();
        p.ShortDescription = row["ShortDescription"].ToString();
        p.LongDescription = row["LongDescription"].ToString();
        p.UnitPrice = (decimal)row["UnitPrice"];
        p.ImageFile = row["ImageFile"].ToString();
        return p;
    }*/

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            //get cart from session and selected item from cart
            CartItemList cart = CartItemList.GetCart();
            CartItem cartItem = cart[selectedProduct.ProductID];

            //if item isn’t in cart, add it; otherwise, increase its quantity
            if (cartItem == null)
            {
                //cart.AddItem(selectedProduct,Convert.ToInt32(txtQuantity.Text));
            }
            else
            {
               // cartItem.AddQuantity(Convert.ToInt32(txtQuantity.Text));
            }
            Response.Redirect("Cart.aspx");
        }
    }
}