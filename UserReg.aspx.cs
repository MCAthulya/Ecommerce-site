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
    public partial class Login : System.Web.UI.Page
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
                Session["userid"] = regid;
            }
            string p = "~/Photos/" + FileUpload1.FileName;
            FileUpload1.SaveAs(MapPath(p));
            string ins = "insert into UserReg values(" + regid + ",'" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + DropDownList1.SelectedItem.Text + "','" + TextBox6.Text + "','" + p + "','User')";
            int i = obj.Fn_NonQuery(ins);
            if (i == 1)
            {
                string ins2 = "insert into logintab values(" + regid + ",'" + TextBox7.Text + "','" + TextBox8.Text + "','User')";
                int j = obj.Fn_NonQuery(ins2);
                Label2.Visible = true;
                Label2.Text = "Successfully Registered";
            }
        }
    }
}