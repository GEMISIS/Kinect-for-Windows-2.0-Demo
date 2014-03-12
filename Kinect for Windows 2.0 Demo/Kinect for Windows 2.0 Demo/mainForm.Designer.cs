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
            this.emotionLabel = new System.Windows.Forms.Label();
            this.emotionStatusLabel = new System.Windows.Forms.Label();
            this.personRadar1 = new Custom_Controls.PersonRadar();
            this.facialDiagram1 = new Kinect_for_Windows_2._0_Demo.FacialDiagram();
            this.colorButton = new System.Windows.Forms.Button();
            this.depthButton = new System.Windows.Forms.Button();
            this.irButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.takePicButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // leftHandStausLabel
            // 
            this.leftHandStausLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.leftHandStausLabel.AutoSize = true;
            this.leftHandStausLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftHandStausLabel.Location = new System.Drawing.Point(809, 242);
            this.leftHandStausLabel.Name = "leftHandStausLabel";
            this.leftHandStausLabel.Size = new System.Drawing.Size(134, 26);
            this.leftHandStausLabel.TabIndex = 1;
            this.leftHandStausLabel.Text = "Not Tracking";
            // 
            // rightHandStatusLabel
            // 
            this.rightHandStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rightHandStatusLabel.AutoSize = true;
            this.rightHandStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rightHandStatusLabel.Location = new System.Drawing.Point(809, 319);
            this.rightHandStatusLabel.Name = "rightHandStatusLabel";
            this.rightHandStatusLabel.Size = new System.Drawing.Size(134, 26);
            this.rightHandStatusLabel.TabIndex = 2;
            this.rightHandStatusLabel.Text = "Not Tracking";
            // 
            // leftHandLabel
            // 
            this.leftHandLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.leftHandLabel.AutoSize = true;
            this.leftHandLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftHandLabel.Location = new System.Drawing.Point(822, 216);
            this.leftHandLabel.Name = "leftHandLabel";
            this.leftHandLabel.Size = new System.Drawing.Size(106, 26);
            this.leftHandLabel.TabIndex = 4;
            this.leftHandLabel.Text = "Left Hand";
            // 
            // rightHandLabel
            // 
            this.rightHandLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rightHandLabel.AutoSize = true;
            this.rightHandLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rightHandLabel.Location = new System.Drawing.Point(814, 293);
            this.rightHandLabel.Name = "rightHandLabel";
            this.rightHandLabel.Size = new System.Drawing.Size(121, 26);
            this.rightHandLabel.TabIndex = 5;
            this.rightHandLabel.Text = "Right Hand";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(12, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(760, 619);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // emotionLabel
            // 
            this.emotionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.emotionLabel.AutoSize = true;
            this.emotionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emotionLabel.Location = new System.Drawing.Point(826, 370);
            this.emotionLabel.Name = "emotionLabel";
            this.emotionLabel.Size = new System.Drawing.Size(93, 26);
            this.emotionLabel.TabIndex = 7;
            this.emotionLabel.Text = "Emotion";
            // 
            // emotionStatusLabel
            // 
            this.emotionStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.emotionStatusLabel.AutoSize = true;
            this.emotionStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emotionStatusLabel.Location = new System.Drawing.Point(809, 396);
            this.emotionStatusLabel.Name = "emotionStatusLabel";
            this.emotionStatusLabel.Size = new System.Drawing.Size(134, 26);
            this.emotionStatusLabel.TabIndex = 6;
            this.emotionStatusLabel.Text = "Not Tracking";
            // 
            // personRadar1
            // 
            this.personRadar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.personRadar1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.personRadar1.Location = new System.Drawing.Point(798, 425);
            this.personRadar1.Name = "personRadar1";
            this.personRadar1.Size = new System.Drawing.Size(150, 150);
            this.personRadar1.TabIndex = 8;
            // 
            // facialDiagram1
            // 
            this.facialDiagram1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.facialDiagram1.Location = new System.Drawing.Point(798, 13);
            this.facialDiagram1.Name = "facialDiagram1";
            this.facialDiagram1.Size = new System.Drawing.Size(150, 200);
            this.facialDiagram1.TabIndex = 3;
            // 
            // colorButton
            // 
            this.colorButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.colorButton.Location = new System.Drawing.Point(778, 581);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(48, 23);
            this.colorButton.TabIndex = 9;
            this.colorButton.Text = "Color";
            this.colorButton.UseVisualStyleBackColor = true;
            this.colorButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // depthButton
            // 
            this.depthButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.depthButton.Location = new System.Drawing.Point(832, 581);
            this.depthButton.Name = "depthButton";
            this.depthButton.Size = new System.Drawing.Size(44, 23);
            this.depthButton.TabIndex = 10;
            this.depthButton.Text = "Depth";
            this.depthButton.UseVisualStyleBackColor = true;
            this.depthButton.Click += new System.EventHandler(this.depthButton_Click);
            // 
            // irButton
            // 
            this.irButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.irButton.Location = new System.Drawing.Point(882, 581);
            this.irButton.Name = "irButton";
            this.irButton.Size = new System.Drawing.Size(29, 23);
            this.irButton.TabIndex = 11;
            this.irButton.Text = "IR";
            this.irButton.UseVisualStyleBackColor = true;
            this.irButton.Click += new System.EventHandler(this.irButton_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(917, 581);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(47, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "LE IR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // takePicButton
            // 
            this.takePicButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.takePicButton.Location = new System.Drawing.Point(840, 609);
            this.takePicButton.Name = "takePicButton";
            this.takePicButton.Size = new System.Drawing.Size(79, 23);
            this.takePicButton.TabIndex = 13;
            this.takePicButton.Text = "Take Picture";
            this.takePicButton.UseVisualStyleBackColor = true;
            this.takePicButton.Click += new System.EventHandler(this.takePicButton_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 637);
            this.Controls.Add(this.takePicButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.irButton);
            this.Controls.Add(this.depthButton);
            this.Controls.Add(this.colorButton);
            this.Controls.Add(this.personRadar1);
            this.Controls.Add(this.emotionLabel);
            this.Controls.Add(this.emotionStatusLabel);
            this.Controls.Add(this.rightHandLabel);
            this.Controls.Add(this.leftHandLabel);
            this.Controls.Add(this.facialDiagram1);
            this.Controls.Add(this.rightHandStatusLabel);
            this.Controls.Add(this.leftHandStausLabel);
            this.Controls.Add(this.pictureBox1);
            this.MinimumSize = new System.Drawing.Size(956, 554);
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
        private Custom_Controls.PersonRadar personRadar1;
        private System.Windows.Forms.Button colorButton;
        private System.Windows.Forms.Button depthButton;
        private System.Windows.Forms.Button irButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button takePicButton;
    }
}

