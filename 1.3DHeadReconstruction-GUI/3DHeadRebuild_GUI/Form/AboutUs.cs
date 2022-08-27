/*
 *    说  明 ： 这个窗体用到了无边框设置，也应用了透明按钮设置。由于窗体采用无边框，因此需要采用
 *    Panel的点击和移动事件用于控制窗体的移动。
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;

namespace ThreeDHeadRebuild
{
    public partial class AboutUs : SkinMain
    {
        private Point mPoint;
        public AboutUs()
        {
            InitializeComponent();
  
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint = new Point(e.X, e.Y);
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {                 
                this.Location = new Point(this.Location.X + e.X - mPoint.X, this.Location.Y + e.Y - mPoint.Y);             
            }
        }
    }
}
