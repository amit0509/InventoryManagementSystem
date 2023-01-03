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

namespace InventoryManagementSystem
{
    public partial class productModuleForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Amit\Documents\adbMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        public productModuleForm()
        {
            InitializeComponent();
            loadCategory();
        }
        public void loadCategory()
        {
            comboCat.Items.Clear();
            SqlCommand cmd = new SqlCommand("SELECT catname FROM categoryTable",conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboCat.Items.Add(reader[0].ToString());
            }
            reader.Close();
            conn.Close();

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to save this user", "Saving....", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO productTable(pname,pqty,pprice,pdescription,pcategory,pdate)VALUES(@pname,@pqty,@pprice,@pdescription,@pcategory,@pdate)", conn);
                    cmd.Parameters.AddWithValue("@pname", txtPName.Text);
                    cmd.Parameters.AddWithValue("@pqty", Convert.ToInt32(txtPQty.Text));
                    cmd.Parameters.AddWithValue("@pprice", Convert.ToInt32(txtPPrice.Text));
                    cmd.Parameters.AddWithValue("@pdescription", txtPDes.Text);
                    cmd.Parameters.AddWithValue("@pcategory", comboCat.Text);
                    cmd.Parameters.AddWithValue("@pdate", dateProduct.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Product has been saved");
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Clear()
        {
            txtPName.Clear();
            txtPQty.Clear();
            txtPPrice.Clear();
            txtPDes.Clear();
            comboCat.Text = " ";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to update this Product record", "Updating....", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE productTable SET pname=@pname, pqty=@pqty, pprice=@pprice, pdescription=@pdescription, pcategory=@pcategory, pdate=@pdate WHERE pid LIKE'" + lblPId.Text + "'", conn);
                    cmd.Parameters.AddWithValue("@pname", txtPName.Text);
                    cmd.Parameters.AddWithValue("@pqty", Convert.ToInt32(txtPQty.Text));
                    cmd.Parameters.AddWithValue("@pprice", Convert.ToInt32(txtPPrice.Text));
                    cmd.Parameters.AddWithValue("@pdescription", txtPDes.Text);
                    cmd.Parameters.AddWithValue("@pcategory", comboCat.Text);
                    cmd.Parameters.AddWithValue("@pdate", dateProduct.Value);


                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Product Record has been updated!");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
