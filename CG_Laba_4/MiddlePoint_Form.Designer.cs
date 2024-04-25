namespace CG_Laba_4
{
    partial class MiddlePoint_Form
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
            this.draw_pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.draw_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // draw_pictureBox
            // 
            this.draw_pictureBox.Location = new System.Drawing.Point(2, 2);
            this.draw_pictureBox.Name = "draw_pictureBox";
            this.draw_pictureBox.Size = new System.Drawing.Size(630, 630);
            this.draw_pictureBox.TabIndex = 1;
            this.draw_pictureBox.TabStop = false;
            // 
            // MiddlePoint_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1126, 644);
            this.Controls.Add(this.draw_pictureBox);
            this.Name = "MiddlePoint_Form";
            this.Text = "MiddlePoint_Form";
            this.Load += new System.EventHandler(this.MiddlePoint_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.draw_pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox draw_pictureBox;
    }
}