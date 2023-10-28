
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
            this._deleteShape = new System.Windows.Forms.DataGridViewButtonColumn();
            this._shapeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._shapeInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._shapeComboBox = new System.Windows.Forms.ComboBox();
            this._buttonInsertShape = new System.Windows.Forms.Button();
            this._groupView = new System.Windows.Forms.GroupBox();
            this._slide1 = new System.Windows.Forms.Button();
            this._menuStrip1 = new System.Windows.Forms.MenuStrip();
            this._helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._lineAddButton = new WindowPowerPoint.BindingToolStripButton();
            this._rectangleAddButton = new WindowPowerPoint.BindingToolStripButton();
            this._ellipseAddButton = new WindowPowerPoint.BindingToolStripButton();
            this._cursorButton = new WindowPowerPoint.BindingToolStripButton();
            this._slideBackground = new System.Windows.Forms.Panel();
            this._canvas = new WindowPowerPoint.DoubleBufferedPanel();
            ((System.ComponentModel.ISupportInitialize)(this._shapeGridView)).BeginInit();
            this._groupView.SuspendLayout();
            this._menuStrip1.SuspendLayout();
            this._toolStrip1.SuspendLayout();
            this._slideBackground.SuspendLayout();
            this.SuspendLayout();
            // 
            // _shapeGridView
            // 
            this._shapeGridView.AllowUserToAddRows = false;
            this._shapeGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._shapeGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._deleteShape,
            this._shapeType,
            this._shapeInfo});
            this._shapeGridView.Location = new System.Drawing.Point(16, 77);
            this._shapeGridView.Margin = new System.Windows.Forms.Padding(2);
            this._shapeGridView.Name = "_shapeGridView";
            this._shapeGridView.ReadOnly = true;
            this._shapeGridView.RowHeadersVisible = false;
            this._shapeGridView.RowHeadersWidth = 51;
            this._shapeGridView.RowTemplate.Height = 24;
            this._shapeGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._shapeGridView.Size = new System.Drawing.Size(251, 742);
            this._shapeGridView.TabIndex = 0;
            this._shapeGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ShapeGridViewCellContentClick);
            // 
            // _deleteShape
            // 
            this._deleteShape.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._deleteShape.FillWeight = 45.72192F;
            this._deleteShape.HeaderText = "刪除";
            this._deleteShape.MinimumWidth = 6;
            this._deleteShape.Name = "_deleteShape";
            this._deleteShape.ReadOnly = true;
            this._deleteShape.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._deleteShape.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this._deleteShape.Text = "刪除";
            this._deleteShape.UseColumnTextForButtonValue = true;
            // 
            // _shapeType
            // 
            this._shapeType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._shapeType.DataPropertyName = "Name";
            this._shapeType.FillWeight = 45.72192F;
            this._shapeType.HeaderText = "形狀";
            this._shapeType.MinimumWidth = 6;
            this._shapeType.Name = "_shapeType";
            this._shapeType.ReadOnly = true;
            this._shapeType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // _shapeInfo
            // 
            this._shapeInfo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this._shapeInfo.DataPropertyName = "Info";
            this._shapeInfo.FillWeight = 208.5562F;
            this._shapeInfo.HeaderText = "資訊";
            this._shapeInfo.MinimumWidth = 6;
            this._shapeInfo.Name = "_shapeInfo";
            this._shapeInfo.ReadOnly = true;
            this._shapeInfo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._shapeInfo.Width = 130;
            // 
            // _shapeComboBox
            // 
            this._shapeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._shapeComboBox.FormattingEnabled = true;
            this._shapeComboBox.Items.AddRange(new object[] {
            "矩形",
            "線",
            "圓形"});
            this._shapeComboBox.Location = new System.Drawing.Point(140, 30);
            this._shapeComboBox.Margin = new System.Windows.Forms.Padding(2);
            this._shapeComboBox.Name = "_shapeComboBox";
            this._shapeComboBox.Size = new System.Drawing.Size(128, 23);
            this._shapeComboBox.TabIndex = 1;
            // 
            // _buttonInsertShape
            // 
            this._buttonInsertShape.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._buttonInsertShape.Location = new System.Drawing.Point(16, 20);
            this._buttonInsertShape.Margin = new System.Windows.Forms.Padding(2);
            this._buttonInsertShape.Name = "_buttonInsertShape";
            this._buttonInsertShape.Size = new System.Drawing.Size(75, 40);
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
            this._groupView.Location = new System.Drawing.Point(1059, 59);
            this._groupView.Margin = new System.Windows.Forms.Padding(2);
            this._groupView.Name = "_groupView";
            this._groupView.Padding = new System.Windows.Forms.Padding(2);
            this._groupView.Size = new System.Drawing.Size(285, 812);
            this._groupView.TabIndex = 3;
            this._groupView.TabStop = false;
            this._groupView.Text = "資料顯示";
            // 
            // _slide1
            // 
            this._slide1.Location = new System.Drawing.Point(4, 11);
            this._slide1.Margin = new System.Windows.Forms.Padding(2);
            this._slide1.Name = "_slide1";
            this._slide1.Size = new System.Drawing.Size(132, 115);
            this._slide1.TabIndex = 5;
            this._slide1.UseVisualStyleBackColor = true;
            // 
            // _menuStrip1
            // 
            this._menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._helpToolStripMenuItem});
            this._menuStrip1.Location = new System.Drawing.Point(0, 0);
            this._menuStrip1.Name = "_menuStrip1";
            this._menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this._menuStrip1.Size = new System.Drawing.Size(1348, 24);
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
            this._ellipseAddButton,
            this._cursorButton});
            this._toolStrip1.Location = new System.Drawing.Point(0, 24);
            this._toolStrip1.Name = "_toolStrip1";
            this._toolStrip1.Size = new System.Drawing.Size(1348, 27);
            this._toolStrip1.TabIndex = 8;
            this._toolStrip1.Text = "toolStrip1";
            // 
            // _lineAddButton
            // 
            this._lineAddButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._lineAddButton.Font = new System.Drawing.Font("Segoe UI Emoji", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._lineAddButton.Image = ((System.Drawing.Image)(resources.GetObject("_lineAddButton.Image")));
            this._lineAddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._lineAddButton.Name = "_lineAddButton";
            this._lineAddButton.Size = new System.Drawing.Size(27, 24);
            this._lineAddButton.Text = "➖";
            this._lineAddButton.Click += new System.EventHandler(this.ClickAddLineButton);
            // 
            // _rectangleAddButton
            // 
            this._rectangleAddButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._rectangleAddButton.Font = new System.Drawing.Font("Segoe UI Emoji", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._rectangleAddButton.Image = ((System.Drawing.Image)(resources.GetObject("_rectangleAddButton.Image")));
            this._rectangleAddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._rectangleAddButton.Name = "_rectangleAddButton";
            this._rectangleAddButton.Size = new System.Drawing.Size(27, 24);
            this._rectangleAddButton.Text = "🔲";
            this._rectangleAddButton.Click += new System.EventHandler(this.ClickAddRectangleButton);
            // 
            // _ellipseAddButton
            // 
            this._ellipseAddButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._ellipseAddButton.Font = new System.Drawing.Font("Segoe UI Emoji", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._ellipseAddButton.Image = ((System.Drawing.Image)(resources.GetObject("_ellipseAddButton.Image")));
            this._ellipseAddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ellipseAddButton.Name = "_ellipseAddButton";
            this._ellipseAddButton.Size = new System.Drawing.Size(27, 24);
            this._ellipseAddButton.Text = "⭕";
            this._ellipseAddButton.Click += new System.EventHandler(this.ClickAddEllipseButton);
            // 
            // _cursorButton
            // 
            this._cursorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._cursorButton.Font = new System.Drawing.Font("Segoe UI Emoji", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._cursorButton.Image = ((System.Drawing.Image)(resources.GetObject("_cursorButton.Image")));
            this._cursorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._cursorButton.Name = "_cursorButton";
            this._cursorButton.Size = new System.Drawing.Size(24, 24);
            this._cursorButton.Text = "⭕";
            this._cursorButton.ToolTipText = "🖱️";
            this._cursorButton.Click += new System.EventHandler(this.ClickCursorButton);
            // 
            // _slideBackground
            // 
            this._slideBackground.AutoScroll = true;
            this._slideBackground.BackColor = System.Drawing.SystemColors.ControlDark;
            this._slideBackground.Controls.Add(this._slide1);
            this._slideBackground.Location = new System.Drawing.Point(14, 59);
            this._slideBackground.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._slideBackground.Name = "_slideBackground";
            this._slideBackground.Size = new System.Drawing.Size(138, 810);
            this._slideBackground.TabIndex = 9;
            // 
            // _canvas
            // 
            this._canvas.AutoSize = true;
            this._canvas.BackColor = System.Drawing.Color.White;
            this._canvas.Location = new System.Drawing.Point(159, 59);
            this._canvas.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._canvas.Name = "_canvas";
            this._canvas.Size = new System.Drawing.Size(895, 810);
            this._canvas.TabIndex = 10;
            // 
            // PowerPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1348, 883);
            this.Controls.Add(this._canvas);
            this.Controls.Add(this._groupView);
            this.Controls.Add(this._toolStrip1);
            this.Controls.Add(this._menuStrip1);
            this.Controls.Add(this._slideBackground);
            this.MainMenuStrip = this._menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PowerPoint";
            this.Text = "PowerPoint2077";
            ((System.ComponentModel.ISupportInitialize)(this._shapeGridView)).EndInit();
            this._groupView.ResumeLayout(false);
            this._menuStrip1.ResumeLayout(false);
            this._menuStrip1.PerformLayout();
            this._toolStrip1.ResumeLayout(false);
            this._toolStrip1.PerformLayout();
            this._slideBackground.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView _shapeGridView;
        private System.Windows.Forms.ComboBox _shapeComboBox;
        private System.Windows.Forms.Button _buttonInsertShape;
        private System.Windows.Forms.GroupBox _groupView;
        private System.Windows.Forms.Button _slide1;
        private System.Windows.Forms.MenuStrip _menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem _helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStrip _toolStrip1;
        private BindingToolStripButton _lineAddButton;
        private BindingToolStripButton _rectangleAddButton;
        private BindingToolStripButton _ellipseAddButton;
        private BindingToolStripButton _cursorButton;
        private System.Windows.Forms.Panel _slideBackground;
        private System.Windows.Forms.DataGridViewButtonColumn _deleteShape;
        private System.Windows.Forms.DataGridViewTextBoxColumn _shapeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn _shapeInfo;
        private DoubleBufferedPanel _canvas;
    }
}
