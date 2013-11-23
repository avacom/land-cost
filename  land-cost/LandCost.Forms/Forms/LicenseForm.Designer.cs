namespace LandCost.Forms
{
    partial class LicenseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LicenseForm));
            this.infoLbl = new System.Windows.Forms.Label();
            this.serialBox = new System.Windows.Forms.TextBox();
            this.exitButton = new System.Windows.Forms.Button();
            this.mainMessageLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // infoLbl
            // 
            this.infoLbl.AutoSize = true;
            this.infoLbl.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.infoLbl.Location = new System.Drawing.Point(9, 34);
            this.infoLbl.Name = "infoLbl";
            this.infoLbl.Size = new System.Drawing.Size(514, 140);
            this.infoLbl.TabIndex = 0;
            this.infoLbl.Text = resources.GetString("infoLbl.Text");
            // 
            // serialBox
            // 
            this.serialBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.serialBox.Location = new System.Drawing.Point(12, 189);
            this.serialBox.Name = "serialBox";
            this.serialBox.ReadOnly = true;
            this.serialBox.Size = new System.Drawing.Size(529, 22);
            this.serialBox.TabIndex = 1;
            this.serialBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(469, 217);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 2;
            this.exitButton.Text = "Вихід";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // mainMessageLbl
            // 
            this.mainMessageLbl.AutoSize = true;
            this.mainMessageLbl.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mainMessageLbl.Location = new System.Drawing.Point(9, 9);
            this.mainMessageLbl.Name = "mainMessageLbl";
            this.mainMessageLbl.Size = new System.Drawing.Size(383, 14);
            this.mainMessageLbl.TabIndex = 0;
            this.mainMessageLbl.Text = "Дана копія продукту LandCost ще не була зареєстрована. ";
            // 
            // LicenseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 255);
            this.Controls.Add(this.mainMessageLbl);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.serialBox);
            this.Controls.Add(this.infoLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LicenseForm";
            this.ShowInTaskbar = false;
            this.Text = "LandCost: незареєстрована копія";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label infoLbl;
        private System.Windows.Forms.TextBox serialBox;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label mainMessageLbl;
    }
}