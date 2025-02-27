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
    public partial class viewpdct : System.Web.UI.Page
    {
        ConnectionClass obj = new ConnectionClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string str = "select Photo,Pro_name,Price,Pro_description,pro_qty from Product where Pro_id='" + Session["pdtid"] + "' and Pro_status='Available'";
                SqlDataReader dr = obj.Fn_Reader(str);
                while(dr.Read())
                {
                    Image1.ImageUrl = dr["Photo"].ToString();
                    Label1.Text = dr["Pro_name"].ToString();
                    Label2.Text = dr["Price"].ToString();
                    Label3.Text = dr["Pro_description"].ToString();
                   /* TextBox1.Text = dr["pro_qty"].ToString()*/;
                }


            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string s = "select max(Cart_id) from Cart";
            string maxcartid = obj.Fn_Scalar(s);
            int cartid = 0;
            if (maxcartid == "")
            {
                cartid = 1;
            }
            else
            {
                int newcartid = Convert.ToInt32(maxcartid);
                cartid = newcartid + 1;
               // Session["cartid"] = cartid;

            }
            string s1 = "select Price from Product where Pro_id='" + Session["pdtid"] + "'";
            string p = obj.Fn_Scalar(s1);
            int price = Convert.ToInt32(p);

            int qty = Convert.ToInt32(TextBox1.Text);
            int subtotal = qty * price;



            string str = "insert into Cart values(" + cartid + "," + Session["uid"] + "," + Session["pdtid"] + ",'" + TextBox1.Text + "'," + subtotal + ")";
            int str1 = obj.Fn_NonQuery(str);

            Response.Redirect("ViewCart.aspx");



        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserHome.aspx");
        }
    }
}