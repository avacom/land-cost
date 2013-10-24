namespace LandCost.Forms
{
    partial class CoefficientValueControl
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
            this.nameBox = new System.Windows.Forms.TextBox();
            this.valueBox = new LandCost.Forms.DecimalTextBox();
            this.SuspendLayout();
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(0, 0);
            this.nameBox.Name = "nameBox";
            this.nameBox.ReadOnly = true;
            this.nameBox.Size = new System.Drawing.Size(292, 20);
            this.nameBox.TabIndex = 0;
            // 
            // valueBox
            // 
            this.valueBox.Location = new System.Drawing.Point(298, 0);
            this.valueBox.Name = "valueBox";
            this.valueBox.Precision = 3;
            this.valueBox.Size = new System.Drawing.Size(59, 20);
            this.valueBox.TabIndex = 1;
            this.valueBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.valueBox.Validating += new System.ComponentModel.CancelEventHandler(this.valueBox_Validating);
            // 
            // CoefficientValueControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.valueBox);
            this.Controls.Add(this.nameBox);
            this.Name = "CoefficientValueControl";
            this.Size = new System.Drawing.Size(360, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nameBox;
        private DecimalTextBox valueBox;
    }
}
