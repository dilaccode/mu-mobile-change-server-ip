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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.LogText = new System.Windows.Forms.RichTextBox();
            this.InputText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LogText
            // 
            this.LogText.BackColor = System.Drawing.Color.Black;
            this.LogText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LogText.ForeColor = System.Drawing.Color.Lime;
            this.LogText.Location = new System.Drawing.Point(0, -2);
            this.LogText.Margin = new System.Windows.Forms.Padding(0);
            this.LogText.Name = "LogText";
            this.LogText.ReadOnly = true;
            this.LogText.Size = new System.Drawing.Size(734, 420);
            this.LogText.TabIndex = 3;
            this.LogText.Text = "[root ple]# hi...";
            this.LogText.TextChanged += new System.EventHandler(this.LogText_TextChanged);
            // 
            // InputText
            // 
            this.InputText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.InputText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InputText.ForeColor = System.Drawing.Color.Lime;
            this.InputText.Location = new System.Drawing.Point(110, 439);
            this.InputText.Name = "InputText";
            this.InputText.Size = new System.Drawing.Size(612, 19);
            this.InputText.TabIndex = 2;
            this.InputText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputText_KeyDown);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(734, 461);
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
            this.Text = "Powerful Bash University";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox LogText;
        private System.Windows.Forms.TextBox InputText;
        private System.Windows.Forms.Label label1;
    }
}

