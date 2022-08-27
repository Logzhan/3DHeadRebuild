using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpGL;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Threading;
using ThreeDHeadRebuild;
using ThreeDHeadRebuild.Src.Util;
using System.IO;
using CCWin;

namespace ThreeDHeadRebuild
{
    public partial class MainForm : SkinMain
    {

        // 重建3D人头函数
        [DllImport("3DHeadReconstruction.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Reconstruction3DHead([MarshalAs(UnmanagedType.LPStr)] string ImgPath, 
                                                      [MarshalAs(UnmanagedType.LPStr)] string SkinImgPath);

        // 重建完成后获取人脸纹理
        [DllImport("3DHeadReconstruction.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Get3DHeadTexture(ref byte data, out ulong size);

        // 获取人脸脸型参数
        [DllImport("3DHeadReconstruction.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetBlendShape(ref int data, out int size);

        [DllImport("3DHeadReconstruction.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetUV(ref float headuv, ref float eyesuv, out ulong headsize, out ulong eyesize);

        [DllImport("3DHeadReconstruction.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void WriteObjFile([MarshalAs(UnmanagedType.LPStr)] string filePath);

        [DllImport("3DHeadReconstruction.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SaveTextureFile([MarshalAs(UnmanagedType.LPStr)] string filePath);

        public MorphableModel mMorphableModel = null;

        bool isLoad3DModel = false;

        public MainForm()
        {
            InitializeComponent();
        }

        csgl gl;
        private void Form1_Load(object sender, EventArgs e)
        {
            // 设置窗体的名称
            this.Text = "三维人头重建软件V1.0  实验室主页：http://www.avc2-lab.net/";

            //实例化OpenGL, 并绑定OpenGL绘制控件
            gl = new csgl(openGLControl);

            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.GLpanel_MouseWheel);

            mMorphableModel = new MorphableModel();

            isLoad3DModel = mMorphableModel.Load3DMorphableModel("model/MorphableModel.bin");

            if (isLoad3DModel == false)
            {
                MessageBox.Show("加载默认3DMM模型失败，请手动指定3DMM模型目录", "ERROR");
            }

            bool status = gl.LoadTexture("model/Output.jpg");

            if (status == false)
            {
                MessageBox.Show("加载默认纹理贴图失败，请手动加载纹理贴图", "ERROR");
            }
        }

        /********************************************************************************************
         * OpenGL 回调函数部分 开始
         ********************************************************************************************/
        /* 
         *  描  述 ：  OpenGL 缩放回调函数                              
         *  功  能 ：  控制OpenGL的摄像机缩放
         */
        private void GLpanel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (gl.Global_MouseInGLPanel)
                gl.zoomCameraDelta(e.Delta / 60);
        }
        // 确定鼠标按下
        private void GLpanel_MouseEnter(object sender, EventArgs e)
        {
            gl.Global_MouseInGLPanel = true;
        }
        // 确定鼠标离开
        private void GLpanel_MouseLeave(object sender, EventArgs e)
        {
            gl.Global_MouseInGLPanel = false;
        }

        // 貌似是OpenGL绘制控件被初始化时被调用
        private void sceneControl1_OpenGLDraw(object sender, PaintEventArgs e)
        {
            gl.Draw(isLoad3DModel, mMorphableModel);
        }

        // 鼠标中间键双击，重置视角
        private void sceneControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                gl.resetView();
            }
        }

        private void sceneControl1_MouseDown(object sender, MouseEventArgs e)
        {
            gl.Global_MouseDown = true;
            gl.setMousePos(e.X, e.Y);
        }

        private void sceneControl1_MouseMove(object sender, MouseEventArgs e)
        {if(gl!=null)
            if (gl.Global_MouseDown)
            { 
                if (e.Button == MouseButtons.Left && Control.ModifierKeys == Keys.Control)
                {
                    gl.moveCameraXY(e.X, e.Y);
                }
                else if (e.Button == MouseButtons.Right && Control.ModifierKeys == Keys.Control)
                {
                    gl.moveCameraZ(e.X, e.Y);
                }
                else if (e.Button == MouseButtons.Left)
                {
                    gl.rotateCamera(e.X, e.Y);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    gl.zoomCamera(e.Y);
                }
            }
        }

        private void sceneControl1_MouseUp(object sender, MouseEventArgs e)
        {
            gl.Global_MouseDown = false;
            gl.setMousePos(e.X, e.Y);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //int x0 =  1273-1024; int y0 =  711-640;

            //int x = this.Width - x0; int y = this.Height - y0;
            //x = x < 950-x0 ? 950-x0 : x; y = y < 600 ? 600 : y;
            //groupBox2.Size = new System.Drawing.Size(x, y);
            //this.Size = new System.Drawing.Size(this.Size.Width < 950 ? 950 : this.Size.Width, this.Size.Height < 600 ? 600 : this.Size.Height);
        }

        private void sceneControl1_Resized(object sender, EventArgs e)
        {if(gl != null)
            gl.resize();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invoke(new EventHandler(delegate
            {
            }));
        }

        // 实验室网页链接
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.avc2-lab.net");
        }

        private void BtnShowAngle_Click(object sender, EventArgs e){
            int w = 0, h = 0, s = 0;


            Debug.WriteLine(w.ToString() + " " + h.ToString() + " " + s.ToString());
        }

        /********************************************************************************************
        * 按钮回调函数
         ********************************************************************************************/
        // 点击这个按钮打开关于我们窗口
        private void Menu_AboutUs_Click(object sender, EventArgs e)
        {
            Form AboutUs = new AboutUs();
            AboutUs.Show();
        }


        // 点击这个按钮创建新的三维人脸
        private void New3DHead_Click(object sender, EventArgs e)
        {
            // 创建打开文件对话框
            OpenFileDialog dialog = new OpenFileDialog();
            // 禁用文件多选
            dialog.Multiselect = false;
            // 对话框标题
            dialog.Title = "请选择人脸照片文件";
            // 选择人脸照片
            dialog.Filter = "图片文件(*.jpg,*.png,*.bmp)|*.jpg;*.png;*.bmp";
            // 确定选择，则获取文件的路径
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;

                // Debug 输出文件路径
                Debug.WriteLine(file);
            }
        }

        private void Menu_Load3DMM_Click(object sender, EventArgs e)
        {
            // 创建打开文件对话框
            OpenFileDialog dialog = new OpenFileDialog();
            // 禁用文件多选
            dialog.Multiselect = false;
            // 对话框标题
            dialog.Title = "请选择3DMM模型文件";
            // 选择人脸照片
            dialog.Filter = "模型文件(*.bin)|*.bin";
            // 确定选择，则获取文件的路径
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mMorphableModel = new MorphableModel();

                isLoad3DModel = mMorphableModel.Load3DMorphableModel(dialog.FileName);

                if (isLoad3DModel == false) {
                    MessageBox.Show("加载3DMM模型失败", "ERROR");
                }
            }
        }

        private void Menu_LoadTexture_Click(object sender, EventArgs e)
        {
            // 创建打开文件对话框
            OpenFileDialog dialog = new OpenFileDialog();
            // 禁用文件多选
            dialog.Multiselect = false;
            // 对话框标题
            dialog.Title = "请选择纹理图片";
            // 选择人脸照片
            dialog.Filter = "图片文件(*.jpg,*.png,*.bmp)|*.jpg;*.png;*.bmp";
            // 确定选择，则获取文件的路径
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;

                gl.LoadTexture(file);

                // Debug 输出文件路径
                Debug.WriteLine(file);
            }
        }

        private void Btn_Model_Ouput_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Title = "选择输出文件夹";

            dialog.Filter = "三维模型文件(*.obj)|*.obj";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;

                // Debug 输出文件路径
                Debug.WriteLine(file);
            }
        }

        private void Btn_Render_Setting_Click(object sender, EventArgs e)
        {
            Form RenderSet = new RenderSetting(this.gl);
            RenderSet.Show();
        }

        private void Btn_FaceReconstruction_Click(object sender, EventArgs e)
        {
            if (isLoad3DModel == false) {
                MessageBox.Show("未加载3DMM模型，请先选择3DMM模型!", "ERROR");
                Menu_Load3DMM_Click(sender, e);

                return;
            }
            OpenFileDialog dialog = new OpenFileDialog();
            // 禁用文件多选
            dialog.Multiselect = false;
            // 对话框标题
            dialog.Title = "请选择人脸照片文件";
            // 选择人脸照片
            dialog.Filter = "图片文件(*.jpg,*.png,*.bmp)|*.jpg;*.png;*.bmp";
            // 确定选择，则获取文件的路径
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;
                // 重建3D人头
                Reconstruction3DHead(file, "skin.png");

                byte[] imgdata = new byte[3200 * 2400 * 3];//存储图片的数组，必须大于等于图片分辨率

                ulong size = new ulong();

                // 获取生成的纹理
                Get3DHeadTexture(ref imgdata[0], out size);

                // 图片显示
                Image texture = Image.FromStream(new MemoryStream(imgdata, 0, (int)size));

                Bitmap img = new Bitmap(texture);

                gl.LoadTexture(img);

                int[] BlendShapePara = new int[100];

                int length = new int();

                // 获取BlendShape值
                GetBlendShape(ref BlendShapePara[0], out length);

                int[] BlendShape = new int[length];

                for (int i = 0; i < length; i++)
                {
                    BlendShape[i] = BlendShapePara[i];
                }

                mMorphableModel.SetMeshBlendShape(mMorphableModel.Head, BlendShape);

                mMorphableModel.ShapeDeformation(mMorphableModel.Head, BlendShape);

                float[] headuvs = new float[mMorphableModel.Head.TriangleNum * 3 * 2];
                float[] eyesuvs = new float[mMorphableModel.Eyes.TriangleNum * 3 * 2];

                ulong headsize = new ulong();
                ulong eyesize  = new ulong();


                Debug.WriteLine("读取UV");
                GetUV(ref headuvs[0], ref eyesuvs[0], out headsize, out eyesize);
                Debug.WriteLine("读取UV完成");

                ulong index = new ulong();

                for (int i = 0; i < mMorphableModel.Head.TriangleNum; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Vec2f v2f;
                        v2f.x = headuvs[index++];
                        v2f.y = 1 - headuvs[index++];

                        mMorphableModel.Head.Texcoords[i][j] = v2f;
                    }
                }
                index = 0;

                for (int i = 0; i < mMorphableModel.Eyes.TriangleNum; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Vec2f v2f;
                        v2f.x = eyesuvs[index++];
                        v2f.y = 1 - eyesuvs[index++];

                        mMorphableModel.Eyes.Texcoords[i][j] = v2f;
                    }
                }

