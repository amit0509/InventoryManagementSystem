using System;
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
    public partial class startForm : Form
    {
        public startForm()
        {
            InitializeComponent();
            timer.Start();
        }
        int timerID = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            timerID += 2;
            progressBar.Value = timerID;
            if(progressBar.Value ==100)
            {
                progressBar.Value = 0;
                timer.Stop();
                loginForm loginForm = new loginForm();
                this.Hide();
                loginForm.ShowDialog();
            }
        }
    }
}
