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
using System.Configuration;

namespace Lab2
{
    //seasons is the parent table
    //episodes is the child table
    public partial class Form1 : Form
    {
        private String parentName = ConfigurationManager.AppSettings["parentname"];
        private String childName = ConfigurationManager.AppSettings["childname"];
        private String selectParent = ConfigurationManager.AppSettings["selectparent"];
        private String selectChild = ConfigurationManager.AppSettings["selectchild"];
        private String foreignKeyName = ConfigurationManager.AppSettings["foreignkeyname"];
        private String foreignKeyCol = ConfigurationManager.AppSettings["foreignkeycol"];

        private SqlDataAdapter daParent, daChild;
        private BindingSource bsParent, bsChild;
        private SqlConnection dbConn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        private DataSet ds;
        private SqlCommandBuilder builder;

        public Form1()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            daChild.Update(ds.Tables[childName]);
            daParent.Update(ds.Tables[parentName]);
            getData();
            this.view.DataSource = bsParent;
            this.viewChildren.DataSource = bsChild;
        }

        private void getData()
        {
            ds = new DataSet();

            daParent = new SqlDataAdapter(selectParent, dbConn);
            daChild = new SqlDataAdapter(selectChild, dbConn);
            daParent.Fill(ds, parentName);
            daChild.Fill(ds, childName);

            builder = new SqlCommandBuilder(daChild);

            DataRelation dr = new DataRelation(foreignKeyName, ds.Tables[parentName].Columns[foreignKeyCol], ds.Tables[childName].Columns[foreignKeyCol]);
            ds.Relations.Add(dr);

            bsParent = new BindingSource(ds, parentName);
            bsChild = new BindingSource(bsParent, foreignKeyName);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            getData();
            this.view.DataSource = bsParent;
            this.viewChildren.DataSource = bsChild;
        }
    }
}

