namespace ThreeDHeadRebuild
{
    partial class RenderSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RenderSetting));
            this.Label_CamerHeight = new System.Windows.Forms.Label();
            this.TrackBar_CamerHeight = new System.Windows.Forms.TrackBar();
            this.ChkBox_LineDisplay = new System.Windows.Forms.CheckBox();
            this.ChkBox_CloseLight = new System.Windows.Forms.CheckBox();
            this.ChkBox_CloseTexture = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Tbr_Model_Height = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar_CamerHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tbr_Model_Height)).BeginInit();
            this.SuspendLayout();
            // 
            // Label_CamerHeight
            // 
            this.Label_CamerHeight.AutoSize = true;
            this.Label_CamerHeight.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_CamerHeight.Location = new System.Drawing.Point(8, 17);
            this.Label_CamerHeight.Name = "Label_CamerHeight";
            this.Label_CamerHeight.Size = new System.Drawing.Size(79, 20);
            this.Label_CamerHeight.TabIndex = 0;
            this.Label_CamerHeight.Text = "摄像机高度";
            // 
            // TrackBar_CamerHeight
            // 
            this.TrackBar_CamerHeight.Location = new System.Drawing.Point(84, 13);
            this.TrackBar_CamerHeight.Maximum = 300;
            this.TrackBar_CamerHeight.Minimum = -300;
            this.TrackBar_CamerHeight.Name = "TrackBar_CamerHeight";
            this.TrackBar_CamerHeight.Size = new System.Drawing.Size(205, 45);
            this.TrackBar_CamerHeight.TabIndex = 2;
            this.TrackBar_CamerHeight.Scroll += new System.EventHandler(this.TrackBar_CamerHeight_Scroll);
            // 
            // ChkBox_LineDisplay
            // 
            this.ChkBox_LineDisplay.AutoSize = true;
            this.ChkBox_LineDisplay.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChkBox_LineDisplay.Location = new System.Drawing.Point(11, 64);
            this.ChkBox_LineDisplay.Name = "ChkBox_LineDisplay";
            this.ChkBox_LineDisplay.Size = new System.Drawing.Size(75, 21);
            this.ChkBox_LineDisplay.TabIndex = 3;
            this.ChkBox_LineDisplay.Text = "线框显示";
            this.ChkBox_LineDisplay.UseVisualStyleBackColor = true;
            this.ChkBox_LineDisplay.CheckedChanged += new System.EventHandler(this.ChkBox_LineDisplay_CheckedChanged);
            // 
            // ChkBox_CloseLight
            // 
            this.ChkBox_CloseLight.AutoSize = true;
            this.ChkBox_CloseLight.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChkBox_CloseLight.Location = new System.Drawing.Point(90, 65);
            this.ChkBox_CloseLight.Name = "ChkBox_CloseLight";
            this.ChkBox_CloseLight.Size = new System.Drawing.Size(75, 21);
            this.ChkBox_CloseLight.TabIndex = 4;
            this.ChkBox_CloseLight.Text = "关闭光照";
            this.ChkBox_CloseLight.UseVisualStyleBackColor = true;
            this.ChkBox_CloseLight.CheckedChanged += new System.EventHandler(this.ChkBox_CloseLight_CheckedChanged);
            // 
            // ChkBox_CloseTexture
            // 
            this.ChkBox_CloseTexture.AutoSize = true;
            this.ChkBox_CloseTexture.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChkBox_CloseTexture.Location = new System.Drawing.Point(168, 65);
            this.ChkBox_CloseTexture.Name = "ChkBox_CloseTexture";
            this.ChkBox_CloseTexture.Size = new System.Drawing.Size(99, 21);
            this.ChkBox_CloseTexture.TabIndex = 5;
            this.ChkBox_CloseTexture.Text = "关闭纹理贴图";
            this.ChkBox_CloseTexture.UseVisualStyleBackColor = true;
            this.ChkBox_CloseTexture.CheckedChanged += new System.EventHandler(this.ChkBox_CloseTexture_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(9, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "模型高度";
            // 
            // Tbr_Model_Height
            // 
            this.Tbr_Model_Height.Location = new System.Drawing.Point(90, 103);
            this.Tbr_Model_Height.Maximum = 300;
            this.Tbr_Model_Height.Minimum = -300;
            this.Tbr_Model_Height.Name = "Tbr_Model_Height";
            this.Tbr_Model_Height.Size = new System.Drawing.Size(199, 45);
            this.Tbr_Model_Height.TabIndex = 7;
            this.Tbr_Model_Height.Scroll += new System.EventHandler(this.Tbr_Model_Height_Scroll);
            // 
            // RenderSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(301, 330);
            this.Controls.Add(this.Tbr_Model_Height);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ChkBox_CloseTexture);
            this.Controls.Add(this.ChkBox_CloseLight);
            this.Controls.Add(this.ChkBox_LineDisplay);
            this.Controls.Add(this.TrackBar_CamerHeight);
            this.Controls.Add(this.Label_CamerHeight);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RenderSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "渲染设置";
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar_CamerHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tbr_Model_Height)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_CamerHeight;
        private System.Windows.Forms.TrackBar TrackBar_CamerHeight;
        private System.Windows.Forms.CheckBox ChkBox_LineDisplay;
        private System.Windows.Forms.CheckBox ChkBox_CloseLight;
        private System.Windows.Forms.CheckBox ChkBox_CloseTexture;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar Tbr_Model_Height;
    }
}