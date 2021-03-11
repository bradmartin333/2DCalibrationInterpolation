
namespace Test
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.numX = new System.Windows.Forms.NumericUpDown();
            this.numY = new System.Windows.Forms.NumericUpDown();
            this.lblZ = new System.Windows.Forms.Label();
            this.lblZOffset = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblZo = new System.Windows.Forms.Label();
            this.lblZoOffset = new System.Windows.Forms.Label();
            this.pbxZ = new System.Windows.Forms.PictureBox();
            this.pbxZo = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxZo)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.numX, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.numY, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblZ, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblZOffset, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblZo, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblZoOffset, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.pbxZ, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.pbxZo, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(647, 227);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // numX
            // 
            this.numX.DecimalPlaces = 3;
            this.numX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numX.Location = new System.Drawing.Point(110, 3);
            this.numX.Name = "numX";
            this.numX.Size = new System.Drawing.Size(101, 23);
            this.numX.TabIndex = 0;
            this.numX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numX.ValueChanged += new System.EventHandler(this.num_Changed);
            // 
            // numY
            // 
            this.numY.DecimalPlaces = 3;
            this.numY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numY.Location = new System.Drawing.Point(110, 40);
            this.numY.Name = "numY";
            this.numY.Size = new System.Drawing.Size(101, 23);
            this.numY.TabIndex = 1;
            this.numY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numY.ValueChanged += new System.EventHandler(this.num_Changed);
            // 
            // lblZ
            // 
            this.lblZ.AutoSize = true;
            this.lblZ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblZ.Location = new System.Drawing.Point(110, 74);
            this.lblZ.Name = "lblZ";
            this.lblZ.Size = new System.Drawing.Size(101, 37);
            this.lblZ.TabIndex = 2;
            this.lblZ.Text = "label1";
            this.lblZ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblZOffset
            // 
            this.lblZOffset.AutoSize = true;
            this.lblZOffset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblZOffset.Location = new System.Drawing.Point(110, 111);
            this.lblZOffset.Name = "lblZOffset";
            this.lblZOffset.Size = new System.Drawing.Size(101, 37);
            this.lblZOffset.TabIndex = 3;
            this.lblZOffset.Text = "label2";
            this.lblZOffset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 37);
            this.label3.TabIndex = 4;
            this.label3.Text = "X Position";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 37);
            this.label4.TabIndex = 5;
            this.label4.Text = "Y Position";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 37);
            this.label5.TabIndex = 6;
            this.label5.Text = "Z Position";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 37);
            this.label6.TabIndex = 7;
            this.label6.Text = "Z Offset";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 37);
            this.label1.TabIndex = 8;
            this.label1.Text = "Zo Position";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 42);
            this.label2.TabIndex = 9;
            this.label2.Text = "Z Position";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblZo
            // 
            this.lblZo.AutoSize = true;
            this.lblZo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblZo.Location = new System.Drawing.Point(110, 148);
            this.lblZo.Name = "lblZo";
            this.lblZo.Size = new System.Drawing.Size(101, 37);
            this.lblZo.TabIndex = 10;
            this.lblZo.Text = "label7";
            this.lblZo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblZoOffset
            // 
            this.lblZoOffset.AutoSize = true;
            this.lblZoOffset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblZoOffset.Location = new System.Drawing.Point(110, 185);
            this.lblZoOffset.Name = "lblZoOffset";
            this.lblZoOffset.Size = new System.Drawing.Size(101, 42);
            this.lblZoOffset.TabIndex = 11;
            this.lblZoOffset.Text = "label8";
            this.lblZoOffset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbxZ
            // 
            this.pbxZ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbxZ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxZ.Location = new System.Drawing.Point(217, 3);
            this.pbxZ.Name = "pbxZ";
            this.tableLayoutPanel1.SetRowSpan(this.pbxZ, 6);
            this.pbxZ.Size = new System.Drawing.Size(209, 221);
            this.pbxZ.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxZ.TabIndex = 12;
            this.pbxZ.TabStop = false;
            // 
            // pbxZo
            // 
            this.pbxZo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbxZo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxZo.Location = new System.Drawing.Point(432, 3);
            this.pbxZo.Name = "pbxZo";
            this.tableLayoutPanel1.SetRowSpan(this.pbxZo, 6);
            this.pbxZo.Size = new System.Drawing.Size(212, 221);
            this.pbxZo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxZo.TabIndex = 13;
            this.pbxZo.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 227);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "interpx";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxZo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.NumericUpDown numX;
        private System.Windows.Forms.NumericUpDown numY;
        private System.Windows.Forms.Label lblZ;
        private System.Windows.Forms.Label lblZOffset;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblZo;
        private System.Windows.Forms.Label lblZoOffset;
        private System.Windows.Forms.PictureBox pbxZ;
        private System.Windows.Forms.PictureBox pbxZo;
    }
}

