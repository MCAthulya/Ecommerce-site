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
    public partial class viewallpdct : System.Web.UI.Page
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
            string str = "select * from Product where Cat_id='" +Session["catid"]+ "'and Pro_status='Available'";
            DataTable dt = obj.Fn_Datatable(str);

            DataList1.DataSource = dt;
            DataList1.DataBind();

        }

        

        protected void ImageButton2_Command(object sender, CommandEventArgs e)
        {
           
            int pid = Convert.ToInt32(e.CommandArgument);
            Session["pdtid"] = pid;
            Response.Redirect("viewpdct.aspx");
        }
    }
}