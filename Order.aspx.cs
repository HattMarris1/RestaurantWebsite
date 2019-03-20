using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

public partial class Order : System.Web.UI.Page
{
    private MenuItem selectedProduct;
    private string connstring = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\\RestaurantDB.mdf; Integrated Security = True";

    protected void Page_Load(object sender, EventArgs e)
    {
        //bind dropdown on first load; get and show product data on every load        
        /*   if (!IsPostBack)
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

                   LinkButton cartBtn = new LinkButton();
                   cartBtn.Attributes.Add("class", "fa fa-cart-plus btn menu__item-add");
                   cartBtn.Attributes.Add("ItemDetails", "");
                   cartBtn.Attributes.Add("aria-hidden", "true");
                   cartBtn.Click += new EventHandler(btnAdd_Click);
                   cartBtn.Text = "<span class=\"sr-only\">Add to Basket</span>";
                   p.Controls.Add(cartBtn);

                   MenuItemContainer.Controls.Add(p);

               }
           };
           */
        SqlConnection conn = new SqlConnection(connstring); 
        conn.Open();
        SqlCommand sqlcmd = new SqlCommand("SELECT * FROM MenuItem", conn);
        SqlDataReader theReader = sqlcmd.ExecuteReader();
        if (theReader.HasRows)
        {
            while (theReader.Read())
            {
                MenuItem mi = readerToMenuItem(theReader);
                                        
                HtmlGenericControl li = new HtmlGenericControl("li");
                li.Attributes.Add("class", "food-menu__item");
                li.Attributes.Add("id", mi.MenuItemID.ToString());

                HtmlGenericControl titleSpan = new HtmlGenericControl("span");
                titleSpan.Attributes.Add("class", "menu__item-title");
                titleSpan.InnerText = mi.Name;
                /*
                if ((bool)theReader["isVeg"])
                {
                    HtmlGenericControl vegIcon = new HtmlGenericControl("i");
                    vegIcon.Attributes.Add("class", "icon icon-leaf");
                    titleSpan.Controls.Add(vegIcon);
                }*/

                HtmlGenericControl priceSpan = new HtmlGenericControl("span");
                priceSpan.Attributes.Add("class", "menu__item-price");
                decimal price = mi.UnitPrice;
                priceSpan.InnerHtml = price.ToString("c");

                Button addButton = new Button();
                addButton.Attributes.Add("class", "fa fa-cart-plus btn menu__item-add");
                addButton.Attributes.Add("ItemID", mi.MenuItemID);
                addButton.Attributes.Add("aria-hidden", "true");
                addButton.Click += new EventHandler(btnAdd_Click);
                addButton.Text = "Add to Basket";

                li.Controls.Add(titleSpan);
                li.Controls.Add(priceSpan);
                li.Controls.Add(addButton);

                menu.Controls.Add(li);
            }
        }
    }
    
        /* selectedProduct = this.GetSelectedProduct();
         lblName.Text = selectedProduct.Name;
         lblShortDescription.Text = selectedProduct.ShortDescription;
         lblLongDescription.Text = selectedProduct.LongDescription;
         lblUnitPrice.Text = selectedProduct.UnitPrice.ToString("c") + " each";
         imgProduct.ImageUrl = "Images/Products/" + selectedProduct.ImageFile;*/
    

    private void Btn_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private MenuItem readerToMenuItem( SqlDataReader theReader)
    {
        if (theReader.HasRows)
        {
            MenuItem menuItem = new MenuItem();
            menuItem.MenuItemID = theReader["Id"].ToString();
            menuItem.Name = theReader["Name"].ToString();
            menuItem.UnitPrice = Convert.ToInt32(theReader["price"]);
            return menuItem;
        }
        else
        {
            return null;
        }
        
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
            string itemID;
            Button clickedButton = (Button)sender;
            itemID = clickedButton.Attributes["ItemID"];
            //get cart from session and selected item from cart
            CartItemList cart = CartItemList.GetCart();
            CartItem cartItem = cart[itemID];
            
            //if item isn’t in cart, add it; otherwise, increase its quantity
            if (cartItem == null)
            {
             SqlConnection conn = new SqlConnection(connstring);
             conn.Open();
              int id=  Convert.ToInt32(itemID);
             SqlCommand sqlcmd = new SqlCommand("SELECT * FROM MenuItem where Id ="+id, conn);
             SqlDataReader theReader = sqlcmd.ExecuteReader();

             theReader.Read();
             MenuItem m = readerToMenuItem(theReader);
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