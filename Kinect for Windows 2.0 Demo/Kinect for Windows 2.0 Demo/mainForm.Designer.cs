namespace Kinect_for_Windows_2._0_Demo
{
    partial class mainForm
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
            this.leftHandStausLabel = new System.Windows.Forms.Label();
            this.rightHandStatusLabel = new System.Windows.Forms.Label();
            this.leftHandLabel = new System.Windows.Forms.Label();
            this.rightHandLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.facialDiagram1 = new Kinect_for_Windows_2._0_Demo.FacialDiagram();
            this.emotionLabel = new System.Windows.Forms.Label();
            this.emotionStatusLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // leftHandStausLabel
            // 
            this.leftHandStausLabel.AutoSize = true;
            this.leftHandStausLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftHandStausLabel.Location = new System.Drawing.Point(789, 242);
            this.leftHandStausLabel.Name = "leftHandStausLabel";
            this.leftHandStausLabel.Size = new System.Drawing.Size(134, 26);
            this.leftHandStausLabel.TabIndex = 1;
            this.leftHandStausLabel.Text = "Not Tracking";
            // 
            // rightHandStatusLabel
            // 
            this.rightHandStatusLabel.AutoSize = true;
            this.rightHandStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rightHandStatusLabel.Location = new System.Drawing.Point(789, 319);
            this.rightHandStatusLabel.Name = "rightHandStatusLabel";
            this.rightHandStatusLabel.Size = new System.Drawing.Size(134, 26);
            this.rightHandStatusLabel.TabIndex = 2;
            this.rightHandStatusLabel.Text = "Not Tracking";
            // 
            // leftHandLabel
            // 
            this.leftHandLabel.AutoSize = true;
            this.leftHandLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftHandLabel.Location = new System.Drawing.Point(802, 216);
            this.leftHandLabel.Name = "leftHandLabel";
            this.leftHandLabel.Size = new System.Drawing.Size(106, 26);
            this.leftHandLabel.TabIndex = 4;
            this.leftHandLabel.Text = "Left Hand";
            // 
            // rightHandLabel
            // 
            this.rightHandLabel.AutoSize = true;
            this.rightHandLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rightHandLabel.Location = new System.Drawing.Point(794, 293);
            this.rightHandLabel.Name = "rightHandLabel";
            this.rightHandLabel.Size = new System.Drawing.Size(121, 26);
            this.rightHandLabel.TabIndex = 5;
            this.rightHandLabel.Text = "Right Hand";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(760, 485);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // facialDiagram1
            // 
            this.facialDiagram1.Location = new System.Drawing.Point(778, 13);
            this.facialDiagram1.Name = "facialDiagram1";
            this.facialDiagram1.Size = new System.Drawing.Size(150, 200);
            this.facialDiagram1.TabIndex = 3;
            // 
            // emotionLabel
            // 
            this.emotionLabel.AutoSize = true;
            this.emotionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emotionLabel.Location = new System.Drawing.Point(806, 370);
            this.emotionLabel.Name = "emotionLabel";
            this.emotionLabel.Size = new System.Drawing.Size(93, 26);
            this.emotionLabel.TabIndex = 7;
            this.emotionLabel.Text = "Emotion";
            // 
            // emotionStatusLabel
            // 
            this.emotionStatusLabel.AutoSize = true;
            this.emotionStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emotionStatusLabel.Location = new System.Drawing.Point(789, 396);
            this.emotionStatusLabel.Name = "emotionStatusLabel";
            this.emotionStatusLabel.Size = new System.Drawing.Size(134, 26);
            this.emotionStatusLabel.TabIndex = 6;
            this.emotionStatusLabel.Text = "Not Tracking";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 515);
            this.Controls.Add(this.emotionLabel);
            this.Controls.Add(this.emotionStatusLabel);
            this.Controls.Add(this.rightHandLabel);
            this.Controls.Add(this.leftHandLabel);
            this.Controls.Add(this.facialDiagram1);
            this.Controls.Add(this.rightHandStatusLabel);
            this.Controls.Add(this.leftHandStausLabel);
            this.Controls.Add(this.pictureBox1);
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kinect for Windows 2.0 Demo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label leftHandStausLabel;
        private System.Windows.Forms.Label rightHandStatusLabel;
        private FacialDiagram facialDiagram1;
        private System.Windows.Forms.Label leftHandLabel;
        private System.Windows.Forms.Label rightHandLabel;
        private System.Windows.Forms.Label emotionLabel;
        private System.Windows.Forms.Label emotionStatusLabel;
    }
}

