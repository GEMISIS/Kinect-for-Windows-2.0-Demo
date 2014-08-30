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
    public enum ImageType
    {
        Color = 0,
        Depth = 1,
        IR = 2,
        LEIR = 3
    }
    public partial class mainForm : Form
    {
        /// <summary>
        /// The Kinect sensor to use.
        /// </summary>
        private KinectSensor kinectSensor = null;

        /// <summary>
        /// A reader for managing multiple frames.
        /// </summary>
        private MultiSourceFrameReader multiSourceFrameReader = null;

        /// <summary>
        /// The bitmap to show for the color image of what the Kinect sees.
        /// </summary>
        private Bitmap colorImageBitmap = null;
        /// <summary>
        /// The raw pixel data for the color image of what the Kinect sees.
        /// </summary>
        private byte[] colorImagePixelData = null;
        /// <summary>
        /// The raw pixel data for the IR image of what the Kinect sees.
        /// </summary>
        private ushort[] irImagePixelData = null;
        private ushort[] irImagePixelDataOld = null;
        /// <summary>
        /// The raw pixel data for the long IR exposure image of what the Kinect sees.
        /// </summary>
        private ushort[] irLEImagePixelData = null;

        long lastTime = 0;
        uint pulses = 0;
        Queue<float> hueValues = new Queue<float>();
        Queue<float> irValues = new Queue<float>();

        /// <summary>
        /// The number of potential bodies.
        /// </summary>
        private int bodyCount = 0;
        /// <summary>
        /// The list of bodies that the Kinect can see and their properties.
        /// </summary>
        private Body[] bodies = null;
        /// <summary>
        /// The index of the body that should be currently tracked.
        /// </summary>
        private int bodyToTrack = -1;

        ImageType imageType = ImageType.Color;

        public mainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (KinectSensor.GetDefault() != null)
            {
                this.kinectSensor = KinectSensor.GetDefault();

                if (this.kinectSensor != null)
                {
                    this.kinectSensor.Open();

                    FrameDescription colorFrameDescription = this.kinectSensor.ColorFrameSource.FrameDescription;
                    this.colorImagePixelData = new byte[colorFrameDescription.Width * colorFrameDescription.Height * 4];
                    FrameDescription irleFrameDescription = this.kinectSensor.LongExposureInfraredFrameSource.FrameDescription;
                    this.irLEImagePixelData = new ushort[irleFrameDescription.LengthInPixels];
                    FrameDescription irFrameDescription = this.kinectSensor.InfraredFrameSource.FrameDescription;
                    this.irImagePixelData = new ushort[irFrameDescription.Width * irFrameDescription.Height];
                    this.irImagePixelDataOld = new ushort[irFrameDescription.Width * irFrameDescription.Height];

                    this.bodyCount = this.kinectSensor.BodyFrameSource.BodyCount;
                    this.bodies = new Body[this.bodyCount];

                    this.multiSourceFrameReader = this.kinectSensor.OpenMultiSourceFrameReader(FrameSourceTypes.Infrared | FrameSourceTypes.Depth | FrameSourceTypes.Color | FrameSourceTypes.Body | FrameSourceTypes.LongExposureInfrared);

                    this.multiSourceFrameReader.MultiSourceFrameArrived += multiSourceFrameReader_MultiSourceFrameArrived;
                }
            }
        }

        void multiSourceFrameReader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            MultiSourceFrameReference msFrameReference = e.FrameReference;

            try
            {
                MultiSourceFrame msFrame = msFrameReference.AcquireFrame();

                if (msFrame != null)
                {
                    LongExposureInfraredFrameReference leirFrameReference = msFrame.LongExposureInfraredFrameReference;
                    InfraredFrameReference irFrameReference = msFrame.InfraredFrameReference;
                    ColorFrameReference colorFrameReference = msFrame.ColorFrameReference;
                    DepthFrameReference depthFrameReference = msFrame.DepthFrameReference;
                    BodyFrameReference bodyFrameReference = msFrame.BodyFrameReference;
                    switch (this.imageType)
                    {
                        case ImageType.Color:
                            useColorFrame(colorFrameReference);
                            break;
                        case ImageType.Depth:
                            useDepthFrame(depthFrameReference);
                            break;
                        case ImageType.IR:
                            useIRFrame(irFrameReference);
                            break;
                        case ImageType.LEIR:
                            useLIRFrame(leirFrameReference);
                            break;
                    }
                    useBodyFrame(bodyFrameReference);
                    //updatePulse(colorFrameReference, irFrameReference, bodyFrameReference);
                }
            }
            catch (Exception ex)
            {
            }
        }

        void updatePulse(ColorFrameReference colorFrameReference, InfraredFrameReference irFrameReference, BodyFrameReference bodyFrameReference)
        {
            long currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            int width = 0;
            int height = 0;
            try
            {
                InfraredFrame IRFrame = irFrameReference.AcquireFrame();

                if (IRFrame != null)
                {
                    using (IRFrame)
                    {
                        width = IRFrame.FrameDescription.Width;
                        height = IRFrame.FrameDescription.Height;

                        IRFrame.CopyFrameDataToArray(this.irImagePixelData);
                    }
                }
            }
            catch (Exception er)
            {
                string message = er.Message;
                Console.WriteLine(message);
                // Don't worry about empty frames.
            }
            try
            {
                if (this.bodyToTrack > -1)
                {
                    BodyFrame bodyFrame = bodyFrameReference.AcquireFrame();

                    if (bodyFrame != null)
                    {
                        using (bodyFrame)
                        {
                            bodyFrame.GetAndRefreshBodyData(this.bodies);

                            Body body = this.bodies[this.bodyToTrack];
                            if (body.IsTracked)
                            {
                                CameraSpacePoint headPosition = body.Joints[JointType.Head].Position;
                                CameraSpacePoint neckPosition = body.Joints[JointType.Neck].Position;

                                float centerX = neckPosition.X - headPosition.X;
                                centerX = headPosition.X + (centerX / 2.0f);

                                float centerY = neckPosition.Y - headPosition.Y;
                                centerY = headPosition.Y + (centerY / 2.0f);

                                centerX += 1.0f;
                                centerX /= 2.0f;

                                centerY += 1.0f;
                                centerY /= 2.0f;

                                if (this.colorImageBitmap != null)
                                {
                                    Color c = this.colorImageBitmap.GetPixel((int)(centerX * this.colorImageBitmap.Width), (int)(centerY * this.colorImageBitmap.Height));

                                    hueValues.Enqueue(c.GetHue());
                                    if (hueValues.Count > 10)
                                    {
                                        hueValues.Dequeue();
                                    }

                                    if (hueValues.Count >= 10)
                                    {
                                        //this.pulseLabel.Text = "Pulse: " + ((float)c.GetHue() / (float)hueValues.Average());
                                        if (c.GetHue() > hueValues.Average())
                                        {
                                            this.pulseLabel.Text = "Pulse: " + ((float)(currentTime - lastTime) / (float)pulses);
                                            //this.pulseLabel.Text = "Pulse: 1";
                                            pulses += 1;
                                        }
                                        if (currentTime - lastTime > 1000 * 5)
                                        {
                                            lastTime = currentTime;
                                            pulses = 0;
                                        }
                                        Console.WriteLine("Hue Average: " + hueValues.Average());
                                    }
                                }

                                if (width > 0 && height > 0)
                                {
                                    ushort irValue = this.irImagePixelData[(int)(centerX * width) + (int)(centerY * height) * width];

                                    irValues.Enqueue(irValue);
                                    if (irValues.Count > 10)
                                    {
                                        irValues.Dequeue();
                                    }

                                    if (irValues.Count >= 10)
                                    {
                                        //Console.WriteLine("IR Average: " + irValues.Average());
                                    }
                                }

                                //Console.WriteLine("Color: " + c.R + ", " + c.G + ", " + c.B);
                                //Console.WriteLine("Position:" + centerX + ", " + centerY);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Console.WriteLine(message);
                // Don't worry about empty frames.
            }
        }

        void useColorFrame(ColorFrameReference colorFrameReference)
        {
            try
            {
                ColorFrame colorFrame = colorFrameReference.AcquireFrame();

                if (colorFrame != null)
                {
                    using (colorFrame)
                    {
                        if (colorFrame.RawColorImageFormat == ColorImageFormat.Bgra)
                        {
                            colorFrame.CopyRawFrameDataToArray(this.colorImagePixelData);
                        }
                        else
                        {
                            colorFrame.CopyConvertedFrameDataToArray(this.colorImagePixelData, ColorImageFormat.Bgra);
                        }

                        this.updateBitmap(colorFrame.FrameDescription.Width, colorFrame.FrameDescription.Height, PixelFormat.Format32bppArgb, this.colorImagePixelData);

                        this.pictureBox1.Image = new Bitmap(this.colorImageBitmap, this.pictureBox1.Width, this.pictureBox1.Height);
                    }
                }
            }
            catch (Exception er)
            {
                string message = er.Message;
                Console.WriteLine(message);
                // Don't worry about empty frames.
            }
        }
        void useDepthFrame(DepthFrameReference depthFrameReference)
        {
            try
            {
                DepthFrame depthFrame = depthFrameReference.AcquireFrame();

                if (depthFrame != null)
                {
                    using (depthFrame)
                    {
                        depthFrame.CopyFrameDataToArray(this.irImagePixelData);

                        this.updateBitmap(depthFrame.FrameDescription.Width, depthFrame.FrameDescription.Height, this.irImagePixelData, true);

                        this.pictureBox1.Image = new Bitmap(this.colorImageBitmap, this.pictureBox1.Width, this.pictureBox1.Height);
                    }
                }
            }
            catch (Exception er)
            {
                string message = er.Message;
                Console.WriteLine(message);
                // Don't worry about empty frames.
            }
        }
        void useIRFrame(InfraredFrameReference irFrameReference)
        {
            try
            {
                InfraredFrame IRFrame = irFrameReference.AcquireFrame();

                if (IRFrame != null)
                {
                    using (IRFrame)
                    {
                        IRFrame.CopyFrameDataToArray(this.irImagePixelData);

                        this.updateBitmap(IRFrame.FrameDescription.Width, IRFrame.FrameDescription.Height, this.irImagePixelData, false);

                        IRFrame.CopyFrameDataToArray(this.irImagePixelDataOld);

                        this.pictureBox1.Image = new Bitmap(this.colorImageBitmap, this.pictureBox1.Width, this.pictureBox1.Height);
                    }
                }
            }
            catch (Exception er)
            {
                string message = er.Message;
                Console.WriteLine(message);
                // Don't worry about empty frames.
            }
        }
        void useLIRFrame(LongExposureInfraredFrameReference leIRFrameReference)
        {
            try
            {
                LongExposureInfraredFrame leIRFrame = leIRFrameReference.AcquireFrame();

                if (leIRFrame != null)
                {
                    using (leIRFrame)
                    {
                        leIRFrame.CopyFrameDataToArray(this.irLEImagePixelData);

                        this.updateBitmap(leIRFrame.FrameDescription.Width, leIRFrame.FrameDescription.Height, this.irLEImagePixelData, false);

                        this.pictureBox1.Image = new Bitmap(this.colorImageBitmap, this.pictureBox1.Width, this.pictureBox1.Height);
                    }
                }
            }
            catch (Exception er)
            {
                string message = er.Message;
                Console.WriteLine(message);
                // Don't worry about empty frames.
            }
        }
        void useBodyFrame(BodyFrameReference bodyFrameReference)
        {
            try
            {
                BodyFrame bodyFrame = bodyFrameReference.AcquireFrame();

                if (bodyFrame != null)
                {
                    using (bodyFrame)
                    {
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
            catch (Exception ex)
            {
                string message = ex.Message;
                Console.WriteLine(message);
                // Don't worry about empty frames.
            }
        }

        private void updateBitmap(int width, int height, PixelFormat pixelFormat, byte[] data)
        {
            this.colorImageBitmap = new Bitmap(width, height, pixelFormat);
            BitmapData bmpData = this.colorImageBitmap.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly, this.colorImageBitmap.PixelFormat);
            IntPtr ptr = bmpData.Scan0;

            int bytes = bmpData.Stride * this.colorImageBitmap.Height;

            Marshal.Copy(data, 0, ptr, bytes);
            this.colorImageBitmap.UnlockBits(bmpData);
        }
        private void updateBitmap(int width, int height, ushort[] rawData, bool colored)
        {
            this.colorImageBitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData bmpData = this.colorImageBitmap.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly, this.colorImageBitmap.PixelFormat);
            IntPtr ptr = bmpData.Scan0;

            int bytes = bmpData.Stride * this.colorImageBitmap.Height;

            if (colored)
            {
                byte[] data = new byte[rawData.Length * 4];
                int pixel = 0;
                for (int i = 0; i < rawData.Length; i += 1)
                {
                    int value = rawData[i];
                    int widthTotal = 5;
                    int heightTotal = 5;
                    int maxArea = 8;
                    if (value == -1)
                    {
                        int temp = 0;
                        int tempTotal = 0;
                        for (int y = 0; y < heightTotal; y += 1)
                        {
                            for (int x = 0; x < widthTotal; x += 1)
                            {
                                if (tempTotal > maxArea || i + (width * -y) - x < 0 || i + (width * y) + x >= rawData.Length)
                                {
                                    break;
                                }

                                if (rawData[i + (width * y) + x] != 0)
                                {
                                    temp += rawData[i + width * y + x];
                                    tempTotal += 1;
                                }
                                if (rawData[i + (width * y) - x] != 0)
                                {
                                    temp += rawData[i + width * y + x];
                                    tempTotal += 1;
                                }
                                if (rawData[i + (width * -y) + x] != 0)
                                {
                                    temp += rawData[i + width * y + x];
                                    tempTotal += 1;
                                }
                                if (rawData[i + (width * -y) - x] != 0)
                                {
                                    temp += rawData[i + width * y + x];
                                    tempTotal += 1;
                                }
                            }
                            if (tempTotal > maxArea)
                            {
                                break;
                            }
                        }
                        if (tempTotal > 0)
                        {
                            value = (temp / tempTotal);
                            rawData[i] = (byte)value;
                        }
                    }
                    byte b = (byte)(255 - (value >> 6));
                    byte g = (byte)(255 - (value >> 4));
                    byte r = (byte)(255 - (value >> 2));
                    data[pixel++] = b;
                    data[pixel++] = g;
                    data[pixel++] = r;
                    data[pixel++] = 255;
                }

                Marshal.Copy(data, 0, ptr, bytes);
            }
            else
            {
                byte[] data = new byte[rawData.Length * 4];
                int pixel = 0;
                for (int i = 0; i < rawData.Length; i += 1)
                {
                    ushort currentRaw = rawData[i];
                    ushort oldRaw = irImagePixelDataOld[i];
                    int rawValue = Math.Abs(rawData[i] - irImagePixelDataOld[i]);
                    if(rawValue > 1000)
                    {
                        byte intensity = (byte)(rawValue >> 8);

                        data[pixel++] = 0;
                        data[pixel++] = 0;
                        data[pixel++] = 255;
                    }
                    else
                    {
                        byte intensity = (byte)(rawValue >> 8);

                        data[pixel++] = intensity;
                        data[pixel++] = 0;
                        data[pixel++] = 0;
                    }

                    data[pixel++] = 255;
                }

                Marshal.Copy(data, 0, ptr, bytes);
            }

            this.colorImageBitmap.UnlockBits(bmpData);
        }
        private void updateBitmap(int width, int height, PixelFormat pixelFormat, ushort[] data)
        {
            this.colorImageBitmap = new Bitmap(width, height, pixelFormat);
            BitmapData bmpData = this.colorImageBitmap.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly, this.colorImageBitmap.PixelFormat);
            IntPtr ptr = bmpData.Scan0;

            int bytes = bmpData.Stride * this.colorImageBitmap.Height;

            byte[] newData = new byte[data.Length * sizeof(byte)];
            Buffer.BlockCopy(data, 0, newData, 0, newData.Length);
            Marshal.Copy(newData, 0, ptr, bytes);
            this.colorImageBitmap.UnlockBits(bmpData);
        }

        private void findBodyToTrack()
        {
            for (int i = 0; i < this.bodyCount;i += 1)
            {
                if (this.bodies[i].IsTracked)
                {
                    if (this.startTrackingGesture(this.bodies[i]))
                    {
                        this.bodyToTrack = i;
                        break;
                    }
                }
            }
        }
        private bool startTrackingGesture(Body body)
        {
            //if (body.Activities[Activity.LookingAway] == DetectionResult.No)
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
                    updateRadar(null);
                    this.bodyToTrack = -1;
                }
                else
                {
                    this.updateEyes(body);
                    this.updateMouth(body);
                    this.updateHands(body);
                    this.updateEmotions(body);
                    this.updateRadar(body);
                }
            }
            else
            {
                this.bodyToTrack = -1;
            }
        }
        private bool stopTrackingGesture(Body body)
        {
           // if (body.Activities[Activity.LookingAway] == DetectionResult.Yes)
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
        private void updateRadar(Body body)
        {
            Point position = new Point(70, 70);
            if (body != null)
            {
                position = new Point((int)((body.Joints[JointType.SpineMid].Position.X + 1) * 150 / 2), (int)(body.Joints[JointType.SpineMid].Position.Z * 150 / 6));
            }
            this.personRadar1.setPosition(position);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.multiSourceFrameReader != null)
            {
                this.multiSourceFrameReader.Dispose();
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

        private void colorButton_Click(object sender, EventArgs e)
        {
            imageType = ImageType.Color;
        }

        private void depthButton_Click(object sender, EventArgs e)
        {
            imageType = ImageType.Depth;
        }

        private void irButton_Click(object sender, EventArgs e)
        {
            imageType = ImageType.IR;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            imageType = ImageType.LEIR;
        }

        private void takePicButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "png files (*.png)|*.png";
            dialog.FilterIndex = 1;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.pictureBox1.Image.Save(dialog.FileName);
            }
        }
    }
}
