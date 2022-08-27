namespace ThreeDHeadRebuild
{
    partial class AboutUs
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutUs));
            this.ProgrammName = new System.Windows.Forms.Label();
            this.label_University = new System.Windows.Forms.Label();
            this.label_LabName = new System.Windows.Forms.Label();
            this.Label_ContactUs = new System.Windows.Forms.Label();
            this.Label_Email = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Btn_close = new System.Windows.Forms.Button();
            this.Panel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProgrammName
            // 
            this.ProgrammName.AutoSize = true;
            this.ProgrammName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ProgrammName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.ProgrammName.Location = new System.Drawing.Point(150, 35);
            this.ProgrammName.Name = "ProgrammName";
            this.ProgrammName.Size = new System.Drawing.Size(134, 19);
            this.ProgrammName.TabIndex = 1;
            this.ProgrammName.Text = "三维人头重建 v1.0";
            // 
            // label_University
            // 
            this.label_University.AutoSize = true;
            this.label_University.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_University.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label_University.Location = new System.Drawing.Point(150, 56);
            this.label_University.Name = "label_University";
            this.label_University.Size = new System.Drawing.Size(99, 20);
            this.label_University.TabIndex = 2;
            this.label_University.Text = "电子科技大学";
            // 
            // label_LabName
            // 
            this.label_LabName.AutoSize = true;
            this.label_LabName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_LabName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label_LabName.Location = new System.Drawing.Point(149, 77);
            this.label_LabName.Name = "label_LabName";
            this.label_LabName.Size = new System.Drawing.Size(189, 20);
            this.label_LabName.TabIndex = 3;
            this.label_LabName.Text = "高级视觉通信与计算实验室";
            // 
            // Label_ContactUs
            // 
            this.Label_ContactUs.AutoSize = true;
            this.Label_ContactUs.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_ContactUs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.Label_ContactUs.Location = new System.Drawing.Point(148, 113);
            this.Label_ContactUs.Name = "Label_ContactUs";
            this.Label_ContactUs.Size = new System.Drawing.Size(266, 20);
            this.Label_ContactUs.TabIndex = 4;
            this.Label_ContactUs.Text = "联系我们：http://www.avc2-lab.net/";
            // 
            // Label_Email
            // 
            this.Label_Email.AutoSize = true;
            this.Label_Email.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Email.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.Label_Email.Location = new System.Drawing.Point(148, 135);
            this.Label_Email.Name = "Label_Email";
            this.Label_Email.Size = new System.Drawing.Size(236, 20);
            this.Label_Email.TabIndex = 5;
            this.Label_Email.Text = "电子邮件：719901725@qq.com";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(4, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(143, 142);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Btn_close
            // 
            this.Btn_close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_close.BackgroundImage")));
            this.Btn_close.FlatAppearance.BorderSize = 0;
            this.Btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_close.Location = new System.Drawing.Point(352, 6);
            this.Btn_close.Name = "Btn_close";
            this.Btn_close.Size = new System.Drawing.Size(18, 18);
            this.Btn_close.TabIndex = 7;
            this.Btn_close.UseVisualStyleBackColor = true;
            this.Btn_close.Click += new System.EventHandler(this.Btn_close_Click);
            // 
            // Panel
            // 
            this.Panel.Controls.Add(this.pictureBox1);
            this.Panel.Controls.Add(this.Btn_close);
            this.Panel.Controls.Add(this.ProgrammName);
            this.Panel.Controls.Add(this.Label_Email);
            this.Panel.Controls.Add(this.label_University);
            this.Panel.Controls.Add(this.Label_ContactUs);
            this.Panel.Controls.Add(this.label_LabName);
            this.Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel.Location = new System.Drawing.Point(0, 0);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(374, 174);
            this.Panel.TabIndex = 8;
            this.Panel.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Paint);
            this.Panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            this.Panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseMove);
            // 
            // AboutUs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.ClientSize = new System.Drawing.Size(374, 174);
            this.Controls.Add(this.Panel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AboutUs";
            this.ShadowStyle = CCWin.RoundStyle.None;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "关于我们";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.Panel.ResumeLayout(false);
            this.Panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ProgrammName;
        private System.Windows.Forms.Label label_University;
        private System.Windows.Forms.Label label_LabName;
        private System.Windows.Forms.Label Label_ContactUs;
        private System.Windows.Forms.Label Label_Email;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Btn_close;
        private System.Windows.Forms.Panel Panel;
    }
}