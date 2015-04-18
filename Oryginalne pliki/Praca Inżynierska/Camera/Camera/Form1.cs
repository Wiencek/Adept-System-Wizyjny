using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Controls;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using AForge.Imaging.Filters;

using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Camera
{
    public partial class Form1 : Form
    {
        //public Bitmap bitmap, Picture, bitmap3;
        //public Color white = Color.White;
        //public Color black = Color.Black;
        //public int thresholdMemory;
        //Bitmap Picture2;
        //int thresholdno, thresholdSize,  ErodeCount, DilateCount;
        //MJPEGStream stream;

        public Form1()
        {
            InitializeComponent();

            this.videoSourcePlayer1.Controls.Add(PictureBoxCameraAxis);
            this.PictureBoxCameraAxis.BackColor = Color.Transparent;
            this.PictureBoxCameraAxis.Paint += new PaintEventHandler(PictureBoxCameraAxis_Paint);
        }

        public void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            videoSourcePlayer1.SignalToStop();
            videoSourcePlayer1.Stop();
        }


        // function initializes stream flow from the camera
        public void Connect_button_Click(object sender, EventArgs e)
        {
            string address = Address.Text;
            string login = Login.Text;
            string password = Password.Text;
            GlobalVar.stream = new MJPEGStream(address);
            GlobalVar.stream.Login = login;
            GlobalVar.stream.Password = password;
            PictureBoxFrame.Visible = false;
            videoSourcePlayer1.Visible = true;
            videoSourcePlayer1.VideoSource = GlobalVar.stream;
            videoSourcePlayer1.Start();
        }
        // functions for axis in the image
        public void CameraAxisCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CameraAxisCheckBox.Checked == true)
            {
                PictureBoxCameraAxis.Visible = true;
            }
            else
            {
                PictureBoxCameraAxis.Visible = false;
            }
        }
        public void PictureBoxCameraAxis_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Point start1 = new System.Drawing.Point((PictureBoxCameraAxis.Width / 2) - 12, (PictureBoxCameraAxis.Height / 2) - 12);
            System.Drawing.Point start2 = new System.Drawing.Point((PictureBoxCameraAxis.Width / 2) - 13, (PictureBoxCameraAxis.Height / 2) - 13);
            System.Drawing.Point endHorizontal1 = new System.Drawing.Point((PictureBoxCameraAxis.Width) - 12, (PictureBoxCameraAxis.Height / 2) - 12);
            System.Drawing.Point endHorizontal2 = new System.Drawing.Point((PictureBoxCameraAxis.Width) - 13, (PictureBoxCameraAxis.Height / 2) - 13);
            System.Drawing.Point endVertical1 = new System.Drawing.Point((PictureBoxCameraAxis.Width / 2) - 12, PictureBoxCameraAxis.Height - 12);
            System.Drawing.Point endVertical2 = new System.Drawing.Point((PictureBoxCameraAxis.Width / 2) - 13, (PictureBoxCameraAxis.Height) - 13);


            e.Graphics.DrawLine(Pens.Yellow, start1, endHorizontal1);
            e.Graphics.DrawLine(Pens.Yellow, start1, endVertical1);
            e.Graphics.DrawLine(Pens.Yellow, start2, endHorizontal2);
            e.Graphics.DrawLine(Pens.Yellow, start2, endVertical2);
        }
        // function initializes image process parameters, gets image from current frame and updates picture box frame 
        public void button1_Click(object sender, EventArgs e)
        {
            GlobalVar.thresholdno = hScrollBar1.Value;
            GlobalVar.thresholdSize = hScrollBar1.Value;
            GlobalVar.DilateCount = 0;
            GlobalVar.ErodeCount = 0;
            GetImage();
            UpdatePBF(GlobalVar.Picture);

        }
        // function clones image from source player to the memory as a picture bitmap
        public void GetImage()
        {
            GlobalVar.Picture = (Bitmap)videoSourcePlayer1.GetCurrentVideoFrame().Clone();

            videoSourcePlayer1.SignalToStop();
            videoSourcePlayer1.Stop();
            videoSourcePlayer1.Visible = false;
        }

        // functon hides current form and initializes server
        public void button2_Click(object sender, EventArgs e)
        { 
            
            this.Hide();
            Form2 f2 = new Form2();
            f2.ShowDialog();
            
        }

       
        // function updates user interface (text box)
        public void updateUI(string s)
        {
            Func<int> del = delegate()
            {
                textBox1.AppendText(s + System.Environment.NewLine);
                return 0;
            };
            Invoke(del);
        }
        // change of threshold value on slider change 
        public void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            GlobalVar.thresholdno = hScrollBar1.Value;
            GlobalVar.threshold();
            UpdatePBF(GlobalVar.bitmap);
            textBox2.Text = GlobalVar.thresholdno.ToString();
        }

        // update of picture box frame
        public void UpdatePBF(Bitmap image)
        {
            PictureBoxFrame.SizeMode = PictureBoxSizeMode.Zoom;
            PictureBoxFrame.Image = image;
            PictureBoxFrame.Visible = true;
        }

        // function calculates coordinates of objects and writes them into text box

        public void Coords()
        {
            // create instance of blob counter
            BlobCounter blobCounter = new BlobCounter();
            // process input image
            blobCounter.ProcessImage(GlobalVar.Picture2);
            // get information about detected objects
            Blob[] blobs = blobCounter.GetObjectsInformation();

            int i = 1;
            foreach (var blob in blobs)
            {
                textBox1.AppendText(i + ". element coordinates X Y : " + blob.CenterOfGravity.ToString()+'\n');
                Graphics g = PictureBoxFrame.CreateGraphics();
                g.DrawEllipse(Pens.Pink, blob.CenterOfGravity.X, blob.CenterOfGravity.Y, 20, 20);
                i++;
            }
            
        }

        // erode image on button click
        public void button4_Click(object sender, EventArgs e)
        {
            GlobalVar.Erode();
            UpdatePBF(GlobalVar.Picture2);
            GlobalVar.ErodeCount += 1;
            textBox3.Text = GlobalVar.ErodeCount.ToString();

        }
        // change of threshold size on slider change
        public void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            GlobalVar.thresholdSize = hScrollBar2.Value;
            GlobalVar.threshold();
            UpdatePBF(GlobalVar.bitmap);
            textBox4.Text = GlobalVar.thresholdSize.ToString();
        }

        
        
        // sve settings button
        public void button3_Click(object sender, EventArgs e)
        {
            hScrollBar1.Enabled = false;
            hScrollBar2.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            button2.Enabled = true;
            textBox1.Enabled = true;

        }
        // ditale image on button click
        public void button5_Click(object sender, EventArgs e)
        {
            GlobalVar.Dilate();
            UpdatePBF(GlobalVar.Picture2);
            GlobalVar.DilateCount += 1;
            textBox5.Text = GlobalVar.DilateCount.ToString();
        }

        public void button6_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }
        // test button
        public void button7_Click(object sender, EventArgs e)
        {
           
            Coords();
        }
        // get image button
        private void button8_Click(object sender, EventArgs e)
        {
            GetImage();

            GlobalVar.threshold();
            for (int i = 0; i < GlobalVar.ErodeCount; i++)
                GlobalVar.Erode();
            for (int i = 0; i < GlobalVar.DilateCount; i++)
                GlobalVar.Dilate();

            UpdatePBF(GlobalVar.Picture2);
            Coords();
        }

    }
};