using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class LoginPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }



    protected void LoginButton_Click(object sender, EventArgs e)
    {
        LoginPasswordErrorLabel.Text = "";
        LoginNameErrorLabel.Text = "";
        string username = LoginUserNameBox.Text;
        string password = LoginPasswordBox.Text;

        SqlConnection conn = new SqlConnection();
        string connstring = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\\RestaurantDB.mdf; Integrated Security = True";
        conn.ConnectionString = connstring;
        conn.Open();
        string namecheck = "SELECT Count(1) FROM Users WHERE UserName = '" + username +"'";
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = new SqlCommand(namecheck, conn);
  
        int nameResult = (int)adapter.SelectCommand.ExecuteScalar();
        if (nameResult == 1)
        {
            string passwordcheck = "Select Password FROM Users where UserName = '" + username + "'";
            adapter.SelectCommand = new SqlCommand(passwordcheck, conn);
            var passwordCheck = adapter.SelectCommand.ExecuteScalar();
            if (passwordCheck.ToString() == password)
            {
                //login successful
                Response.Redirect("Order.aspx");
            }
            else
            {
                //password wrong
                LoginPasswordErrorLabel.Text = "Incorrect Password";
                
            }
        }
        else
        {
            //user doesn't exist
            LoginNameErrorLabel.Text = "User does not exist";
        }
        
    }
}