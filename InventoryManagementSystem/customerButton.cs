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
    public partial class customerButton : PictureBox
    {
        public customerButton()
        {
            InitializeComponent();
        }
        private Image normalImage;
        private Image hoverImage;

        public Image NormalImage
        {
            get { return normalImage; }
            set { normalImage = value; }
        }

        public Image HoverImage
        { 
          get { return hoverImage; }
          set { hoverImage = value; }

        }

        private void customerButton_MouseHover(object sender, EventArgs e)
        {
            this.Image = hoverImage;
        }

        private void customerButton_MouseLeave(object sender, EventArgs e)
        {
            this.Image=normalImage;

        }
    }
}
