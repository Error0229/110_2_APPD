
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
            this._splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._groupView = new System.Windows.Forms.GroupBox();
            this._buttonInsertShape = new System.Windows.Forms.Button();
            this._shapeGridView = new System.Windows.Forms.DataGridView();
            this._deleteShape = new System.Windows.Forms.DataGridViewButtonColumn();
            this._shapeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._shapeInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._shapeComboBox = new System.Windows.Forms.ComboBox();
            this._splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._slide1 = new System.Windows.Forms.Button();
            this._menuStrip1 = new System.Windows.Forms.MenuStrip();
            this._helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._lineAddButton = new WindowPowerPoint.BindingToolStripButton();
            this._rectangleAddButton = new WindowPowerPoint.BindingToolStripButton();
            this._ellipseAddButton = new WindowPowerPoint.BindingToolStripButton();
            this._cursorButton = new WindowPowerPoint.BindingToolStripButton();
            this._undoButton = new WindowPowerPoint.BindingToolStripButton();
            this._redoButton = new WindowPowerPoint.BindingToolStripButton();
            this._canvas = new WindowPowerPoint.DoubleBufferedPanel();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer2)).BeginInit();
            this._splitContainer2.Panel1.SuspendLayout();
            this._splitContainer2.Panel2.SuspendLayout();
            this._splitContainer2.SuspendLayout();
            this._groupView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._shapeGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer1)).BeginInit();
            this._splitContainer1.Panel1.SuspendLayout();
            this._splitContainer1.Panel2.SuspendLayout();
            this._splitContainer1.SuspendLayout();
            this._menuStrip1.SuspendLayout();
            this._toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _splitContainer2
            // 
            resources.ApplyResources(this._splitContainer2, "_splitContainer2");
            this._splitContainer2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this._splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this._splitContainer2.Name = "_splitContainer2";
            // 
            // _splitContainer2.Panel1
            // 
            this._splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this._splitContainer2.Panel1.Controls.Add(this._canvas);
            resources.ApplyResources(this._splitContainer2.Panel1, "_splitContainer2.Panel1");
            // 
            // _splitContainer2.Panel2
            // 
            this._splitContainer2.Panel2.Controls.Add(this._groupView);
            this._splitContainer2.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.SplitContainer2Adjust);
            // 
            // _groupView
            // 
            resources.ApplyResources(this._groupView, "_groupView");
            this._groupView.BackColor = System.Drawing.SystemColors.ControlLight;
            this._groupView.Controls.Add(this._buttonInsertShape);
            this._groupView.Controls.Add(this._shapeGridView);
            this._groupView.Controls.Add(this._shapeComboBox);
            this._groupView.Name = "_groupView";
            this._groupView.TabStop = false;
            // 
            // _buttonInsertShape
            // 
            resources.ApplyResources(this._buttonInsertShape, "_buttonInsertShape");
            this._buttonInsertShape.Name = "_buttonInsertShape";
            this._buttonInsertShape.UseVisualStyleBackColor = true;
            this._buttonInsertShape.Click += new System.EventHandler(this.ButtonInsertShapeClick);
            // 
            // _shapeGridView
            // 
            this._shapeGridView.AllowUserToAddRows = false;
            resources.ApplyResources(this._shapeGridView, "_shapeGridView");
            this._shapeGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._shapeGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._deleteShape,
            this._shapeType,
            this._shapeInfo});
            this._shapeGridView.Name = "_shapeGridView";
            this._shapeGridView.ReadOnly = true;
            this._shapeGridView.RowHeadersVisible = false;
            this._shapeGridView.RowTemplate.Height = 24;
            this._shapeGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._shapeGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ShapeGridViewCellContentClick);
            // 
            // _deleteShape
            // 
            this._deleteShape.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this._deleteShape.FillWeight = 45.72192F;
            resources.ApplyResources(this._deleteShape, "_deleteShape");
            this._deleteShape.Name = "_deleteShape";
            this._deleteShape.ReadOnly = true;
            this._deleteShape.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this._deleteShape.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this._deleteShape.Text = "刪除";
            this._deleteShape.UseColumnTextForButtonValue = true;
            // 
            // _shapeType
            // 
            this._shapeType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this._shapeType.DataPropertyName = "Name";
            this._shapeType.FillWeight = 45.72192F;
            resources.ApplyResources(this._shapeType, "_shapeType");
            this._shapeType.Name = "_shapeType";
            this._shapeType.ReadOnly = true;
            this._shapeType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // _shapeInfo
            // 
            this._shapeInfo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._shapeInfo.DataPropertyName = "Info";
            this._shapeInfo.FillWeight = 208.5562F;
            resources.ApplyResources(this._shapeInfo, "_shapeInfo");
            this._shapeInfo.Name = "_shapeInfo";
            this._shapeInfo.ReadOnly = true;
            this._shapeInfo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // _shapeComboBox
            // 
            resources.ApplyResources(this._shapeComboBox, "_shapeComboBox");
            this._shapeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._shapeComboBox.FormattingEnabled = true;
            this._shapeComboBox.Items.AddRange(new object[] {
            resources.GetString("_shapeComboBox.Items"),
            resources.GetString("_shapeComboBox.Items1"),
            resources.GetString("_shapeComboBox.Items2")});
            this._shapeComboBox.Name = "_shapeComboBox";
            // 
            // _splitContainer1
            // 
            resources.ApplyResources(this._splitContainer1, "_splitContainer1");
            this._splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this._splitContainer1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this._splitContainer1.Name = "_splitContainer1";
            // 
            // _splitContainer1.Panel1
            // 
            this._splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this._splitContainer1.Panel1.Controls.Add(this._slide1);
            resources.ApplyResources(this._splitContainer1.Panel1, "_splitContainer1.Panel1");
            // 
            // _splitContainer1.Panel2
            // 
            this._splitContainer1.Panel2.Controls.Add(this._splitContainer2);
            this._splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.SplitContainer1Adjust);
            // 
            // _slide1
            // 
            resources.ApplyResources(this._slide1, "_slide1");
            this._slide1.Name = "_slide1";
            this._slide1.UseVisualStyleBackColor = true;
            // 
            // _menuStrip1
            // 
            this._menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._helpToolStripMenuItem});
            resources.ApplyResources(this._menuStrip1, "_menuStrip1");
            this._menuStrip1.Name = "_menuStrip1";
            // 
            // _helpToolStripMenuItem
            // 
            this._helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._aboutToolStripMenuItem});
            this._helpToolStripMenuItem.Name = "_helpToolStripMenuItem";
            resources.ApplyResources(this._helpToolStripMenuItem, "_helpToolStripMenuItem");
            // 
            // _aboutToolStripMenuItem
            // 
            this._aboutToolStripMenuItem.Name = "_aboutToolStripMenuItem";
            resources.ApplyResources(this._aboutToolStripMenuItem, "_aboutToolStripMenuItem");
            // 
            // _toolStrip1
            // 
            this._toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._lineAddButton,
            this._rectangleAddButton,
            this._ellipseAddButton,
            this._cursorButton,
            this._undoButton,
            this._redoButton});
            resources.ApplyResources(this._toolStrip1, "_toolStrip1");
            this._toolStrip1.Name = "_toolStrip1";
            // 
            // _lineAddButton
            // 
            this._lineAddButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this._lineAddButton, "_lineAddButton");
            this._lineAddButton.Name = "_lineAddButton";
            this._lineAddButton.Click += new System.EventHandler(this.ClickAddLineButton);
            // 
            // _rectangleAddButton
            // 
            this._rectangleAddButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this._rectangleAddButton, "_rectangleAddButton");
            this._rectangleAddButton.Name = "_rectangleAddButton";
            this._rectangleAddButton.Click += new System.EventHandler(this.ClickAddRectangleButton);
            // 
            // _ellipseAddButton
            // 
            this._ellipseAddButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this._ellipseAddButton, "_ellipseAddButton");
            this._ellipseAddButton.Name = "_ellipseAddButton";
            this._ellipseAddButton.Click += new System.EventHandler(this.ClickAddEllipseButton);
            // 
            // _cursorButton
            // 
            this._cursorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this._cursorButton, "_cursorButton");
            this._cursorButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this._cursorButton.Image = global::WindowPowerPoint.Properties.Resources.cursor_166x256;
            this._cursorButton.Name = "_cursorButton";
            this._cursorButton.Click += new System.EventHandler(this.ClickCursorButton);
            // 
            // _undoButton
            // 
            this._undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this._undoButton, "_undoButton");
            this._undoButton.Name = "_undoButton";
            this._undoButton.Click += new System.EventHandler(this.UndoButtonClick);
            // 
            // _redoButton
            // 
            resources.ApplyResources(this._redoButton, "_redoButton");
            this._redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._redoButton.Name = "_redoButton";
            this._redoButton.Click += new System.EventHandler(this.RedoButtonClick);
            // 
            // _canvas
            // 
            resources.ApplyResources(this._canvas, "_canvas");
            this._canvas.BackColor = System.Drawing.Color.White;
            this._canvas.Name = "_canvas";
            // 
            // PowerPoint
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Controls.Add(this._toolStrip1);
            this.Controls.Add(this._menuStrip1);
            this.Controls.Add(this._splitContainer1);
            this.MainMenuStrip = this._menuStrip1;
            this.Name = "PowerPoint";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this._splitContainer2.Panel1.ResumeLayout(false);
            this._splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer2)).EndInit();
            this._splitContainer2.ResumeLayout(false);
            this._groupView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._shapeGridView)).EndInit();
            this._splitContainer1.Panel1.ResumeLayout(false);
            this._splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer1)).EndInit();
            this._splitContainer1.ResumeLayout(false);
            this._menuStrip1.ResumeLayout(false);
            this._menuStrip1.PerformLayout();
            this._toolStrip1.ResumeLayout(false);
            this._toolStrip1.PerformLayout();
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
        private DoubleBufferedPanel _canvas;
        private BindingToolStripButton _undoButton;
        private BindingToolStripButton _redoButton;
        private System.Windows.Forms.SplitContainer _splitContainer1;
        private System.Windows.Forms.SplitContainer _splitContainer2;
        private System.Windows.Forms.DataGridViewButtonColumn _deleteShape;
        private System.Windows.Forms.DataGridViewTextBoxColumn _shapeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn _shapeInfo;
    }
}
