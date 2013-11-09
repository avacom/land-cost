namespace LandCost.Forms
{
    partial class FunctionalUsageCoefficientsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.usageBox = new System.Windows.Forms.ComboBox();
            this.coefPanel = new System.Windows.Forms.GroupBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Функціональне призначення";
            // 
            // usageBox
            // 
            this.usageBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.usageBox.FormattingEnabled = true;
            this.usageBox.Location = new System.Drawing.Point(171, 6);
            this.usageBox.Name = "usageBox";
            this.usageBox.Size = new System.Drawing.Size(255, 21);
            this.usageBox.Sorted = true;
            this.usageBox.TabIndex = 1;
            // 
            // coefPanel
            // 
            this.coefPanel.Location = new System.Drawing.Point(15, 33);
            this.coefPanel.Name = "coefPanel";
            this.coefPanel.Size = new System.Drawing.Size(411, 304);
            this.coefPanel.TabIndex = 2;
            this.coefPanel.TabStop = false;
            this.coefPanel.Text = "Локальні коефіцієнти";
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(270, 343);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 3;
            this.saveBtn.Text = "Зберегти";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(351, 343);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 4;
            this.cancelBtn.Text = "Скасувати";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // FunctionalUsageCoefficientsForm
            // 
            this.AcceptButton = this.saveBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(440, 379);
            this.Controls.Add(this.coefPanel);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.usageBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FunctionalUsageCoefficientsForm";
            this.ShowInTaskbar = false;
            this.Text = "Набір коефіцієнтів для функціонального призначення";
            this.Load += new System.EventHandler(this.FunctionalUsageCoefficientsForm_Load);
            this.Shown += new System.EventHandler(this.FunctionalUsageCoefficientsForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox usageBox;
        private System.Windows.Forms.GroupBox coefPanel;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button cancelBtn;
    }
}