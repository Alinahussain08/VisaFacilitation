using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.OracleClient;
using Ajax;


public partial class MasterPage : System.Web.UI.MasterPage
{
    protected string name = "";
    protected string pno = "";
    protected string RoleName = "";
    protected String role;
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["HRS_USER_ID"].ToString() == "" || Session["HRS_USER_ID"].ToString() == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (!IsPostBack)
        {
            //if (Session["MED_REF_ROLE"].ToString() == null || Session["MED_REF_ROLE"].ToString() == "")
            //{
                
            //    aRefApprove.HRef = "#";
            //    aRefInbox.HRef = "#";
            //    aRefVerify.HRef = "#";
            //    aRefByStaff.HRef = "#";
            //}
            //if (Session["MED_REF_ROLE"].ToString() == "D" || Session["MED_REF_ROLE"].ToString() == "M" || Session["MED_REF_ROLE"].ToString() == "" || Session["MED_REF_ROLE"].ToString() == null)
            //{
            //    aRefVerify.HRef = "#";
            //}
            //if (Session["MED_REF_ROLE"].ToString() == "S")
            //{
            //    aRefApprove.HRef = "#";
            //    aRefInbox.HRef = "#";
            //    aRefVerify.HRef = "#";
            //    aRefRequest.HRef = "#";
            //    aMRNos.HRef = "#";
            //    aRefByStaff.HRef = "#";
            //}
        }

        if (string.IsNullOrEmpty(Session["HRS_USER_ID"] as string))
        {
            Response.Redirect("Login.aspx", true);
        }

        //name = Session["name"].ToString();
        pno = "1472";// Session["MR_PNO"].ToString();
        //role = Session["role"].ToString();

        //if (string.IsNullOrEmpty(Session["MR_PNO"] as string))
        //{
        //    pno = "0";
        //}
        liDashboard.Visible = true;
        liMedicalCenter.Visible = true;
        liEmployees.Visible = true;
        liHospital.Visible = true;
        //liQr.Visible = true;
        liReports.Visible = true;
        //liUnlock.Visible = true;



    }



}

