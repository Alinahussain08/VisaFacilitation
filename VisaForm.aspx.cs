using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Oracle.ManagedDataAccess.Client;
using System.Data.OleDb;
using System.Globalization;
using System.ServiceModel;
using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using System.Windows.Forms;
using System.Xml.Linq;
//using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Activities.Statements;
//using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Activities.Expressions;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
//using Oracle.ManagedDataAccess.Client;
//using Oracle.ManagedDataAccess.Client;
//using Oracle.ManagedDataAccess.Client;
//using Oracle.ManagedDataAccess.Client;

namespace VisaFacilitation
{
    public partial class PreRegister1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["HRS_USER_ID"] as string))
            {
                Response.Redirect("Login.aspx", true);
            }

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Session["APPROVAL_REQ_ID"] as string))
                {
                    // The approver is reviewing an existing request, fill fields with approval data
                    fillFieldsForApproval(Session["APPROVAL_REQ_ID"].ToString());
                }
                else
                {
                    // The employee is submitting a new request, fill fields with employee's data
                    fillEmployeeInformation();
                }

                // Bind dropdowns and load dependents
                ddlDestination.DataSource = country_name_list();
                ddlDestination.DataBind();
               
                changeButtons();
            }
        }

        private void changeButtons()
        {
            
                if (!string.IsNullOrEmpty(Session["APPROVAL_REQ_ID"] as string))
            {
                btnSubmitTravelInfo.Visible = false;
                btnSubmitPersonalInfo.Visible = false;
                btnsaveDependent.Visible = false;
                Button2.Visible = false;


                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    string query = @"SELECT DISTINCT wf.STEP_ID
  FROM VISAFORM.ATF_WF_INSTANCE_STEPS wf
 WHERE wf.STEP_SORT_NO = ((SELECT COUNT(*)
                               FROM VISAFORM.ATF_WF_INSTANCE_STEPS
                              WHERE APPROVED = 'Y' AND REQ_ID = :REQUEST_ID)+1)
                         AND wf.APP_ID = 2";
                    OracleCommand cmd = new OracleCommand(query, con);
                    cmd.Parameters.Add(":REQUEST_ID", OracleType.VarChar).Value = Session["APPROVAL_REQ_ID"];
                    con.Open();
                    OracleDataReader reader = cmd.ExecuteReader();

                    string step_id = "";
                    if (reader.Read())
                    {
                        step_id = reader["STEP_ID"].ToString();
                    }

                    

                }

            }

            else {
               
                    btnVerify.Visible = false; }
        }

        private void fillFieldsForApproval(string req_id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                string query = @"SELECT TD.*,
                                TR.*,
                                e.NAME,
                                e.DESIGNATION
                         FROM VISAFORM.VFF_TRIP_DETAILS TD
                         JOIN VISAFORM.VFF_TRAVEL_REQUEST TR
                             ON TD.TRAVEL_REQUEST_ID = TR.TRAVEL_REQUEST_ID
                         JOIN VISAFORM.MR_HRS_EMPLOYEE e ON TR.PNO = e.PNO
                         WHERE TD.TRAVEL_REQUEST_ID = :REQUEST_ID";


                OracleCommand cmd = new OracleCommand(query, con);
                cmd.Parameters.Add(":REQUEST_ID", OracleType.VarChar).Value = req_id;
                con.Open();
                OracleDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // Populate the approval form fields and disable them
                    ddlDestination.SelectedValue = reader["DESTINATION"].ToString();
                    ddlDestination.Enabled = false;
                    txtDesignation.Text = reader["DESIGNATION"].ToString();
                    txtDesignation.Enabled = false;
                    txtCourseTitle.Text = reader["COURSE_TITLE"].ToString();
                    txtCourseTitle.Enabled = false;
                    ddlPurpose.Text = reader["PURPOSE"].ToString();
                    ddlPurpose.Enabled = false;
                    ddlLocation.Text = reader["LOCATION"].ToString();
                    ddlLocation.Enabled = false;
                    txttripnumber.Text = reader["TRIPNUMBER"].ToString();
                    txttripnumber.Enabled = false;
                    txtTrainingInstitution.Text = reader["TRAINING_INSTITUTE"].ToString();
                    txtTrainingInstitution.Enabled = false;
                    txtVenue.Text = reader["VENUE"].ToString();
                    txtVenue.Enabled = false;

                    txtDurationTo.Text = reader["FROM_DATE"].ToString();
                    txtDurationTo.Enabled = false;

                    txtDurationFrom.Text = reader["TO_DATE"].ToString();
                    txtDurationFrom.Enabled = false;

                    txtPassportNumber.Text = reader["PASSPORT_NUMBER"].ToString();
                    txtPassportNumber.Enabled = false;

                    txtDOB.Text = reader["DOB"].ToString();
                    txtDOB.Enabled = false;

                    txtPassportIssueDate.Text = reader["PASSPORT_ISSUE_DATE"].ToString();
                    txtPassportIssueDate.Enabled = false;

                    txtPassportExpiryDate.Text = DateTime.Parse(reader["PASSPORT_EXPIRY_DATE"].ToString()).ToString();
                    txtPassportExpiryDate.Enabled = false;
                }
            }
        }


        protected void chkAddDependents_CheckedChanged(object sender, EventArgs e)
        {
            dependentSection.Visible = chkAddDependents.Checked;
            if (chkAddDependents.Checked)
            {
                LoadDependents();
            }

        }




        private void fillEmployeeInformation()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                string query = @"SELECT e.DESIGNATION, e.DOB, e.NAME
                                 FROM MR_HRS_EMPLOYEE e
                                 WHERE e.PNO = :PNO AND e.ACTIVE_FLAG = 'Y'";

                using (OracleCommand cmd = new OracleCommand(query, con))
                {

                  
                    cmd.Parameters.Add(":PNO", Session["PNO"].ToString());



                    con.Open();
                    using (OracleDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            // Assuming you have these controls on your page
                            txtDesignation.Text = rd["DESIGNATION"].ToString();
                            txtDOB.Text = Convert.ToDateTime(rd["DOB"]).ToString("yyyy-MM-dd");
                            //txtDateOfBirth.Text = Convert.ToDateTime(rd["DOB"]).ToString("yyyy-MM-dd");
                            txtName.Text = rd["NAME"].ToString();
                        }
                    }

                }
            }
        }


        public static List<string> country_name_list()
        {
            List<string> culturelist = new List<string>();
            CultureInfo[] getcultureinfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo getculture in getcultureinfo)
            {
                RegionInfo getregioninfo = new RegionInfo(getculture.LCID);
                if (!(culturelist.Contains(getregioninfo.EnglishName)))
                {
                    culturelist.Add(getregioninfo.EnglishName);

                }
            }
            culturelist.Sort();
            return culturelist;
        }



        protected void btnsaveDependent_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtDependentName.Text;
                string relation = ddlRelation.SelectedValue;
                string passportNo = txtPassportNo.Text;
                DateTime dateOfBirth = DateTime.Parse(txtDateOfBirth.Text);
                DateTime passportExpiry = DateTime.Parse(txtPassportExpiry.Text);
                DateTime passportIssue = DateTime.Parse(txtPassportIssue.Text);

                bool success = InsertDependent(name, relation, passportNo, dateOfBirth, passportExpiry, passportIssue);

                if (success)
                {
                    LoadDependents();
                    ClearDependentFields();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Dependent added successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Failed to add dependent. Please check the database connection and try again.');", true);
                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Failed to add dependent. Please try again.');", true);
            }
        }



        private bool InsertDependent(string NAME, string RELATION, string PASSPORT_NUMBER, DateTime DOB, DateTime PASSPORT_EXPIRY_DATE, DateTime PASSPORT_ISSUE_DATE)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                string query = @"INSERT INTO VFF_TRAVELLING_DEPENDENTS (travel_request_id, dependent_id, Name, Relation, Passport_Number, DOB, Passport_Expiry_DATE, Passport_Issue_DATE, PNO) 
                         VALUES (:travel_request_id, (select nvl(max(DEPENDENT_ID)+1,1 ) from VFF_TRAVELLING_DEPENDENTS), :NAME, :RELATION, :PASSPORT_NUMBER, :DOB, :PASSPORT_EXPIRY_DATE, :PASSPORT_ISSUE_DATE, :PNO)";
                OracleCommand cmd = new OracleCommand(query, conn);


                cmd.Parameters.Add(":traveL_request_id", OracleType.VarChar).Value = Session["REQ_ID"].ToString();
                cmd.Parameters.Add(":NAME", OracleType.VarChar).Value = NAME;
                cmd.Parameters.Add(":RELATION", OracleType.VarChar).Value = RELATION;
                cmd.Parameters.Add(":PASSPORT_NUMBER", OracleType.VarChar).Value = PASSPORT_NUMBER;
                cmd.Parameters.Add(":DOB", OracleType.DateTime).Value = DOB;
                cmd.Parameters.Add(":PASSPORT_EXPIRY_DATE", OracleType.DateTime).Value = PASSPORT_EXPIRY_DATE;
                cmd.Parameters.Add(":PASSPORT_ISSUE_DATE", OracleType.DateTime).Value = PASSPORT_ISSUE_DATE;
                cmd.Parameters.Add(":PNO", OracleType.Number).Value = int.Parse(Session["PNO"].ToString());


                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Log the error
                    System.Diagnostics.Debug.WriteLine(string.Format("Error inserting dependent: {0}", ex.Message));
                    return false;
                }
            }
        }
        private void LoadDependents()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                string query = "SELECT * FROM VFF_TRAVELLING_DEPENDENTS  where pno = '" + Session["PNO"].ToString() + "' AND DELETE_FLAG IS NULL";
                OracleDataAdapter adapter = new OracleDataAdapter(query, conn);
                DataTable dt = new DataTable();
                try
                {
                    adapter.Fill(dt);
                    ////gvDependents.DataSource = dt;
                    ////gvDependents.DataBind();
                    ////gvDependents.Visible = dt.Rows.Count > 0;

                    //// Debug information
                  
                    System.Diagnostics.Debug.WriteLine(string.Format("Loaded {0} dependents", dt.Rows.Count));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("Error loading dependents: {0}", ex.Message));
                }
            }
        }

        private void ClearDependentFields()
        {
            txtDependentName.Text = string.Empty;
            ddlRelation.SelectedIndex = 0;
            txtPassportNo.Text = string.Empty;
            txtDateOfBirth.Text = string.Empty;
            txtPassportExpiry.Text = string.Empty;
            txtPassportIssue.Text = string.Empty;
        }

        protected void gvDependents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRow")
            {
                int dependentId = Convert.ToInt32(e.CommandArgument);  // Use ID from the CommandArgument
                if (DeleteDependent(dependentId))
                {
                    LoadDependents();  // Refresh the GridView after deletion
                }
            }
        }

        private bool DeleteDependent(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                string query = "DELETE FROM DEPENDENTS WHERE ID = :DEPENDENT_id";
                OracleCommand cmd = new OracleCommand(query, conn);
                cmd.Parameters.Add(":DEPENDENT_id", OracleType.Number).Value = id;

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    // Log the error
                    return false;
                }
            }
        }

        private bool SaveTripDetails()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                string query = @"INSERT INTO vff_trip_details 
                         (travel_request_id, trip_no, destination, purpose,training_institute, course_title, venue, from_date, to_date, location, tripnumber) 
                         VALUES 
                         (:traveL_request_id, (SELECT NVL(MAX(trip_no) + 1, 1) FROM vff_trip_details), 
                          :destination, :purpose,:training_institute, :course_title, :venue, :from_date, :to_date, :location, :tripnumber)";

                using (OracleCommand cmd = new OracleCommand(query, conn))
                {
                    // Add parameters with proper OracleType
                    cmd.Parameters.Add(":traveL_request_id", OracleType.VarChar).Value = Session["REQ_ID"].ToString();
                    cmd.Parameters.Add(":destination", OracleType.VarChar).Value = ddlDestination.SelectedValue;
                    cmd.Parameters.Add(":purpose", OracleType.VarChar).Value = ddlPurpose.Text;
                    cmd.Parameters.Add(":course_title", OracleType.VarChar).Value = txtCourseTitle.Text;
                    cmd.Parameters.Add(":training_institute", OracleType.VarChar).Value = txtTrainingInstitution.Text;
                    cmd.Parameters.Add(":venue", OracleType.VarChar).Value = txtVenue.Text;
                    cmd.Parameters.Add(":from_date", OracleType.DateTime).Value = DateTime.Parse(txtDurationFrom.Text);
                    cmd.Parameters.Add(":to_date", OracleType.DateTime).Value = DateTime.Parse(txtDurationTo.Text);
                    cmd.Parameters.Add(":location", OracleType.VarChar).Value = ddlLocation.SelectedValue;
                    cmd.Parameters.Add(":tripnumber", OracleType.VarChar).Value = txttripnumber.Text;
                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        // Log the error as needed
                        System.Diagnostics.Debug.WriteLine("Error saving trip details:", ex.Message);
                        // Optional: throw the exception or handle it according to your application's needs
                        return false;
                    }
                }
            }
        }
        private bool SaveTravelRequest()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                string query = @"INSERT INTO VFF_TRAVEL_REQUEST
                         (TRAVEL_REQUEST_ID, DOB, NAME, DESIGNATION, PASSPORT_NUMBER, PASSPORT_EXPIRY_DATE, PASSPORT_ISSUE_DATE, PNO) 
                         VALUES 
                         ((SELECT NVL(MAX(TRAVEL_REQUEST_ID) + 1, 1) FROM VFF_TRAVEL_REQUEST), 
                          :DOB,:NAME, :DESIGNATION, :PASSPORT_NUMBER, :PASSPORT_EXPIRY_DATE, :PASSPORT_ISSUE_DATE, :PNO
)";

                using (OracleCommand cmd = new OracleCommand(query, conn))
                {
                    try
                    {
                        // Parse the date values without using 'out'
                        DateTime dob = DateTime.Parse(txtDOB.Text);
                        DateTime passportExpiryDate = DateTime.Parse(txtPassportExpiryDate.Text);
                        DateTime passportIssueDate = DateTime.Parse(txtPassportIssueDate.Text);

                        // Add parameters with proper OracleType

                        cmd.Parameters.Add(":DOB", OracleType.DateTime).Value = dob;
                        cmd.Parameters.Add(":NAME", OracleType.VarChar).Value = txtName.Text;
                        cmd.Parameters.Add(":DESIGNATION", OracleType.VarChar).Value = txtDesignation.Text;
                        


                        cmd.Parameters.Add(":PASSPORT_NUMBER", OracleType.VarChar).Value = txtPassportNumber.Text;
                        cmd.Parameters.Add(":PASSPORT_EXPIRY_DATE", OracleType.DateTime).Value = passportExpiryDate;
                        cmd.Parameters.Add(":PASSPORT_ISSUE_DATE", OracleType.DateTime).Value = passportIssueDate;
                        cmd.Parameters.Add(":PNO", OracleType.Number).Value =int.Parse(Session["PNO"].ToString());
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();


                        cmd.Parameters.Clear();
                        cmd.CommandText = "SELECT MAX(TRAVEL_REQUEST_ID) FROM VFF_TRAVEL_REQUEST";
                        Session["REQ_ID"] = Convert.ToInt32(cmd.ExecuteScalar());
                        return rowsAffected > 0;
                    }
                    catch (FormatException fe)
                    {
                        // Handle date parsing errors
                        System.Diagnostics.Debug.WriteLine("Invalid date format:", fe.Message);
                        return false;
                    }
                    catch (Exception ex)
                    {
                        // Log other errors
                        System.Diagnostics.Debug.WriteLine("Error saving travel request details:", ex.Message);
                        return false;
                    }
                }
            }
        }


        protected void btnSubmitPersonalInfo_Click(object sender, EventArgs e)
        {
            if (SaveTravelRequest())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Personal information saved successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Failed to save personal information. Please try again.');", true);
            }
        }
        protected void btnSubmitTravelInfo_Click(object sender, EventArgs e)
        {
            if (SaveTripDetails())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('TRAVEL information saved successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Failed to save Travel information. Please try again.');", true);
            }
        }

        protected void btnsaveform_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;

                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    string query = @"  SELECT w.STEP_ID,
         w.STEP_SORT_NO,
         e.NAME    AS APPROVER_NAME,
         w.APPROVER_TYPE,
         CASE
             WHEN STEP_ID = 1
             THEN
                 :PNO
             ELSE
                 w.APPROVER_VALUE
         END       AS APPROVER_VALUE,
         w.STEP_DESC
    FROM VISAFORM.ATF_WF_TEMPLATES w
         LEFT JOIN VISAFORM.MR_HRS_EMPLOYEE e
             ON CASE
                    WHEN    STEP_ID = 1
                    THEN
                        :PNO
                    ELSE
                        w.APPROVER_VALUE
                END =
                e.PNO
   WHERE     w.APP_ID = 2
         AND w.WF_ID = 5
