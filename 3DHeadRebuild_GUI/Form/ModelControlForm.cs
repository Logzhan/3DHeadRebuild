using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreeDHeadRebuild
{
    public partial class ModelControlForm : Form
    {
        MainForm mMainForm = null;
        public ModelControlForm(MainForm mainform)
        {

            InitializeComponent();

            this.mMainForm = mainform;

            InitSettingList();
        }

        private void InitSettingList() {

            if (mMainForm != null) {

                
                int FaceShapeNum = mMainForm.mMorphableModel.Head.FaceShapeNum - 1;
        
                for (int i = 0; i < FaceShapeNum; i++)
                {
                    TrackBar bar = new TrackBar();
                    bar.Minimum = 0;
                    bar.Maximum = 100;
                    bar.TickFrequency = 1;
                    bar.Name = i.ToString();
                    bar.Value = mMainForm.mMorphableModel.Head.BlendShapeParameter[i];

                    Label label = new Label();

                    // 显示BlendShape对应的名称
                    label.Text = mMainForm.mMorphableModel.Head.ShapeName[i];
                    label.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    //label.Size = new System.Drawing.Size(120, 17);
                    // 这个10是用来确定每列放置的TrackBar的数量
                    int col = i / 10;
                    label.Location = new Point(10  + col * 240, 20 + (i - (col * 10)) * 40);
                    bar.Location   = new Point(120 + col * 240, 20 + (i - (col * 10)) * 40);

                    bar.Scroll += delegate(object sender, EventArgs e)
                    {
                        int index = int.Parse(bar.Name);
                        mMainForm.mMorphableModel.Head.BlendShapeParameter[index] = bar.Value;
                        mMainForm.mMorphableModel.ShapeDeformation(mMainForm.mMorphableModel.Head, 
                                                                   mMainForm.mMorphableModel.Head.BlendShapeParameter);
                    };

                    this.Controls.Add(bar);
                    this.Controls.Add(label);
                }
            }
        }

        private void BlendShapeList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ModelControlForm_Load(object sender, EventArgs e)
        {

        }
    }
}
