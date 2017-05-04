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
    private MenuItem selectedProduct;
    private string connstring = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\\RestaurantDB.mdf; Integrated Security = True";

    protected void Page_Load(object sender, EventArgs e)
    {
        //bind dropdown on first load; get and show product data on every load        
        if (!IsPostBack)
        {
           DataTable itemDetails= getMenuItems("SELECT * FROM MenuItem");
            foreach (DataRow row in itemDetails.Rows)
            {
                Panel p = new Panel();
                p.ID = row["id"].ToString();
                Label nameLabel = new Label();
                nameLabel.Text = row["Name"].ToString();

                Image img = new Image();
                img.ImageUrl = "Images/MenuItems/" + row["Image"];
                img.Width = 100;
                img.Height = 100;
                p.Controls.Add(img);
                p.Controls.Add(nameLabel);
                Button btn = new Button();

                btn.Attributes.Add("OnClick", "\"Btn_Click\""); //new EventHandler(this.btnAdd_Click);
                p.Controls.Add(btn);
                
                MenuItemContainer.Controls.Add(p);
               
            }
        };
       /* selectedProduct = this.GetSelectedProduct();
        lblName.Text = selectedProduct.Name;
        lblShortDescription.Text = selectedProduct.ShortDescription;
        lblLongDescription.Text = selectedProduct.LongDescription;
        lblUnitPrice.Text = selectedProduct.UnitPrice.ToString("c") + " each";
        imgProduct.ImageUrl = "Images/Products/" + selectedProduct.ImageFile;*/
    }

    private void Btn_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private DataTable getMenuItems(string itemGet)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = connstring;
        conn.Open();
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = new SqlCommand(itemGet, conn);
        DataTable dt = new DataTable();
        adapter.Fill(dt);
        return dt;
    }

    private MenuItem dataTableToMenuItem(DataTable dt)
    {
        MenuItem menuItem = new MenuItem();
        menuItem.MenuItemID = dt.Rows[0]["id"].ToString();
        menuItem.Name = dt.Rows[0]["Name"].ToString();
        menuItem.UnitPrice =Convert.ToInt32(dt.Rows[0]["price"]);
        return menuItem;
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
            int itemID;
            Button clickedButton = (Button)sender;
            Panel clickedPanel = (Panel)clickedButton.Parent;
            itemID = Convert.ToInt32(clickedPanel.ID);
            //get cart from session and selected item from cart
            CartItemList cart = CartItemList.GetCart();
            CartItem cartItem = cart[itemID];
            
            //if item isn’t in cart, add it; otherwise, increase its quantity
            if (cartItem == null)
            {
                DataTable dt = getMenuItems("Select * from MenuItem where id ='" + itemID+"'");
                MenuItem m = dataTableToMenuItem(dt);
                cart.AddItem(m,1);
            }
            else
            {
                cartItem.AddQuantity(1);
            }
            Response.Redirect("Cart.aspx");
        }
    }
}