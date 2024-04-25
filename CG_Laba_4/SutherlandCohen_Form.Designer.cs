namespace CG_Laba_4
{
    partial class SutherlandCohen_Form
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
            this.draw_pictureBox.Location = new System.Drawing.Point(1, 1);
            this.draw_pictureBox.Name = "draw_pictureBox";
            this.draw_pictureBox.Size = new System.Drawing.Size(630, 630);
            this.draw_pictureBox.TabIndex = 0;
            this.draw_pictureBox.TabStop = false;
            // 
            // SutherlandCohen_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1169, 641);
            this.Controls.Add(this.draw_pictureBox);
            this.Name = "SutherlandCohen_Form";
            this.Text = "SutherlandCohen_Form";
            ((System.ComponentModel.ISupportInitialize)(this.draw_pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox draw_pictureBox;
    }
}