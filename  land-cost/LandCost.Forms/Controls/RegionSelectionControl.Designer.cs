namespace LandCost.Forms
{
    partial class RegionSelectionControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.regionBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.usageBox = new System.Windows.Forms.ComboBox();
            this.coefPanel = new System.Windows.Forms.GroupBox();
            this.coefValsCtl = new LandCost.Forms.CoefficientValueSetControl();
            this.coefPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Район";
            // 
            // regionBox
            // 
            this.regionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.regionBox.FormattingEnabled = true;
            this.regionBox.Location = new System.Drawing.Point(162, 7);
            this.regionBox.Name = "regionBox";
            this.regionBox.Size = new System.Drawing.Size(225, 21);
            this.regionBox.TabIndex = 2;
            this.regionBox.SelectedIndexChanged += new System.EventHandler(this.regionBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Функціональне призначення";
            // 
            // usageBox
            // 
            this.usageBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.usageBox.FormattingEnabled = true;
            this.usageBox.Location = new System.Drawing.Point(162, 34);
            this.usageBox.Name = "usageBox";
            this.usageBox.Size = new System.Drawing.Size(225, 21);
            this.usageBox.TabIndex = 4;
            this.usageBox.SelectedIndexChanged += new System.EventHandler(this.usageBox_SelectedIndexChanged);
            // 
            // coefPanel
            // 
            this.coefPanel.Controls.Add(this.coefValsCtl);
            this.coefPanel.Location = new System.Drawing.Point(0, 72);
            this.coefPanel.Name = "coefPanel";
            this.coefPanel.Size = new System.Drawing.Size(390, 272);
            this.coefPanel.TabIndex = 5;
            this.coefPanel.TabStop = false;
            this.coefPanel.Text = "Локальні коефіцієнти";
            // 
            // coefValsCtl
            // 
            this.coefValsCtl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.coefValsCtl.List = null;
            this.coefValsCtl.Location = new System.Drawing.Point(3, 16);
            this.coefValsCtl.Name = "coefValsCtl";
            this.coefValsCtl.ReadOnly = false;
            this.coefValsCtl.Size = new System.Drawing.Size(384, 253);
            this.coefValsCtl.TabIndex = 0;
            // 
            // RegionSelectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.coefPanel);
            this.Controls.Add(this.usageBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.regionBox);
            this.Controls.Add(this.label1);
            this.Name = "RegionSelectionControl";
            this.Size = new System.Drawing.Size(390, 347);
            this.coefPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox regionBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox usageBox;
        private System.Windows.Forms.GroupBox coefPanel;
        private CoefficientValueSetControl coefValsCtl;
    }
}
