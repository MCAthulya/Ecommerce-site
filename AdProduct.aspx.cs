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
    public partial class AdProduct : System.Web.UI.Page
    {
        ConnectionClass obj = new ConnectionClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string str = "select Cat_id,Cat_name from Category";
                DataSet ds = obj.Fn_Dataset(str);
                DropDownList1.DataSource = ds;
                DropDownList1.DataTextField = "Cat_name";
                DropDownList1.DataValueField = "Cat_id";
                DropDownList1.DataBind();
               
                Bind_Grid();
                

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string p = "~/Photopdct/" + FileUpload1.FileName;
            FileUpload1.SaveAs(MapPath(p));
            string ins = "insert into Product values('" + DropDownList1.SelectedItem.Value + "','" + TextBox1.Text + "'," + TextBox2.Text + "," + TextBox3.Text + ",'" + p + "','" + convertquotes (TextBox4.Text)+ "','Available')";
            int i = obj.Fn_NonQuery(ins);
            Bind_Grid();
            Label7.Visible = true;
            Label7.Text = "Successfully inserted";

        }
        public void Bind_Grid()
        {
            string str = "select Category.Cat_name,Product.Pro_id,Product.Pro_name,Product.pro_qty,Product.Price,Product.Photo,Product.Pro_description,Product.Pro_status FROM Category INNER JOIN Product ON Category.Cat_id = Product.Cat_id";
            DataTable dt = obj.Fn_Datatable(str);
            GridView1.DataSource = dt;
            GridView1.DataBind();


        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int i = e.RowIndex;
            int getid = Convert.ToInt32(GridView1.DataKeys[i].Value);
            string s = "Delete from Product where  Pro_id=" + getid + "";
            int j = obj.Fn_NonQuery(s);
            Bind_Grid();

            Label8.Visible = true;
            Label8.Text = "successfully Deleted";
        }
        public string convertquotes(string str)
        {
            return str.Replace("'", "''");
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int selectedRowIndex = e.NewSelectedIndex;
            string selectedProductId = GridView1.DataKeys[selectedRowIndex].Value.ToString();
            Session["ProductId "] = selectedProductId;


            string str = "select * from Product WHERE Pro_id ='" + Session["ProductId "] + "'";
            SqlDataReader dr = obj.Fn_Reader(str);
            while (dr.Read())
            {



                Image1.ImageUrl = dr["Photo"].ToString();
                TextBox5.Text = dr["Pro_description"].ToString();
                TextBox6.Text = dr["Price"].ToString();
                TextBox7.Text = dr["pro_qty"].ToString();
                TextBox8.Text = dr["Pro_status"].ToString();
                Panel1.Visible = true;

            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string p1 = "~/Photopdct/" + FileUpload2.FileName;
            FileUpload2.SaveAs(MapPath(p1));

            string str1 = "update Product set Photo='" + p1 + "',Pro_description='" + TextBox5.Text + "',Price='"+TextBox6.Text+"',pro_qty='"+TextBox7.Text+"',Pro_status='" + TextBox8.Text + "' WHERE Pro_id ='" + Session["ProductId "] + "'";
            int i1 = obj.Fn_NonQuery(str1);
            if (i1 == 1)
            {
                Label9.Visible = true;
                Label9.Text = "updated successfully";
            }
            Bind_Grid();
        }
    }
}