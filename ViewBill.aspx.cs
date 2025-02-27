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
    public partial class ViewBill : System.Web.UI.Page
    {
        ConnectionClass obj = new ConnectionClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string str = "select Username,Useremail,UserPhone,Address,Location,State,Pincode from UserReg where UserId='" + Session["uid"] + "'";
                SqlDataReader dr = obj.Fn_Reader(str);
                while (dr.Read())
                {
                    Label6.Text = dr["Username"].ToString();
                    Label7.Text = dr["Useremail"].ToString();
                    Label8.Text = dr["UserPhone"].ToString();
                    Label9.Text = dr["Address"].ToString() + " , " + dr["Location"].ToString() + " , " + dr["State"].ToString() + " , " + dr["Pincode"].ToString();
                }
                gridbind();
                string grand_tot = "select sum(Total_Price) from Orderr where Reg_id='" + Session["uid"] + "' and Order_status='Order'";
                string gr = obj.Fn_Scalar(grand_tot);
                Label12.Text = gr;

                string str1 = "select count(Accnt_no) from Account where Reg_id=" + Session["uid"] + " ";
                string s = obj.Fn_Scalar(str1);

                if (s == "1")
                {

                    string str2 = "select Accnt_no,Balance_amount from Account where Reg_id=" + Session["uid"] + "";
                    SqlDataReader dr3 = obj.Fn_Reader(str2);
                    while (dr3.Read())
                    {

                        TextBox2.Text = dr3["Accnt_no"].ToString();
                        TextBox3.Text = dr3["Balance_amount"].ToString();

                    }



                }




            }

        }
        public void gridbind()
        {

            string sel = "SELECT Product.Pro_name, Orderr.Quantity, Orderr.Total_Price FROM Orderr INNER JOIN Product ON Orderr.Pro_id = Product.Pro_id where Reg_id='" + Session["uid"] + "'";
            DataTable dt = obj.Fn_Datatable(sel);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            Label21.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Label21.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            string date = Label21.Text;
            string acno = TextBox2.Text;
            int ac = Convert.ToInt32(acno);
            int bal = Convert.ToInt32(TextBox3.Text);

            string max_acid = "select max(Accnt_no) from Account where Reg_id='" + Session["uid"] + "' ";
            string m_acid = obj.Fn_Scalar(max_acid);
            int m_acno = Convert.ToInt32(m_acid);
            if (m_acno == 0)
            {
                string ins = "insert into Account values('" + ac + "','" + Session["uid"] + "','" + DropDownList1.SelectedItem.Text + "','" + bal + "','" + date + "')";
                int ins_acc = obj.Fn_NonQuery(ins);
            }
            else
            {
                string up_bal = "update Account set Balance_amount='" + bal + "' where Accnt_no='" + ac + "' and Reg_id='" + Session["uid"] + "'";
                int upd_bal = obj.Fn_NonQuery(up_bal);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string total = "select Grand_price from Bill where Reg_id=" + Session["uid"] + "  and Payment_Status='unpaid'";
            string grand = obj.Fn_Scalar(total);
            int g_total = Convert.ToInt32(grand);

            BalanceReference1.ServiceClient ob = new BalanceReference1.ServiceClient();
            int a = Convert.ToInt32(TextBox2.Text);
            string bal = ob.GetBalance(a);
            int bal_amt = Convert.ToInt32(bal);


            if (bal_amt > g_total)
            {
                int new_bal = (bal_amt - g_total);
                int ac = Convert.ToInt32(TextBox2.Text);
                string upb = "update Account set Balance_amount=" + new_bal + " where Reg_id=" + Session["uid"] + " and Accnt_no=" + a + " ";
                int up = obj.Fn_NonQuery(upb);
              
                string sel1 = "select Order_id from Orderr where Order_status='Order' and  Reg_id=" + Session["uid"] + " ";
                SqlDataReader dr1 = obj.Fn_Reader(sel1);
                List<int> oid = new List<int>();
                while (dr1.Read())
                {
                    oid.Add(Convert.ToInt32(dr1["Order_id"].ToString()));
                }
               
                //stock updation
                string sel2 = "select Pro_id,Quantity from Orderr where Order_status='Order' and Reg_id="+ Session["uid"] + "" ;
                SqlDataReader dr2 = obj.Fn_Reader(sel2);
                List<int> pid1 = new List<int>();
                List<int> Qty1 = new List<int>();
                while (dr2.Read())
                {
                    pid1.Add(Convert.ToInt32(dr2["Pro_id"].ToString()));
                    Qty1.Add(Convert.ToInt32(dr2["Quantity"].ToString()));
                }

                for (int index = 0; index < Qty1.Count; index++)
                {

                    int p = pid1[index];
                    int q = Qty1[index];

                    string sel3 = "select Product.Pro_qty from Product inner join Orderr on Product.Pro_id=Orderr.Pro_id where Product.Pro_id='" + p + "' and Orderr.Order_status='Order'";
                    int stock = Convert.ToInt32(obj.Fn_Scalar(sel3));


                    int upstock = stock - q;

                    string upstk = "update Product set pro_qty=" + upstock + " where Pro_id=" + p + "";
                    int s = obj.Fn_NonQuery(upstk);




                }
                foreach (int n in oid)
                {
                    string update = "update Orderr set Order_status='Paid' where Reg_id=" + Session["uid"] + " and Order_id=" + n + " and Order_status='Order'";
                    int b = obj.Fn_NonQuery(update);
                }
                Label22.Text = "Successfully paid";
                Label22.Visible = true;
            }
        }
        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

            int ac = Convert.ToInt32(TextBox2.Text);
            string str = "select count(Accnt_no) from Account where Accnt_no=" + ac + " and Reg_id=" + Session["uid"] + " ";
            string i = obj.Fn_Scalar(str);
            int h = Convert.ToInt32(i);
            if (h > 0)
            {
                Label23.Visible = true;
                Label23.Text = "Account details already entered";

            }
        }
    }
}