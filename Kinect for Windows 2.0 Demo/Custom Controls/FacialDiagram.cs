using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kinect_for_Windows_2._0_Demo
{
    public enum Eye
    {
        Right = 0,
        Left = 1
    }
    public enum EyeStatus
    {
        Closed = 0,
        Open = 1
    }
    public enum MouthStatus
    {
        Closed = 0,
        Open = 1,
        Talking = 2
    }
    public partial class FacialDiagram : UserControl
    {
        public FacialDiagram()
        {
            InitializeComponent();
        }

        public void setMouth(MouthStatus status)
        {
            switch (status)
            {
                case MouthStatus.Closed:
                    this.mouthPictureBox.Image = Custom_Controls.Properties.Resources.mouth_closed;
                    break;
                case MouthStatus.Open:
                    this.mouthPictureBox.Image = Custom_Controls.Properties.Resources.mouth_open;
                    break;
                case MouthStatus.Talking:
                    this.mouthPictureBox.Image = Custom_Controls.Properties.Resources.mouth_open;
                    break;
            }
        }

        public void setEyes(Eye eye, EyeStatus status)
        {
            switch (eye)
            {
                case Eye.Left:
                    this.setEye(this.leftEyePictureBox, status);
                    break;
                case Eye.Right:
                    this.setEye(this.rightEyePictureBox, status);
                    break;
                default:
                    this.setEye(this.leftEyePictureBox, EyeStatus.Closed);
                    this.setEye(this.rightEyePictureBox, EyeStatus.Closed);
                    break;
            }
        }

        private void setEye(PictureBox eyePictureBox, EyeStatus status)
        {
            switch (status)
            {
                case EyeStatus.Closed:
                    eyePictureBox.Image = Custom_Controls.Properties.Resources.eye_closed;
                    break;
                case EyeStatus.Open:
                    eyePictureBox.Image = Custom_Controls.Properties.Resources.eye_open;
                    break;
            }
        }
    }
}