                headuvs = null;
                eyesuvs = null;
                imgdata = null;
               
            }
        }
        private int Build3DHead(string ImgPath, string SkinPath) {
            return 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void ShapeControl_Click(object sender, EventArgs e)
        {
            if (isLoad3DModel == false)
            {
                MessageBox.Show("未加载3DMM模型，请先选择3DMM模型!", "ERROR");
                Menu_Load3DMM_Click(sender, e);
                return;
            }
            Form ModelControl = new ModelControlForm(this);
            ModelControl.Show();
        }

        private void openGLControl_Load(object sender, EventArgs e)
        {
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ShapeAdjust_Click(object sender, EventArgs e)
        {
            if (isLoad3DModel == false)
            {
                MessageBox.Show("未加载3DMM模型，请先选择3DMM模型!", "ERROR");
                Menu_Load3DMM_Click(sender, e);
                return;
            }
            Form ModelControl = new ModelControlForm(this);
            ModelControl.Show();
        }

        private void Btn_Render_Set_Click(object sender, EventArgs e)
        {
            Form RenderSet = new RenderSetting(this.gl);
            RenderSet.Show();
        }

        private void Setting_Click(object sender, EventArgs e)
        {

        }

        private void Btn_model_output_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Title = "选择输出文件夹";

            dialog.Filter = "三维模型文件(*.obj)|*.obj";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;

                //WriteObjFile(file);
                mMorphableModel.WriteObjFile(file);

                MessageBox.Show("模型导出成功!", "消息");
                // Debug 输出文件路径
                Debug.WriteLine(file);
            }
        }

        private void Btn_TextureExport_Click(object sender, EventArgs e)
        {
                        SaveFileDialog dialog = new SaveFileDialog();

            dialog.Title = "选择输出文件夹";

            dialog.Filter = "图片文件(*.jpg,*.png,*.bmp)|*.jpg;*.png;*.bmp";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;

                //WriteObjFile(file);
                bool status = SaveTextureFile(file);

                if (status == true) MessageBox.Show("导出成功!", "消息");
                else {
                    MessageBox.Show("纹理贴图导出失败，请检查是否先重建人头或者检查输出文件名是否合法!", "Error");
                }
                // Debug 输出文件路径
                Debug.WriteLine(file);
            }
            
        }


    }
}
