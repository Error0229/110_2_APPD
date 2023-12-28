
namespace WindowPowerPoint
{
    partial class InsertShapeDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InsertShapeDialog));
            this._textBox1 = new System.Windows.Forms.TextBox();
            this._textBox2 = new System.Windows.Forms.TextBox();
            this._textBox3 = new System.Windows.Forms.TextBox();
            this._textBox4 = new System.Windows.Forms.TextBox();
            this._label1 = new System.Windows.Forms.Label();
            this._label2 = new System.Windows.Forms.Label();
            this._label3 = new System.Windows.Forms.Label();
            this._label4 = new System.Windows.Forms.Label();
            this._buttonOK = new System.Windows.Forms.Button();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this._textBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._textBox1.Location = new System.Drawing.Point(46, 50);
            this._textBox1.Name = "textBox1";
            this._textBox1.Size = new System.Drawing.Size(85, 20);
            this._textBox1.TabIndex = 0;
            this._textBox1.TextChanged += new System.EventHandler(this.HandleTextBox1TextChanged);
            // 
            // textBox2
            // 
            this._textBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._textBox2.Location = new System.Drawing.Point(200, 50);
            this._textBox2.Name = "textBox2";
            this._textBox2.Size = new System.Drawing.Size(88, 20);
            this._textBox2.TabIndex = 1;
            this._textBox2.TextChanged += new System.EventHandler(this.HandleTextBox2TextChanged);
            // 
            // textBox3
            // 
            this._textBox3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._textBox3.Location = new System.Drawing.Point(49, 131);
            this._textBox3.Name = "textBox3";
            this._textBox3.Size = new System.Drawing.Size(88, 20);
            this._textBox3.TabIndex = 2;
            this._textBox3.TextChanged += new System.EventHandler(this.HandleTextBox3TextChanged);
            // 
            // textBox4
            // 
            this._textBox4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._textBox4.Location = new System.Drawing.Point(203, 131);
            this._textBox4.Name = "textBox4";
            this._textBox4.Size = new System.Drawing.Size(88, 20);
            this._textBox4.TabIndex = 3;
            this._textBox4.TextChanged += new System.EventHandler(this.HandleTextBox4TextChanged);
            // 
            // label1
            // 
            this._label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._label1.AutoSize = true;
            this._label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._label1.Location = new System.Drawing.Point(46, 31);
            this._label1.Name = "label1";
            this._label1.Size = new System.Drawing.Size(77, 13);
            this._label1.TabIndex = 4;
            this._label1.Text = "左上角座標 X";
            // 
            // label2
            // 
            this._label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._label2.AutoSize = true;
            this._label2.Location = new System.Drawing.Point(200, 31);
            this._label2.Name = "label2";
            this._label2.Size = new System.Drawing.Size(77, 13);
            this._label2.TabIndex = 5;
            this._label2.Text = "左上角座標 Y";
            // 
            // label3
            // 
            this._label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._label3.AutoSize = true;
            this._label3.Location = new System.Drawing.Point(46, 115);
            this._label3.Name = "label3";
            this._label3.Size = new System.Drawing.Size(77, 13);
            this._label3.TabIndex = 6;
            this._label3.Text = "右下角座標 X";
            // 
            // label4
            // 
            this._label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._label4.AutoSize = true;
            this._label4.Location = new System.Drawing.Point(200, 115);
            this._label4.Name = "label4";
            this._label4.Size = new System.Drawing.Size(77, 13);
            this._label4.TabIndex = 7;
            this._label4.Text = "右下角座標 Y";
            // 
            // buttonOK
            // 
            this._buttonOK.AccessibleName = "ButtonOK";
            this._buttonOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._buttonOK.AutoSize = true;
            this._buttonOK.Location = new System.Drawing.Point(46, 171);
            this._buttonOK.Name = "buttonOK";
            this._buttonOK.Size = new System.Drawing.Size(85, 25);
            this._buttonOK.TabIndex = 8;
            this._buttonOK.Text = "OK";
            this._buttonOK.UseVisualStyleBackColor = true;
            this._buttonOK.Click += new System.EventHandler(this.ClickButtonInsert);
            // 
            // buttonCancel
            // 
            this._buttonCancel.AccessibleName = "ButtonCancel";
            this._buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._buttonCancel.Location = new System.Drawing.Point(203, 171);
            this._buttonCancel.Name = "buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(88, 25);
            this._buttonCancel.TabIndex = 9;
            this._buttonCancel.Text = "Cancel";
            this._buttonCancel.UseVisualStyleBackColor = true;
            this._buttonCancel.Click += new System.EventHandler(this.ClickButtonCancel);
            // 
            // _flowLayoutPanel
            // 
            this._flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._flowLayoutPanel.Name = "_flowLayoutPanel";
            this._flowLayoutPanel.Size = new System.Drawing.Size(339, 217);
            this._flowLayoutPanel.TabIndex = 10;
            // 
            // Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 217);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonOK);
            this.Controls.Add(this._label4);
            this.Controls.Add(this._label3);
            this.Controls.Add(this._label2);
            this.Controls.Add(this._label1);
            this.Controls.Add(this._textBox4);
            this.Controls.Add(this._textBox3);
            this.Controls.Add(this._textBox2);
            this.Controls.Add(this._textBox1);
            this.Controls.Add(this._flowLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Dialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _textBox1;
        private System.Windows.Forms.TextBox _textBox2;
        private System.Windows.Forms.TextBox _textBox3;
        private System.Windows.Forms.TextBox _textBox4;
        private System.Windows.Forms.Label _label1;
        private System.Windows.Forms.Label _label2;
        private System.Windows.Forms.Label _label3;
        private System.Windows.Forms.Label _label4;
        private System.Windows.Forms.Button _buttonOK;
        private System.Windows.Forms.Button _buttonCancel;
        private System.Windows.Forms.FlowLayoutPanel _flowLayoutPanel;
    }
}