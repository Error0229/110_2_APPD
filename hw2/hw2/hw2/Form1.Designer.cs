
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
            this.ShapeGridView = new System.Windows.Forms.DataGridView();
            this.DeleteShape = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ShapeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShapeInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShapeComboBox = new System.Windows.Forms.ComboBox();
            this.ButtonInsertShape = new System.Windows.Forms.Button();
            this.GroupView = new System.Windows.Forms.GroupBox();
            this.Slide1 = new System.Windows.Forms.Button();
            this.Slide2 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.說明ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.關於ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.ShapeGridView)).BeginInit();
            this.GroupView.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ShapeGridView
            // 
            this.ShapeGridView.AllowUserToAddRows = false;
            this.ShapeGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ShapeGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DeleteShape,
            this.ShapeType,
            this.ShapeInfo});
            this.ShapeGridView.Location = new System.Drawing.Point(14, 67);
            this.ShapeGridView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ShapeGridView.Name = "ShapeGridView";
            this.ShapeGridView.ReadOnly = true;
            this.ShapeGridView.RowHeadersVisible = false;
            this.ShapeGridView.RowHeadersWidth = 51;
            this.ShapeGridView.RowTemplate.Height = 24;
            this.ShapeGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ShapeGridView.Size = new System.Drawing.Size(215, 643);
            this.ShapeGridView.TabIndex = 0;
            this.ShapeGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ShapeGridView_CellContentClick);
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
            // ShapeComboBox
            // 
            this.ShapeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ShapeComboBox.FormattingEnabled = true;
            this.ShapeComboBox.Items.AddRange(new object[] {
            "矩形",
            "線"});
            this.ShapeComboBox.Location = new System.Drawing.Point(120, 26);
            this.ShapeComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ShapeComboBox.Name = "ShapeComboBox";
            this.ShapeComboBox.Size = new System.Drawing.Size(110, 21);
            this.ShapeComboBox.TabIndex = 1;
            // 
            // ButtonInsertShape
            // 
            this.ButtonInsertShape.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonInsertShape.Location = new System.Drawing.Point(14, 17);
            this.ButtonInsertShape.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ButtonInsertShape.Name = "ButtonInsertShape";
            this.ButtonInsertShape.Size = new System.Drawing.Size(64, 35);
            this.ButtonInsertShape.TabIndex = 2;
            this.ButtonInsertShape.Text = "新增";
            this.ButtonInsertShape.UseVisualStyleBackColor = true;
            this.ButtonInsertShape.Click += new System.EventHandler(this.ButtonInsertShapeClick);
            // 
            // GroupView
            // 
            this.GroupView.Controls.Add(this.ButtonInsertShape);
            this.GroupView.Controls.Add(this.ShapeGridView);
            this.GroupView.Controls.Add(this.ShapeComboBox);
            this.GroupView.Location = new System.Drawing.Point(946, 35);
            this.GroupView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GroupView.Name = "GroupView";
            this.GroupView.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GroupView.Size = new System.Drawing.Size(244, 714);
            this.GroupView.TabIndex = 3;
            this.GroupView.TabStop = false;
            this.GroupView.Text = "資料顯示";
            // 
            // Slide1
            // 
            this.Slide1.Location = new System.Drawing.Point(11, 145);
            this.Slide1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Slide1.Name = "Slide1";
            this.Slide1.Size = new System.Drawing.Size(113, 100);
            this.Slide1.TabIndex = 4;
            this.Slide1.UseVisualStyleBackColor = true;
            // 
            // Slide2
            // 
            this.Slide2.Location = new System.Drawing.Point(11, 41);
            this.Slide2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Slide2.Name = "Slide2";
            this.Slide2.Size = new System.Drawing.Size(113, 100);
            this.Slide2.TabIndex = 5;
            this.Slide2.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.說明ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1213, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 說明ToolStripMenuItem
            // 
            this.說明ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.關於ToolStripMenuItem});
            this.說明ToolStripMenuItem.Name = "說明ToolStripMenuItem";
            this.說明ToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.說明ToolStripMenuItem.Text = "說明";
            // 
            // 關於ToolStripMenuItem
            // 
            this.關於ToolStripMenuItem.Name = "關於ToolStripMenuItem";
            this.關於ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.關於ToolStripMenuItem.Text = "關於";
            // 
            // PowerPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1213, 765);
            this.Controls.Add(this.Slide2);
            this.Controls.Add(this.Slide1);
            this.Controls.Add(this.GroupView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "PowerPoint";
            this.Text = "PowerPoint-🤡";
            ((System.ComponentModel.ISupportInitialize)(this.ShapeGridView)).EndInit();
            this.GroupView.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ShapeGridView;
        private System.Windows.Forms.ComboBox ShapeComboBox;
        private System.Windows.Forms.Button ButtonInsertShape;
        private System.Windows.Forms.GroupBox GroupView;
        private System.Windows.Forms.Button Slide1;
        private System.Windows.Forms.Button Slide2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 說明ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 關於ToolStripMenuItem;
        private System.Windows.Forms.DataGridViewButtonColumn DeleteShape;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShapeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShapeInfo;
    }
}

