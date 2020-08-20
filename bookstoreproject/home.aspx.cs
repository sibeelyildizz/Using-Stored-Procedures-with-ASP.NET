using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace bookstoreproject
{
    public partial class home : System.Web.UI.Page
    {
        SqlConnection Cnn;
        SqlCommand Cmd;
        SqlDataAdapter Da;
        DataSet Ds;


        private void filterByType()
        {
            Cnn = new SqlConnection("Data Source=DESKTOP-LR30959\\SQLEXPRESS; Initial Catalog=kvt; trusted_connection=yes");
            Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;//Command Tipi 

            Cmd.CommandText = "tursayisi";//SP Adı 


            Cmd.Connection = Cnn;
            if (Cnn.State == ConnectionState.Closed) Cnn.Open();
       
            SqlDataReader dr = Cmd.ExecuteReader();
            while (dr.Read())
            {

                Chart1.Series["kitaplar"].Points.AddXY(dr[0].ToString(), dr[1].ToString());
            }
        }
      
        private void getBookNumber()
        {
            Cnn = new SqlConnection("Data Source=DESKTOP-LR30959\\SQLEXPRESS; Initial Catalog=kvt; trusted_connection=yes");
            Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;//Command Tipi 

            Cmd.CommandText = "kitapsayisi";//SP Adı 
 

            Cmd.Connection = Cnn;//Commandin kullanacagı Connection 
            if (Cnn.State == ConnectionState.Closed) Cnn.Open();
            
            SqlDataReader dr = Cmd.ExecuteReader();
            while (dr.Read())
            {

                Chart1.Series["kitaplar"].Points.AddXY(dr[0].ToString(), dr[1].ToString());
            }


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Cnn = new SqlConnection("Data Source=DESKTOP-LR30959\\SQLEXPRESS; Initial Catalog=kvt; trusted_connection=yes"); 
            Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;//Command Tipi 

            Cmd.CommandText = "selectkitaplar";//SP Adı 


            Cmd.Connection = Cnn;//Commandin kullanacagı Connection 
            if (Cnn.State == ConnectionState.Closed) Cnn.Open();
            Da = new SqlDataAdapter(Cmd);
            Ds = new DataSet();

            Da.Fill(Ds);
            GridView1.DataSource = Ds.Tables[0];
            GridView1.DataBind();
            deneme();
            
                
        }
        private void deneme()
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (DropDownList1.Text == "Türlerin Stok Durumunu Göster ")
                filterByType();
            if (DropDownList1.Text == "Kitapların Stok Durumunu Göster")
                getBookNumber();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
           SqlConnection Cnn = new SqlConnection("Data Source=DESKTOP-LR30959\\SQLEXPRESS; Initial Catalog=kvt; trusted_connection=yes");
           
           SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = Cnn;
            
            Cmd.CommandType = CommandType.StoredProcedure;//Command Tipi 

            Cmd.CommandText = "kitapekle";//SP Adı 

            
            Cmd.Parameters.AddWithValue("kitapAdi", TextBox1.Text);// Dısarıdan Store Procedure parametre ekliyoruz.. 
            Cmd.Parameters.AddWithValue("yazar", TextBox2.Text);
            Cmd.Parameters.AddWithValue("yayinevi",TextBox3.Text);
            Cmd.Parameters.AddWithValue("turID", DropDownList2.Text);
            Cmd.Parameters.AddWithValue("fiyat", TextBox5.Text);
            if (Cnn.State == ConnectionState.Closed) Cnn.Open();
            try
            {
                int k = Cmd.ExecuteNonQuery();
                if (k != 0)
                {
                   
                }
            }
            catch (Exception)
            {

                Response.Write("Beklenmedik bir hata oluştu.");
            }
                 
           
            Cnn.Close();


        }


    }
}