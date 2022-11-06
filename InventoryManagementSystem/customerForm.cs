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

namespace InventoryManagementSystem
{
    public partial class customerForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Amit\Documents\adbMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        public customerForm()
        {
            InitializeComponent();
            loadCustomer();
        }
        public void loadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM customerTable", conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
            }
            dr.Close();
            conn.Close();
        }

        private void btnCAdd_Click(object sender, EventArgs e)
        {
            customerModuleForm moduleForm = new customerModuleForm();
            moduleForm.btnSave.Enabled = true;
            moduleForm.btnUpdate.Enabled = false;
            moduleForm.ShowDialog();
            loadCustomer();
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             string colName = dgvCustomer.Columns[e.ColumnIndex].Name;
            if (colName =="Edit")
            {
                customerModuleForm customerModule = new customerModuleForm();
                customerModule.lblCId.Text =dgvCustomer.Rows[e.RowIndex].Cells[0].Value.ToString();
                customerModule.txtCName.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                customerModule.txtCPhone.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();

                customerModule.btnSave.Enabled=false;
                customerModule.btnUpdate.Enabled=true;
                customerModule.ShowDialog();


            }
            else if (colName =="Delete")
            {
                if (MessageBox.Show("Are you want to delete this user?", "Delete User Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("DELETE FROM customerTable WHERE cid LIKE '" + dgvCustomer.Rows[e.RowIndex].Cells[0].Value.ToString() + "' ", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("User Record has been deleted!");

                }
            }
            loadCustomer();
        }
    }
    }
