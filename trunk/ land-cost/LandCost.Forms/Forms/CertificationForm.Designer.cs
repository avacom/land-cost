namespace LandCost.Forms
{
    partial class CertificationForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.ownerBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ownerLocationBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.addressBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.landNameBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.docAttribBox = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.areaBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.coefBox = new System.Windows.Forms.GroupBox();
            this.coefValSetCtl = new LandCost.Forms.CoefficientValueSetControl();
            this.label11 = new System.Windows.Forms.Label();
            this.oneMoreCheck = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.evalM2Box2 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.evalBox2 = new System.Windows.Forms.TextBox();
            this.evalBox = new System.Windows.Forms.TextBox();
            this.evalTotalBox2 = new System.Windows.Forms.TextBox();
            this.evalTotalBox = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.executorBox = new System.Windows.Forms.ComboBox();
            this.chiefBox = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.dateBox = new System.Windows.Forms.DateTimePicker();
            this.docBox = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.evalM2Box = new System.Windows.Forms.TextBox();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.довідкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.pdfMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.printMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.fileLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.processLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.exportPDFDialog = new System.Windows.Forms.SaveFileDialog();
            this.totalSquareBox = new LandCost.Forms.DecimalTextBox();
            this.buildSquareBox = new LandCost.Forms.DecimalTextBox();
            this.indexCoefBox = new LandCost.Forms.DecimalTextBox();
            this.kfBox2 = new LandCost.Forms.DecimalTextBox();
            this.kfBox = new LandCost.Forms.DecimalTextBox();
            this.km3Box = new LandCost.Forms.DecimalTextBox();
            this.priceBox = new LandCost.Forms.DecimalTextBox();
            this.km2Box = new LandCost.Forms.DecimalTextBox();
            this.squareBox = new LandCost.Forms.DecimalTextBox();
            this.coefBox.SuspendLayout();
            this.menu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Власник (користувач)";
            // 
            // ownerBox
            // 
            this.ownerBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ownerBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ownerBox.Location = new System.Drawing.Point(185, 59);
            this.ownerBox.Name = "ownerBox";
            this.ownerBox.Size = new System.Drawing.Size(225, 20);
            this.ownerBox.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(419, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 26);
            this.label3.TabIndex = 5;
            this.label3.Text = "Місцезнаходження власника\r\n(користувача)";
            // 
            // ownerLocationBox
            // 
            this.ownerLocationBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ownerLocationBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ownerLocationBox.Location = new System.Drawing.Point(579, 59);
            this.ownerLocationBox.Name = "ownerLocationBox";
            this.ownerLocationBox.Size = new System.Drawing.Size(210, 20);
            this.ownerLocationBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(162, 26);
            this.label4.TabIndex = 7;
            this.label4.Text = "Місцезнаходження земельної \r\nділянки";
            // 
            // addressBox
            // 
            this.addressBox.Location = new System.Drawing.Point(185, 92);
            this.addressBox.Name = "addressBox";
            this.addressBox.Size = new System.Drawing.Size(225, 20);
            this.addressBox.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(419, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Назва земельної ділянки";
            // 
            // landNameBox
            // 
            this.landNameBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.landNameBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.landNameBox.Location = new System.Drawing.Point(579, 92);
            this.landNameBox.Name = "landNameBox";
            this.landNameBox.Size = new System.Drawing.Size(210, 20);
            this.landNameBox.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Площа земельної ділянки";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(419, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(146, 26);
            this.label7.TabIndex = 13;
            this.label7.Text = "Документ, що встановлює \r\nправо на землю";
            // 
            // docAttribBox
            // 
            this.docAttribBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.docAttribBox.Location = new System.Drawing.Point(579, 154);
            this.docAttribBox.Name = "docAttribBox";
            this.docAttribBox.Size = new System.Drawing.Size(210, 20);
            this.docAttribBox.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 157);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(155, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Економіко-планувальна зона";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(419, 189);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Коефіцієнт Км2";
            // 
            // areaBox
            // 
            this.areaBox.Location = new System.Drawing.Point(185, 154);
            this.areaBox.Name = "areaBox";
            this.areaBox.ReadOnly = true;
            this.areaBox.Size = new System.Drawing.Size(99, 20);
            this.areaBox.TabIndex = 18;
            this.areaBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 189);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(154, 26);
            this.label10.TabIndex = 20;
            this.label10.Text = "Середня вартість земельної \r\nділянки";
            // 
            // coefBox
            // 
            this.coefBox.Controls.Add(this.coefValSetCtl);
            this.coefBox.Location = new System.Drawing.Point(16, 228);
            this.coefBox.Name = "coefBox";
            this.coefBox.Size = new System.Drawing.Size(397, 205);
            this.coefBox.TabIndex = 22;
            this.coefBox.TabStop = false;
            this.coefBox.Text = "Локальні коефіцієнти";
            // 
            // coefValSetCtl
            // 
            this.coefValSetCtl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.coefValSetCtl.List = null;
            this.coefValSetCtl.Location = new System.Drawing.Point(3, 16);
            this.coefValSetCtl.Name = "coefValSetCtl";
            this.coefValSetCtl.ReadOnly = false;
            this.coefValSetCtl.Size = new System.Drawing.Size(391, 186);
            this.coefValSetCtl.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(419, 228);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(134, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Сукупний коефіцієнт Км3";
            // 
            // oneMoreCheck
            // 
            this.oneMoreCheck.AutoSize = true;
            this.oneMoreCheck.Location = new System.Drawing.Point(422, 276);
            this.oneMoreCheck.Name = "oneMoreCheck";
            this.oneMoreCheck.Size = new System.Drawing.Size(367, 17);
            this.oneMoreCheck.TabIndex = 26;
            this.oneMoreCheck.Text = "Ділянка зайнята поточним або відведена під майбутнє будівництво";
            this.oneMoreCheck.UseVisualStyleBackColor = true;
            this.oneMoreCheck.CheckedChanged += new System.EventHandler(this.oneMoreCheck_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 454);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "Коефіцієнт Кф";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(419, 329);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(123, 39);
            this.label13.TabIndex = 32;
            this.label13.Text = "Коефіцієнт індексації \r\nнормативної грошової \r\nоцінки К(і)";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(13, 483);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(151, 39);
            this.label14.TabIndex = 34;
            this.label14.Text = "Нормативна грошова оцінка\r\nм2 земельної ділянки під\r\nзабудовою, грн";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(419, 379);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(157, 13);
            this.label15.TabIndex = 35;
            this.label15.Text = "Площа з/д під забудовою, м2";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(419, 420);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(152, 13);
            this.label16.TabIndex = 37;
            this.label16.Text = "Загальна площа земель, м2";
            // 
            // evalM2Box2
            // 
            this.evalM2Box2.Enabled = false;
            this.evalM2Box2.Location = new System.Drawing.Point(501, 483);
            this.evalM2Box2.Name = "evalM2Box2";
            this.evalM2Box2.ReadOnly = true;
            this.evalM2Box2.Size = new System.Drawing.Size(117, 20);
            this.evalM2Box2.TabIndex = 40;
            this.evalM2Box2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 533);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(151, 39);
            this.label17.TabIndex = 41;
            this.label17.Text = "Нормативна грошова оцінка\r\nземельної ділянки під\r\nзабудовою, грн";
            // 
            // evalBox2
            // 
            this.evalBox2.Enabled = false;
            this.evalBox2.Location = new System.Drawing.Point(501, 530);
            this.evalBox2.Name = "evalBox2";
            this.evalBox2.ReadOnly = true;
            this.evalBox2.Size = new System.Drawing.Size(117, 20);
            this.evalBox2.TabIndex = 43;
            this.evalBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // evalBox
            // 
            this.evalBox.Location = new System.Drawing.Point(167, 530);
            this.evalBox.Name = "evalBox";
            this.evalBox.ReadOnly = true;
            this.evalBox.Size = new System.Drawing.Size(117, 20);
            this.evalBox.TabIndex = 42;
            this.evalBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // evalTotalBox2
            // 
            this.evalTotalBox2.Enabled = false;
            this.evalTotalBox2.Location = new System.Drawing.Point(501, 585);
            this.evalTotalBox2.Name = "evalTotalBox2";
            this.evalTotalBox2.ReadOnly = true;
            this.evalTotalBox2.Size = new System.Drawing.Size(117, 20);
            this.evalTotalBox2.TabIndex = 46;
            this.evalTotalBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // evalTotalBox
            // 
            this.evalTotalBox.Location = new System.Drawing.Point(167, 585);
            this.evalTotalBox.Name = "evalTotalBox";
            this.evalTotalBox.ReadOnly = true;
            this.evalTotalBox.Size = new System.Drawing.Size(117, 20);
            this.evalTotalBox.TabIndex = 45;
            this.evalTotalBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(13, 588);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(151, 26);
            this.label18.TabIndex = 44;
            this.label18.Text = "Нормативна грошова оцінка\r\nземельної ділянки, грн";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(12, 628);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(104, 13);
            this.label19.TabIndex = 47;
            this.label19.Text = "Довідку склав (-ла)";
            // 
            // executorBox
            // 
            this.executorBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.executorBox.FormattingEnabled = true;
            this.executorBox.Location = new System.Drawing.Point(167, 625);
            this.executorBox.Name = "executorBox";
            this.executorBox.Size = new System.Drawing.Size(213, 21);
            this.executorBox.TabIndex = 48;
            // 
            // chiefBox
            // 
            this.chiefBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.chiefBox.FormattingEnabled = true;
            this.chiefBox.Location = new System.Drawing.Point(579, 625);
            this.chiefBox.Name = "chiefBox";
            this.chiefBox.Size = new System.Drawing.Size(211, 21);
            this.chiefBox.TabIndex = 50;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(419, 628);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(124, 13);
            this.label20.TabIndex = 49;
            this.label20.Text = "Довідку перевірив (-ла)";
            // 
            // dateBox
            // 
            this.dateBox.Location = new System.Drawing.Point(349, 33);
            this.dateBox.Name = "dateBox";
            this.dateBox.Size = new System.Drawing.Size(155, 20);
            this.dateBox.TabIndex = 54;
            // 
            // docBox
            // 
            this.docBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.docBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.docBox.FormattingEnabled = true;
            this.docBox.Location = new System.Drawing.Point(579, 127);
            this.docBox.Name = "docBox";
            this.docBox.Size = new System.Drawing.Size(210, 21);
            this.docBox.TabIndex = 55;
            this.docBox.SelectedIndexChanged += new System.EventHandler(this.docBox_SelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(419, 157);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(110, 13);
            this.label21.TabIndex = 56;
            this.label21.Text = "Атрибути документа";
            // 
            // evalM2Box
            // 
            this.evalM2Box.Location = new System.Drawing.Point(167, 483);
            this.evalM2Box.Name = "evalM2Box";
            this.evalM2Box.ReadOnly = true;
            this.evalM2Box.Size = new System.Drawing.Size(117, 20);
            this.evalM2Box.TabIndex = 39;
            this.evalM2Box.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.довідкаToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(804, 24);
            this.menu.TabIndex = 57;
            this.menu.Text = "menuStrip1";
            // 
            // довідкаToolStripMenuItem
            // 
            this.довідкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveMenu,
            this.saveAsMenu,
            this.pdfMenu,
            this.printMenu,
            this.toolStripMenuItem1,
            this.closeMenu});
            this.довідкаToolStripMenuItem.Name = "довідкаToolStripMenuItem";
            this.довідкаToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.довідкаToolStripMenuItem.Text = "Довідка";
            // 
            // saveMenu
            // 
            this.saveMenu.Image = global::LandCost.Forms.Properties.Resources.save;
            this.saveMenu.Name = "saveMenu";
            this.saveMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveMenu.Size = new System.Drawing.Size(181, 22);
            this.saveMenu.Text = "Зберегти";
            this.saveMenu.Click += new System.EventHandler(this.saveMenu_Click);
            // 
            // saveAsMenu
            // 
            this.saveAsMenu.Name = "saveAsMenu";
            this.saveAsMenu.Size = new System.Drawing.Size(181, 22);
            this.saveAsMenu.Text = "Зберегти як...";
            this.saveAsMenu.Click += new System.EventHandler(this.saveAsMenu_Click);
            // 
            // pdfMenu
            // 
            this.pdfMenu.Name = "pdfMenu";
            this.pdfMenu.Size = new System.Drawing.Size(181, 22);
            this.pdfMenu.Text = "Експортувати в PDF";
            this.pdfMenu.Click += new System.EventHandler(this.pdfMenu_Click);
            // 
            // printMenu
            // 
            this.printMenu.Image = global::LandCost.Forms.Properties.Resources.print;
            this.printMenu.Name = "printMenu";
            this.printMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.printMenu.Size = new System.Drawing.Size(181, 22);
            this.printMenu.Text = "Друкувати";
            this.printMenu.Click += new System.EventHandler(this.printMenu_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(178, 6);
            // 
            // closeMenu
            // 
            this.closeMenu.Image = global::LandCost.Forms.Properties.Resources.exit;
            this.closeMenu.Name = "closeMenu";
            this.closeMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.closeMenu.Size = new System.Drawing.Size(181, 22);
            this.closeMenu.Text = "Закрити";
            this.closeMenu.Click += new System.EventHandler(this.closeMenu_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileLabel,
            this.processLbl});
            this.statusStrip1.Location = new System.Drawing.Point(0, 662);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(804, 22);
            this.statusStrip1.Stretch = false;
            this.statusStrip1.TabIndex = 58;
            this.statusStrip1.Text = "statusBar";
            // 
            // fileLabel
            // 
            this.fileLabel.AutoSize = false;
            this.fileLabel.Name = "fileLabel";
            this.fileLabel.Size = new System.Drawing.Size(670, 17);
            this.fileLabel.Text = "Нова довідка";
            this.fileLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // processLbl
            // 
            this.processLbl.AutoSize = false;
            this.processLbl.Name = "processLbl";
            this.processLbl.Size = new System.Drawing.Size(115, 17);
            this.processLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // saveDialog
            // 
            this.saveDialog.DefaultExt = "lcc";
            this.saveDialog.Filter = "Довідки про грошову оцінку|*.lcc";
            this.saveDialog.Title = "Зберегти довідку про грошову оцінку";
            // 
            // printDialog
            // 
            this.printDialog.UseEXDialog = true;
            // 
            // exportPDFDialog
            // 
            this.exportPDFDialog.DefaultExt = "pdf";
            this.exportPDFDialog.Filter = "Portable Doc Format|*.pdf";
            this.exportPDFDialog.Title = "Експортувати довідку в PDF";
            // 
            // totalSquareBox
            // 
            this.totalSquareBox.BackColor = System.Drawing.SystemColors.Control;
            this.totalSquareBox.Location = new System.Drawing.Point(579, 417);
            this.totalSquareBox.Name = "totalSquareBox";
            this.totalSquareBox.Precision = 3;
            this.totalSquareBox.ReadOnly = true;
            this.totalSquareBox.Size = new System.Drawing.Size(84, 20);
            this.totalSquareBox.TabIndex = 38;
            this.totalSquareBox.Text = "0";
            this.totalSquareBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // buildSquareBox
            // 
            this.buildSquareBox.BackColor = System.Drawing.SystemColors.Control;
            this.buildSquareBox.Location = new System.Drawing.Point(579, 376);
            this.buildSquareBox.Name = "buildSquareBox";
            this.buildSquareBox.Precision = 3;
            this.buildSquareBox.ReadOnly = true;
            this.buildSquareBox.Size = new System.Drawing.Size(84, 20);
            this.buildSquareBox.TabIndex = 36;
            this.buildSquareBox.Text = "0";
            this.buildSquareBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // indexCoefBox
            // 
            this.indexCoefBox.Location = new System.Drawing.Point(579, 326);
            this.indexCoefBox.Name = "indexCoefBox";
            this.indexCoefBox.Precision = 4;
            this.indexCoefBox.ReadOnly = true;
            this.indexCoefBox.Size = new System.Drawing.Size(84, 20);
            this.indexCoefBox.TabIndex = 33;
            this.indexCoefBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // kfBox2
            // 
            this.kfBox2.Enabled = false;
            this.kfBox2.Location = new System.Drawing.Point(501, 451);
            this.kfBox2.Name = "kfBox2";
            this.kfBox2.Precision = 3;
            this.kfBox2.ReadOnly = true;
            this.kfBox2.Size = new System.Drawing.Size(117, 20);
            this.kfBox2.TabIndex = 30;
            this.kfBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // kfBox
            // 
            this.kfBox.Location = new System.Drawing.Point(167, 447);
            this.kfBox.Name = "kfBox";
            this.kfBox.Precision = 2;
            this.kfBox.Size = new System.Drawing.Size(117, 20);
            this.kfBox.TabIndex = 28;
            this.kfBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // km3Box
            // 
            this.km3Box.Location = new System.Drawing.Point(579, 225);
            this.km3Box.Name = "km3Box";
            this.km3Box.Precision = 4;
            this.km3Box.ReadOnly = true;
            this.km3Box.Size = new System.Drawing.Size(84, 20);
            this.km3Box.TabIndex = 24;
            this.km3Box.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // priceBox
            // 
            this.priceBox.Location = new System.Drawing.Point(185, 186);
            this.priceBox.Name = "priceBox";
            this.priceBox.Precision = 2;
            this.priceBox.ReadOnly = true;
            this.priceBox.Size = new System.Drawing.Size(99, 20);
            this.priceBox.TabIndex = 21;
            this.priceBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // km2Box
            // 
            this.km2Box.Location = new System.Drawing.Point(579, 186);
            this.km2Box.Name = "km2Box";
            this.km2Box.Precision = 3;
            this.km2Box.ReadOnly = true;
            this.km2Box.Size = new System.Drawing.Size(84, 20);
            this.km2Box.TabIndex = 19;
            this.km2Box.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // squareBox
            // 
            this.squareBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.squareBox.Location = new System.Drawing.Point(185, 127);
            this.squareBox.Name = "squareBox";
            this.squareBox.Precision = 0;
            this.squareBox.Size = new System.Drawing.Size(99, 20);
            this.squareBox.TabIndex = 12;
            this.squareBox.Text = "0";
            this.squareBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CertificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 684);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.docBox);
            this.Controls.Add(this.dateBox);
            this.Controls.Add(this.chiefBox);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.executorBox);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.evalTotalBox2);
            this.Controls.Add(this.evalTotalBox);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.evalBox2);
            this.Controls.Add(this.evalBox);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.evalM2Box2);
            this.Controls.Add(this.evalM2Box);
            this.Controls.Add(this.totalSquareBox);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.buildSquareBox);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.indexCoefBox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.kfBox2);
            this.Controls.Add(this.kfBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.oneMoreCheck);
            this.Controls.Add(this.km3Box);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.coefBox);
            this.Controls.Add(this.priceBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.km2Box);
            this.Controls.Add(this.areaBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.docAttribBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.squareBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.landNameBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.addressBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ownerLocationBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ownerBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CertificationForm";
            this.ShowInTaskbar = false;
            this.Text = "Грошова оцінка";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CertificationForm_FormClosing);
            this.coefBox.ResumeLayout(false);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ownerBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ownerLocationBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox addressBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox landNameBox;
        private System.Windows.Forms.Label label6;
        private DecimalTextBox squareBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox docAttribBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox areaBox;
        private DecimalTextBox km2Box;
        private System.Windows.Forms.Label label10;
        private DecimalTextBox priceBox;
        private System.Windows.Forms.GroupBox coefBox;
        private System.Windows.Forms.Label label11;
        private DecimalTextBox km3Box;
        private System.Windows.Forms.CheckBox oneMoreCheck;
        private System.Windows.Forms.Label label12;
        private DecimalTextBox kfBox;
        private DecimalTextBox kfBox2;
        private System.Windows.Forms.Label label13;
        private DecimalTextBox indexCoefBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private DecimalTextBox buildSquareBox;
        private DecimalTextBox totalSquareBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox evalM2Box2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox evalBox2;
        private System.Windows.Forms.TextBox evalBox;
        private System.Windows.Forms.TextBox evalTotalBox2;
        private System.Windows.Forms.TextBox evalTotalBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox executorBox;
        private System.Windows.Forms.ComboBox chiefBox;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.DateTimePicker dateBox;
        private CoefficientValueSetControl coefValSetCtl;
        private System.Windows.Forms.ComboBox docBox;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox evalM2Box;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem довідкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMenu;
        private System.Windows.Forms.ToolStripMenuItem saveAsMenu;
        private System.Windows.Forms.ToolStripMenuItem printMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem closeMenu;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel fileLabel;
        private System.Windows.Forms.SaveFileDialog saveDialog;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Windows.Forms.ToolStripMenuItem pdfMenu;
        private System.Windows.Forms.SaveFileDialog exportPDFDialog;
        private System.Windows.Forms.ToolStripStatusLabel processLbl;

    }
}