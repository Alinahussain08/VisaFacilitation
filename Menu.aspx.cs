using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Menu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["HRS_USER_ID"] as string))
        {
            Response.Redirect("Login.aspx", true);
        }

        if (!IsPostBack)
        {
          UpdatePendingInboxRequestNumber();
        }

    }

    protected void btnInitiate_Click(object sender, EventArgs e)
    {
        Response.Redirect("./VisaForm.aspx",true);
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        Response.Redirect("./Inbox.aspx", true);
    }


    private void UpdatePendingInboxRequestNumber()
    {
        OracleConnection oc = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        OracleCommand command = new OracleCommand
        {
            Connection = oc
        };

        try
        {
            oc.Open();
            command.CommandText = @"SELECT count (*)
  FROM VISAFORM.ATF_WF_INSTANCE_STEPS  i
       INNER JOIN VFF_TRAVEL_REQUEST r ON i.REQ_ID = r.TRAVEL_REQUEST_ID
 WHERE     i.APPROVER_ID = :APPROVER_ID
       AND i.APPROVED IS NULL
       AND (i.STEP_SORT_NO = (  (SELECT COUNT (*)
                                   FROM ATF_WF_INSTANCE_STEPS
                                  WHERE APPROVED = 'Y' AND REQ_ID = i.REQ_ID)
                              + 1))
       AND r.DELETE_FLAG IS null
       AND i.APP_ID = 2 ";
            command.Parameters.Add(new OracleParameter("APPROVER_ID", Session["PNO"].ToString()));
            OracleDataReader reader = command.ExecuteReader();

            string numPending = "";
            if (reader.Read())
            {
                numPending = reader["COUNT(*)"].ToString();
                numPendingRequests1.InnerText = numPending;
                numPendingRequests2.InnerText = numPending;
            }

        }
        catch
        {
            numPendingRequests1.InnerText = "0";
            numPendingRequests2.InnerText = "0";
        }
        finally
        {
            oc.Close();
            oc.Dispose();
            command.Dispose();
        }
    }

}