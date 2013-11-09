namespace LandCost.Forms
{
    partial class LocalCoefficientValueForm
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
            this.coefBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.valBox = new LandCost.Forms.DecimalTextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Локальний коефіцієнт";
            // 
            // coefBox
            // 
            this.coefBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.coefBox.FormattingEnabled = true;
            this.coefBox.Location = new System.Drawing.Point(137, 6);
            this.coefBox.Name = "coefBox";
            this.coefBox.Size = new System.Drawing.Size(260, 21);
            this.coefBox.Sorted = true;
            this.coefBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Значення";
            // 
            // valBox
            // 
            this.valBox.Location = new System.Drawing.Point(137, 33);
            this.valBox.Name = "valBox";
            this.valBox.Precision = 2;
            this.valBox.Size = new System.Drawing.Size(75, 20);
            this.valBox.TabIndex = 4;
            this.valBox.Text = "1";
            this.valBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.valBox.Value = 1D;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(241, 62);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "Зберегти";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(322, 62);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Скасувати";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // LocalCoefficientValueForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(411, 96);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.valBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.coefBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LocalCoefficientValueForm";
            this.ShowInTaskbar = false;
            this.Text = "Значення локального коефіцієнта";
            this.Load += new System.EventHandler(this.LocalCoefficientValueForm_Load);
            this.Shown += new System.EventHandler(this.LocalCoefficientValueForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox coefBox;
        private System.Windows.Forms.Label label2;
        private DecimalTextBox valBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}