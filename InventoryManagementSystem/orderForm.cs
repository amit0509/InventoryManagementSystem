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
    public partial class orderForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Amit\Documents\adbMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public orderForm()
        {
            InitializeComponent();
            loadOrder();
        }
        public void loadOrder()
        {
            double total = 0;
            int i = 0;
            dgvOrder.Rows.Clear();
            cmd = new SqlCommand("SELECT oid, odate, O.pid, P.pname, O.cid, C.cname, qty, price, total FROM orderTable AS O JOIN customerTable AS C ON O.cid=C.cid JOIN productTable AS P ON O.pid=P.pid WHERE CONCAT(oid, odate, O.pid, P.pname, O.cid, C.cname, qty, price, total) LIKE '%"+txtSearch.Text+"%'", conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvOrder.Rows.Add(i, dr[0].ToString(), Convert.ToDateTime( dr[1].ToString()).ToString("dd/mm/yyyy"), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString());
                total+=Convert.ToInt32(dr[8].ToString());
            }
            dr.Close();
            conn.Close();
            
            lblQty.Text = i.ToString();
            lblTotal.Text = total.ToString();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            orderModuleForm orderModue = new orderModuleForm();
            orderModue.ShowDialog();
            loadOrder();
        }

        private void dgvOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvOrder.Columns[e.ColumnIndex].Name;
            if (colName == "Delete")
            {
                if (MessageBox.Show("Are you want to delete this Order?", "Delete Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("DELETE FROM orderTable WHERE oid LIKE '" + dgvOrder.Rows[e.RowIndex].Cells[1].Value.ToString() + "' ", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Order has been deleted!");

                    cmd = new SqlCommand("UPDATE productTable SET  pqty= (pqty+@pqty) WHERE pid LIKE'" + dgvOrder.Rows[e.RowIndex].Cells[3].Value.ToString() + "'", conn);
                    cmd.Parameters.AddWithValue("@pqty", Convert.ToInt32(dgvOrder.Rows[e.RowIndex].Cells[5].Value.ToString()));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            loadOrder();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadOrder();
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            int i = 0;
            double total=0;
            DateTime from = datePicker1.Value;
            DateTime to = datePicker2.Value;
            dgvOrder.Rows.Clear();
            cmd = new SqlCommand("SELECT oid, odate, O.pid, P.pname, O.cid, C.cname, qty, price, total FROM orderTable AS O JOIN customerTable AS C ON O.cid=C.cid JOIN productTable AS P ON O.pid=P.pid WHERE odate BETWEEN '" + from.ToString("yyyy-MM-dd") + "' AND '" + to.ToString("yyyy-MM-dd") + "'", conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvOrder.Rows.Add(i, dr[0].ToString(), Convert.ToDateTime(dr[1].ToString()).ToString("dd/mm/yyyy"), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString());
                total += Convert.ToInt32(dr[8].ToString());
            }
            dr.Close();
            conn.Close();

            lblQty.Text = i.ToString();
            lblTotal.Text = total.ToString();
        }


        private void clearBtn_Click(object sender, EventArgs e)
        {
            int i = 0;
            double total = 0;

            dgvOrder.Rows.Clear();
            cmd = new SqlCommand("SELECT oid, odate, O.pid, P.pname, O.cid, C.cname, qty, price, total FROM orderTable AS O JOIN customerTable AS C ON O.cid=C.cid JOIN productTable AS P ON O.pid=P.pid ", conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvOrder.Rows.Add(i, dr[0].ToString(), Convert.ToDateTime(dr[1].ToString()).ToString("dd/mm/yyyy"), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString());
                total += Convert.ToInt32(dr[8].ToString());
            }
            dr.Close();
            conn.Close();

            lblQty.Text = i.ToString();
            lblTotal.Text = total.ToString();
        }
    }
}
