using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Site : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //check if the "loggedIn" session state is false or not set
        if (!((Session["loggedIn"] == null) || (bool)Session["loggedIn"] == false))
        {
            LogoutButton.Visible = false;
        }
        else
        {
            LogoutButton.Visible = true;
        }
        /*
        //fill 'Days Until' label if it hasn't been overridden
        if (lblDaysUntilHalloween != null)
        {
            int daysUntil = DaysUntilHalloween();
            switch (daysUntil)
            {
                case 0:
                    lblDaysUntilHalloween.Text = "Happy Halloween!";
                    break;
                case 1:
                    lblDaysUntilHalloween.Text = "Tomorrow is Halloween!";
                    break;
                default:
                    lblDaysUntilHalloween.Text = string.Format(
                        "There are only {0} days left until Halloween!", daysUntil);
                    break;
            }
        }*/
    }

    private int DaysUntilHalloween()
    {
        DateTime halloween = new DateTime(DateTime.Today.Year, 10, 31);
        if (DateTime.Today > halloween)
            halloween = halloween.AddYears(1);
        TimeSpan ts = halloween - DateTime.Today;
        return ts.Days;
    }

    protected void LogoutButton_Click(object sender, EventArgs e)
    {

    }
}
