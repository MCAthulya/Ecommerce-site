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
    public partial class UserHome : System.Web.UI.Page
    {
        ConnectionClass obj = new ConnectionClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind_grid();
            }

        }
        public void Bind_grid()
        {
            string str = "select * from Category";
            DataTable dt= obj.Fn_Datatable(str);

            DataList1.DataSource = dt;
            DataList1.DataBind();

        }

        

        protected void ImageButton1_Command(object sender, CommandEventArgs e)
        {
           
            int cid = Convert.ToInt32(e.CommandArgument);
            Session["catid"] = cid;
            Response.Redirect("viewallpdct.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string str = "select * from Category where Cat_name like '%" + TextBox1.Text + "%'";
            string s = obj.Fn_Scalar(str);

        }
    }
}