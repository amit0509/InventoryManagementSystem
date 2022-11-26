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
    public partial class orderModuleForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Amit\Documents\adbMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public orderModuleForm()
        {
            InitializeComponent();
            loadCustomer();
            loadProduct();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void loadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cmd = new SqlCommand("SELECT cid, cname FROM customerTable WHERE CONCAT(cid, cname) LIKE '%"+textSearchCustomer.Text+"%'", conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            conn.Close();
        }

        public void loadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM productTable WHERE CONCAT(pid, pname, pprice, pdescription, pcategory) LIKE '%" +textSearchProduct.Text+ "%'", conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            conn.Close();
        }

        private void textSearchCustomer_TextChanged(object sender, EventArgs e)
        {
            loadCustomer();
        }

        private void textSearchProduct_TextChanged(object sender, EventArgs e)
        {
            loadProduct();
        }


        int qty;

        private void numericUpDownQty_ValueChanged(object sender, EventArgs e)
        {
            if(Convert.ToInt32(numericUpDownQty.Value)>qty)
            {
                MessageBox.Show("Quantity is not enough in stock!","warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int total=Convert.ToInt32(txtPrice.Text) * Convert.ToInt32(numericUpDownQty.Value);
            txtTotal.Text = total.ToString();
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCId.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPId.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
            qty = Convert.ToInt32(dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString());

        }
    }
}
