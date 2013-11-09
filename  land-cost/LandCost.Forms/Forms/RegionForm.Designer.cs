namespace LandCost.Forms
{
    partial class RegionForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.areaBox = new System.Windows.Forms.ComboBox();
            this.coefPanel = new System.Windows.Forms.GroupBox();
            this.polyBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.usagePanel = new System.Windows.Forms.Panel();
            this.statusLbl = new System.Windows.Forms.Label();
            this.priceBox = new LandCost.Forms.DecimalTextBox();
            this.numberBox = new LandCost.Forms.DecimalTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Номер району";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(308, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Економіко-планувальна зона";
            // 
            // areaBox
            // 
            this.areaBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.areaBox.FormattingEnabled = true;
            this.areaBox.Location = new System.Drawing.Point(469, 6);
            this.areaBox.Name = "areaBox";
            this.areaBox.Size = new System.Drawing.Size(137, 21);
            this.areaBox.Sorted = true;
            this.areaBox.TabIndex = 5;
            this.areaBox.SelectedIndexChanged += new System.EventHandler(this.areaBox_SelectedIndexChanged);
            // 
            // coefPanel
            // 
            this.coefPanel.Location = new System.Drawing.Point(311, 76);
            this.coefPanel.Name = "coefPanel";
            this.coefPanel.Size = new System.Drawing.Size(393, 288);
            this.coefPanel.TabIndex = 7;
            this.coefPanel.TabStop = false;
            this.coefPanel.Text = "Значення локальних коефіцієнтів";
            // 
            // polyBtn
            // 
            this.polyBtn.Location = new System.Drawing.Point(99, 33);
            this.polyBtn.Name = "polyBtn";
            this.polyBtn.Size = new System.Drawing.Size(110, 23);
            this.polyBtn.TabIndex = 0;
            this.polyBtn.Text = "Обрати";
            this.polyBtn.UseVisualStyleBackColor = true;
            this.polyBtn.Click += new System.EventHandler(this.polyBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(548, 370);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 9;
            this.saveBtn.Text = "Зберегти";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(629, 370);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 10;
            this.cancelBtn.Text = "Скасувати";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(308, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Середня вартість ділянки";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Район на карті";
            // 
            // usagePanel
            // 
            this.usagePanel.Location = new System.Drawing.Point(15, 76);
            this.usagePanel.Name = "usagePanel";
            this.usagePanel.Size = new System.Drawing.Size(290, 288);
            this.usagePanel.TabIndex = 14;
            // 
            // statusLbl
            // 
            this.statusLbl.Image = global::LandCost.Forms.Properties.Resources.error;
            this.statusLbl.Location = new System.Drawing.Point(215, 35);
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(32, 21);
            this.statusLbl.TabIndex = 1;
            // 
            // priceBox
            // 
            this.priceBox.Location = new System.Drawing.Point(469, 35);
            this.priceBox.Name = "priceBox";
            this.priceBox.Precision = 2;
            this.priceBox.ReadOnly = true;
            this.priceBox.Size = new System.Drawing.Size(137, 20);
            this.priceBox.TabIndex = 13;
            this.priceBox.Text = "0";
            this.priceBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.priceBox.Value = 0D;
            // 
            // numberBox
            // 
            this.numberBox.Location = new System.Drawing.Point(99, 6);
            this.numberBox.Name = "numberBox";
            this.numberBox.Precision = 0;
            this.numberBox.Size = new System.Drawing.Size(148, 20);
            this.numberBox.TabIndex = 3;
            this.numberBox.Text = "0";
            this.numberBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numberBox.Value = 0D;
            // 
            // RegionForm
            // 
            this.AcceptButton = this.saveBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(716, 405);
            this.Controls.Add(this.usagePanel);
            this.Controls.Add(this.priceBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.polyBtn);
            this.Controls.Add(this.statusLbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.coefPanel);
            this.Controls.Add(this.areaBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numberBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegionForm";
            this.ShowInTaskbar = false;
            this.Text = "Район";
            this.Load += new System.EventHandler(this.RegionForm_Load);
            this.Shown += new System.EventHandler(this.RegionForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DecimalTextBox numberBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox areaBox;
        private System.Windows.Forms.GroupBox coefPanel;
        private System.Windows.Forms.Label statusLbl;
        private System.Windows.Forms.Button polyBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private DecimalTextBox priceBox;
        private System.Windows.Forms.Panel usagePanel;
    }
}