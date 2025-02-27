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
    public partial class ViewCart : System.Web.UI.Page
    {
        ConnectionClass obj = new ConnectionClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grid_bind();
            }
        }

        public void grid_bind()
        {
            string str = "SELECT Cart.Cart_id,Product.Pro_name,Cart.Quantity,Cart.Subtotal FROM Product JOIN Cart ON Product.Pro_id = Cart.Pro_id where Reg_id='"+Session["uid"]+"'";
            DataSet ds = obj.Fn_Dataset(str);

            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            grid_bind();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int i=e.RowIndex;
            int getid = Convert.ToInt32(GridView1.DataKeys[i].Value);
            string str = "delete from Cart where Cart_id=" + getid + "";
            int j = obj.Fn_NonQuery(str);
            grid_bind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int i = e.RowIndex;
            int getid = Convert.ToInt32(GridView1.DataKeys[i].Value);
            TextBox txtquantity = (TextBox)GridView1.Rows[i].Cells[1].Controls[0];

            string sel_price = "select Price from Product where Pro_id='" + Session["pdtid"] + "'";
            string pr = obj.Fn_Scalar(sel_price);
            int val_price = Convert.ToInt32(pr);
            int quant = Convert.ToInt32(txtquantity.Text);
            int sub_total = val_price * quant;

            string strupd = "update Cart set Quantity=" + txtquantity.Text + ",Subtotal=" + sub_total + "where Cart_id=" + getid + "";
            int upd = obj.Fn_NonQuery(strupd);
            GridView1.EditIndex = -1;
            grid_bind();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            grid_bind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sel = "select max(Cart_id) from Cart";
            string sel_id = obj.Fn_Scalar(sel);
            int id = Convert.ToInt32(sel_id);
            for (int k = 1; k <= id; k++)
            {


                string p_id = "select Pro_id from Cart where Cart_id=" + k + " ";
                string p = obj.Fn_Scalar(p_id);
                string qua = "select Quantity from Cart where Cart_id=" + k + " ";
                string qu_a = obj.Fn_Scalar(qua);

                string tot = "select Subtotal from Cart where Cart_id=" + k + " ";
                string tot_s = obj.Fn_Scalar(tot);


               


                string c_sel = "select * from Cart where Cart_id=" + k + " and Reg_id=" + Session["uid"] + " ";

                SqlDataReader dr = obj.Fn_Reader(c_sel);

                while (dr.Read())
                {
                    p_id = dr["Pro_id"].ToString();
                    qua = dr["Quantity"].ToString();
                    tot = dr["Subtotal"].ToString();
                }
                string ins_or = "insert into Orderr values(" + p_id + "," + Session["uid"] + "," + qua + ",'" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "'," + tot + ",'Order')";
                int ins_i = obj.Fn_NonQuery(ins_or);
                string del = "delete from Cart where Cart_id=" + k + " ";
                int del_i = obj.Fn_NonQuery(del);

            }
            string gr_tot = "select sum(Total_Price) from Orderr where Reg_id=" + Session["uid"] + " and Order_status='Order' ";
            string grtot_i = obj.Fn_Scalar(gr_tot);
            int grand_tot = Convert.ToInt32(grtot_i);

            string ins_bill = "insert into Bill values(" + Session["uid"] + "," + grand_tot + ",'" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "','unpaid')";
            int bill_i = obj.Fn_NonQuery(ins_bill);
            Response.Redirect("ViewBill.aspx");

        }
    }
 }
    