namespace CG_Laba_4
{
    partial class Main_Form
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
            this.cyrusBeck_button = new System.Windows.Forms.Button();
            this.sutherlandCohen_button = new System.Windows.Forms.Button();
            this.middlePoint_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cyrusBeck_button
            // 
            this.cyrusBeck_button.Location = new System.Drawing.Point(196, 191);
            this.cyrusBeck_button.Name = "cyrusBeck_button";
            this.cyrusBeck_button.Size = new System.Drawing.Size(125, 56);
            this.cyrusBeck_button.TabIndex = 0;
            this.cyrusBeck_button.Text = "Цирус-Бек";
            this.cyrusBeck_button.UseVisualStyleBackColor = true;
            this.cyrusBeck_button.Click += new System.EventHandler(this.cyrusBeck_button_Click);
            // 
            // sutherlandCohen_button
            // 
            this.sutherlandCohen_button.Location = new System.Drawing.Point(327, 191);
            this.sutherlandCohen_button.Name = "sutherlandCohen_button";
            this.sutherlandCohen_button.Size = new System.Drawing.Size(125, 56);
            this.sutherlandCohen_button.TabIndex = 1;
            this.sutherlandCohen_button.Text = "Сазерленд-Коэн";
            this.sutherlandCohen_button.UseVisualStyleBackColor = true;
            this.sutherlandCohen_button.Click += new System.EventHandler(this.sutherlandCohen_button_Click);
            // 
            // middlePoint_button
            // 
            this.middlePoint_button.Location = new System.Drawing.Point(458, 191);
            this.middlePoint_button.Name = "middlePoint_button";
            this.middlePoint_button.Size = new System.Drawing.Size(125, 56);
            this.middlePoint_button.TabIndex = 2;
            this.middlePoint_button.Text = "Средняя точка";
            this.middlePoint_button.UseVisualStyleBackColor = true;
            this.middlePoint_button.Click += new System.EventHandler(this.middlePoint_button_Click);
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.middlePoint_button);
            this.Controls.Add(this.sutherlandCohen_button);
            this.Controls.Add(this.cyrusBeck_button);
            this.Name = "Main_Form";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Button cyrusBeck_button;
        private Button sutherlandCohen_button;
        private Button middlePoint_button;
    }
}