using System;
using System.IO;
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


namespace CompleteProgram
{
    public partial class Camera : Form
    {

        Bitmap bitmap;
        Blob[] blobs;
        public Camera()
        {
            InitializeComponent();

            string currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo(currentCulture);

            ci.NumberFormat.NumberDecimalSeparator = ",";
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;

            this.videoSourcePlayer1.Controls.Add(PictureBoxCameraAxis);
            this.PictureBoxCameraAxis.BackColor = Color.Transparent;
            this.PictureBoxCameraAxis.Paint += new PaintEventHandler(PictureBoxCameraAxis_Paint);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            videoSourcePlayer1.SignalToStop();
            videoSourcePlayer1.Stop();
        }

        public void video_NewFrame(object sender, ref Bitmap image)
        {
            // process the frame dynamically, method just in case
        
        }

        private void Connect_button_Click(object sender, EventArgs e)
        {
            string address = Address.Text;
            string login = Login.Text;
            string password = Password.Text;
            MJPEGStream stream = new MJPEGStream(address);
            stream.Login = login;
            stream.Password = password;
            //stream.NewFrame += new NewFrameEventHandler(video_NewFrame);
            //stream.Start();

            PictureBoxFrame.Visible = false;
            videoSourcePlayer1.Visible = true;
            videoSourcePlayer1.VideoSource = stream;
            videoSourcePlayer1.Start();

        }

        private void CameraAxisCheckBox_CheckedChanged(object sender, EventArgs e)
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

        private void PictureBoxCameraAxis_Paint(object sender, PaintEventArgs e)
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


        /// <summary>
        /// Trzeba sparametryzować : blobCounter Min Width i height oraz sparametryzować elementy rgb, w głównym programie (tym z macierzami) najpierws 
        /// sprawdzić długość txt (czy są łącznie 192 kolumny danych) a jeśli tak odpowiednio posortować
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            videoSourcePlayer1.SignalToStop();
            videoSourcePlayer1.Stop();
            videoSourcePlayer1.Visible = false;

            Bitmap bitmap1 = (Bitmap)bitmap.Clone();

            int R = Convert.ToInt32(R_value.Text);
            int G = Convert.ToInt32(G_value.Text);
            int B = Convert.ToInt32(B_value.Text);
            
            //process the frame before display in PictureBox
            Color white = Color.White;
            Color black = Color.Black;
            for (int y = 0; y < bitmap.Height; y++)
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color temp = bitmap.GetPixel(x, y);
                    
