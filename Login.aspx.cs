using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace prjct1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        ConnectionClass obj = new ConnectionClass();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            string s = "select count(Reg_id) from logintab where Username='" + TextBox1.Text + "' and Password='" + TextBox2.Text + "'";
            string i = obj.Fn_Scalar(s);
            int i1 = Convert.ToInt32(i);
            if (i1 == 1)
            {
                string s1 = "select Reg_id from logintab where Username = '" + TextBox1.Text + "' and Password = '" + TextBox2.Text + "'";
                string regid = obj.Fn_Scalar(s1);
                Session["uid"] = regid;

                string s3 = "select User_type from logintab  where Username = '" + TextBox1.Text + "' and Password = '" + TextBox2.Text + "'";
                string logtype = obj.Fn_Scalar(s3);
                if (logtype == "Admin")
                {
                    
                    Response.Redirect("AdminHome.aspx");
                    
                }
                else if (logtype == "User")
                {
                    Response.Redirect("UserHome.aspx");
                }
                
                
            }
        }
    }
}