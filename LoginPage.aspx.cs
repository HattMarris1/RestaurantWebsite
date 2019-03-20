using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class LoginPage : System.Web.UI.Page
{
    string connstring = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\\RestaurantDB.mdf; Integrated Security = True";
    protected void Page_Load(object sender, EventArgs e)
    {
        //check if the "loggedIn" session state is false or not set
        if (!((Session["loggedIn"] == null) || (bool)Session["loggedIn"] == false))
        {
            Response.Redirect("Order.aspx");
        }
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        LoginPasswordErrorLabel.Text = "";
        LoginNameErrorLabel.Text = "";
        string username = LoginUserNameBox.Text;
        string password = LoginPasswordBox.Text;

        SqlConnection conn = new SqlConnection();
       
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
                Session["loggedIn"] = true;
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

    protected void RegisterButton_Click(object sender, EventArgs e)
    {
        RegisterPasswordErrorLabel.Text = "";
        RegisterNameErrorLabel.Text = "";
        string username = RegisterUserNameBox.Text;
        string password = RegisterPasswordBox.Text;

        SqlConnection conn = new SqlConnection();

        conn.ConnectionString = connstring;
        conn.Open();
        string namecheck = "SELECT Count(1) FROM Users WHERE UserName = '" + username + "'";
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = new SqlCommand(namecheck, conn);

        int nameResult = (int)adapter.SelectCommand.ExecuteScalar();
        if (nameResult == 0)
        {
            if (RegisterPasswordBox.Text==RegisterPasswordReenterBox.Text)
            {
                //register successful, add user's details to db
                string updateusers = "insert into users (UserName,Password) values ('" + RegisterUserNameBox.Text + "','" + RegisterPasswordBox.Text + "')";
                adapter.InsertCommand = new SqlCommand(updateusers,conn);
                adapter.InsertCommand.ExecuteNonQuery();
                Response.Redirect("Order.aspx");
            }
            else
            {
                //password wrong
                LoginPasswordErrorLabel.Text = "Passwords don't match";

            }
        }
        else
        {
            //user already exists
            RegisterNameErrorLabel.Text = "UserName Taken";
        }
    }
}