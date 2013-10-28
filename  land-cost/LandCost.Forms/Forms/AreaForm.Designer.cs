namespace LandCost.Forms
{
    partial class AreaForm
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
            this.numberBox = new LandCost.Forms.DecimalTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.coefBox = new LandCost.Forms.DecimalTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.priceBox = new LandCost.Forms.DecimalTextBox();
            this.regionPanel = new System.Windows.Forms.GroupBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Номер зони";
            // 
            // numberBox
            // 
            this.numberBox.Location = new System.Drawing.Point(258, 6);
            this.numberBox.Name = "numberBox";
            this.numberBox.Precision = 0;
            this.numberBox.Size = new System.Drawing.Size(152, 20);
            this.numberBox.TabIndex = 1;
            this.numberBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Коефіцієнт Км2";
            // 
            // coefBox
            // 
            this.coefBox.Location = new System.Drawing.Point(258, 32);
            this.coefBox.Name = "coefBox";
            this.coefBox.Precision = 2;
            this.coefBox.Size = new System.Drawing.Size(152, 20);
            this.coefBox.TabIndex = 3;
            this.coefBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(240, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Середня вартість земельної ділянки, грн / м2";
            // 
            // priceBox
            // 
            this.priceBox.Location = new System.Drawing.Point(258, 58);
            this.priceBox.Name = "priceBox";
            this.priceBox.Precision = 2;
            this.priceBox.Size = new System.Drawing.Size(152, 20);
            this.priceBox.TabIndex = 5;
            this.priceBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // regionPanel
            // 
            this.regionPanel.Location = new System.Drawing.Point(15, 88);
            this.regionPanel.Name = "regionPanel";
            this.regionPanel.Size = new System.Drawing.Size(395, 175);
            this.regionPanel.TabIndex = 6;
            this.regionPanel.TabStop = false;
            this.regionPanel.Text = "Райони";
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(254, 269);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 7;
            this.saveBtn.Text = "Зберегти";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(335, 269);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 8;
            this.cancelBtn.Text = "Скасувати";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // AreaForm
            // 
            this.AcceptButton = this.saveBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(431, 305);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.regionPanel);
            this.Controls.Add(this.priceBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.coefBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numberBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AreaForm";
            this.ShowInTaskbar = false;
            this.Text = "Економіко-планувальна зона";
            this.Load += new System.EventHandler(this.AreaForm_Load);
            this.Shown += new System.EventHandler(this.AreaForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DecimalTextBox numberBox;
        private System.Windows.Forms.Label label2;
        private DecimalTextBox coefBox;
        private System.Windows.Forms.Label label3;
        private DecimalTextBox priceBox;
        private System.Windows.Forms.GroupBox regionPanel;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button cancelBtn;
    }
}