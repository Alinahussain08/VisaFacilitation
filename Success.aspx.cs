using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Success : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["HRS_USER_ID"] as string))
        {
            Response.Redirect("Login.aspx", true);
        }

        pSuccessMessage.InnerText = Session["SUCCESS_MESSAGE"].ToString();
        if (!IsPostBack)
        {
            Session["APPROVAL_REQ_ID"] = null;
        }
    }

    public void btnBackToInbox_Click(object sender, EventArgs e)
    {
        Session["REINITIATED_STATUS"] = false;
        Response.Redirect("./Inbox.aspx", true);
    }
}