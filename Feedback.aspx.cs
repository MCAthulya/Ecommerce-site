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
    public partial class Feedback : System.Web.UI.Page
    {

        ConnectionClass obj = new ConnectionClass();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            string str = "insert into Feedback values(" + Session["uid"] + ",'" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "','" + TextBox1.Text + "','','Active')";
            int i = obj.Fn_NonQuery(str);
            if(i==1)
            {
                Label2.Visible = true;
                Label2.Text = "Send Successfiily";
            }

            string str1 = "select Reply from Feedback where Reg_id=" + Session["uid"] + "";
            string k = obj.Fn_Scalar(str1);

            if(k=="1")
            {
                Label3.Visible = true;
                string str2= "select Reply from Feedback where Reg_id=" + Session["uid"] + "";
                SqlDataReader dr =obj.Fn_Reader(str2);
                while(dr.Read())
                {
                    Label4.Text = dr["Reply"].ToString();
                    Label4.Visible = true;
                   
                }



                
            }

        }
    }
}