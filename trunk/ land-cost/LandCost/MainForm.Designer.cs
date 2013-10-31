namespace LandCost
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.openMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.profilesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.curProfileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTable = new System.Windows.Forms.TableLayoutPanel();
            this.map = new GMap.NET.WindowsForms.GMapControl();
            this.toolPanel = new System.Windows.Forms.Panel();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.evalBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.searchBtn = new System.Windows.Forms.Button();
            this.addressBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.infoPanel = new System.Windows.Forms.Panel();
            this.areaBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.openDialog = new System.Windows.Forms.OpenFileDialog();
            this.regionSelCtl = new LandCost.Forms.RegionSelectionControl();
            this.priceBox = new LandCost.Forms.DecimalTextBox();
            this.regionBox = new LandCost.Forms.DecimalTextBox();
            this.km2Box = new LandCost.Forms.DecimalTextBox();
            this.menuStrip1.SuspendLayout();
            this.mainTable.SuspendLayout();
            this.toolPanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.infoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.settingsMenu,
            this.helpMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMenu,
            this.toolStripMenuItem1,
            this.exitMenu});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(48, 20);
            this.fileMenu.Text = "Файл";
            // 
            // openMenu
            // 
            this.openMenu.Image = global::LandCost.Properties.Resources.open;
            this.openMenu.Name = "openMenu";
            this.openMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openMenu.Size = new System.Drawing.Size(208, 22);
            this.openMenu.Text = "Відкрити довідку";
            this.openMenu.Click += new System.EventHandler(this.openMenu_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(205, 6);
            // 
            // exitMenu
            // 
            this.exitMenu.Image = global::LandCost.Properties.Resources.exit;
            this.exitMenu.Name = "exitMenu";
            this.exitMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.exitMenu.Size = new System.Drawing.Size(208, 22);
            this.exitMenu.Text = "Вихід";
            this.exitMenu.Click += new System.EventHandler(this.exitMenu_Click);
            // 
            // settingsMenu
            // 
            this.settingsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.profilesMenu,
            this.curProfileMenu});
            this.settingsMenu.Name = "settingsMenu";
            this.settingsMenu.Size = new System.Drawing.Size(101, 20);
            this.settingsMenu.Text = "Налаштування";
            // 
            // profilesMenu
            // 
            this.profilesMenu.Name = "profilesMenu";
            this.profilesMenu.Size = new System.Drawing.Size(179, 22);
            this.profilesMenu.Text = "Профілі";
            this.profilesMenu.Click += new System.EventHandler(this.profilesMenu_Click);
            // 
            // curProfileMenu
            // 
            this.curProfileMenu.Enabled = false;
            this.curProfileMenu.Name = "curProfileMenu";
            this.curProfileMenu.Size = new System.Drawing.Size(179, 22);
            this.curProfileMenu.Text = "Поточний профіль";
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMenu});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(75, 20);
            this.helpMenu.Text = "Допомога";
            // 
            // aboutMenu
            // 
            this.aboutMenu.Name = "aboutMenu";
            this.aboutMenu.Size = new System.Drawing.Size(154, 22);
            this.aboutMenu.Text = "Про програму";
            this.aboutMenu.Click += new System.EventHandler(this.aboutMenu_Click);
            // 
            // mainTable
            // 
            this.mainTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.mainTable.ColumnCount = 2;
            this.mainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 423F));
            this.mainTable.Controls.Add(this.map, 0, 0);
            this.mainTable.Controls.Add(this.toolPanel, 1, 0);
            this.mainTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTable.Location = new System.Drawing.Point(0, 24);
            this.mainTable.Name = "mainTable";
            this.mainTable.RowCount = 1;
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTable.Size = new System.Drawing.Size(1008, 664);
            this.mainTable.TabIndex = 3;
            // 
            // map
            // 
            this.map.Bearing = 0F;
            this.map.CanDragMap = true;
            this.map.Dock = System.Windows.Forms.DockStyle.Fill;
            this.map.EmptyTileColor = System.Drawing.Color.Navy;
            this.map.GrayScaleMode = false;
            this.map.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.map.LevelsKeepInMemmory = 5;
            this.map.Location = new System.Drawing.Point(5, 5);
            this.map.MarkersEnabled = true;
            this.map.MaxZoom = 17;
            this.map.MinZoom = 3;
            this.map.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.map.Name = "map";
            this.map.NegativeMode = false;
            this.map.PolygonsEnabled = true;
            this.map.RetryLoadTile = 0;
            this.map.RoutesEnabled = true;
            this.map.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Fractional;
            this.map.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.map.ShowTileGridLines = false;
            this.map.Size = new System.Drawing.Size(573, 654);
            this.map.TabIndex = 2;
            this.map.Zoom = 0D;
            this.map.OnPolygonClick += new GMap.NET.WindowsForms.PolygonClick(this.map_OnPolygonClick);
            this.map.OnPolygonEnter += new GMap.NET.WindowsForms.PolygonEnter(this.map_OnPolygonEnter);
            this.map.OnPolygonLeave += new GMap.NET.WindowsForms.PolygonLeave(this.map_OnPolygonLeave);
            // 
            // toolPanel
            // 
            this.toolPanel.Controls.Add(this.buttonPanel);
            this.toolPanel.Controls.Add(this.regionSelCtl);
            this.toolPanel.Controls.Add(this.panel1);
            this.toolPanel.Controls.Add(this.infoPanel);
            this.toolPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolPanel.Location = new System.Drawing.Point(586, 5);
            this.toolPanel.Name = "toolPanel";
            this.toolPanel.Size = new System.Drawing.Size(417, 654);
            this.toolPanel.TabIndex = 3;
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.cancelBtn);
            this.buttonPanel.Controls.Add(this.evalBtn);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.Location = new System.Drawing.Point(0, 619);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(417, 35);
            this.buttonPanel.TabIndex = 7;
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(84, 5);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 1;
            this.cancelBtn.Text = "Скасувати";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // evalBtn
            // 
            this.evalBtn.Location = new System.Drawing.Point(3, 5);
            this.evalBtn.Name = "evalBtn";
            this.evalBtn.Size = new System.Drawing.Size(75, 23);
            this.evalBtn.TabIndex = 0;
            this.evalBtn.Text = "Оцінити";
            this.evalBtn.UseVisualStyleBackColor = true;
            this.evalBtn.Click += new System.EventHandler(this.evalBtn_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.searchBtn);
            this.panel1.Controls.Add(this.addressBox);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 141);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(417, 61);
            this.panel1.TabIndex = 5;
            // 
            // searchBtn
            // 
            this.searchBtn.Image = global::LandCost.Properties.Resources.search;
            this.searchBtn.Location = new System.Drawing.Point(327, 25);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(55, 23);
            this.searchBtn.TabIndex = 10;
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // addressBox
            // 
            this.addressBox.Location = new System.Drawing.Point(21, 27);
            this.addressBox.Name = "addressBox";
            this.addressBox.Size = new System.Drawing.Size(300, 20);
            this.addressBox.TabIndex = 9;
            this.addressBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.addressBox_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Адреса";
            // 
            // infoPanel
            // 
            this.infoPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.infoPanel.Controls.Add(this.areaBox);
            this.infoPanel.Controls.Add(this.label4);
            this.infoPanel.Controls.Add(this.label3);
            this.infoPanel.Controls.Add(this.priceBox);
            this.infoPanel.Controls.Add(this.regionBox);
            this.infoPanel.Controls.Add(this.km2Box);
            this.infoPanel.Controls.Add(this.label2);
            this.infoPanel.Controls.Add(this.label1);
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.infoPanel.Location = new System.Drawing.Point(0, 0);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Size = new System.Drawing.Size(417, 141);
            this.infoPanel.TabIndex = 4;
            // 
            // areaBox
            // 
            this.areaBox.Location = new System.Drawing.Point(244, 18);
            this.areaBox.Name = "areaBox";
            this.areaBox.ReadOnly = true;
            this.areaBox.Size = new System.Drawing.Size(138, 20);
            this.areaBox.TabIndex = 8;
            this.areaBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Вартість земельної ділянки";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Коефіцієнт Км2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Район";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Економіко-планувальна зона";
            // 
            // openDialog
            // 
            this.openDialog.DefaultExt = "lcc";
            this.openDialog.Filter = "Довідки про грошову оцінку|*.lcc";
            this.openDialog.Title = "Відкрити довідку про грошову оцінку";
            // 
            // regionSelCtl
            // 
            this.regionSelCtl.CurrentRegion = null;
            this.regionSelCtl.Dock = System.Windows.Forms.DockStyle.Top;
            this.regionSelCtl.Location = new System.Drawing.Point(0, 202);
            this.regionSelCtl.Name = "regionSelCtl";
            this.regionSelCtl.RegionList = null;
            this.regionSelCtl.Size = new System.Drawing.Size(417, 344);
            this.regionSelCtl.TabIndex = 6;
            this.regionSelCtl.Visible = false;
            this.regionSelCtl.SelectionMade += new System.EventHandler(this.regionSelCtl_SelectionMade);
            // 
            // priceBox
            // 
            this.priceBox.Location = new System.Drawing.Point(244, 96);
            this.priceBox.Name = "priceBox";
            this.priceBox.Precision = 2;
            this.priceBox.ReadOnly = true;
            this.priceBox.Size = new System.Drawing.Size(138, 20);
            this.priceBox.TabIndex = 5;
            this.priceBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // regionBox
            // 
            this.regionBox.Location = new System.Drawing.Point(244, 44);
            this.regionBox.Name = "regionBox";
            this.regionBox.Precision = 0;
            this.regionBox.ReadOnly = true;
            this.regionBox.Size = new System.Drawing.Size(138, 20);
            this.regionBox.TabIndex = 2;
            this.regionBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // km2Box
            // 
            this.km2Box.Location = new System.Drawing.Point(244, 70);
            this.km2Box.Name = "km2Box";
            this.km2Box.Precision = 3;
            this.km2Box.ReadOnly = true;
            this.km2Box.Size = new System.Drawing.Size(138, 20);
            this.km2Box.TabIndex = 3;
            this.km2Box.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 688);
            this.Controls.Add(this.mainTable);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1024, 726);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LandCost - грошова оцінка";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mainTable.ResumeLayout(false);
            this.toolPanel.ResumeLayout(false);
            this.buttonPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.infoPanel.ResumeLayout(false);
            this.infoPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsMenu;
        private System.Windows.Forms.ToolStripMenuItem profilesMenu;
        private System.Windows.Forms.ToolStripMenuItem curProfileMenu;
        private System.Windows.Forms.TableLayoutPanel mainTable;
        private System.Windows.Forms.Panel toolPanel;
        private System.Windows.Forms.Panel infoPanel;
        private System.Windows.Forms.TextBox areaBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private Forms.DecimalTextBox priceBox;
        private Forms.DecimalTextBox regionBox;
        private Forms.DecimalTextBox km2Box;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private GMap.NET.WindowsForms.GMapControl map;
        private System.Windows.Forms.Panel panel1;
        private Forms.RegionSelectionControl regionSelCtl;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button evalBtn;
        private System.Windows.Forms.TextBox addressBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem openMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitMenu;
        private System.Windows.Forms.OpenFileDialog openDialog;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem aboutMenu;
    }
}