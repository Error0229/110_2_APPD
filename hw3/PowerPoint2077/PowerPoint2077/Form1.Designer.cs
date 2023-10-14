
namespace WindowPowerPoint
{
    partial class PowerPoint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PowerPoint));
            this._shapeGridView = new System.Windows.Forms.DataGridView();
            this.DeleteShape = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ShapeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShapeInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._shapeComboBox = new System.Windows.Forms.ComboBox();
            this._buttonInsertShape = new System.Windows.Forms.Button();
            this._groupView = new System.Windows.Forms.GroupBox();
            this._slide1 = new System.Windows.Forms.Button();
            this._slide2 = new System.Windows.Forms.Button();
            this._menuStrip1 = new System.Windows.Forms.MenuStrip();
            this._helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._lineAddButton = new System.Windows.Forms.ToolStripButton();
            this._rectangleAddButton = new System.Windows.Forms.ToolStripButton();
            this._ellipseAddButton = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this._shapeGridView)).BeginInit();
            this._groupView.SuspendLayout();
            this._menuStrip1.SuspendLayout();
            this._toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _shapeGridView
            // 
            this._shapeGridView.AllowUserToAddRows = false;
            this._shapeGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._shapeGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DeleteShape,
            this.ShapeType,
            this.ShapeInfo});
            this._shapeGridView.Location = new System.Drawing.Point(14, 67);
            this._shapeGridView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._shapeGridView.Name = "_shapeGridView";
            this._shapeGridView.ReadOnly = true;
            this._shapeGridView.RowHeadersVisible = false;
            this._shapeGridView.RowHeadersWidth = 51;
            this._shapeGridView.RowTemplate.Height = 24;
            this._shapeGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._shapeGridView.Size = new System.Drawing.Size(215, 643);
            this._shapeGridView.TabIndex = 0;
            this._shapeGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ShapeGridViewCellContentClick);
            // 
            // DeleteShape
            // 
            this.DeleteShape.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DeleteShape.FillWeight = 45.72192F;
            this.DeleteShape.HeaderText = "刪除";
            this.DeleteShape.MinimumWidth = 6;
            this.DeleteShape.Name = "DeleteShape";
            this.DeleteShape.ReadOnly = true;
            this.DeleteShape.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DeleteShape.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.DeleteShape.Text = "刪除";
            this.DeleteShape.UseColumnTextForButtonValue = true;
            // 
            // ShapeType
            // 
            this.ShapeType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ShapeType.DataPropertyName = "Name";
            this.ShapeType.FillWeight = 45.72192F;
            this.ShapeType.HeaderText = "形狀";
            this.ShapeType.MinimumWidth = 6;
            this.ShapeType.Name = "ShapeType";
            this.ShapeType.ReadOnly = true;
            this.ShapeType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ShapeInfo
            // 
            this.ShapeInfo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ShapeInfo.DataPropertyName = "Info";
            this.ShapeInfo.FillWeight = 208.5562F;
            this.ShapeInfo.HeaderText = "資訊";
            this.ShapeInfo.MinimumWidth = 6;
            this.ShapeInfo.Name = "ShapeInfo";
            this.ShapeInfo.ReadOnly = true;
            this.ShapeInfo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ShapeInfo.Width = 130;
            // 
            // _shapeComboBox
            // 
            this._shapeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._shapeComboBox.FormattingEnabled = true;
            this._shapeComboBox.Items.AddRange(new object[] {
            "矩形",
            "線"});
            this._shapeComboBox.Location = new System.Drawing.Point(120, 26);
            this._shapeComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._shapeComboBox.Name = "_shapeComboBox";
            this._shapeComboBox.Size = new System.Drawing.Size(110, 21);
            this._shapeComboBox.TabIndex = 1;
            // 
            // _buttonInsertShape
            // 
            this._buttonInsertShape.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonInsertShape.Location = new System.Drawing.Point(14, 17);
            this._buttonInsertShape.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._buttonInsertShape.Name = "_buttonInsertShape";
            this._buttonInsertShape.Size = new System.Drawing.Size(64, 35);
            this._buttonInsertShape.TabIndex = 2;
            this._buttonInsertShape.Text = "新增";
            this._buttonInsertShape.UseVisualStyleBackColor = true;
            this._buttonInsertShape.Click += new System.EventHandler(this.ButtonInsertShapeClick);
            // 
            // _groupView
            // 
            this._groupView.Controls.Add(this._buttonInsertShape);
            this._groupView.Controls.Add(this._shapeGridView);
            this._groupView.Controls.Add(this._shapeComboBox);
            this._groupView.Location = new System.Drawing.Point(908, 41);
            this._groupView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._groupView.Name = "_groupView";
            this._groupView.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._groupView.Size = new System.Drawing.Size(244, 714);
            this._groupView.TabIndex = 3;
            this._groupView.TabStop = false;
            this._groupView.Text = "資料顯示";
            // 
            // _slide1
            // 
            this._slide1.Location = new System.Drawing.Point(2, 113);
            this._slide1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._slide1.Name = "_slide1";
            this._slide1.Size = new System.Drawing.Size(113, 100);
            this._slide1.TabIndex = 4;
            this._slide1.UseVisualStyleBackColor = true;
            // 
            // _slide2
            // 
            this._slide2.Location = new System.Drawing.Point(2, 9);
            this._slide2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._slide2.Name = "_slide2";
            this._slide2.Size = new System.Drawing.Size(113, 100);
            this._slide2.TabIndex = 5;
            this._slide2.UseVisualStyleBackColor = true;
            // 
            // _menuStrip1
            // 
            this._menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._helpToolStripMenuItem});
            this._menuStrip1.Location = new System.Drawing.Point(0, 0);
            this._menuStrip1.Name = "_menuStrip1";
            this._menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this._menuStrip1.Size = new System.Drawing.Size(1155, 24);
            this._menuStrip1.TabIndex = 7;
            this._menuStrip1.Text = "menuStrip1";
            // 
            // _helpToolStripMenuItem
            // 
            this._helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._aboutToolStripMenuItem});
            this._helpToolStripMenuItem.Name = "_helpToolStripMenuItem";
            this._helpToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this._helpToolStripMenuItem.Text = "說明";
            // 
            // _aboutToolStripMenuItem
            // 
            this._aboutToolStripMenuItem.Name = "_aboutToolStripMenuItem";
            this._aboutToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this._aboutToolStripMenuItem.Text = "關於";
            // 
            // _toolStrip1
            // 
            this._toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._lineAddButton,
            this._rectangleAddButton,
            this._ellipseAddButton});
            this._toolStrip1.Location = new System.Drawing.Point(0, 24);
            this._toolStrip1.Name = "_toolStrip1";
            this._toolStrip1.Size = new System.Drawing.Size(1155, 25);
            this._toolStrip1.TabIndex = 8;
            this._toolStrip1.Text = "toolStrip1";
            // 
            // _lineAddButton
            // 
            this._lineAddButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._lineAddButton.Font = new System.Drawing.Font("Segoe UI Emoji", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lineAddButton.Image = ((System.Drawing.Image)(resources.GetObject("_lineAddButton.Image")));
            this._lineAddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._lineAddButton.Name = "_lineAddButton";
            this._lineAddButton.Size = new System.Drawing.Size(28, 22);
            this._lineAddButton.Text = "➖";
            this._lineAddButton.Click += new System.EventHandler(this._lineAddButtonClick);
            // 
            // _rectangleAddButton
            // 
            this._rectangleAddButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._rectangleAddButton.Font = new System.Drawing.Font("Segoe UI Emoji", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._rectangleAddButton.Image = ((System.Drawing.Image)(resources.GetObject("_rectangleAddButton.Image")));
            this._rectangleAddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._rectangleAddButton.Name = "_rectangleAddButton";
            this._rectangleAddButton.Size = new System.Drawing.Size(28, 22);
            this._rectangleAddButton.Text = "🔲";
            this._rectangleAddButton.Click += new System.EventHandler(this._rectangleAddButtonClick);
            // 
            // _ellipseAddButton
            // 
            this._ellipseAddButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._ellipseAddButton.Font = new System.Drawing.Font("Segoe UI Emoji", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._ellipseAddButton.Image = ((System.Drawing.Image)(resources.GetObject("_ellipseAddButton.Image")));
            this._ellipseAddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ellipseAddButton.Name = "_ellipseAddButton";
            this._ellipseAddButton.Size = new System.Drawing.Size(28, 22);
            this._ellipseAddButton.Text = "⭕";
            this._ellipseAddButton.Click += new System.EventHandler(this._ellipseAddButtonClick);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this._slide2);
            this.panel1.Controls.Add(this._slide1);
            this.panel1.Location = new System.Drawing.Point(12, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(118, 693);
            this.panel1.TabIndex = 9;
            // 
            // PowerPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 765);
            this.Controls.Add(this._groupView);
            this.Controls.Add(this._toolStrip1);
            this.Controls.Add(this._menuStrip1);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this._menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "PowerPoint";
            this.Text = "PowerPoint-🤡";
            ((System.ComponentModel.ISupportInitialize)(this._shapeGridView)).EndInit();
            this._groupView.ResumeLayout(false);
            this._menuStrip1.ResumeLayout(false);
            this._menuStrip1.PerformLayout();
            this._toolStrip1.ResumeLayout(false);
            this._toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView _shapeGridView;
        private System.Windows.Forms.ComboBox _shapeComboBox;
        private System.Windows.Forms.Button _buttonInsertShape;
        private System.Windows.Forms.GroupBox _groupView;
        private System.Windows.Forms.Button _slide1;
        private System.Windows.Forms.Button _slide2;
        private System.Windows.Forms.MenuStrip _menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem _helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _aboutToolStripMenuItem;
        private System.Windows.Forms.DataGridViewButtonColumn _deleteShape;
        private System.Windows.Forms.DataGridViewTextBoxColumn _shapeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn _shapeInfo;
        private System.Windows.Forms.ToolStrip _toolStrip1;
        private System.Windows.Forms.ToolStripButton _lineAddButton;
        private System.Windows.Forms.ToolStripButton _rectangleAddButton;
        private System.Windows.Forms.ToolStripButton _ellipseAddButton;
        private System.Windows.Forms.DataGridViewButtonColumn DeleteShape;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShapeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShapeInfo;
        private System.Windows.Forms.Panel panel1;
    }
}

