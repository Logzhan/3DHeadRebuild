using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
namespace ThreeDHeadRebuild
{
    public partial class MiniForm : Form
    {
        System.Windows.Forms.Timer mTimer;
        public MiniForm()
        {
            InitializeComponent();
            Label_Owner.Text = "本程序所有权为电子科技大学科B545\n实验室所有,详情请参见关于。";
        }

        private void MiniForm_Load(object sender, EventArgs e)
        {
            int x = (System.Windows.Forms.SystemInformation.WorkingArea.Width - this.Size.Width) / 2;
            int y = (System.Windows.Forms.SystemInformation.WorkingArea.Height - this.Size.Height) / 2;
            this.StartPosition = FormStartPosition.Manual; //窗体的位置由Location属性决定
            this.Location = (Point)new Size(x, y);         //窗体的起始位置为(x,y)

            mTimer = new System.Windows.Forms.Timer();

            mTimer.Interval = 1500;

            mTimer.Enabled = true;

            mTimer.Tick += new EventHandler(timer_Tick);

        }
        void timer_Tick(object sender, EventArgs e)
        {
            this.Close();
            mTimer.Stop();
        }
    }
}
