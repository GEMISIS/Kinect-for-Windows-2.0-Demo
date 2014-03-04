using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Microsoft.Kinect;

namespace Kinect_for_Windows_2._0_Demo
{
    public partial class mainForm : Form
    {
        /// <summary>
        /// The Kinect sensor to use.
        /// </summary>
        private KinectSensor kinectSensor = null;

        /// <summary>
        /// The reader for reading data from a color frame.
        /// </summary>
        private ColorFrameReader colorFrameReader = null;
        /// <summary>
        /// The reader for getting information on the bodies the Kinect can see.
        /// </summary>
        private BodyFrameReader bodyFrameReader = null;

        /// <summary>
        /// The bitmap to show for the color image of what the Kinect sees.
        /// </summary>
        private Bitmap colorImageBitmap = null;
        /// <summary>
        /// The raw pixel data for the color image of what the Kinect sees.
        /// </summary>
        private byte[] colorImagePixelData = null;

        /// <summary>
        /// The list of bodies that the Kinect can see and their properties.
        /// </summary>
        private List<Body> bodies = new List<Body>();
        /// <summary>
        /// The index of the body that should be currently tracked.
        /// </summary>
        private int bodyToTrack = -1;

        public mainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (KinectSensor.KinectSensors.Count > 0)
            {
                this.kinectSensor = KinectSensor.Default;

                if (this.kinectSensor != null)
                {
                    this.kinectSensor.Open();

                    this.colorFrameReader = this.kinectSensor.ColorFrameSource.OpenReader();
                    this.bodyFrameReader = this.kinectSensor.BodyFrameSource.OpenReader();

                    this.setupColorFrameReader();
                    this.setupBodyFrameReader();
                }
            }
        }

        private void setupColorFrameReader()
        {
            if (this.colorFrameReader != null)
            {
                FrameDescription colorFrameDescription = this.kinectSensor.ColorFrameSource.FrameDescription;
                this.colorImagePixelData = new byte[colorFrameDescription.Width * colorFrameDescription.Height * 4];
             
                this.colorFrameReader.FrameArrived += colorFrameReader_FrameArrived;
            }
        }

        private void setupBodyFrameReader()
        {
            if (this.bodyFrameReader != null)
            {
                this.bodyFrameReader.FrameArrived += bodyFrameReader_FrameArrived;
            }
        }

        void colorFrameReader_FrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            ColorFrameReference colorFrameReference = e.FrameReference;

