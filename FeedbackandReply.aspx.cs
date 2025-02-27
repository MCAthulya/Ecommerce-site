using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;
namespace prjct1
{
    public partial class FeedbackandReply : System.Web.UI.Page
    {

        ConnectionClass obj = new ConnectionClass();
        protected void Page_Load(object sender, EventArgs e)
        {


            if(!IsPostBack)
            {
                bind();
            }

            

            
        }

        public void bind()
        {
            string str = "select * from Feedback";

            DataSet ds = obj.Fn_Dataset(str);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        protected void LinkButton1_Command(object sender, CommandEventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string feedbackId = btn.CommandArgument;


            //get regid for the selected feedback
            string str = "select Reg_id from Feedback where Feedback_id=" + feedbackId + "";
            string regid = obj.Fn_Scalar(str);
            Session["regfid"] = regid;
            //get useremail for the selected regid
            string str1 = "select Username, Useremail from UserReg where UserId=" + regid + "";
            SqlDataReader dr = obj.Fn_Reader(str1);
            while (dr.Read())
            {
                txtSenderEmail.Text = dr["Useremail"].ToString();
                Label6.Text = dr["Username"].ToString();
            }
            Panel1.Visible = true;

            Label7.Text = txtSubject.Text;
            Label8.Text = txtMessageBody.Text;
            txtRecipientEmail.Text = "athiradrive@gmail.com";

        }



        
        protected void btnSend_Click(object sender, EventArgs e)
        {
           
            SendEmail2("Admin","athiradrive@gmail.com", "rqgo baqj nlhl mpze", Label6.Text, txtSenderEmail.Text,Label7.Text,Label8.Text);

            string sel = "update Feedback set Reply='" + txtMessageBody.Text + "' where Reg_id='" + Session["regfid"] + "'";
            int s = obj.Fn_NonQuery(sel);
        }

        public static void SendEmail2(string yourName, string yourGmailUserName, string yourGmailPassword, string toName, string toEmail, string subject, string body)

        {
            string to = toEmail; //To address    
            string from = yourGmailUserName; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = body;
            message.Subject = subject;
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential(yourGmailUserName, yourGmailPassword);
            client.EnableSsl = true;
            client.UseDefaultCredentials = true;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }




    }




}