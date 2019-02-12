using System.Windows.Forms;

namespace SimulatorUI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.drawingArea = new SimulatorUI.DrawingArea();
            this.SuspendLayout();
            // 
            // drawingArea
            // 
            this.drawingArea.BackColor = System.Drawing.SystemColors.Control;
            this.drawingArea.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("drawingArea.BackgroundImage")));
            this.drawingArea.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.drawingArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawingArea.Location = new System.Drawing.Point(0, 0);
            this.drawingArea.Name = "drawingArea";
            this.drawingArea.Size = new System.Drawing.Size(800, 450);
            this.drawingArea.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.drawingArea);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.AllowTransparency = true;
            this.TopMost = true;
            this.WindowState = FormWindowState.Maximized;
        }

        #endregion

        private DrawingArea drawingArea;
    }
}