            try
            {
                ColorFrame colorFrame = colorFrameReference.AcquireFrame();

                if (colorFrame != null)
                {
                    using (colorFrame)
                    {
                        if (colorFrame.RawColorImageFormat == ColorImageFormat.Rgba)
                        {
                            colorFrame.CopyRawFrameDataToArray(this.colorImagePixelData);
                        }
                        else
                        {
                            colorFrame.CopyConvertedFrameDataToArray(this.colorImagePixelData, ColorImageFormat.Rgba);
                        }

                        this.updateBitmap(colorFrame.FrameDescription.Width, colorFrame.FrameDescription.Height);

                        this.pictureBox1.Image = (Image)(new Bitmap(this.colorImageBitmap, this.pictureBox1.Image.Size));
                    }
                }
            }
            catch (Exception)
            {
                // Don't worry about empty frames.
            }
        }
        void bodyFrameReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            BodyFrameReference bodyFrameReference = e.FrameReference;

            try
            {
                BodyFrame bodyFrame = bodyFrameReference.AcquireFrame();

                if (bodyFrame != null)
                {
                    using (bodyFrame)
                    {
                        this.bodies.Clear();
                        bodyFrame.GetAndRefreshBodyData(this.bodies);
                        if (this.bodyToTrack < 0)
                        {
                            this.findBodyToTrack();
                        }

                        if (this.bodyToTrack > -1)
                        {
                            this.updateCurrentBody();
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Don't worry about empty frames.
            }
        }

        private void updateBitmap(int width, int height)
        {
            this.colorImageBitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData bmpData = this.colorImageBitmap.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly, this.colorImageBitmap.PixelFormat);
            IntPtr ptr = bmpData.Scan0;

            int bytes = bmpData.Stride * this.colorImageBitmap.Height;
            byte[] rgbaValues = new byte[bytes];

            Marshal.Copy(rgbaValues, 0, ptr, bytes);
            this.colorImageBitmap.UnlockBits(bmpData);
        }

        private void findBodyToTrack()
        {
            foreach (Body body in this.bodies)
            {
                if (body.IsTracked)
                {
                    if (this.startTrackingGesture(body))
                    {
                        this.bodyToTrack = this.bodies.IndexOf(body);
                        break;
                    }
                }
            }
        }
        private bool startTrackingGesture(Body body)
        {
            if (body.Activities[Activity.LookingAway] == DetectionResult.No)
            {
                if (body.HandLeftState == HandState.Lasso && body.HandRightState == HandState.Open)
                {
                    return true;
                }
            }
            return false;
        }

        private void updateCurrentBody()
        {
            Body body = this.bodies[this.bodyToTrack];

            using (body)
            {
                if (body.IsTracked)
                {
                    if (stopTrackingGesture(body))
                    {
                        facialDiagram1.setEyes(Eye.Left, EyeStatus.Closed);
                        facialDiagram1.setEyes(Eye.Right, EyeStatus.Closed);
                        facialDiagram1.setMouth(MouthStatus.Closed);
                        this.leftHandStausLabel.Text = "Not Tracking";
                        this.rightHandStatusLabel.Text = "Not Tracking";
                        this.emotionStatusLabel.Text = "Not Tracking";
                        this.bodyToTrack = -1;
                    }
                    else
                    {
                        this.updateEyes(body);
                        this.updateMouth(body);
                        this.updateHands(body);
                        this.updateEmotions(body);
                    }
                }
                else
                {
                    this.bodyToTrack = -1;
                }
            }
        }
        private bool stopTrackingGesture(Body body)
        {
            if (body.Activities[Activity.LookingAway] == DetectionResult.Yes)
            {
                if (body.HandLeftState == HandState.Open && body.HandRightState == HandState.Lasso)
                {
                    return true;
                }
            }
            return false;
        }
 
        private void updateEyes(Body body)
        {
            switch (body.Activities[Activity.EyeLeftClosed])
            {
                case DetectionResult.No:
                    facialDiagram1.setEyes(Eye.Left, EyeStatus.Open);
                    break;
                case DetectionResult.Yes:
                default:
                    facialDiagram1.setEyes(Eye.Left, EyeStatus.Closed);
                    break;
            }
            switch (body.Activities[Activity.EyeRightClosed])
            {
                case DetectionResult.No:
                    facialDiagram1.setEyes(Eye.Right, EyeStatus.Open);
                    break;
                case DetectionResult.Yes:
                default:
                    facialDiagram1.setEyes(Eye.Right, EyeStatus.Closed);
                    break;
            }
        }
        private void updateMouth(Body body)
        {
            switch (body.Activities[Activity.MouthOpen])
            {
                case DetectionResult.Yes:
                    facialDiagram1.setMouth(MouthStatus.Open);
                    break;
                case DetectionResult.No:
                default:
                    facialDiagram1.setMouth(MouthStatus.Closed);
                    break;
            }
        }
        private void updateHands(Body body)
        {
            switch (body.HandLeftState)
            {
                case HandState.Closed:
                    this.leftHandStausLabel.Text = "Closed";
                    break;
                case HandState.Open:
                    this.leftHandStausLabel.Text = "Open";
                    break;
                case HandState.Lasso:
                    this.leftHandStausLabel.Text = "Lasso";
                    break;
                case HandState.NotTracked:
                    this.leftHandStausLabel.Text = "Not Tracked";
                    break;
            }
            switch (body.HandRightState)
            {
                case HandState.Closed:
                    this.rightHandStatusLabel.Text = "Closed";
                    break;
                case HandState.Open:
                    this.rightHandStatusLabel.Text = "Open";
                    break;
                case HandState.Lasso:
                    this.rightHandStatusLabel.Text = "Lasso";
                    break;
                case HandState.NotTracked:
                    this.rightHandStatusLabel.Text = "Not Tracked";
                    break;
            }
        }
        private void updateEmotions(Body body)
        {
            switch (body.Expressions[Expression.Happy])
            {
                case DetectionResult.Yes:
                    this.emotionStatusLabel.Text = "Happy";
                    break;
                case DetectionResult.No:
                    this.emotionStatusLabel.Text = "Unhappy";
                    break;
            }
            switch (body.Expressions[Expression.Neutral])
            {
                case DetectionResult.Yes:
                default:
                    this.emotionStatusLabel.Text = "Neutral";
                    break;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.colorFrameReader != null)
            {
                this.colorFrameReader.Dispose();
            }
            if (this.bodyFrameReader != null)
            {
                this.bodyFrameReader.Dispose();
            }

            if (this.kinectSensor != null)
            {
                this.kinectSensor.Close();
            }

            if (this.colorImageBitmap != null)
            {
                this.colorImageBitmap.Dispose();
            }

            this.pictureBox1.Dispose();
        }
    }
}
