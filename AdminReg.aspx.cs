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
    public partial class AdminReg : System.Web.UI.Page

    {
        ConnectionClass obj = new ConnectionClass();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string s = "select max(Reg_id) from logintab";
            string maxregid = obj.Fn_Scalar(s);
            int regid = 0;
            if (maxregid == "")
            {
                regid = 1;
            }
            else
            {
                int newregid = Convert.ToInt32(maxregid);
                regid = newregid + 1;
            }
            string ins = "insert into AdminReg values(" + regid + ",'" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "')";
            int i = obj.Fn_NonQuery(ins);
            if(i==1)
            {
                string ins_log = "insert into logintab values('" + regid + "','" + TextBox6.Text + "','" + TextBox7.Text + "','Admin')";
                int j = obj.Fn_NonQuery(ins_log);
                if(j==1)
                {
                    Label2.Visible = true;
                    Label2.Text = "Registration Successful";
                }
            }
        }
    }
}