ORDER BY STEP_SORT_NO ASC";


                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(":PNO", int.Parse(Session["PNO"].ToString()));
                        conn.Open();
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            cmd.CommandText = @"INSERT INTO VISAFORM.ATF_WF_INSTANCE_STEPS(
    APP_ID,
    REQ_ID,
    STEP_ID,
    STEP_SORT_NO,
    STEP_DESC,
    APPROVER_ID,
    APPROVER_TYPE,
    APPROVED
) VALUES(
    2,
    :REQ_ID,
    :STEP_ID,
    :STEP_SORT_NO,
    :STEP_DESC,
    :APPROVER_ID,
    :APPROVER_TYPE,
    :APPROVED
)";

                            cmd.Parameters.Clear();

                            string reqId = Session["REQ_ID"].ToString();
                            string stepId = reader["STEP_ID"].ToString();
                            string stepSortNo = reader["STEP_SORT_NO"].ToString();
                            string stepDesc = reader["STEP_DESC"].ToString();
                            string approverType = reader["APPROVER_TYPE"].ToString();

                            // Set approverId and approved based on the condition
                            string approverId;
                            object approved;

                            if (stepId == "1")
                            {
                                approverId = Session["PNO"].ToString();
                                approved = "Y"; // Note: 'Y' is passed as a string, not in quotes
                            }
                            else
                            {
                                approverId = reader["APPROVER_VALUE"].ToString();
                                approved = DBNull.Value;
                            }

                            // Add parameters to the command
                            cmd.Parameters.Add(":REQ_ID", reqId);
                            cmd.Parameters.Add(":STEP_ID", stepId);
                            cmd.Parameters.Add(":STEP_SORT_NO", stepSortNo);
                            cmd.Parameters.Add(":STEP_DESC", stepDesc);
                            cmd.Parameters.Add(":APPROVER_ID", approverId);
                            cmd.Parameters.Add(":APPROVER_TYPE", approverType);
                            cmd.Parameters.Add(":APPROVED", approved);

                            cmd.ExecuteNonQuery();


                            //                            cmd.CommandText = @"INSERT INTO ATF.ATF_WF_INSTANCE_STEPS(
                            //    APP_ID,
                            //    REQ_ID,
                            //    STEP_ID,
                            //    STEP_SORT_NO,
                            //    STEP_DESC,
                            //    APPROVER_ID,
                            //    APPROVER_TYPE,
                            //    APPROVED
                            //) VALUES(
                            //    2,
                            //    :REQ_ID,
                            //    :STEP_ID,
                            //    :STEP_SORT_NO,
                            //    :STEP_DESC,
                            //    :APPROVER_ID,
                            //    :APPROVER_TYPE,
                            //    :APPROVED
                            //    )";
                            //                            cmd.Parameters.Clear();
                            //                            cmd.Parameters.Add(" :REQ_ID", Session["REQ_ID"].ToString());
                            //                            cmd.Parameters.Add(":STEP_ID", reader["STEP_ID"].ToString());
                            //                            cmd.Parameters.Add(":STEP_SORT_NO", reader["STEP_SORT_NO"].ToString());
                            //                            cmd.Parameters.Add(":STEP_DESC", reader["STEP_DESC"].ToString());
                            //                            if (reader["STEP_ID"].ToString() == "1")
                            //                            {
                            //                                cmd.Parameters.Add(":APPROVER_ID", int.Parse(Session["PNO"].ToString()));
                            //                                cmd.Parameters.Add(":APPROVED", "Y");
                            //                            }
                            //                            else
                            //                            {
                            //                                cmd.Parameters.Add(":APPROVER_ID", reader["APPROVER_ID"].ToString());
                            //                                cmd.Parameters.Add(":APPROVED", DBNull.Value);

                            //                            }

                            //                            cmd.Parameters.Add(":APPROVER_TYPE", reader["APPROVER_TYPE"].ToString());


                            //                            cmd.ExecuteNonQuery();


                        }
                    }


                }
            }
            catch (Exception ex) {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert(" +  ex.Message + ");", true);
            }

            }

        //protected void gvDependents_RowDataBound(object sender, GridViewRowEventArgs e) {
        //    string test = gvDependents.Columns[6].HeaderText;
        //    gvDependents.Columns[6].Visible = false;
        //}
     
        protected void btnVerify_Click(object sender, EventArgs e) {

            string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                string query = @"
                                UPDATE VISAFORM.ATF_WF_INSTANCE_STEPS
                                   SET APPROVED = 'Y'
                                 WHERE     REQ_ID = :REQ_ID
                                       AND STEP_ID =
                                           (SELECT DISTINCT wf.STEP_ID
                                              FROM VISAFORM.ATF_WF_INSTANCE_STEPS wf
                                             WHERE     wf.STEP_SORT_NO =
                                                       (  (SELECT COUNT (*)
                                                             FROM VISAFORM.ATF_WF_INSTANCE_STEPS
                                                            WHERE APPROVED = 'Y' AND REQ_ID = :REQ_ID)
                                                        + 1)
                                                   AND wf.APP_ID = :APP_ID)";
                using (OracleCommand cmd = new OracleCommand(query, con))
                {
                    cmd.Parameters.Add(":REQ_ID", Session["APPROVAL_REQ_ID"].ToString());
                    cmd.Parameters.Add(":APP_ID", 2);
                    con.Open();
                    cmd.ExecuteNonQuery();


                }
                Session["SUCCESS_MESSAGE"] = "Travel Request Id " + Session["APPROVAL_REQ_ID"].ToString() + " Has Been Sent";
                Response.Redirect("./Success.aspx", false);

            }


        }
    }
        public class Country
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }
    }