using CongLibrary.CustomControls;

namespace Mu_Change_Server_IP
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.LogText = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RightMouseMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CopyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PopupFeedbackPanel = new System.Windows.Forms.Panel();
            this.PopupFeedbackText = new System.Windows.Forms.Label();
            this.PopupFeedbackImage = new System.Windows.Forms.PictureBox();
            this.InputText = new CongLibrary.CustomControls.CongInputText();
            this.RightMouseMenu.SuspendLayout();
            this.PopupFeedbackPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PopupFeedbackImage)).BeginInit();
            this.SuspendLayout();
            // 
            // LogText
            // 
            this.LogText.BackColor = System.Drawing.Color.Black;
            this.LogText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LogText.ContextMenuStrip = this.RightMouseMenu;
            this.LogText.ForeColor = System.Drawing.Color.Lime;
            this.LogText.Location = new System.Drawing.Point(0, -2);
            this.LogText.Margin = new System.Windows.Forms.Padding(0);
            this.LogText.Name = "LogText";
            this.LogText.ReadOnly = true;
            this.LogText.Size = new System.Drawing.Size(734, 420);
            this.LogText.TabIndex = 3;
            this.LogText.Text = "[root ple]# hi...";
            this.LogText.TextChanged += new System.EventHandler(this.LogText_TextChanged);
            this.LogText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LogText_KeyDown);
            this.LogText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LogText_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Lime;
            this.label1.Location = new System.Drawing.Point(-4, 439);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "[root ple]#";
            // 
            // RightMouseMenu
            // 
            this.RightMouseMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyMenuItem,
            this.PasteMenuItem});
            this.RightMouseMenu.Name = "contextMenuStrip1";
            this.RightMouseMenu.Size = new System.Drawing.Size(108, 48);
            // 
            // CopyMenuItem
            // 
            this.CopyMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("CopyMenuItem.Image")));
            this.CopyMenuItem.Name = "CopyMenuItem";
            this.CopyMenuItem.Size = new System.Drawing.Size(107, 22);
            this.CopyMenuItem.Text = "Copy";
            this.CopyMenuItem.Click += new System.EventHandler(this.CopyMenuItem_Click);
            // 
            // PasteMenuItem
            // 
            this.PasteMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("PasteMenuItem.Image")));
            this.PasteMenuItem.Name = "PasteMenuItem";
            this.PasteMenuItem.Size = new System.Drawing.Size(107, 22);
            this.PasteMenuItem.Text = "Paste";
            this.PasteMenuItem.Click += new System.EventHandler(this.PasteMenuItem_Click);
            // 
            // PopupFeedbackPanel
            // 
            this.PopupFeedbackPanel.BackColor = System.Drawing.Color.White;
            this.PopupFeedbackPanel.Controls.Add(this.PopupFeedbackText);
            this.PopupFeedbackPanel.Controls.Add(this.PopupFeedbackImage);
            this.PopupFeedbackPanel.Location = new System.Drawing.Point(486, 41);
            this.PopupFeedbackPanel.Name = "PopupFeedbackPanel";
            this.PopupFeedbackPanel.Size = new System.Drawing.Size(107, 22);
            this.PopupFeedbackPanel.TabIndex = 5;
            // 
            // PopupFeedbackText
            // 
            this.PopupFeedbackText.AutoSize = true;
            this.PopupFeedbackText.ForeColor = System.Drawing.Color.Black;
            this.PopupFeedbackText.Location = new System.Drawing.Point(23, 1);
            this.PopupFeedbackText.Name = "PopupFeedbackText";
            this.PopupFeedbackText.Size = new System.Drawing.Size(108, 19);
            this.PopupFeedbackText.TabIndex = 1;
            this.PopupFeedbackText.Text = "Feedback...";
            // 
            // PopupFeedbackImage
            // 
            this.PopupFeedbackImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PopupFeedbackImage.Image = ((System.Drawing.Image)(resources.GetObject("PopupFeedbackImage.Image")));
            this.PopupFeedbackImage.Location = new System.Drawing.Point(2, 1);
            this.PopupFeedbackImage.Name = "PopupFeedbackImage";
            this.PopupFeedbackImage.Padding = new System.Windows.Forms.Padding(2, 1, 1, 1);
            this.PopupFeedbackImage.Size = new System.Drawing.Size(20, 20);
            this.PopupFeedbackImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PopupFeedbackImage.TabIndex = 0;
            this.PopupFeedbackImage.TabStop = false;
            // 
            // InputText
            // 
            this.InputText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(13)))), ((int)(((byte)(13)))));
            this.InputText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InputText.ContextMenuStrip = this.RightMouseMenu;
            this.InputText.ForeColor = System.Drawing.Color.Lime;
            this.InputText.Location = new System.Drawing.Point(110, 439);
            this.InputText.Name = "InputText";
            this.InputText.Size = new System.Drawing.Size(612, 19);
            this.InputText.TabIndex = 2;
            this.InputText.TextChanged += new System.EventHandler(this.InputText_TextChanged);
            this.InputText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputText_KeyDown);
            this.InputText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.InputText_MouseDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(734, 461);
            this.Controls.Add(this.PopupFeedbackPanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.InputText);
            this.Controls.Add(this.LogText);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Powerful Bash University";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.RightMouseMenu.ResumeLayout(false);
            this.PopupFeedbackPanel.ResumeLayout(false);
            this.PopupFeedbackPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PopupFeedbackImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox LogText;
        private CongInputText InputText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip RightMouseMenu;
        private System.Windows.Forms.ToolStripMenuItem CopyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PasteMenuItem;
        private System.Windows.Forms.Panel PopupFeedbackPanel;
        private System.Windows.Forms.Label PopupFeedbackText;
        private System.Windows.Forms.PictureBox PopupFeedbackImage;
    }
}

