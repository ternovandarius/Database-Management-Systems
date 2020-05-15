using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Lab1
{
    //seasons is the parent table
    //episodes is the child table
    public partial class Form1 : Form
    {
        private BindingSource bsParent, bsChild;
        private String connString = "Server=DESKTOP-944BR9T\\SQLEXPRESS;Database=Ternovan_Lab;Trusted_Connection=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void getData()
        {
            SqlConnection dbConn = new SqlConnection(connString);
            DataSet ds = new DataSet();

            SqlDataAdapter daParent = new SqlDataAdapter("SELECT * FROM Seasons", dbConn);
            SqlDataAdapter daChild = new SqlDataAdapter("select * from Episodes", dbConn);
            daParent.Fill(ds, "Seasons");
            daChild.Fill(ds, "Episodes");
            DataRelation dr = new DataRelation("SeasonsEpisodes", ds.Tables["Seasons"].Columns["SeasonID"], ds.Tables["Episodes"].Columns["SeasonID"]);
            ds.Relations.Add(dr);
            bsParent = new BindingSource(ds, "Seasons");
            bsChild = new BindingSource(bsParent, "SeasonsEpisodes");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = int.Parse(viewChildren.SelectedCells[0].Value.ToString());
            SqlConnection dbConn = new SqlConnection(connString);
            try
            {
                dbConn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Episodes WHERE EpisodeID=@Id ", dbConn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
                getData();
                this.view.DataSource = bsParent;
                this.viewChildren.DataSource = bsChild;
            }
            catch (SqlException s)
            {
                Console.WriteLine(s.Message);
            }
            finally
            {
                dbConn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox1.Text);
            int nr = int.Parse(textBox2.Text);
            String title = textBox3.Text;
            String date = textBox4.Text;
            int seasonID = int.Parse(view.SelectedCells[0].Value.ToString());

            SqlConnection dbConn = new SqlConnection(connString);
            try
            {
                dbConn.Open();
                SqlCommand cmd = new SqlCommand("set identity_insert Episodes ON", dbConn);
                cmd.ExecuteNonQuery();
                SqlCommand comm = new SqlCommand("insert into Episodes (EpisodeID, EpisodeNr, EpisodeTitle, DateAired, SeasonID) values (@Id, @Nr, @Title, @Date, @SID)", dbConn);
                comm.Parameters.AddWithValue("@Id", id);
                comm.Parameters.AddWithValue("@Nr", nr);
                comm.Parameters.AddWithValue("@Title", title);
                comm.Parameters.AddWithValue("@Date", DateTime.Parse(date)); //date needs a YYYY-MM-DD  or DD-MM-YYYY format
                comm.Parameters.AddWithValue("@SID", seasonID);
                comm.ExecuteNonQuery();
                getData();
                this.view.DataSource = bsParent;
                this.viewChildren.DataSource = bsChild;
            }
            catch (SqlException s)
            {
                Console.WriteLine(s.Message);
            }
            finally
            {
                dbConn.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = int.Parse(viewChildren.SelectedCells[0].Value.ToString());
            try
            {
                int nr = int.Parse(textBox2.Text);
                String title = textBox3.Text;
                String date = textBox4.Text;
                int seasonID = int.Parse(view.SelectedCells[0].Value.ToString());
                SqlConnection dbConn = new SqlConnection(connString);
                try
                {
                    dbConn.Open();
                    SqlCommand comm = new SqlCommand("update Episodes set EpisodeNr=@Nr, EpisodeTitle=@Title, DateAired=@Date, SeasonID=@SID where EpisodeID = @Id", dbConn);
                    comm.Parameters.AddWithValue("@Id", id);
                    comm.Parameters.AddWithValue("@Nr", nr);
                    comm.Parameters.AddWithValue("@Title", title);
                    comm.Parameters.AddWithValue("@Date", DateTime.Parse(date));    //date needs a YYYY-MM-DD  or DD-MM-YYYY format
                    comm.Parameters.AddWithValue("@SID", seasonID);
                    comm.ExecuteNonQuery();
                    getData();
                    this.view.DataSource = bsParent;
                    this.viewChildren.DataSource = bsChild;
                }
                catch (SqlException s)
                {
                    Console.WriteLine(s.Message);
                }
                finally
                {
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        private void Form_Load(object sender, EventArgs e)
        {
            getData();
            this.view.DataSource = bsParent;
            this.viewChildren.DataSource = bsChild;
        }
    }
}

