
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
            this._shapeGridView = new System.Windows.Forms.DataGridView();
            this._deleteShape = new System.Windows.Forms.DataGridViewButtonColumn();
            this._shapeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._shapeInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._shapeComboBox = new System.Windows.Forms.ComboBox();
            this._buttonInsertShape = new System.Windows.Forms.Button();
            this._groupView = new System.Windows.Forms.GroupBox();
            this._slide1 = new System.Windows.Forms.Button();
            this._slide2 = new System.Windows.Forms.Button();
            this._menuStrip1 = new System.Windows.Forms.MenuStrip();
            this._helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this._shapeGridView)).BeginInit();
            this._groupView.SuspendLayout();
            this._menuStrip1.SuspendLayout();
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
            this._shapeGridView.Location = new System.Drawing.Point(19, 82);
            this._shapeGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._shapeGridView.Name = "_shapeGridView";
            this._shapeGridView.ReadOnly = true;
            this._shapeGridView.RowHeadersVisible = false;
            this._shapeGridView.RowHeadersWidth = 51;
            this._shapeGridView.RowTemplate.Height = 24;
            this._shapeGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._shapeGridView.Size = new System.Drawing.Size(287, 791);
            this._shapeGridView.TabIndex = 0;
            this._shapeGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ShapeGridViewCellContentClick);
            // 
            // DeleteShape
            // 
            this._deleteShape.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._deleteShape.FillWeight = 45.72192F;
            this._deleteShape.HeaderText = "刪除";
            this._deleteShape.MinimumWidth = 6;
            this._deleteShape.Name = "DeleteShape";
            this._deleteShape.ReadOnly = true;
            this._deleteShape.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._deleteShape.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this._deleteShape.Text = "刪除";
            this._deleteShape.UseColumnTextForButtonValue = true;
            // 
            // ShapeType
            // 
            this._shapeType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._shapeType.DataPropertyName = "Name";
            this._shapeType.FillWeight = 45.72192F;
            this._shapeType.HeaderText = "形狀";
            this._shapeType.MinimumWidth = 6;
            this._shapeType.Name = "ShapeType";
            this._shapeType.ReadOnly = true;
            this._shapeType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ShapeInfo
            // 
            this._shapeInfo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this._shapeInfo.DataPropertyName = "Info";
            this._shapeInfo.FillWeight = 208.5562F;
            this._shapeInfo.HeaderText = "資訊";
            this._shapeInfo.MinimumWidth = 6;
            this._shapeInfo.Name = "ShapeInfo";
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
            "線"});
            this._shapeComboBox.Location = new System.Drawing.Point(160, 32);
            this._shapeComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._shapeComboBox.Name = "_shapeComboBox";
            this._shapeComboBox.Size = new System.Drawing.Size(145, 24);
            this._shapeComboBox.TabIndex = 1;
            // 
            // _buttonInsertShape
            // 
            this._buttonInsertShape.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonInsertShape.Location = new System.Drawing.Point(19, 21);
            this._buttonInsertShape.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._buttonInsertShape.Name = "_buttonInsertShape";
            this._buttonInsertShape.Size = new System.Drawing.Size(85, 43);
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
            this._groupView.Location = new System.Drawing.Point(1211, 50);
            this._groupView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._groupView.Name = "_groupView";
            this._groupView.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._groupView.Size = new System.Drawing.Size(325, 879);
            this._groupView.TabIndex = 3;
            this._groupView.TabStop = false;
            this._groupView.Text = "資料顯示";
            // 
            // _slide1
            // 
            this._slide1.Location = new System.Drawing.Point(15, 178);
            this._slide1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._slide1.Name = "_slide1";
            this._slide1.Size = new System.Drawing.Size(151, 123);
            this._slide1.TabIndex = 4;
            this._slide1.UseVisualStyleBackColor = true;
            // 
            // _slide2
            // 
            this._slide2.Location = new System.Drawing.Point(15, 50);
            this._slide2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._slide2.Name = "_slide2";
            this._slide2.Size = new System.Drawing.Size(151, 123);
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
            this._menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this._menuStrip1.Size = new System.Drawing.Size(1554, 28);
            this._menuStrip1.TabIndex = 7;
            this._menuStrip1.Text = "menuStrip1";
            // 
            // _helpToolStripMenuItem
            // 
            this._helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._aboutToolStripMenuItem});
            this._helpToolStripMenuItem.Name = "_helpToolStripMenuItem";
            this._helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this._helpToolStripMenuItem.Text = "說明";
            // 
            // _aboutToolStripMenuItem
            // 
            this._aboutToolStripMenuItem.Name = "_aboutToolStripMenuItem";
            this._aboutToolStripMenuItem.Size = new System.Drawing.Size(124, 26);
            this._aboutToolStripMenuItem.Text = "關於";
            // 
            // PowerPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1554, 942);
            this.Controls.Add(this._slide2);
            this.Controls.Add(this._slide1);
            this.Controls.Add(this._groupView);
            this.Controls.Add(this._menuStrip1);
            this.MainMenuStrip = this._menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "PowerPoint";
            this.Text = "PowerPoint-🤡";
            ((System.ComponentModel.ISupportInitialize)(this._shapeGridView)).EndInit();
            this._groupView.ResumeLayout(false);
            this._menuStrip1.ResumeLayout(false);
            this._menuStrip1.PerformLayout();
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
    }
}

