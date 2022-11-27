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
    public partial class loginForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Amit\Documents\adbMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public loginForm()
        {
            InitializeComponent();
        }

        private void checkBoxPass_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxPass.Checked==false)
                txtPass.UseSystemPasswordChar = true;
            else
                txtPass.UseSystemPasswordChar = false;
        }

        private void lblClear_Click(object sender, EventArgs e)
        {
            txtPass.Clear();
            txtName.Clear();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Application", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand("SELECT * FROM userTable WHERE username=@username AND password=@password", conn);
                cmd.Parameters.AddWithValue("@username", txtName.Text);
                cmd.Parameters.AddWithValue("@password", txtPass.Text);

                conn.Open();
                dr = cmd.ExecuteReader();
                dr.Read();
                if(dr.HasRows)
                {
                    MessageBox.Show("Welcome "+dr["fullname"].ToString()+"!", "Access Granted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mainForm main = new mainForm();
                    this.Hide();
                    main.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Invalid User Name or Password!", "Access Denied", MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
