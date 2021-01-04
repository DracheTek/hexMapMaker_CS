namespace hexMapMaker_CS
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.zup = new System.Windows.Forms.Button();
            this.aup = new System.Windows.Forms.Button();
            this.adn = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.othersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.zdn = new System.Windows.Forms.Button();
            this.Generate = new System.Windows.Forms.Button();
            this.DebugLabel = new System.Windows.Forms.Label();
            this.drawTile = new System.Windows.Forms.Button();
            this.panview = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.debugText = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // zup
            // 
            this.zup.Location = new System.Drawing.Point(109, 33);
            this.zup.Name = "zup";
            this.zup.Size = new System.Drawing.Size(62, 23);
            this.zup.TabIndex = 2;
            this.zup.Text = "Zoom +";
            this.zup.UseVisualStyleBackColor = true;
            this.zup.Click += new System.EventHandler(this.zup_Click);
            // 
            // aup
            // 
            this.aup.Location = new System.Drawing.Point(370, 33);
            this.aup.Name = "aup";
            this.aup.Size = new System.Drawing.Size(75, 23);
            this.aup.TabIndex = 4;
            this.aup.Text = "Alpha +";
            this.aup.UseVisualStyleBackColor = true;
            this.aup.Click += new System.EventHandler(this.aup_Click);
            // 
            // adn
            // 
            this.adn.Location = new System.Drawing.Point(649, 33);
            this.adn.Name = "adn";
            this.adn.Size = new System.Drawing.Size(75, 23);
            this.adn.TabIndex = 5;
            this.adn.Text = "Alpha -";
            this.adn.UseVisualStyleBackColor = true;
            this.adn.Click += new System.EventHandler(this.adn_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.othersToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(787, 25);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // othersToolStripMenuItem
            // 
            this.othersToolStripMenuItem.Name = "othersToolStripMenuItem";
            this.othersToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.othersToolStripMenuItem.Text = "Others";
            this.othersToolStripMenuItem.Click += new System.EventHandler(this.othersToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 126F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 121F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.zup, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.adn, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.aup, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.zdn, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.Generate, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.DebugLabel, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.drawTile, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.panview, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 405);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(787, 61);
            this.tableLayoutPanel1.TabIndex = 7;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // zdn
            // 
            this.zdn.Location = new System.Drawing.Point(235, 33);
            this.zdn.Name = "zdn";
            this.zdn.Size = new System.Drawing.Size(75, 23);
            this.zdn.TabIndex = 6;
            this.zdn.Text = "Zoom -";
            this.zdn.UseVisualStyleBackColor = true;
            this.zdn.Click += new System.EventHandler(this.zdn_Click);
            // 
            // Generate
            // 
            this.Generate.Location = new System.Drawing.Point(23, 33);
            this.Generate.Name = "Generate";
            this.Generate.Size = new System.Drawing.Size(75, 23);
            this.Generate.TabIndex = 7;
            this.Generate.Text = "Generate";
            this.Generate.UseVisualStyleBackColor = true;
            this.Generate.Click += new System.EventHandler(this.Generate_Click);
            // 
            // DebugLabel
            // 
            this.DebugLabel.AutoSize = true;
            this.DebugLabel.Location = new System.Drawing.Point(23, 0);
            this.DebugLabel.Name = "DebugLabel";
            this.DebugLabel.Size = new System.Drawing.Size(35, 12);
            this.DebugLabel.TabIndex = 8;
            this.DebugLabel.Text = "DEBUG";
            // 
            // drawTile
            // 
            this.drawTile.Location = new System.Drawing.Point(108, 2);
            this.drawTile.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.drawTile.Name = "drawTile";
            this.drawTile.Size = new System.Drawing.Size(63, 18);
            this.drawTile.TabIndex = 9;
            this.drawTile.Text = "Draw Tile";
            this.drawTile.UseVisualStyleBackColor = true;
            this.drawTile.Click += new System.EventHandler(this.drawTile_Click);
            // 
            // panview
            // 
            this.panview.Location = new System.Drawing.Point(234, 2);
            this.panview.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panview.Name = "panview";
            this.panview.Size = new System.Drawing.Size(76, 18);
            this.panview.TabIndex = 10;
            this.panview.Text = "Pan View";
            this.panview.UseVisualStyleBackColor = true;
            this.panview.Click += new System.EventHandler(this.panview_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.debugText, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(787, 380);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(581, 374);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // debugText
            // 
            this.debugText.AcceptsReturn = true;
            this.debugText.Dock = System.Windows.Forms.DockStyle.Right;
            this.debugText.Location = new System.Drawing.Point(589, 2);
            this.debugText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.debugText.Multiline = true;
            this.debugText.Name = "debugText";
            this.debugText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.debugText.Size = new System.Drawing.Size(196, 376);
            this.debugText.TabIndex = 2;
            this.debugText.Text = "1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\n0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(787, 466);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button zup;
        private System.Windows.Forms.Button aup;
        private System.Windows.Forms.Button adn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem othersToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button zdn;
        private System.Windows.Forms.Button Generate;
        private System.Windows.Forms.Label DebugLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button drawTile;
        private System.Windows.Forms.Button panview;
        private System.Windows.Forms.TextBox debugText;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}

