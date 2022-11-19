using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace db_datagridview
{

    public partial class Form1 : Form
    {
        
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;
        void griddoldur()//FILL DATAGRIDVIEW 
        {
            con = new SqlConnection("Data Source=DESKTOP-228OGM4;Initial Catalog=test;Integrated Security=True");
            da = new SqlDataAdapter("Select *From Rehber", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "Rehber");
            
            dataGridView1.DataSource = ds.Tables["Rehber"];
            con.Close();
        }
        private void textBox5_TextChanged(object sender, EventArgs e)//RESULT WHILE TYPING 
        {
            if (txtAra.Text == "")
            {
                griddoldur();
            }
            else
            {
                this.dataGridView1.DataSource = null;
                this.dataGridView1.Rows.Clear();
                con = new SqlConnection("Data Source=DESKTOP-228OGM4;Initial Catalog=test;Integrated Security=True");
                string txtstr = txtAra.Text;
                da = new SqlDataAdapter("SELECT * FROM Rehber WHERE ad LIKE '"+txtstr+"%'",con);
                ds = new DataSet();
                con.Open();
                da.Fill(ds, "Rehber");
                dataGridView1.DataSource = ds.Tables["Rehber"];
                con.Close();
            }

        }
        void txtTemizle()//CLEAN TEXTBOXS
        {
            txtAd.Clear();
            txtSoyad.Clear();
            txtTelNo.Clear();
            txtNo.Clear();
        }

        private void button1_Click(object sender, EventArgs e) // CREATE
        {
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO Rehber(ad,soyad,telno) values ('" + txtAd.Text + "','" + txtSoyad.Text + "','" + txtTelNo.Text + "')";
            cmd.ExecuteNonQuery(); 
            con.Close();
            griddoldur();
        }
        private void button3_Click(object sender, EventArgs e)//UPDATE
        {

            

                cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "update Rehber set ad='" + txtAd.Text + "',soyad='" + txtSoyad.Text + "',telno='" + txtTelNo.Text + "' where id=" + txtNo.Text + "";
                cmd.ExecuteNonQuery();
                con.Close();
                griddoldur();
            
            
        }
        private void button2_Click(object sender, EventArgs e)//DELETE
        {
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "delete from Rehber where id=" + txtNo.Text + "";
            cmd.ExecuteNonQuery();
            con.Close();
            txtTemizle();
            griddoldur();
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ClearSelection();
            griddoldur();
        }

        

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtTelNo.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        
    }
    
}

