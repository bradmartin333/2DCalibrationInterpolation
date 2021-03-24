
namespace UsePython
{
    partial class Form1
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
            this.numX = new System.Windows.Forms.NumericUpDown();
            this.numY = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblZ = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            this.SuspendLayout();
            // 
            // numX
            // 
            this.numX.DecimalPlaces = 3;
            this.numX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numX.Location = new System.Drawing.Point(40, 12);
            this.numX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numX.Name = "numX";
            this.numX.Size = new System.Drawing.Size(80, 23);
            this.numX.TabIndex = 0;
            this.numX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numX.ValueChanged += new System.EventHandler(this.num_ValueChanged);
            // 
            // numY
            // 
            this.numY.DecimalPlaces = 3;
            this.numY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numY.Location = new System.Drawing.Point(156, 12);
            this.numY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numY.Name = "numY";
            this.numY.Size = new System.Drawing.Size(80, 23);
            this.numY.TabIndex = 1;
            this.numY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numY.ValueChanged += new System.EventHandler(this.num_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(256, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Z";
            // 
            // lblZ
            // 
            this.lblZ.AutoSize = true;
            this.lblZ.Location = new System.Drawing.Point(276, 16);
            this.lblZ.Name = "lblZ";
            this.lblZ.Size = new System.Drawing.Size(75, 15);
            this.lblZ.TabIndex = 6;
            this.lblZ.Text = "INITIALIZING";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 51);
            this.Controls.Add(this.lblZ);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numY);
            this.Controls.Add(this.numX);
            this.Name = "Form1";
            this.Text = "UsePython";
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numX;
        private System.Windows.Forms.NumericUpDown numY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblZ;
    }
}

