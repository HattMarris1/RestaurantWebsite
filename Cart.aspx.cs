using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cart : System.Web.UI.Page
{
    private CartItemList cart;

    protected void Page_Load(object sender, EventArgs e)
    {
        //takes the user to the login page if they're not logged in
        if ((bool)Session["loggedIn"] != true)
        {
            Response.Redirect("LoginPage.aspx");
        }
        //retrieve cart object from session on every post back
        cart = CartItemList.GetCart();

        //on initial page load, add cart items to list control
        //and set master page logo image tool tip
        if (!IsPostBack) {
            this.DisplayCart();
        }
    }

    private void DisplayCart()
    {
        //remove all current items from list control
        lstCart.Items.Clear();
        decimal subtotal = 0;

        //loop cart and add each item's Display value to the control
        for (int i = 0; i < cart.Count; i++)
        {
            lstCart.Items.Add(this.cart[i].Display());
            subtotal += this.cart[i].price;
        }
        costLabel.Text = subtotal.ToString("c");
    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        //if cart contains items and user has selected an item...
        if (cart.Count > 0)
        {
            if (lstCart.SelectedIndex > -1)
            {

                //remove selected item from cart and re-add cart items 
                cart.RemoveAt(lstCart.SelectedIndex);
                this.DisplayCart();
            }
            else
            { //if no item is selected, notify user 
                lblMessage.Text = "Please select an item to remove.";
            }
        }
    }
    protected void btnEmpty_Click(object sender, EventArgs e)
    {
        //if cart has items, clear both cart and list control
        if (cart.Count > 0)
        {
            cart.Clear();
            lstCart.Items.Clear();
        }
    }
    protected void btnCheckOut_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "That page hasn't been implemented yet.";
    }

    protected void btnContinue_Click(object sender, EventArgs e)
    {

    }
}