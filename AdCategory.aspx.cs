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
    public partial class AdCategory : System.Web.UI.Page
    {
        ConnectionClass obj = new ConnectionClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind_Grid();
            }
        }
        public void Bind_Grid()
        {
            string str = "select * from Category";
            DataSet ds = obj.Fn_Dataset(str);
            GridView1.DataSource = ds;
            GridView1.DataBind();


        }




        protected void Button1_Click(object sender, EventArgs e)
        {
            string p = "~/Photocat/" + FileUpload1.FileName;
            FileUpload1.SaveAs(MapPath(p));

            string s = "insert into Category values('" + TextBox1.Text + "','" + p + "','" + convertquotes(TextBox2.Text) + "','Available')";
            int i = obj.Fn_NonQuery(s);
            if (i == 1)
            {
                Label4.Visible = true;
                Label4.Text = "successfully Inserted";
                Bind_Grid();
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int i = e.RowIndex;
            int getid = Convert.ToInt32(GridView1.DataKeys[i].Value);
            string s = "Delete from Category where  Cat_id=" + getid + "";
            int j = obj.Fn_NonQuery(s);
            Bind_Grid();

            Label5.Visible = true;
            Label5.Text = "successfully Deleted";
        }

        public string convertquotes(string str)
        {
            return str.Replace("'", "''");
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int selectedRowIndex = e.NewSelectedIndex;
            string selectedCategoryId = GridView1.DataKeys[selectedRowIndex].Value.ToString();
            Session["CategoryId"] = selectedCategoryId;
        

        string str = "select * from Category WHERE Cat_id ='" + Session["CategoryId"] + "'";
        SqlDataReader dr = obj.Fn_Reader(str);
                while (dr.Read())
                {
                   


                    Image1.ImageUrl = dr["Cat_image"].ToString();
        TextBox3.Text = dr["Cat_description"].ToString();
        TextBox4.Text = dr["Cat_status"].ToString();
        Panel1.Visible = true;

                }


}

protected void Button2_Click(object sender, EventArgs e)
        {
            

            string p1 = "~/Photocat/" + FileUpload2.FileName;
            FileUpload2.SaveAs(MapPath(p1));

            string str1 = "update Category set Cat_image='" + p1 + "',Cat_description='" + TextBox3.Text + "',Cat_status='" + TextBox4.Text + "' WHERE Cat_id ='" + Session["CategoryId"] + "'";
            int i1 = obj.Fn_NonQuery(str1);
            if(i1==1)
            {
                Label7.Visible = true;
                Label7.Text = "updated successfully";
            }
            Bind_Grid();
        }

      
    }
}