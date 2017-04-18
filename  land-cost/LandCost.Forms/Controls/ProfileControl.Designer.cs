using LandCost.Entities;
namespace LandCost.Forms
{
    partial class ProfileControl
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
            this.settingsTabControl = new System.Windows.Forms.TabControl();
            this.generalTab = new System.Windows.Forms.TabPage();
            this.loadXlsBtn = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.executorBox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chiefBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.indexCoefArableEdit = new LandCost.Forms.DecimalTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.indexCoefAgricultureEdit = new LandCost.Forms.DecimalTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.indexCoefEdit = new LandCost.Forms.DecimalTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.agencyAddressEdit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.agencyNameEdit = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.profileNameEdit = new System.Windows.Forms.TextBox();
            this.documentTab = new System.Windows.Forms.TabPage();
            this.usageTab = new System.Windows.Forms.TabPage();
            this.coefficientsTab = new System.Windows.Forms.TabPage();
            this.areaTab = new System.Windows.Forms.TabPage();
            this.regionTab = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.regionPanel = new System.Windows.Forms.Panel();
            this.mapPanel = new System.Windows.Forms.GroupBox();
            this.loadBtn = new System.Windows.Forms.Button();
            this.mapUnboundRegLabel = new System.Windows.Forms.Label();
            this.mapRegLabel = new System.Windows.Forms.Label();
            this.mapFileLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.categoryBox = new System.Windows.Forms.TextBox();
            this.loadMapDlg = new System.Windows.Forms.OpenFileDialog();
            this.xlsDialog = new System.Windows.Forms.OpenFileDialog();
            this.settingsTabControl.SuspendLayout();
            this.generalTab.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.regionTab.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.mapPanel.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsTabControl
            // 
            this.settingsTabControl.Controls.Add(this.generalTab);
            this.settingsTabControl.Controls.Add(this.documentTab);
            this.settingsTabControl.Controls.Add(this.usageTab);
            this.settingsTabControl.Controls.Add(this.coefficientsTab);
            this.settingsTabControl.Controls.Add(this.areaTab);
            this.settingsTabControl.Controls.Add(this.regionTab);
            this.settingsTabControl.Controls.Add(this.tabPage1);
            this.settingsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsTabControl.Location = new System.Drawing.Point(0, 0);
            this.settingsTabControl.Name = "settingsTabControl";
            this.settingsTabControl.SelectedIndex = 0;
            this.settingsTabControl.Size = new System.Drawing.Size(865, 474);
            this.settingsTabControl.TabIndex = 0;
            // 
            // generalTab
            // 
            this.generalTab.Controls.Add(this.loadXlsBtn);
            this.generalTab.Controls.Add(this.groupBox4);
            this.generalTab.Controls.Add(this.groupBox3);
            this.generalTab.Controls.Add(this.groupBox2);
            this.generalTab.Controls.Add(this.groupBox1);
            this.generalTab.Location = new System.Drawing.Point(4, 22);
            this.generalTab.Name = "generalTab";
            this.generalTab.Padding = new System.Windows.Forms.Padding(3);
            this.generalTab.Size = new System.Drawing.Size(857, 448);
            this.generalTab.TabIndex = 0;
            this.generalTab.Text = "Загальне";
            this.generalTab.UseVisualStyleBackColor = true;
            // 
            // loadXlsBtn
            // 
            this.loadXlsBtn.Location = new System.Drawing.Point(664, 311);
            this.loadXlsBtn.Name = "loadXlsBtn";
            this.loadXlsBtn.Size = new System.Drawing.Size(112, 23);
            this.loadXlsBtn.TabIndex = 10;
            this.loadXlsBtn.Text = "Завантажити XLS";
            this.loadXlsBtn.UseVisualStyleBackColor = true;
            this.loadXlsBtn.Click += new System.EventHandler(this.loadXlsBtn_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.executorBox);
            this.groupBox4.Location = new System.Drawing.Point(465, 144);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(311, 161);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Виконавці (один на рядок)";
            // 
            // executorBox
            // 
            this.executorBox.Location = new System.Drawing.Point(6, 19);
            this.executorBox.Multiline = true;
            this.executorBox.Name = "executorBox";
            this.executorBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.executorBox.Size = new System.Drawing.Size(299, 136);
            this.executorBox.TabIndex = 1;
            this.executorBox.Validating += new System.ComponentModel.CancelEventHandler(this.executorBox_Validating);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chiefBox);
            this.groupBox3.Location = new System.Drawing.Point(465, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(311, 132);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Керівники (один на рядок)";
            // 
            // chiefBox
            // 
            this.chiefBox.Location = new System.Drawing.Point(6, 19);
            this.chiefBox.Multiline = true;
            this.chiefBox.Name = "chiefBox";
            this.chiefBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.chiefBox.Size = new System.Drawing.Size(299, 97);
            this.chiefBox.TabIndex = 0;
            this.chiefBox.Validating += new System.ComponentModel.CancelEventHandler(this.chiefBox_Validating);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.indexCoefArableEdit);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.indexCoefAgricultureEdit);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.indexCoefEdit);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(6, 180);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(453, 125);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Загальнодержавні коефіцієнти";
            // 
            // indexCoefArableEdit
            // 
            this.indexCoefArableEdit.Location = new System.Drawing.Point(333, 82);
            this.indexCoefArableEdit.Name = "indexCoefArableEdit";
            this.indexCoefArableEdit.Precision = 4;
            this.indexCoefArableEdit.Size = new System.Drawing.Size(99, 20);
            this.indexCoefArableEdit.TabIndex = 5;
            this.indexCoefArableEdit.Text = "0";
            this.indexCoefArableEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.indexCoefArableEdit.Value = 0D;
            this.indexCoefArableEdit.Validating += new System.ComponentModel.CancelEventHandler(this.indexCoefArableEdit_Validating);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 85);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(159, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Коефіцієнт індексації для ріллі";
            // 
            // indexCoefAgricultureEdit
            // 
            this.indexCoefAgricultureEdit.Location = new System.Drawing.Point(333, 53);
            this.indexCoefAgricultureEdit.Name = "indexCoefAgricultureEdit";
            this.indexCoefAgricultureEdit.Precision = 4;
            this.indexCoefAgricultureEdit.Size = new System.Drawing.Size(99, 20);
            this.indexCoefAgricultureEdit.TabIndex = 3;
            this.indexCoefAgricultureEdit.Text = "0";
            this.indexCoefAgricultureEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.indexCoefAgricultureEdit.Value = 0D;
            this.indexCoefAgricultureEdit.Validating += new System.ComponentModel.CancelEventHandler(this.indexCoefAgricultureEdit_Validating);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(298, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Коефіцієнт індексації для сільськогосподарських земель";
            // 
            // indexCoefEdit
            // 
            this.indexCoefEdit.Location = new System.Drawing.Point(333, 25);
            this.indexCoefEdit.Name = "indexCoefEdit";
            this.indexCoefEdit.Precision = 4;
            this.indexCoefEdit.Size = new System.Drawing.Size(99, 20);
            this.indexCoefEdit.TabIndex = 1;
            this.indexCoefEdit.Text = "0";
            this.indexCoefEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.indexCoefEdit.Value = 0D;
            this.indexCoefEdit.Validating += new System.ComponentModel.CancelEventHandler(this.indexCoefEdit_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(310, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Коефіцієнт індексації для несільськогосподарських земель";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.agencyAddressEdit);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.agencyNameEdit);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.profileNameEdit);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(453, 168);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметри профілю та установи";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Назва установи, що здійснює оцінку";
            // 
            // agencyAddressEdit
            // 
            this.agencyAddressEdit.Location = new System.Drawing.Point(213, 125);
            this.agencyAddressEdit.Multiline = true;
            this.agencyAddressEdit.Name = "agencyAddressEdit";
            this.agencyAddressEdit.Size = new System.Drawing.Size(219, 37);
            this.agencyAddressEdit.TabIndex = 5;
            this.agencyAddressEdit.Validating += new System.ComponentModel.CancelEventHandler(this.agencyAddressEdit_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Назва профілю";
            // 
            // agencyNameEdit
            // 
            this.agencyNameEdit.Location = new System.Drawing.Point(213, 53);
            this.agencyNameEdit.Multiline = true;
            this.agencyNameEdit.Name = "agencyNameEdit";
            this.agencyNameEdit.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.agencyNameEdit.Size = new System.Drawing.Size(219, 63);
            this.agencyNameEdit.TabIndex = 4;
            this.agencyNameEdit.Validating += new System.ComponentModel.CancelEventHandler(this.agencyNameEdit_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(197, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Адреса установи, що здійснює оцінку";
            // 
            // profileNameEdit
            // 
            this.profileNameEdit.Location = new System.Drawing.Point(213, 27);
            this.profileNameEdit.Name = "profileNameEdit";
            this.profileNameEdit.Size = new System.Drawing.Size(219, 20);
            this.profileNameEdit.TabIndex = 3;
            this.profileNameEdit.Validating += new System.ComponentModel.CancelEventHandler(this.profileNameEdit_Validating);
            // 
            // documentTab
            // 
            this.documentTab.Location = new System.Drawing.Point(4, 22);
            this.documentTab.Name = "documentTab";
            this.documentTab.Padding = new System.Windows.Forms.Padding(3);
            this.documentTab.Size = new System.Drawing.Size(857, 448);
            this.documentTab.TabIndex = 1;
            this.documentTab.Text = "Правовстановлюючі документи";
            this.documentTab.UseVisualStyleBackColor = true;
            // 
            // usageTab
            // 
            this.usageTab.Location = new System.Drawing.Point(4, 22);
            this.usageTab.Name = "usageTab";
            this.usageTab.Padding = new System.Windows.Forms.Padding(3);
            this.usageTab.Size = new System.Drawing.Size(857, 448);
            this.usageTab.TabIndex = 2;
            this.usageTab.Text = "Функціональні призначення";
            this.usageTab.UseVisualStyleBackColor = true;
            // 
            // coefficientsTab
            // 
            this.coefficientsTab.Location = new System.Drawing.Point(4, 22);
            this.coefficientsTab.Name = "coefficientsTab";
            this.coefficientsTab.Padding = new System.Windows.Forms.Padding(3);
            this.coefficientsTab.Size = new System.Drawing.Size(857, 448);
            this.coefficientsTab.TabIndex = 3;
            this.coefficientsTab.Text = "Локальні коефіцієнти";
            this.coefficientsTab.UseVisualStyleBackColor = true;
            // 
            // areaTab
            // 
            this.areaTab.Location = new System.Drawing.Point(4, 22);
            this.areaTab.Name = "areaTab";
            this.areaTab.Padding = new System.Windows.Forms.Padding(3);
            this.areaTab.Size = new System.Drawing.Size(857, 448);
            this.areaTab.TabIndex = 6;
            this.areaTab.Text = "Економіко-планувальні зони";
            this.areaTab.UseVisualStyleBackColor = true;
            // 
            // regionTab
            // 
            this.regionTab.Controls.Add(this.tableLayoutPanel1);
            this.regionTab.Location = new System.Drawing.Point(4, 22);
            this.regionTab.Name = "regionTab";
            this.regionTab.Padding = new System.Windows.Forms.Padding(3);
            this.regionTab.Size = new System.Drawing.Size(857, 448);
            this.regionTab.TabIndex = 4;
            this.regionTab.Text = "Райони";
            this.regionTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.Controls.Add(this.regionPanel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.mapPanel, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(851, 442);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // regionPanel
            // 
            this.regionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.regionPanel.Location = new System.Drawing.Point(3, 3);
            this.regionPanel.Name = "regionPanel";
            this.regionPanel.Size = new System.Drawing.Size(545, 436);
            this.regionPanel.TabIndex = 0;
            // 
            // mapPanel
            // 
            this.mapPanel.Controls.Add(this.loadBtn);
            this.mapPanel.Controls.Add(this.mapUnboundRegLabel);
            this.mapPanel.Controls.Add(this.mapRegLabel);
            this.mapPanel.Controls.Add(this.mapFileLabel);
            this.mapPanel.Controls.Add(this.label7);
            this.mapPanel.Controls.Add(this.label6);
            this.mapPanel.Controls.Add(this.label5);
            this.mapPanel.Location = new System.Drawing.Point(554, 3);
            this.mapPanel.Name = "mapPanel";
            this.mapPanel.Size = new System.Drawing.Size(294, 146);
            this.mapPanel.TabIndex = 1;
            this.mapPanel.TabStop = false;
            this.mapPanel.Text = "Карта";
            // 
            // loadBtn
            // 
            this.loadBtn.Location = new System.Drawing.Point(20, 106);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(75, 23);
            this.loadBtn.TabIndex = 6;
            this.loadBtn.Text = "Завантажити";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.loadBtn_Click);
            // 
            // mapUnboundRegLabel
            // 
            this.mapUnboundRegLabel.AutoSize = true;
            this.mapUnboundRegLabel.Location = new System.Drawing.Point(139, 79);
            this.mapUnboundRegLabel.Name = "mapUnboundRegLabel";
            this.mapUnboundRegLabel.Size = new System.Drawing.Size(39, 13);
            this.mapUnboundRegLabel.TabIndex = 5;
            this.mapUnboundRegLabel.Text = "немає";
            // 
            // mapRegLabel
            // 
            this.mapRegLabel.AutoSize = true;
            this.mapRegLabel.Location = new System.Drawing.Point(139, 52);
            this.mapRegLabel.Name = "mapRegLabel";
            this.mapRegLabel.Size = new System.Drawing.Size(39, 13);
            this.mapRegLabel.TabIndex = 4;
            this.mapRegLabel.Text = "немає";
            // 
            // mapFileLabel
            // 
            this.mapFileLabel.AutoSize = true;
            this.mapFileLabel.Location = new System.Drawing.Point(139, 27);
            this.mapFileLabel.Name = "mapFileLabel";
            this.mapFileLabel.Size = new System.Drawing.Size(39, 13);
            this.mapFileLabel.TabIndex = 3;
            this.mapFileLabel.Text = "немає";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Неприв\'язані райони:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Райони:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Файл:";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(857, 448);
            this.tabPage1.TabIndex = 7;
            this.tabPage1.Text = "Категорії земель";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.categoryBox);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(3, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(851, 442);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Категорії (одна на рядок)";
            // 
            // categoryBox
            // 
            this.categoryBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.categoryBox.Location = new System.Drawing.Point(3, 16);
            this.categoryBox.Multiline = true;
            this.categoryBox.Name = "categoryBox";
            this.categoryBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.categoryBox.Size = new System.Drawing.Size(845, 423);
            this.categoryBox.TabIndex = 0;
            this.categoryBox.Validating += new System.ComponentModel.CancelEventHandler(this.categoryBox_Validating);
            // 
            // loadMapDlg
            // 
            this.loadMapDlg.DefaultExt = "dxf";
            this.loadMapDlg.Filter = "DXF Files | *.dxf";
            this.loadMapDlg.Title = "Завантажити карту";
            // 
            // xlsDialog
            // 
            this.xlsDialog.DefaultExt = "xls";
            this.xlsDialog.Filter = "файли XLS|*.xls";
            this.xlsDialog.Title = "Завантажити XLS";
            // 
            // ProfileControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.settingsTabControl);
            this.MinimumSize = new System.Drawing.Size(865, 315);
            this.Name = "ProfileControl";
            this.Size = new System.Drawing.Size(865, 474);
            this.Validated += new System.EventHandler(this.ProfileControl_Validated);
            this.settingsTabControl.ResumeLayout(false);
            this.generalTab.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.regionTab.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.mapPanel.ResumeLayout(false);
            this.mapPanel.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl settingsTabControl;
        private System.Windows.Forms.TabPage generalTab;
        private System.Windows.Forms.TabPage documentTab;
        private System.Windows.Forms.TextBox agencyAddressEdit;
        private System.Windows.Forms.TextBox agencyNameEdit;
        private System.Windows.Forms.TextBox profileNameEdit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private DecimalTextBox indexCoefEdit;
        private System.Windows.Forms.TabPage usageTab;
        private System.Windows.Forms.TabPage coefficientsTab;
        private System.Windows.Forms.TabPage regionTab;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel regionPanel;
        private System.Windows.Forms.GroupBox mapPanel;
        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.Label mapUnboundRegLabel;
        private System.Windows.Forms.Label mapRegLabel;
        private System.Windows.Forms.Label mapFileLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.OpenFileDialog loadMapDlg;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox executorBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox chiefBox;
        private System.Windows.Forms.TabPage areaTab;
        private System.Windows.Forms.Button loadXlsBtn;
        private System.Windows.Forms.OpenFileDialog xlsDialog;
        private DecimalTextBox indexCoefAgricultureEdit;
        private System.Windows.Forms.Label label8;
        private DecimalTextBox indexCoefArableEdit;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox categoryBox;
    }
}