                    if(RMoreThan.Checked)
                    {
                        if(GMoreThan.Checked)
                        {
                            if(BMoreThan.Checked)
                            {
                                if ((temp.R > R) && (temp.G > G) && (temp.B > B))
                                    bitmap1.SetPixel(x, y, white);
                                else
                                    bitmap1.SetPixel(x, y, black);
                            }
                            else
                            {
                                if ((temp.R > R) && (temp.G > G) && (temp.B < B))
                                    bitmap1.SetPixel(x, y, white);
                                else
                                    bitmap1.SetPixel(x, y, black);
                            }
                        }
                        else
                        {
                            if(BMoreThan.Checked)
                            {
                                if ((temp.R > R) && (temp.G < G) && (temp.B > B))
                                    bitmap1.SetPixel(x, y, white);
                                else
                                    bitmap1.SetPixel(x, y, black);
                            }
                            else
                            {
                                if ((temp.R > R) && (temp.G < G) && (temp.B < B))
                                    bitmap1.SetPixel(x, y, white);
                                else
                                    bitmap1.SetPixel(x, y, black);
                            }
                        }
                    }
                    else
                    {
                        if(GMoreThan.Checked)
                        {
                            if(BMoreThan.Checked)
                            {
                                if ((temp.R < R) && (temp.G > G) && (temp.B > B))
                                    bitmap1.SetPixel(x, y, white);
                                else
                                    bitmap1.SetPixel(x, y, black);
                            }
                            else
                            {
                                if ((temp.R < R) && (temp.G > G) && (temp.B < B))
                                    bitmap1.SetPixel(x, y, white);
                                else
                                    bitmap1.SetPixel(x, y, black);
                            }
                        }
                        else
                        {
                            if(BMoreThan.Checked)
                            {
                                if ((temp.R < R) && (temp.G < G) && (temp.B > B))
                                    bitmap1.SetPixel(x, y, white);
                                else
                                    bitmap1.SetPixel(x, y, black);
                            }
                            else
                            {
                                if ((temp.R < R) && (temp.G < G) && (temp.B < B))
                                    bitmap1.SetPixel(x, y, white);
                                else
                                    bitmap1.SetPixel(x, y, black);
                            }
                        }
                    }
                }
            // create instance of blob counter
            BlobCounter blobCounter = new BlobCounter();
            //filter shapes
            blobCounter.FilterBlobs = true;
            blobCounter.MinHeight = Convert.ToInt32(MinH_value.Text);
            blobCounter.MinWidth = Convert.ToInt32(MinW_value.Text);
            // process input image
            blobCounter.ProcessImage(bitmap1);
            // get information about detected objects
            blobs = blobCounter.GetObjectsInformation();
            int counter_of_blobs = 0;
            if (blobs.Length == 192)
            {
                Blob[][] blobs_sorted = new Blob[12][];
                for (int i = 0; i < 12; i++)
                {
                    blobs_sorted[i] = new Blob[16];
                }
                List<Blob> blobs_list = blobs.ToList<Blob>();
                blobs_list.Sort(delegate(Blob a, Blob b)
                {
                    if (a.CenterOfGravity.Y == b.CenterOfGravity.Y)
                        return 0;
                    else if (a.CenterOfGravity.Y > b.CenterOfGravity.Y)
                        return 1;
                    else
                        return -1;
                });
                blobs = blobs_list.ToArray<Blob>();
                for (int i = 0; i < 12; i++)
                {
                    for (int j = 0; j < 16; j++)
                    {
                        blobs_sorted[i][j] = blobs[i * 16 + j];
                    }
                }
                for (int i = 0; i < 12; i++)
                {
                    blobs_list = blobs_sorted[i].ToList<Blob>();
                    blobs_list.Sort(delegate(Blob a, Blob b)
                    {
                        if (a.CenterOfGravity.X == b.CenterOfGravity.X)
                            return 0;
                        else if (a.CenterOfGravity.X > b.CenterOfGravity.X)
                            return 1;
                        else
                            return -1;
                    });
                    for (int j = 0; j < 16; j++)
                    {
                        blobs[i * 16 + j] = blobs_list[j];
                    }
                }
            }
            else if(blobs.Length > 10)
                MessageBox.Show("Wrong RGB value","Error");


            foreach (var blob in blobs)
            {
                counter_of_blobs++;
                listBox1.Items.Add(counter_of_blobs + "\t" + blob.CenterOfGravity.ToString());
                
            }
            
            PictureBoxFrame.Image = bitmap1;
            PictureBoxFrame.SizeMode = PictureBoxSizeMode.Zoom;
            PictureBoxFrame.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bitmap = (Bitmap)videoSourcePlayer1.GetCurrentVideoFrame().Clone();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt";
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                using(StreamWriter writer = new StreamWriter(saveFileDialog1.FileName))
                {
                    int counter_of_blobs = 0;
                    foreach (var blob in blobs)
                    {
                        counter_of_blobs++;
                        if (counter_of_blobs < 192)
                            writer.Write(blob.CenterOfGravity.X.ToString() + "\t");
                        else
                        {
                            writer.WriteLine(blob.CenterOfGravity.X.ToString());
                            counter_of_blobs = 0;
                        }

                    }

                    foreach (var blob in blobs)
                    {
                        counter_of_blobs++;
                        if (counter_of_blobs < 192)
                            writer.Write(blob.CenterOfGravity.Y.ToString() + "\t");
                        else
                            writer.Write(blob.CenterOfGravity.Y.ToString());
                    }
                }
            }
        }

    }
}
