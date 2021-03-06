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
using SupermarketTuto.DataAccess;

namespace SupermarketTuto
{
    public partial class SellerForm : Form
    {
        Image file;

        public SellerForm()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
        private void display()
        {
            SqlConnect loaddata1 = new SqlConnect();

            loaddata1.retrieveData("Select * From SellerTbl where Date between '" + fromDateTimePicker.Value.ToString("MM-dd-yyyy") + "' and '" + toDateTimePicker.Value.ToString("MM-dd-yyyy") + "'");
            SellDGV.DataSource = loaddata1.table;


        }

        private void SellerForm_Load(object sender, EventArgs e)
        {
            display();
            ContextMenuStrip mnu = new ContextMenuStrip();
            ToolStripMenuItem mnuDelete = new ToolStripMenuItem("Delete");
            //Assign event handlers
            mnuDelete.Click += new EventHandler(mnuDelete_Click);
            //Add to main context menu
            mnu.Items.AddRange(new ToolStripItem[] { mnuDelete });
            //Assign to datagridview
            SellDGV.ContextMenuStrip = mnu;

        }
        private void mnuDelete_Click(object? sender, EventArgs e)
        {
            SqlConnect loaddata2 = new SqlConnect();

            loaddata2.commandExc("Delete From SellerTbl Where SellerId=" + SellId.Text + "");

            foreach (DataGridViewRow row in SellDGV.SelectedRows)
            {
                SellDGV.Rows.RemoveAt(row.Index);

            }
        }


        private void edit2Button_Click(object sender, EventArgs e)
        {
            SqlConnect loaddata3 = new SqlConnect();


            try
            {
                if (SellId.Text == "" || SellName.Text == "" || SellAge.Text == "" || SellPhone.Text == "" || SellPass.Text == "")
                {
                    MessageBox.Show("Missing Information");

                }
                else
                {

                    loaddata3.commandExc("Update SellerTbl set SellerName='" + SellName.Text + "',SellerAge='" + SellAge.Text + "',SellerPhone='" + SellPhone.Text + "',SellerPass='" + SellPass.Text + "' where SellerId=" + SellId.Text + ";");
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
            SqlConnect loaddata4 = new SqlConnect();

            try
            {
                if (SellId.Text == "" || SellName.Text == "" || SellAge.Text == "" || SellPhone.Text == "" || SellPass.Text == "")
                {
                    MessageBox.Show("Select The Category to Delete");
                }
                else
                {

                    loaddata4.commandExc("Delete From SellerTbl Where SellerId=" + SellId.Text + "");

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


        private void add2Button_Click(object sender, EventArgs e)
        {
            

            SqlConnect loaddata5 = new SqlConnect();

            try
            {


                if (SellId.Text == "" || SellName.Text == "" || SellAge.Text == "" || SellPhone.Text == "" || SellPass.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    loaddata5.commandExc("Insert Into SellerTbl values(" + SellId.Text + ",'" + SellName.Text + "'," + SellAge.Text + "," + SellPhone.Text + ",'" + SellPass.Text + "','" + dateTimePicker.Value.ToString("MM-dd-yyyy") + "')");
                    MessageBox.Show("Seller added successfuly");



                    //SqlConnection con = new SqlConnection("Data Source=.; Initial Catalog=smarketdb;Integrated Security=True;");
                    //SqlCommand cmd = new SqlCommand("Insert Into Test (Photo) Values(@pic)", con);
                    //SqlCommand cmd = new SqlCommand("Insert Into SellerTbl values(" + SellId.Text + ",'" + SellName.Text + "'," + SellAge.Text + "," + SellPhone.Text + ",'" + SellPass.Text + "@pic" + "')");
                    //MemoryStream stream = new MemoryStream();
                    //pictureBox.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //byte[] pic = stream.ToArray();
                    //cmd.Parameters.AddWithValue("@Pic", pic);


                    SellId.Text = "";
                    SellName.Text = "";
                    SellAge.Text = "";
                    SellPhone.Text = "";
                    SellPass.Text = "";
                    MessageBox.Show("Seller added successfuly");


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            if (confirm == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void SellDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SellId.Text = SellDGV.SelectedRows[0].Cells[0].Value.ToString();
            SellName.Text = SellDGV.SelectedRows[0].Cells[1].Value.ToString();
            SellAge.Text = SellDGV.SelectedRows[0].Cells[2].Value.ToString();
            SellPhone.Text = SellDGV.SelectedRows[0].Cells[3].Value.ToString();
            SellPass.Text = SellDGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void SellDGV_MouseDown(object sender, MouseEventArgs e)
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem sellerDelete = new ToolStripMenuItem("Delete");
            sellerDelete.Click += new EventHandler(sellerDelete_Click);
            menu.Items.AddRange(new ToolStripItem[] {sellerDelete });
            SellDGV.ContextMenuStrip = menu;
            SellDGV.AllowUserToAddRows = false;


        }

        private void sellerDelete_Click(object sender, EventArgs e)
        {
            SqlConnect loaddata6 = new SqlConnect();

            loaddata6.commandExc("Delete From Items Where SellerId=" + SellDGV.CurrentRow.Cells[0].Value.ToString() + "");

            foreach (DataGridViewRow row in SellDGV.Rows)
            {
                SellDGV.Rows.RemoveAt(row.Index);
            }


        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            Stream myStream = null;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JPG (.JPG)|*.jpg";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = dialog.OpenFile()) != null)
                    {
                        //file = Image.FromFile(dialog.FileName);
                        //pictureBox.Image = file;
                        string FileName = dialog.FileName;
                        if (myStream.Length > 512000)
                        {
                            MessageBox.Show("File Size limit exceeded");
                        }
                        else
                        {
                            pictureBox.Load(FileName);
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTime chosenDate = dateTimePicker.Value;

            int day = chosenDate.Day;
            int month = chosenDate.Month;
            int year = chosenDate.Year;

            

        }

        private void dataDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            //DateTime chosen = fromDateTimePicker.Value;
            //int day = chosen.Day;
            //int month = chosen.Month;
            //int year = chosen.Year;
            display();
        }

        private void todateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            display();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {

        }
    }
}


