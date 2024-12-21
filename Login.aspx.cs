using System;
using System.IO;
using System.Net;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Net.Mail;


public partial class Login : System.Web.UI.Page
{

    string username, password, pno;
    //void Page_Init(object sender, EventArgs e)
    //{
    //    ViewStateUserKey = Session.SessionID;

    //}

    //protected override void OnInit(EventArgs e)
    //{
    //    // Assuming that your page makes use of ASP.NET session state and the SessionID is stable.
    //    ViewStateUserKey = Session.SessionID;
    //}

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (User.Identity.IsAuthenticated)
            ViewStateUserKey = Session.SessionID;
    }




    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtUserId.Visible = true;
            txtPassword.Visible = true;
        }

        if (Request.QueryString["id"] != null)
        {


            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            OracleCommand cmd = new OracleCommand();/*
            OracleDataReader rd;
            con.Open();
            try
            {
                cmd.CommandText = "SELECT u.USER_ID, u.PASSWORD, u.PWD, u.Name, u.email, u.LOCATION_ID, u.SUB_LOCATION_ID, u.VMS_ROLE, u.LOCKED_FLAG, u.VMS_FUEL_ROLE, MED_REF_ROLE, u.FUEL, u.PNO, u.MED_STAFF, u.MED_STAFF_ID, u.IS_DOCTOR, u.EDU_SOCIETY, u.EMPLOYER_CODE, u.CBS_COMPANY_ID, u.CBS, u.CBS_ROLE, u.HR, (SELECT EMPLOYEE_GROUP FROM MR_HRS_EMPLOYEE WHERE PNO = u.pno and EMPLOYER_CODE=u.EMPLOYER_CODE AND ROWNUM = 1) EMPLOYEE_GROUP FROM MR_USERS u where u.PNO ='" + username + "' and rownum=1";
                cmd.Connection = con;
                rd = cmd.ExecuteReader();
                rd.Read();
                if (rd.HasRows)
                {
                    if (rd["LOCKED_FLAG"].ToString() == "Y")
                    {
                        lblStatus.Text = "This user is locked due to too many failed login attempts. Please contact system administrator.";
                    }
                    else
                    {
                        Session["EMAIL"] = rd["EMAIL"].ToString();
                        string email = rd["EMAIL"].ToString();
                        Session["MR_EMPLOYER_CODE"] = rd["EMPLOYER_CODE"].ToString();
                        Session["MR_MED_STAFF_ID"] = rd["MED_STAFF_ID"].ToString();
                        Session["MR_EMPLOYEE_GROUP"] = rd["EMPLOYEE_GROUP"].ToString();
                        Session["MR_USER_ID"] = rd["USER_ID"].ToString();
                        Session["MR_LOCATION_ID"] = rd["LOCATION_ID"].ToString();
                        Session["MR_SUB_LOCATION_ID"] = rd["SUB_LOCATION_ID"].ToString();
                        Session["MR_PNO"] = rd["PNO"].ToString();
                        Session["MR_MED_STAFF_ID"] = rd["MED_STAFF_ID"].ToString();
                        string a = rd["MED_STAFF_ID"].ToString();
                        Session["MR_MED_STAFF"] = rd["MED_STAFF"].ToString();
                        Session["MR_IS_DOCTOR"] = rd["IS_DOCTOR"].ToString();
                        Session["EDU_SOCIETY"] = rd["EDU_SOCIETY"].ToString();
                        Session["MED_REF_ROLE"] = rd["MED_REF_ROLE"].ToString();
                        Session["HR"] = rd["HR"].ToString();
                        if (Session["MR_MED_STAFF"].ToString() == "Y")
                        {
                            Response.Redirect("~/RefInbox.aspx", false);
                        }
                        else
                        {
                            Response.Redirect("~/RefRequest.aspx", false);
                        }
                    }
                }
                else
                {
                    lblStatus.Text = "Invalid User ID/Password.";
                    con.Dispose();
                }
            }

            catch (Exception ex)
            {
                lblStatus.Text = "An error has been occured. Please consult System Administrator";

            }
            finally
            {
                con.Close();
                con.Dispose();

            }*/
        }


    }

    public bool PasswordOk(string pwd)
    {
        // string s = "^(?=.[0-9])(?=.[a-z])(?=.[A-Z])(?=.[~!@#$%^&*()]).{8,}$";
        string s = "(?=.[a-zA-Z0-9~!@#$%^&()]).{8,}";
        return Regex.IsMatch(pwd, @s);

    }

    public bool captcha()
    {
        string result = "";
        string url = "https://www.google.com/recaptcha/api/siteverify?secret=6Le3_cUeAAAAAJDgPSEOj7pcspmGJpKaUmk3-tvU&response=" + txtToken.Value + "&remoteip=" + HttpContext.Current.Request.UserHostAddress.ToString();
        WebRequest request = HttpWebRequest.Create(url);
        WebResponse response = request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string urlText = reader.ReadToEnd();
        // Response.Write(urlText.ToString());

        int j = 1;
        foreach (string tok in urlText.Split(','))
        {
            if (j == 1)
            {
                //  Response.Write("<br/>" + tok);
                result = tok;
            };
            j++;
        }
        if (result.Contains("true"))
        {
            return true;
        }
        else
        {
            return false;
        }

    }


    protected void Authenticate_login(object sender, EventArgs e)
    {
        string username = txtUserId.Text.ToLower();


        string pwd = txtPassword.Text;

        username = username.Replace("'", "");
        username = username.Replace("ʹ", "");
        username = username.Replace("ʺ", "");
        username = username.Replace("\"", "");
        username = username.Replace("--", "");
        username = username.Replace("=", "");
        username = username.Replace("||", "");
        username = username.Replace("<", "＜");
        username = username.Replace(">", "");

        // pwd = pwd.Replace("'", "");

        pwd = pwd.Replace("ʹ", "");
        pwd = pwd.Replace("ʺ", "");
        pwd = pwd.Replace("\"", "");
        pwd = pwd.Replace("--", "");
        pwd = pwd.Replace("=", "");
        pwd = pwd.Replace("||", "");
        pwd = pwd.Replace("<", "");
        pwd = pwd.Replace(">", "");
        pwd = pwd.Replace("*", "");
        pwd = pwd.Replace("OR", "");
        pwd = pwd.Replace("AND", "");



        try
        {
            if (!PasswordOk(pwd))
            {
                lblStatus.Text = "Your password does not meet New Password Policy.";
                return;
            }

        }
        catch (Exception ex)
        {
            lblStatus.Text = lblStatus.Text + " Cant lock."; // + ex.Message;
        }

        hfPassword.Value = pwd;
        hfUserId.Value = username;

        pwd = md5(hfPassword.Value);

        if (string.IsNullOrEmpty(Session["unm"] as string))
        {
            Session["unm"] = username;

        }

        if (username == Session["unm"].ToString())
        {
            if (string.IsNullOrEmpty(Session["atempt"] as string))
            {
                Session["atempt"] = "1";
                // lblStatus.Text = "1st wrong atempt!";
            }
            else if (Session["atempt"].ToString() == "1")
            {
                Session["atempt"] = "2";
                // lblStatus.Text = "2nd wrong atempt!";
            }
            else if (Session["atempt"].ToString() == "2")
            {
                Session["atempt"] = "3";
                // lblStatus.Text = "3rd wrong atempt!";
            }
            else if (Session["atempt"].ToString() == "3")
            {
                Session["atempt"] = "4";
                // lblStatus.Text = "3rd wrong atempt!";
            }
            else if (Session["atempt"].ToString() == "4")
            {
                Session["atempt"] = "5";
                // lblStatus.Text = "3rd wrong atempt!";
            }
            else if (Session["atempt"].ToString() == "5")
            {
                Session["atempt"] = "6";
                // block user in db
                OracleConnection con2 = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                OracleCommand cmd2 = new OracleCommand();
                cmd2.Connection = con2;
                con2.Open();
                cmd2.CommandText = "Update VFF_HRS_SIGNUP set LOCKED_FLAG='Y' where lower(USER_ID) = lower('" + hfUserId.Value + "')";
                cmd2.ExecuteNonQuery();
                con2.Close();
                lblStatus.Text = "This user is locked out. Please contact IT!";
                Session.Abandon();
                Session["atempt"] = null;
                return;
            }
        }

        OracleConnection oraCon = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        oraCon.Open();
        try
        {
            OracleConnection con;
            OracleCommand cmd = new OracleCommand();
            OracleDataReader rd;


            cmd.Connection = oraCon;
            cmd.CommandText = "SELECT name, Locked_flag, pno FROM VISAFORM.MR_USERS u WHERE LOWER (USER_ID) = LOWER (:username) AND (u.Pwd = :pwd OR PWD = :md5pwd)";
            cmd.Parameters.AddWithValue(":username", hfUserId.Value);
            cmd.Parameters.AddWithValue(":pwd", hfPassword.Value);
            string hashed_pwd = md5(hfPassword.Value);
            cmd.Parameters.AddWithValue(":md5pwd", md5(hfPassword.Value));

            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                if (rd["LOCKED_FLAG"].ToString() == "Y")
                {
                    lblStatus.Text = "This user is locked due to too many failed login attempts. Please contact system administrator.";
                }
                else
                {
                    Session["HRS_NAME"] = rd["NAME"].ToString();
                    Session["HRS_USER_ID"] = txtUserId.Text;
                    Session["PNO"] = rd["PNO"].ToString();


                    Response.Redirect("Menu.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
        }
        finally
        {
            oraCon.Close();
        }
    }




    public static string md5(string sPassword)
    {
        System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] bs = System.Text.Encoding.UTF8.GetBytes(sPassword);
        bs = x.ComputeHash(bs);
        System.Text.StringBuilder s = new System.Text.StringBuilder();
        foreach (byte b in bs)
        {
            s.Append(b.ToString("x2").ToLower());
        }
        return s.ToString();

    }

    //protected void Replicate_log()
    //{
    //    try
    //    {
    //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnection"].ToString());
    //        SqlCommand cmd = new SqlCommand();
    //        SqlDataReader rd;
    //        con.Open();
    //        cmd.CommandText = "SELECT * from dbo.VU_visitor_out_events";
    //        cmd.Connection = con;
    //        rd = cmd.ExecuteReader();
    //        while (rd.Read())
    //        {
    //            OracleConnection con3 = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    //            OracleCommand cmd3 = new OracleCommand();
    //            cmd3.Connection = con3;
    //            OracleDataReader rd3;
    //            con3.Open();
    //            cmd3.CommandText = "select count(*) from RFID_LOGS where tableid=" + rd["tableid"].ToString() + " and cardnumber='" + rd["cardnumber"].ToString()+"'";
    //            rd3 = cmd3.ExecuteReader();
    //            while (rd3.Read())
    //            {
    //                if (rd3[0].ToString() == "0")
    //                {
    //                    OracleConnection con2 = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    //                    OracleCommand cmd2 = new OracleCommand();
    //                    cmd2.Connection = con2;
    //                    con2.Open();
    //                    cmd2.CommandText = "Insert into RFID_LOGS values("+rd["tableid"].ToString()+",'"+ rd["cardnumber"].ToString() + "','"+ rd["terminal"].ToString() + "','Successful','"+ rd["logstatus"].ToString() + "',to_date('"+rd["eventdatetime"].ToString()+ "','YYYY/MM/DD HH24:MI:SS'),null,null,null)";
    //                    cmd2.ExecuteNonQuery();
    //                    con2.Close();
    //                }
    //            }
    //            con3.Close();
    //        }
    //        con.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        lblStatus.Text = ex.Message+" Error getting logs.";

    //    }
    //}

    private string Base64Decode(string data)
    {
        string result = string.Empty;
        try
        {
            data = System.Web.HttpUtility.UrlDecode(data);
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            // if base64 were to be embedded in a URL, the use of the forward slash would be interpreted as a URL divider rather than
            // part of the base64. As a result, other characters such as dash (-), underscore (_), period
            // (.), colon (:) and exclamation point (!) are used in some implementations.
            // http://www.sans.org/reading_room/whitepapers/auditing/base64-pwned_33759
            // base64-pwned_33759.pdf
            data = data.Replace('-', '/');
            data = data.Replace('_', '/');
            data = data.Replace('.', '/');
            data = data.Replace(':', '/');
            data = data.Replace('!', '/');
            byte[] todecode_byte = Convert.FromBase64String(data);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            result = new String(decoded_char);
        }
        catch (Exception ex)
        {
            Response.Write("Error in Sign on:" + ex.Message);
        }
        return result;
    }

    public bool IsNumber(String strNumber)
    {
        if (Regex.IsMatch(strNumber, @"^\d+$"))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public bool sendEmail(string subject, int code, string userId)
    {
        OracleConnection CMS_CON = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        OracleCommand command = null;
        OracleDataReader rd = null;
        bool ret = false;
        string body = "";
        string to = "";
        string Name = "";
        try
        {

            CMS_CON.Open();
            command = new OracleCommand();
            command.Connection = CMS_CON;
            // End of connection Objects

            //get email address of the user from the database and just send email to admin department
            //string from = HttpContext.Current.Session["VMS_USER"].ToString();
            string from = "it@ffc.com.pk";

            command.CommandText = "select NAME, EMAIL, location_id from mr_users where lower(user_id)=lower('" + userId + "')";
            rd = command.ExecuteReader();
            string gender = "", mob = "", location = "", alternate_no = "";
            rd.Read();
            if (rd.HasRows)
            {

                to = rd["email"].ToString();
                Name = rd["NAME"].ToString();
            }
            else
            {
                //to="m_adil@ffc.com.pk";
            }

            rd.Close();
            mob = mob.Trim().Replace("-", "");


            MailMessage objEmail = new MailMessage();
            //objEmail.Bcc = from;
            string toEmail = "shahid_khan@ffc.com.pk";

            //objEmail.Bcc.Add(toEmail);
            //toEmail = to;

            objEmail.To.Add(toEmail);

            objEmail.From = new MailAddress(from, "IS Division, FFC");
            //objEmail.ReplyToList.Add("badar_mehboob@ffc.com.pk");
            objEmail.Subject = subject;// "Test ";// +DropDownList_Catg.SelectedValue;

            body = "<br>Yuor Login Code is: <b>" + code + "</b>. Please use this code to complete login to FC Medical Portal. <br><br>";
            body = body +
              "<br>--------------------------------------------------<br>" +
          "This is a system generated email.Please do not reply.";
            String EmailBody = body;// +"---actually will be sent to--" + to;// "AD";// TextBox_User_Comments.Text.Trim();


            gender = "<br>Dear " + Name + ",<br><br>"; ;
            objEmail.Body = gender + EmailBody;
            objEmail.IsBodyHtml = true;



            objEmail.Priority = MailPriority.High;

            SmtpClient smtp = new SmtpClient("ghms.ffc.com.pk");

            //to change the port (default is 25), we set the port property
            smtp.Port = 26;


            //SmtpMail.SmtpServer = "smtp.ffc.com.pk";


            try
            {
                //SmtpMail.Send(objEmail);

                smtp.Send(objEmail);

                //ClientScript.RegisterStartupScript(this.GetType(), "Subject:" + subject + "", "alert('" + to + "body::::" + body + "----');", true);

            }
            catch (Exception exc)
            {
                ret = false;
            }

        }
        catch (Exception ex)
        {
            CMS_CON.Close();
            CMS_CON.Dispose();
            rd.Close();
            ret = false;
            //Label_Status.Visible = true;
            //Label_Status.Text = ex.Message;

        }
        finally
        {
            CMS_CON.Close();
            CMS_CON.Dispose();
            rd.Close();
        }
        return ret;
    }

}