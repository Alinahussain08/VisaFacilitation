using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inbox : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = ConfigurationManager.AppSettings["title"];

        if (string.IsNullOrEmpty(Session["HRS_USER_ID"] as string))
        {
            Response.Redirect("Login.aspx", true);
        }

        if (!IsPostBack)
        {
            addInboxData();
        }


    }

    private void addInboxData()
    {

        OracleConnection oc = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        OracleCommand cmd = new OracleCommand
        {
            Connection = oc
        };

        try
        {
            oc.Open();
            cmd.CommandText = @"  SELECT TD.*, e.NAME AS INITIATOR_NAME
    FROM VISAFORM.VFF_TRIP_DETAILS TD
    JOIN VISAFORM.MR_HRS_EMPLOYEE e ON PNO=(SELECT PNO FROM VISAFORM.VFF_TRAVEL_REQUEST WHERE TRAVEL_REQUEST_ID=TD.TRAVEL_REQUEST_ID)
   WHERE TD.TRAVEL_REQUEST_ID IN
             (SELECT i.REQ_ID
                FROM VISAFORM.ATF_WF_INSTANCE_STEPS i
               WHERE     i.APPROVER_ID = :PNO
               AND i.APP_ID=2
                     AND i.APPROVED IS NULL
                     AND (i.STEP_SORT_NO =
                          (  (SELECT COUNT (*)
                                FROM ATF_WF_INSTANCE_STEPS
                               WHERE     APPROVED = 'Y'
                                     AND REQ_ID = i.REQ_ID)
                           + 1)))
ORDER BY TRAVEL_REQUEST_ID";
            cmd.Parameters.Add(new OracleParameter(":PNO", Session["PNO"].ToString()));

            OracleDataReader reader =  cmd.ExecuteReader();

            DataTable dt = new DataTable();

            // Load the data from the reader into the DataTable
            dt.Load(reader);
            reader.Close();
            InboxTransferRequests.DataSource = dt;
            InboxTransferRequests.DataBind();
        }
        catch (Exception ex)
        {
            lblRedBottom.Text = "There was an error while fetching your pending requests. Please contact IT if issue persists. Error: " + ex.Message;

        }
        finally
        {
            oc.Close(); 
            oc.Dispose();
        }
    }
    public void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("./Menu.aspx", true);
    }

    protected void GridViewInboxTransferRequests_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Check if the current row is a data row
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    InboxTransferRequests.Columns[9].Visible = false;// Index 6 corresponds to the reinitiated column (0-based index)
        //}
    }


    public void btnDetails_Click(object sender, EventArgs e)
    {
        ClearMessages();
        bool atLeastOneRowSelected = false;
        // Loop through each row in the GridView
        foreach (GridViewRow row in InboxTransferRequests.Rows)
        {
            // Find the CheckBox control in the current row
            CheckBox chk = (CheckBox)row.FindControl("CheckBoxInbox");

            // Check if the CheckBox is checked
            if (chk != null && chk.Checked)
            {
                atLeastOneRowSelected = true;

                string reqID = row.Cells[1].Text;
                Session["APPROVAL_REQ_ID"] = reqID; // 1st column of the gridview is set to REQ_ID on the aspx page.
                break; // Exit loop if at least one CheckBox is checked
            }
        }



        if (atLeastOneRowSelected)
        {
            Response.Redirect("./VisaForm.aspx", true);
        }
        else
        {
            lblRedBottom.Text = "You need to select a request to proceed*";

        }
    }
    //public void btnDetails_Click(object sender, EventArgs e)
    //{
    //    ClearMessages();
    //    bool atLeastOneRowSelected = false;
    //    // Loop through each row in the GridView
    //    foreach (GridViewRow row in InboxTransferRequests.Rows)
    //    {
    //        // Find the CheckBox control in the current row
    //        CheckBox chk = (CheckBox)row.FindControl("CheckBoxInbox");

    //        // Check if the CheckBox is checked
    //        if (chk != null && chk.Checked)
    //        {
    //            atLeastOneRowSelected = true;

    //            string reqID = row.Cells[1].Text;
    //            Session["APPROVAL_REQ_ID"] = reqID; // 1st column of the gridview is set to REQ_ID on the aspx page.
    //            break; // Exit loop if at least one CheckBox is checked
    //        }
    //    }

    //    if (atLeastOneRowSelected)
    //    {
    //        // Redirect to VisaFormDetails.aspx where the request details will be displayed
    //        Response.Redirect("./VisaFormDetails.aspx", true);
    //    }
    //    else
    //    {
    //        lblRedBottom.Text = "You need to select a request to proceed*";
    //    }
    //}


    public void ClearMessages()
    {
        lblGreenBottom.Text = "";
        lblRedBottom.Text = "";
    }
    
}