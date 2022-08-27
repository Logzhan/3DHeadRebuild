namespace ThreeDHeadRebuild
{
    partial class MiniForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MiniForm));
            this.University = new System.Windows.Forms.PictureBox();
            this.SoftWareName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label_Owner = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.University)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // University
            // 
            this.University.Image = global::ThreeDHeadRebuild.Properties.Resources.Usetc;
            this.University.InitialImage = ((System.Drawing.Image)(resources.GetObject("University.InitialImage")));
            this.University.Location = new System.Drawing.Point(14, 181);
            this.University.Name = "University";
            this.University.Size = new System.Drawing.Size(48, 48);
            this.University.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.University.TabIndex = 0;
            this.University.TabStop = false;
            // 
            // SoftWareName
            // 
            this.SoftWareName.AutoSize = true;
            this.SoftWareName.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SoftWareName.Location = new System.Drawing.Point(119, 17);
            this.SoftWareName.Name = "SoftWareName";
            this.SoftWareName.Size = new System.Drawing.Size(180, 28);
            this.SoftWareName.TabIndex = 1;
            this.SoftWareName.Text = "三维人头重建软件";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ThreeDHeadRebuild.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(6, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 97);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // Label_Owner
            // 
            this.Label_Owner.AutoSize = true;
            this.Label_Owner.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Owner.Location = new System.Drawing.Point(66, 188);
            this.Label_Owner.Name = "Label_Owner";
            this.Label_Owner.Size = new System.Drawing.Size(80, 17);
            this.Label_Owner.TabIndex = 3;
            this.Label_Owner.Text = "本程序所有权";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(124, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Version v 1.0.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(124, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "http://www.avc2-lab.net/";
            // 
            // MiniForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 244);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Label_Owner);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.SoftWareName);
            this.Controls.Add(this.University);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MiniForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MiniForm";
            this.Load += new System.EventHandler(this.MiniForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.University)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox University;
        private System.Windows.Forms.Label SoftWareName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label Label_Owner;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

    }
}