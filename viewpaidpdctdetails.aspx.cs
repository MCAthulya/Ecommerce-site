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
    public partial class viewpaidpdctdetails : System.Web.UI.Page
    {
        ConnectionClass obj = new ConnectionClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = "SELECT Product.Pro_id, Product.Pro_name,Bill.Reg_id, Bill.Date,dbo.Orderr.Order_id, Orderr.Quantity FROM Product INNER JOIN  Orderr ON Product.Pro_id = Orderr.Pro_id INNER JOIN Bill ON Orderr.Reg_id = Bill.Reg_id where Payment_Status = 'Paid'    GROUP BY Product.Pro_id, Product.Pro_name, Bill.Reg_id,Bill.Date,Orderr.Order_id, Orderr.Quantity ";
            DataTable dt = obj.Fn_Datatable(str);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string s1 = "update Bill set Payment_Status='Deliver' where Payment_Status='Paid'";
            int i = obj.Fn_NonQuery(s1);

            if(i==1)
            {
                Label2.Visible = true;
                Label2.Text = "successfully delivered";
            }
        }
    }
}