namespace Prompter
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.ButtonToLeft = new System.Windows.Forms.ToolStripButton();
            this.ButtonToRight = new System.Windows.Forms.ToolStripButton();
            this.ToggleBlockResize = new System.Windows.Forms.ToolStripButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.ListButtons = new System.Windows.Forms.ListBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1,
            this.ButtonToLeft,
            this.ButtonToRight,
            this.ToggleBlockResize});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(210, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 25);
            // 
            // ButtonToLeft
            // 
            this.ButtonToLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonToLeft.Image = ((System.Drawing.Image)(resources.GetObject("ButtonToLeft.Image")));
            this.ButtonToLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonToLeft.Name = "ButtonToLeft";
            this.ButtonToLeft.Size = new System.Drawing.Size(23, 22);
            this.ButtonToLeft.Text = "toolStripButton2";
            this.ButtonToLeft.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.ButtonToLeft.ToolTipText = "Left";
            this.ButtonToLeft.Click += new System.EventHandler(this.ButtonToLeft_Click);
            // 
            // ButtonToRight
            // 
            this.ButtonToRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonToRight.Image = ((System.Drawing.Image)(resources.GetObject("ButtonToRight.Image")));
            this.ButtonToRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonToRight.Name = "ButtonToRight";
            this.ButtonToRight.Size = new System.Drawing.Size(23, 22);
            this.ButtonToRight.Text = "toolStripButton1";
            this.ButtonToRight.ToolTipText = "Right";
            this.ButtonToRight.Click += new System.EventHandler(this.ButtonToRight_Click);
            // 
            // ToggleBlockResize
            // 
            this.ToggleBlockResize.CheckOnClick = true;
            this.ToggleBlockResize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToggleBlockResize.Image = ((System.Drawing.Image)(resources.GetObject("ToggleBlockResize.Image")));
            this.ToggleBlockResize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToggleBlockResize.Name = "ToggleBlockResize";
            this.ToggleBlockResize.Size = new System.Drawing.Size(23, 22);
            this.ToggleBlockResize.Text = "Block resize";
            this.ToggleBlockResize.CheckedChanged += new System.EventHandler(this.ToggleBlockResize_CheckedChanged);
            // 
            // ListButtons
            // 
            this.ListButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListButtons.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ListButtons.FormattingEnabled = true;
            this.ListButtons.ItemHeight = 21;
            this.ListButtons.Location = new System.Drawing.Point(0, 25);
            this.ListButtons.Name = "ListButtons";
            this.ListButtons.Size = new System.Drawing.Size(210, 467);
            this.ListButtons.TabIndex = 1;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(210, 492);
            this.Controls.Add(this.ListButtons);
            this.Controls.Add(this.toolStrip1);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Prompter";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip toolStrip1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private ToolStripTextBox toolStripTextBox1;
        private ToolStripButton ButtonToLeft;
        private ToolStripButton ButtonToRight;
        private ToolStripButton ToggleBlockResize;
        private ListBox ListButtons;
    }
}