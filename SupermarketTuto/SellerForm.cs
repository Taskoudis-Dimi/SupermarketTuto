﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using SupermarketTuto.DataAccess;

namespace SupermarketTuto
{
    public partial class SellerForm : Form
    {

        SqlConnect loaddata = new SqlConnect();


        public SellerForm()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        ////SqlConnection Con = new SqlConnection(@"Data Source=DIMITRISTASKOUD\DIMITRIS_TASKOUD;Initial Catalog=smarketdb;Integrated Security=True");
        //SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-FF268DF\SQLEXPRESS;Initial Catalog=smarketdb;Integrated Security=True");

        private void display()
        {
            //Con.Open();
            //string query = "Select * From SellerTbl;";
            //SqlDataAdapter adapter = new SqlDataAdapter(query, Con);
            //SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            //var table = new DataSet();
            //adapter.Fill(table);
            //SellDGV.DataSource = table.Tables[0];
            //Con.Close();


            loaddata.retrieveData("Select * From SellerTbl");
            SellDGV.DataSource = loaddata.table;


        }



        private void SellerForm_Load(object sender, EventArgs e)
        {
            display();
        }

        private void edit2Button_Click(object sender, EventArgs e)
        {

            try
            {
                if (SellId.Text == "" || SellName.Text == "" || SellAge.Text == "" || SellPhone.Text == "" || SellPass.Text == "")
                {

                }
                else
                {
                    //Con.Open();
                    //string query = "Update SellerTbl set SellerName='" + SellName.Text + "',SellerAge='" + SellAge.Text + "',SellerPhone='" + SellPhone.Text + "',SellerPass='" + SellPass.Text + "' where SellerId=" + SellId.Text + ";";
                    //SqlCommand cmd = new SqlCommand(query, Con);
                    //cmd.ExecuteNonQuery();
                    //MessageBox.Show("Product Successfully Updated");
                    //Con.Close();

                    loaddata.commandExc("Update SellerTbl set SellerName='" + SellName.Text + "',SellerAge='" + SellAge.Text + "',SellerPhone='" + SellPhone.Text + "',SellerPass='" + SellPass.Text + "' where SellerId=" + SellId.Text + ";");
                    MessageBox.Show("Product Successfully Updated");

                    display();
                    SellId.Text = "";
                    SellName.Text = "";
                    SellAge.Text = "";
                    SellPhone.Text = "";
                    SellPass.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void delete2Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SellId.Text == "" || SellName.Text == "" || SellAge.Text == "" || SellPhone.Text == "" || SellPass.Text == "")
                {
                    MessageBox.Show("Select The Category to Delete");
                }
                else
                {
                    //Con.Open();
                    //string query = "Delete From SellerTbl Where SellerId=" + SellId.Text + "";
                    //SqlCommand cmd = new SqlCommand(query, Con);
                    //cmd.ExecuteNonQuery();
                    //MessageBox.Show("Seller Deleted Successfully");
                    //Con.Close();

                    loaddata.commandExc("Delete From SellerTbl Where SellerId=" + SellId.Text + "");

                    MessageBox.Show("Seller Deleted Successfully");
                    SellId.Text = "";
                    SellName.Text = "";
                    SellAge.Text = "";
                    SellPhone.Text = "";
                    SellPass.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //TODO Age = Date in Textbox and db
        private void add2Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SellId.Text == "" || SellName.Text == "" || SellAge.Text == "" || SellPhone.Text == "" || SellPass.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    //Con.Open();
                    //string query = "Insert Into SellerTbl values(" + SellId.Text + ",'" + SellName.Text + "'," + SellAge.Text + "," + SellPhone.Text + ",'" + SellPass.Text + "')";
                    //SqlCommand cmd = new SqlCommand(query, Con);
                    //cmd.BeginExecuteNonQuery();
                    //MessageBox.Show("Product added successfuly");
                    //Con.Close();

                    loaddata.commandExc("Insert Into SellerTbl values(" + SellId.Text + ",'" + SellName.Text + "'," + SellAge.Text + "," + SellPhone.Text + ",'" + SellPass.Text + "')");
                    MessageBox.Show("Product added successfuly");
                    SellId.Text = "";
                    SellName.Text = "";
                    SellAge.Text = "";
                    SellPhone.Text = "";
                    SellPass.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SellDGV_CellContentClicks(object sender, DataGridViewCellEventArgs e)
        {
            SellId.Text = SellDGV.SelectedRows[0].Cells[0].Value.ToString();
            SellName.Text = SellDGV.SelectedRows[0].Cells[1].Value.ToString();
            SellAge.Text = SellDGV.SelectedRows[0].Cells[2].Value.ToString();
            SellPhone.Text = SellDGV.SelectedRows[0].Cells[3].Value.ToString();
            SellPass.Text = SellDGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void products2Button_Click(object sender, EventArgs e)
        {
            ProductsForm productsForm = new ProductsForm();
            productsForm.Show();
            this.Hide();

        }

        private void categories2Button_Click(object sender, EventArgs e)
        {
            CategoryForm categoryForm = new CategoryForm();
            categoryForm.Show();
            this.Hide();
        }



        private void logOutLabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogIn login = new LogIn();
            login.Show();
        }

        private void SellerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Confirm to close", "Exit", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (confirm == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}