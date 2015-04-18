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
    public partial class Form2 : Form
    {
        public Form2()
        {
            
            InitializeComponent();
            PictureBoxFrame.Visible = false;
            videoSourcePlayer1.Visible = true;
            videoSourcePlayer1.VideoSource = GlobalVar.stream;
            videoSourcePlayer1.Start();
            Thread tcpServerRunThread = new Thread(new ThreadStart(TcpServerRun));
            tcpServerRunThread.Start();
            
        }

        public void TcpServerRun()
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Any, 5004); //server listens for any ip adress client at port 5004
            tcpListener.Start();
            

            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                Thread tcpHandlerThread = new Thread(new ParameterizedThreadStart(tcpHandler));
                tcpHandlerThread.Start(client);
            }
        }

        public void tcpHandler(object client)
        {
            TcpClient mClient = (TcpClient)client;
            NetworkStream stream = mClient.GetStream();
            byte[] Message = new byte[1024];
            byte[] Answer = new byte[1024];
            string answer;

            stream.Read(Message, 0, Message.Length);
            GlobalVar.Picture = (Bitmap)videoSourcePlayer1.GetCurrentVideoFrame().Clone();

            GlobalVar.threshold();
            for (int i = 0; i < GlobalVar.ErodeCount; i++)
                GlobalVar.Erode();
            for (int i = 0; i < GlobalVar.DilateCount; i++)
                GlobalVar.Dilate();
            BlobCounter blobCounter = new BlobCounter();
            // process input image
            blobCounter.ProcessImage(GlobalVar.Picture2);
            // get information about detected objects
            Blob[] blobs = blobCounter.GetObjectsInformation();

            // cross-thread operation , delegate used
            PictureBoxFrame.Invoke((Action)delegate
            {
                PictureBoxFrame.SizeMode = PictureBoxSizeMode.Zoom;
                PictureBoxFrame.Image = GlobalVar.Picture2;
                PictureBoxFrame.Visible = true;
                foreach (var blob in blobs)
                {
                    textBox1.AppendText(blob.CenterOfGravity.ToString() + '\n');
                    Graphics g = PictureBoxFrame.CreateGraphics();
                    g.DrawEllipse(Pens.Pink, blob.CenterOfGravity.X, blob.CenterOfGravity.Y, 20, 20);
                }
            });
            Thread.Sleep(1000);
            int j = 0;
            answer = "";
            for (int i = 1; i<blobs.Length;i+=2)
            {
                answer += (i - j).ToString() + ',' + blobs[i - 1].CenterOfGravity.X.ToString() + ',' + blobs[i - 1].CenterOfGravity.Y.ToString() + ',' + (int)0 + ',' + blobs[i].CenterOfGravity.X.ToString() + ',' + blobs[i].CenterOfGravity.Y.ToString() + ',' + (int)0 + " ";
                
                j++;
            }

            Answer = System.Text.Encoding.ASCII.GetBytes(answer);
            stream.Write(Answer, 0, Answer.Length);

            stream.Close();
            mClient.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }
        
    }
}
