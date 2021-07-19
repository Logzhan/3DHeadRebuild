using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ThreeDHeadRebuild
{
    public partial class RenderSetting : Form
    {
        csgl gl;

        public RenderSetting()
        {
            InitializeComponent();
        }

        public RenderSetting(csgl gl)
        {
            this.gl = gl;
            InitializeComponent();
        }

        private void TrackBar_CamerHeight_Scroll(object sender, EventArgs e)
        {
            if (gl != null) {
                gl.SetCameraHeight(this.TrackBar_CamerHeight.Value);

            }
            
        }

        private void ChkBox_LineDisplay_CheckedChanged(object sender, EventArgs e)
        {
            var cbx = ((CheckBox)sender);

            if (cbx.Name == "ChkBox_LineDisplay")
            {
                gl.IsLineDisplay = cbx.Checked;
            }
        }

        private void ChkBox_CloseLight_CheckedChanged(object sender, EventArgs e)
        {
            var cbx = ((CheckBox)sender);

            if (cbx.Name == "ChkBox_CloseLight")
            {
                gl.ConfigOpenGLight(!cbx.Checked);
            }
        }

        private void ChkBox_CloseTexture_CheckedChanged(object sender, EventArgs e)
        {
            var cbx = ((CheckBox)sender);

            if (cbx.Name == "ChkBox_CloseTexture")
            {
                gl.ConfigTexture(!cbx.Checked);
            }
        }

        private void Tbr_Model_Height_Scroll(object sender, EventArgs e)
        {
            gl.Model_HeightOffset = this.Tbr_Model_Height.Value;
        }
    }
}
