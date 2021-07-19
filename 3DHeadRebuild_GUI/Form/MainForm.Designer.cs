namespace ThreeDHeadRebuild
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ThreeDHeadRebuild.GroupBoxEx groupBoxEx1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.GLpanel = new System.Windows.Forms.Panel();
            this.openGLControl = new SharpGL.OpenGLControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.New3DHead = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_OpenModel = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Edit_FaceShape_Adjust = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Render_Setting = new System.Windows.Forms.ToolStripMenuItem();
            this.工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Load3DMM = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_LoadTexture = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Tool = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助文档ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_AboutUs = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.line = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.Btn_Setting = new System.Windows.Forms.Button();
            this.mini = new System.Windows.Forms.Button();
            this.Pic_Programm = new System.Windows.Forms.PictureBox();
            this.Btn_Close = new System.Windows.Forms.Button();
            this.Btn_TextureExport = new System.Windows.Forms.Button();
            this.Btn_model_output = new System.Windows.Forms.Button();
            this.Pic_school_logo = new System.Windows.Forms.PictureBox();
            this.Btn_Rebuild_Set = new System.Windows.Forms.Button();
            this.Btn_Render_Setting = new System.Windows.Forms.Button();
            this.ShapeAdjust = new System.Windows.Forms.Button();
            this.Btn_FaceReconstruction = new System.Windows.Forms.Button();
            groupBoxEx1 = new ThreeDHeadRebuild.GroupBoxEx(this.components);
            groupBoxEx1.SuspendLayout();
            this.GLpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Programm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_school_logo)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxEx1
            // 
            groupBoxEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            groupBoxEx1.Controls.Add(this.GLpanel);
            groupBoxEx1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            groupBoxEx1.Location = new System.Drawing.Point(8, 74);
            groupBoxEx1.Name = "groupBoxEx1";
            groupBoxEx1.Size = new System.Drawing.Size(996, 580);
            groupBoxEx1.TabIndex = 22;
            groupBoxEx1.TabStop = false;
            groupBoxEx1.Text = "3D显示";
            // 
            // GLpanel
            // 
            this.GLpanel.Controls.Add(this.openGLControl);
            this.GLpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GLpanel.Location = new System.Drawing.Point(3, 17);
            this.GLpanel.Name = "GLpanel";
            this.GLpanel.Size = new System.Drawing.Size(990, 560);
            this.GLpanel.TabIndex = 10;
            this.GLpanel.MouseEnter += new System.EventHandler(this.GLpanel_MouseEnter);
            this.GLpanel.MouseLeave += new System.EventHandler(this.GLpanel_MouseLeave);
            // 
            // openGLControl
            // 
            this.openGLControl.AutoSize = true;
            this.openGLControl.BitDepth = 24;
            this.openGLControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openGLControl.DrawFPS = true;
            this.openGLControl.FrameRate = 20;
            this.openGLControl.Location = new System.Drawing.Point(0, 0);
            this.openGLControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.NativeWindow;
            this.openGLControl.Size = new System.Drawing.Size(990, 560);
            this.openGLControl.TabIndex = 0;
            this.openGLControl.OpenGLDraw += new System.Windows.Forms.PaintEventHandler(this.sceneControl1_OpenGLDraw);
            this.openGLControl.Resized += new System.EventHandler(this.sceneControl1_Resized);
            this.openGLControl.Load += new System.EventHandler(this.openGLControl_Load);
            this.openGLControl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.sceneControl1_MouseDoubleClick);
            this.openGLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sceneControl1_MouseDown);
            this.openGLControl.MouseEnter += new System.EventHandler(this.GLpanel_MouseEnter);
            this.openGLControl.MouseLeave += new System.EventHandler(this.GLpanel_MouseLeave);
            this.openGLControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.sceneControl1_MouseMove);
            this.openGLControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.sceneControl1_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Btn_TextureExport);
            this.panel1.Controls.Add(this.Btn_model_output);
            this.panel1.Controls.Add(this.Pic_school_logo);
            this.panel1.Controls.Add(this.linkLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 659);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1110, 51);
            this.panel1.TabIndex = 12;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.linkLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.linkLabel1.Location = new System.Drawing.Point(746, 18);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(353, 17);
            this.linkLabel1.TabIndex = 12;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Lab of Advanced Visual Communication & Computing (AVC2)";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Aqua;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Btn_Rebuild_Set);
            this.panel2.Controls.Add(this.Btn_Render_Setting);
            this.panel2.Controls.Add(this.ShapeAdjust);
            this.panel2.Controls.Add(this.Btn_FaceReconstruction);
            this.panel2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel2.Location = new System.Drawing.Point(1009, 74);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(101, 581);
            this.panel2.TabIndex = 13;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.编辑ToolStripMenuItem,
            this.Menu_Edit,
            this.工具ToolStripMenuItem,
            this.Menu_Tool,
            this.帮助HToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(-1, 37);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(309, 25);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Add,
            this.Menu_OpenModel});
            this.编辑ToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.编辑ToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.编辑ToolStripMenuItem.Text = "文件(F)";
            // 
            // Menu_Add
            // 
            this.Menu_Add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.Menu_Add.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.New3DHead});
            this.Menu_Add.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.Menu_Add.Name = "Menu_Add";
            this.Menu_Add.Size = new System.Drawing.Size(172, 22);
            this.Menu_Add.Text = "新建";
            // 
            // New3DHead
            // 
            this.New3DHead.Name = "New3DHead";
            this.New3DHead.Size = new System.Drawing.Size(133, 22);
            this.New3DHead.Text = "三维人脸...";
            this.New3DHead.Click += new System.EventHandler(this.New3DHead_Click);
            // 
            // Menu_OpenModel
            // 
            this.Menu_OpenModel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.Menu_OpenModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.Menu_OpenModel.Name = "Menu_OpenModel";
            this.Menu_OpenModel.Size = new System.Drawing.Size(172, 22);
            this.Menu_OpenModel.Text = "直接打开三维模型";
            // 
            // Menu_Edit
            // 
            this.Menu_Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Edit_FaceShape_Adjust,
            this.Menu_Render_Setting});
            this.Menu_Edit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.Menu_Edit.Name = "Menu_Edit";
            this.Menu_Edit.Size = new System.Drawing.Size(59, 21);
            this.Menu_Edit.Text = "编辑(E)";
            // 
            // Menu_Edit_FaceShape_Adjust
            // 
            this.Menu_Edit_FaceShape_Adjust.Name = "Menu_Edit_FaceShape_Adjust";
            this.Menu_Edit_FaceShape_Adjust.Size = new System.Drawing.Size(148, 22);
            this.Menu_Edit_FaceShape_Adjust.Text = "三维脸型调整";
            // 
            // Menu_Render_Setting
            // 
            this.Menu_Render_Setting.Name = "Menu_Render_Setting";
            this.Menu_Render_Setting.Size = new System.Drawing.Size(148, 22);
            this.Menu_Render_Setting.Text = "三维渲染设置";
            // 
            // 工具ToolStripMenuItem
            // 
            this.工具ToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Load3DMM,
            this.Menu_LoadTexture});
            this.工具ToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.工具ToolStripMenuItem.Name = "工具ToolStripMenuItem";
            this.工具ToolStripMenuItem.Size = new System.Drawing.Size(64, 21);
            this.工具ToolStripMenuItem.Text = "模型(M)";
            // 
            // Menu_Load3DMM
            // 
            this.Menu_Load3DMM.Name = "Menu_Load3DMM";
            this.Menu_Load3DMM.Size = new System.Drawing.Size(148, 22);
            this.Menu_Load3DMM.Text = "加载形变模型";
            this.Menu_Load3DMM.Click += new System.EventHandler(this.Menu_Load3DMM_Click);
            // 
            // Menu_LoadTexture
            // 
            this.Menu_LoadTexture.Name = "Menu_LoadTexture";
            this.Menu_LoadTexture.Size = new System.Drawing.Size(148, 22);
            this.Menu_LoadTexture.Text = "加载纹理";
            this.Menu_LoadTexture.Click += new System.EventHandler(this.Menu_LoadTexture_Click);
            // 
            // Menu_Tool
            // 
            this.Menu_Tool.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.Menu_Tool.Name = "Menu_Tool";
            this.Menu_Tool.Size = new System.Drawing.Size(59, 21);
            this.Menu_Tool.Text = "工具(T)";
            // 
            // 帮助HToolStripMenuItem
            // 
            this.帮助HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.帮助文档ToolStripMenuItem,
            this.Menu_AboutUs});
            this.帮助HToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
            this.帮助HToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.帮助HToolStripMenuItem.Text = "帮助(H)";
            // 
            // 帮助文档ToolStripMenuItem
            // 
            this.帮助文档ToolStripMenuItem.Name = "帮助文档ToolStripMenuItem";
            this.帮助文档ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.帮助文档ToolStripMenuItem.Text = "帮助文档";
            // 
            // Menu_AboutUs
            // 
            this.Menu_AboutUs.Name = "Menu_AboutUs";
            this.Menu_AboutUs.Size = new System.Drawing.Size(124, 22);
            this.Menu_AboutUs.Text = "关于我们";
            this.Menu_AboutUs.Click += new System.EventHandler(this.Menu_AboutUs_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label1.Location = new System.Drawing.Point(48, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(335, 19);
            this.label1.TabIndex = 17;
            this.label1.Text = "三维人头重建 - 电子科技大学高级视觉通信与计算实验室";
            // 
            // line
            // 
            this.line.AutoSize = true;
            this.line.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.line.Location = new System.Drawing.Point(1038, 9);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(11, 12);
            this.line.TabIndex = 20;
            this.line.Text = "|";
            // 
            // Btn_Setting
            // 
            this.Btn_Setting.FlatAppearance.BorderSize = 0;
            this.Btn_Setting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Setting.ForeColor = System.Drawing.Color.Transparent;
            this.Btn_Setting.Image = global::ThreeDHeadRebuild.Properties.Resources.btn_setting_18px_1;
            this.Btn_Setting.Location = new System.Drawing.Point(1009, 8);
            this.Btn_Setting.Name = "Btn_Setting";
            this.Btn_Setting.Size = new System.Drawing.Size(16, 16);
            this.Btn_Setting.TabIndex = 21;
            this.Btn_Setting.UseVisualStyleBackColor = true;
            // 
            // mini
            // 
            this.mini.FlatAppearance.BorderSize = 0;
            this.mini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mini.ForeColor = System.Drawing.Color.Transparent;
            this.mini.Image = global::ThreeDHeadRebuild.Properties.Resources.btn_mini_18px_1;
            this.mini.Location = new System.Drawing.Point(1060, 7);
            this.mini.Name = "mini";
            this.mini.Size = new System.Drawing.Size(18, 18);
            this.mini.TabIndex = 19;
            this.mini.UseVisualStyleBackColor = true;
            this.mini.Click += new System.EventHandler(this.mini_Click);
            // 
            // Pic_Programm
            // 
            this.Pic_Programm.Image = global::ThreeDHeadRebuild.Properties.Resources.logo;
            this.Pic_Programm.Location = new System.Drawing.Point(9, 4);
            this.Pic_Programm.Name = "Pic_Programm";
            this.Pic_Programm.Size = new System.Drawing.Size(30, 30);
            this.Pic_Programm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pic_Programm.TabIndex = 16;
            this.Pic_Programm.TabStop = false;
            // 
            // Btn_Close
            // 
            this.Btn_Close.FlatAppearance.BorderSize = 0;
            this.Btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Close.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Btn_Close.Image = global::ThreeDHeadRebuild.Properties.Resources.btn_close_18px_1;
            this.Btn_Close.Location = new System.Drawing.Point(1084, 7);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(18, 18);
            this.Btn_Close.TabIndex = 15;
            this.Btn_Close.UseVisualStyleBackColor = true;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // Btn_TextureExport
            // 
            this.Btn_TextureExport.FlatAppearance.BorderSize = 0;
            this.Btn_TextureExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_TextureExport.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_TextureExport.ForeColor = System.Drawing.Color.Gainsboro;
            this.Btn_TextureExport.Image = global::ThreeDHeadRebuild.Properties.Resources.photo_32px;
            this.Btn_TextureExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_TextureExport.Location = new System.Drawing.Point(131, 9);
            this.Btn_TextureExport.Name = "Btn_TextureExport";
            this.Btn_TextureExport.Size = new System.Drawing.Size(121, 33);
            this.Btn_TextureExport.TabIndex = 21;
            this.Btn_TextureExport.Text = "纹理图片导出";
            this.Btn_TextureExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Btn_TextureExport.UseVisualStyleBackColor = true;
            this.Btn_TextureExport.Click += new System.EventHandler(this.Btn_TextureExport_Click);
            // 
            // Btn_model_output
            // 
            this.Btn_model_output.FlatAppearance.BorderSize = 0;
            this.Btn_model_output.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_model_output.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_model_output.ForeColor = System.Drawing.Color.Gainsboro;
            this.Btn_model_output.Image = global::ThreeDHeadRebuild.Properties.Resources.ouput_32px;
            this.Btn_model_output.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_model_output.Location = new System.Drawing.Point(9, 9);
            this.Btn_model_output.Name = "Btn_model_output";
            this.Btn_model_output.Size = new System.Drawing.Size(121, 33);
            this.Btn_model_output.TabIndex = 20;
            this.Btn_model_output.Text = "三维模型导出";
            this.Btn_model_output.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Btn_model_output.UseVisualStyleBackColor = true;
            this.Btn_model_output.Click += new System.EventHandler(this.Btn_model_output_Click);
            // 
            // Pic_school_logo
            // 
            this.Pic_school_logo.Image = global::ThreeDHeadRebuild.Properties.Resources.Usetc;
            this.Pic_school_logo.Location = new System.Drawing.Point(709, 9);
            this.Pic_school_logo.Name = "Pic_school_logo";
            this.Pic_school_logo.Size = new System.Drawing.Size(35, 35);
            this.Pic_school_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pic_school_logo.TabIndex = 13;
            this.Pic_school_logo.TabStop = false;
            // 
            // Btn_Rebuild_Set
            // 
            this.Btn_Rebuild_Set.FlatAppearance.BorderSize = 0;
            this.Btn_Rebuild_Set.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Rebuild_Set.ForeColor = System.Drawing.Color.Gainsboro;
            this.Btn_Rebuild_Set.Image = global::ThreeDHeadRebuild.Properties.Resources.settings_48px;
            this.Btn_Rebuild_Set.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Btn_Rebuild_Set.Location = new System.Drawing.Point(4, 494);
            this.Btn_Rebuild_Set.Name = "Btn_Rebuild_Set";
            this.Btn_Rebuild_Set.Size = new System.Drawing.Size(93, 78);
            this.Btn_Rebuild_Set.TabIndex = 19;
            this.Btn_Rebuild_Set.Text = "三维重建设置";
            this.Btn_Rebuild_Set.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Btn_Rebuild_Set.UseVisualStyleBackColor = true;
            // 
            // Btn_Render_Setting
            // 
            this.Btn_Render_Setting.FlatAppearance.BorderSize = 0;
            this.Btn_Render_Setting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Render_Setting.ForeColor = System.Drawing.Color.Gainsboro;
            this.Btn_Render_Setting.Image = global::ThreeDHeadRebuild.Properties.Resources.render_set_48px;
            this.Btn_Render_Setting.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Btn_Render_Setting.Location = new System.Drawing.Point(4, 206);
            this.Btn_Render_Setting.Name = "Btn_Render_Setting";
            this.Btn_Render_Setting.Size = new System.Drawing.Size(93, 78);
            this.Btn_Render_Setting.TabIndex = 18;
            this.Btn_Render_Setting.Text = "三维渲染设置";
            this.Btn_Render_Setting.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Btn_Render_Setting.UseVisualStyleBackColor = true;
            this.Btn_Render_Setting.Click += new System.EventHandler(this.Btn_Render_Set_Click);
            // 
            // ShapeAdjust
            // 
            this.ShapeAdjust.FlatAppearance.BorderSize = 0;
            this.ShapeAdjust.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShapeAdjust.ForeColor = System.Drawing.Color.Gainsboro;
            this.ShapeAdjust.Image = global::ThreeDHeadRebuild.Properties.Resources.adjustment_48px;
            this.ShapeAdjust.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ShapeAdjust.Location = new System.Drawing.Point(4, 110);
            this.ShapeAdjust.Name = "ShapeAdjust";
            this.ShapeAdjust.Size = new System.Drawing.Size(93, 78);
            this.ShapeAdjust.TabIndex = 17;
            this.ShapeAdjust.Text = "三维脸型调整";
            this.ShapeAdjust.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ShapeAdjust.UseVisualStyleBackColor = true;
            this.ShapeAdjust.Click += new System.EventHandler(this.ShapeAdjust_Click);
            // 
            // Btn_FaceReconstruction
            // 
            this.Btn_FaceReconstruction.FlatAppearance.BorderSize = 0;
            this.Btn_FaceReconstruction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_FaceReconstruction.ForeColor = System.Drawing.Color.Gainsboro;
            this.Btn_FaceReconstruction.Image = global::ThreeDHeadRebuild.Properties.Resources.add_48px;
            this.Btn_FaceReconstruction.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Btn_FaceReconstruction.Location = new System.Drawing.Point(4, 20);
            this.Btn_FaceReconstruction.Name = "Btn_FaceReconstruction";
            this.Btn_FaceReconstruction.Size = new System.Drawing.Size(93, 78);
            this.Btn_FaceReconstruction.TabIndex = 15;
            this.Btn_FaceReconstruction.Text = " 重建三维人脸";
            this.Btn_FaceReconstruction.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Btn_FaceReconstruction.UseVisualStyleBackColor = true;
            this.Btn_FaceReconstruction.Click += new System.EventHandler(this.Btn_FaceReconstruction_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.ClientSize = new System.Drawing.Size(1110, 710);
            this.Controls.Add(groupBoxEx1);
            this.Controls.Add(this.Btn_Setting);
            this.Controls.Add(this.line);
            this.Controls.Add(this.mini);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Pic_Programm);
            this.Controls.Add(this.Btn_Close);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.CornerRadius = 1;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.ShadowStyle = CCWin.RoundStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "三维人头重建";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            groupBoxEx1.ResumeLayout(false);
            this.GLpanel.ResumeLayout(false);
            this.GLpanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Programm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_school_logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Menu_Add;
        private System.Windows.Forms.ToolStripMenuItem New3DHead;
        private System.Windows.Forms.ToolStripMenuItem Menu_OpenModel;
        private System.Windows.Forms.ToolStripMenuItem Menu_Load3DMM;
        private System.Windows.Forms.ToolStripMenuItem Menu_LoadTexture;
        private System.Windows.Forms.Button Btn_FaceReconstruction;
        private System.Windows.Forms.Button Btn_Close;
        private System.Windows.Forms.PictureBox Pic_Programm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox Pic_school_logo;
        private System.Windows.Forms.Button mini;
        private System.Windows.Forms.Label line;
        private System.Windows.Forms.Button Btn_Setting;
        private System.Windows.Forms.ToolStripMenuItem Menu_Tool;
        private System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助文档ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Menu_AboutUs;
        private System.Windows.Forms.ToolStripMenuItem Menu_Edit;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel GLpanel;
        private SharpGL.OpenGLControl openGLControl;
        private System.Windows.Forms.Button ShapeAdjust;
        private System.Windows.Forms.Button Btn_Render_Setting;
        private System.Windows.Forms.Button Btn_Rebuild_Set;
        private System.Windows.Forms.Button Btn_model_output;
        private System.Windows.Forms.ToolStripMenuItem Menu_Edit_FaceShape_Adjust;
        private System.Windows.Forms.ToolStripMenuItem Menu_Render_Setting;
        private System.Windows.Forms.Button Btn_TextureExport;

    }
}

