﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }
        private Form activeForm=null;
        private void openChildForm(Form childForm)
        {
            if (activeForm!=null)
                activeForm.Close();

            activeForm=childForm;
            childForm.TopLevel=false;
            childForm.FormBorderStyle=FormBorderStyle.None;
            childForm.Dock=DockStyle.Fill;
            panelMain.Controls.Add(childForm);
            panelMain.Tag=childForm;
            childForm.BringToFront();
            childForm.Show();

        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            openChildForm(new userForm());
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            openChildForm(new customerForm());
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            openChildForm(new categoryForm());
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            openChildForm(new productForm());
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            openChildForm(new orderForm());
        }

    }
}
