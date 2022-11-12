namespace VRCOSCDataHub
{
    partial class DataHubDesign
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataHubDesign));
            this.label1 = new System.Windows.Forms.Label();
            this.PortNumberChanger = new System.Windows.Forms.NumericUpDown();
            this.ServerToggle = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.PortNumberChanger)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port:";
            // 
            // PortNumberChanger
            // 
            this.PortNumberChanger.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.PortNumberChanger.ForeColor = System.Drawing.Color.White;
            this.PortNumberChanger.Location = new System.Drawing.Point(47, 15);
            this.PortNumberChanger.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.PortNumberChanger.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PortNumberChanger.Name = "PortNumberChanger";
            this.PortNumberChanger.Size = new System.Drawing.Size(234, 20);
            this.PortNumberChanger.TabIndex = 1;
            this.PortNumberChanger.Value = new decimal(new int[] {
            9002,
            0,
            0,
            0});
            // 
            // ServerToggle
            // 
            this.ServerToggle.AutoSize = true;
            this.ServerToggle.Location = new System.Drawing.Point(15, 41);
            this.ServerToggle.Name = "ServerToggle";
            this.ServerToggle.Size = new System.Drawing.Size(56, 17);
            this.ServerToggle.TabIndex = 2;
            this.ServerToggle.Text = "Active";
            this.ServerToggle.UseVisualStyleBackColor = true;
            this.ServerToggle.CheckedChanged += new System.EventHandler(this.ServerToggle_CheckedChanged);
            // 
            // DataHubDesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(296, 69);
            this.Controls.Add(this.ServerToggle);
            this.Controls.Add(this.PortNumberChanger);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DataHubDesign";
            this.Text = "VRC OSC Data Hub";
            this.Load += new System.EventHandler(this.DataHubDesign_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PortNumberChanger)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown PortNumberChanger;
        private System.Windows.Forms.CheckBox ServerToggle;
    }
}

