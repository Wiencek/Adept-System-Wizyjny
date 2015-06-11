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

namespace CompleteProgram
{
    public partial class ObjectCoordinates : Form
    {
        double[,] T;
        double[,] object1;
        double[,] object2;
        double[,] xkki1;
        double[,] xkki2;
        double[,] ykki1;
        double[,] ykki2;
        double[,] xkko1;
        double[,] xkko2;
        double[,] ykko1;
        double[,] ykko2;
        double[,] xod1;
        double[,] yod1;
        double[,] xod2;
        double[,] yod2;
        double[,] Tk1;
        double[,] Tk2;
        double[] xk1;
        double[] xk2;
        double[,] xkkos1;
        double[,] ykkos1;
        double[,] xkkos2;
        double[,] ykkos2;
        double[,] xods1;
        double[,] yods1;
        double[,] xods2;
        double[,] yods2;
        double[, ,] C1B;
        double[, ,] C2B;
        double[][] error1;
        double[][] error2;
        double[,] K1;
        double[,] K2;
        double x1, x2, y1, y2, z1, z2, dx, dy, pxx1, pxy1, nx01, ny01, pxx2, pxy2, nx02, ny02, zf1, zf2;
        public ObjectCoordinates()
        {
            InitializeComponent();

            string currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo(currentCulture);

            ci.NumberFormat.NumberDecimalSeparator = ",";
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;

            xkki1 = new double[12, 16];
            xkki2 = new double[12, 16];
            ykki1 = new double[12, 16];
            ykki2 = new double[12, 16];
            xkko1 = new double[12, 16];
            xkko2 = new double[12, 16];
            ykko1 = new double[12, 16];
            ykko2 = new double[12, 16];
            xod1 = new double[12, 16];
            yod1 = new double[12, 16];
            xod2 = new double[12, 16];
            yod2 = new double[12, 16];
            Tk1 = new double[4, 4];
            Tk2 = new double[4, 4];
            xk1 = new double[9];
            xk2 = new double[9];
            xkkos1 = new double[12, 16];
            ykkos1 = new double[12, 16];
            xkkos2 = new double[12, 16];
            ykkos2 = new double[12, 16];
            xods1 = new double[12, 16];
            yods1 = new double[12, 16];
            xods2 = new double[12, 16];
            yods2 = new double[12, 16];
            C1B = new double[12, 16, 2];
            C2B = new double[12, 16, 2];
            error1 = new double[6][];
            error2 = new double[6][];
            for (int i = 0; i < 6; i++)
            {
                error1[i] = new double[9];
                error2[i] = new double[9];
            }
            K1 = new double[4, 5];
            K2 = new double[4, 5];
            object1 = new double[4, 2];
            object2 = new double[4, 2];
            T = new double[4, 4];
        }

        MainForm mainFrm;
        public ObjectCoordinates(MainForm callingForm, double cam1x, double cam1y, double cam1z, double cam2x, double cam2y, double cam2z) : this()
        {
            mainFrm = callingForm;

            textBox_x1.Text = cam1x.ToString();
            textBox_y1.Text = cam1y.ToString();
            textBox_z1.Text = cam1z.ToString();
            textBox_x2.Text = cam2x.ToString();
            textBox_y2.Text = cam2y.ToString();
            textBox_z2.Text = cam2z.ToString();

            backgroundWorker1.RunWorkerAsync();
        }

        private void Load1_Click(object sender, EventArgs e)
        {
            //openFileDialog1.Filter = "Text files (*.txt)|*.txt";
            //openFileDialog1.CheckFileExists = true;
            //openFileDialog1.CheckPathExists = true;
            //DialogResult result = openFileDialog1.ShowDialog();
            //if(result == )
            //{
                using(System.IO.StreamReader reader = new System.IO.StreamReader("set_cam0_paramset0.txt"))
                {
                    int wordCounter;
                    for (int k = 0; k < 2; k++)
                    {
                        wordCounter = 0;
                        string line = reader.ReadLine();
                        string[] words = line.Split('\t');
                        for (int i = 0; i < 12; i++)
                        {
                            for (int j = 0; j < 16; j++)
                            {
                                C1B[i, j, k] = Double.Parse(words[wordCounter]);
                                wordCounter++;
                            }
                        }
                    }
                }
                Calibrate1.Enabled = true;
                Camera1Error.Enabled = false;
            //}
        }

        private void Calibrate1_Click(object sender, EventArgs e)
        {
            x1 = Convert.ToDouble(textBox_x1.Text);
            y1 = Convert.ToDouble(textBox_y1.Text);
            z1 = Convert.ToDouble(textBox_z1.Text);
            zf1 = Convert.ToDouble(textBox_f1.Text);
            pxx1 = Convert.ToDouble(textBox_pxx1.Text);
            pxy1 = Convert.ToDouble(textBox_pxy1.Text);
            nx01 = Convert.ToDouble(textBox_nx01.Text);
            ny01 = Convert.ToDouble(textBox_ny01.Text);
            dx = Convert.ToDouble(textBox_dx.Text);
            dy = Convert.ToDouble(textBox_dy.Text);

            ObjectCoordinatesCamera.kalibracja(x1, y1, z1, dx, dy, Tk1, xk1, zf1, pxx1, pxy1, nx01, ny01, C1B, xkko1, ykko1, xkki1, ykki1, xod1, yod1, xkkos1, ykkos1, xods1, yods1, error1, K1);
            displayTc1.Clear();
            string specifier = "0.00000";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    displayTc1.Text += Tk1[i, j].ToString(specifier) + "  ";
                }
                displayTc1.Text += "\n";
            }

            textBox_n11.Text = "k1= " + K1[0, 0].ToString(specifier) + " k2= k3= p1= p2= 0";
            textBox_n12.Text = "k1= " + K1[1, 0].ToString(specifier) + " k2= " + K1[1, 1].ToString(specifier) + " k3= p1= p2= 0";
            textBox_n13.Text = "k1= " + K1[2, 0].ToString(specifier) + " k2= " + K1[2, 1].ToString(specifier) + " k3= " + K1[2, 2].ToString(specifier) + " p1= p2= 0";
            textBox_n15.Text = "k1= " + K1[3, 0].ToString(specifier) + " k2= " + K1[3, 1].ToString(specifier) + " k3= " + K1[3, 2].ToString(specifier) + " p1= " + K1[3, 3].ToString(specifier) + " p2=  " + K1[3, 4].ToString(specifier);
 
            Calibrate1.Enabled = false;
            Camera1Error.Enabled = true;
        }

        private void Load2_Click(object sender, EventArgs e)
        {
            //openFileDialog1.Filter = "Text files (*.txt)|*.txt";
            //openFileDialog1.CheckFileExists = true;
            //openFileDialog1.CheckPathExists = true;
            //DialogResult result = openFileDialog1.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            using (System.IO.StreamReader reader = new System.IO.StreamReader("set_cam1_paramset0.txt"))
                {
                    int wordCounter;
                    for (int k = 0; k < 2; k++)
                    {
                        wordCounter = 0;
                        string line = reader.ReadLine();
                        string[] words = line.Split('\t');
                        for (int i = 0; i < 12; i++)
                        {
                            for (int j = 0; j < 16; j++)
                            {
                                C2B[i, j, k] = Double.Parse(words[wordCounter]);
                                wordCounter++;
                            }
                        }
                    }
                }
                Calibrate2.Enabled = true;
                Camera2Error.Enabled = false;
            //}
        }

        private void Calibrate2_Click(object sender, EventArgs e)
        {
            x2 = Convert.ToDouble(textBox_x2.Text);
            y2 = Convert.ToDouble(textBox_y2.Text);
            z2 = Convert.ToDouble(textBox_z2.Text);
            zf2 = Convert.ToDouble(textBox_f2.Text);
            pxx2 = Convert.ToDouble(textBox_pxx2.Text);
            pxy2 = Convert.ToDouble(textBox_pxy2.Text);
            nx02 = Convert.ToDouble(textBox_nx02.Text);
            ny02 = Convert.ToDouble(textBox_ny02.Text);
            dx = Convert.ToDouble(textBox_dx.Text);
            dy = Convert.ToDouble(textBox_dy.Text);

            ObjectCoordinatesCamera.kalibracja(x2, y2, z2, dx, dy, Tk2, xk2, zf2, pxx2, pxy2, nx02, ny02, C2B, xkko2, ykko2, xkki2, ykki2, xod2, yod2, xkkos2, ykkos2, xods2, yods2, error2, K2);
            displayTc2.Clear();
            string specifier = "0.00000";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    displayTc2.Text += Tk2[i, j].ToString(specifier) + "  ";
                }
                displayTc2.Text += "\n";
            }
            textBox_n21.Text = "k1= " + K2[0, 0].ToString(specifier) + " k2= k3= p1= p2= 0";
            textBox_n22.Text = "k1= " + K2[1, 0].ToString(specifier) + " k2= " + K2[1, 1].ToString(specifier) + " k3= p1= p2= 0";
            textBox_n23.Text = "k1= " + K2[2, 0].ToString(specifier) + " k2= " + K2[2, 1].ToString(specifier) + " k3= " + K2[2, 2].ToString(specifier) + " p1= p2= 0";
            textBox_n25.Text = "k1= " + K2[3, 0].ToString(specifier) + " k2= " + K2[3, 1].ToString(specifier) + " k3= " + K2[3, 2].ToString(specifier) + " p1= " + K2[3, 3].ToString(specifier) + " p2=  " + K2[3, 4].ToString(specifier);

            Calibrate2.Enabled = false;
            Camera2Error.Enabled = true;
        }

        private void textBox_x1_Leave(object sender, EventArgs e)
        {
            textBox_x1.Text = textBox_x1.Text.Replace('-', ' ');
            textBox_x1.Text = textBox_x1.Text.Replace('.', ',');
            textBox_x1.Text.Trim();
        }

        private void textBox_x2_Leave(object sender, EventArgs e)
        {
            textBox_x2.Text = textBox_x2.Text.Replace('.', ',');
            if (textBox_x2.Text.Length != 0)
            {
                char toCheck = textBox_x2.Text[0];
                if (toCheck != '-')
                    textBox_x2.Text = "-" + textBox_x2.Text;
            }
        }

        private void Camera1Error_Click(object sender, EventArgs e)
        {
            MessageBox.Show("alfa = " + xk1[0] + ", beta = " + xk1[1] + ", gamma = " + xk1[2] + 
                ", dx = " + xk1[3] + ", dy = " + xk1[4] + ", dz = " + xk1[5] + "\nErrors values are given for 5 factors" +
                "\nAbsolute error of x coordinate\ndxm = " + error1[4][0] + "\nixm = " + error1[4][1] + "\njxm = " + error1[4][2] + 
                "\nAbsolute error of y coordinate\ndym = " + error1[4][3] + "\niym = " + error1[4][4] + "\njym = " + error1[4][5] +
                "\nAbsolute error of distance\ndrm = " + error1[4][6] + "\nirm = " + error1[4][7] + "\njrm = " + error1[4][8]
                ,"Error analysis");
        }

        private void Camera2Error_Click(object sender, EventArgs e)
        {
            MessageBox.Show("alfa = " + xk2[0] + ", beta = " + xk2[1] + ", gamma = " + xk2[2] +
                ", dx = " + xk2[3] + ", dy = " + xk2[4] + ", dz = " + xk2[5] + "\nErrors values are given for 5 factors" +
                "\nAbsolute error of x coordinate\ndxm = " + error2[4][0] + "\nixm = " + error2[4][1] + "\njxm = " + error2[4][2] +
                "\nAbsolute error of y coordinate\ndym = " + error2[4][3] + "\niym = " + error2[4][4] + "\njym = " + error2[4][5] +
                "\nAbsolute error of distance\ndrm = " + error2[4][6] + "\nirm = " + error2[4][7] + "\njrm = " + error2[4][8]
                , "Error analysis");
        }

        private void Calculate_Click(object sender, EventArgs e)
        {
            displayT.Clear();
            int nullcounter = 0;
            int missing = 0;
            if ((x11.Text.Length != 0) && (x21.Text.Length != 0)
                && (y11.Text.Length != 0) && (y21.Text.Length != 0))
            {
                object1[0, 0] = Convert.ToDouble(x11.Text);
                object1[0, 0] = -(object1[0, 0] - nx01) * pxx1;
                object1[0, 1] = Convert.ToDouble(y11.Text);
                object1[0, 1] = -(object1[0, 1] - ny01) * pxy1;
                object2[0, 0] = Convert.ToDouble(x21.Text);
                object2[0, 0] = -(object2[0, 0] - nx02) * pxx2;
                object2[0, 1] = Convert.ToDouble(y21.Text);
                object2[0, 1] = -(object2[0, 1] - ny02) * pxy2;
            }
            else
            {
                nullcounter++;
                missing = 1;
            }
            
            if ((x12.Text.Length != 0) && (x22.Text.Length != 0)
                && (y12.Text.Length != 0) && (y22.Text.Length != 0))
            {
                object1[1, 0] = Convert.ToDouble(x12.Text);
                object1[1, 0] = -(object1[1, 0] - nx01) * pxx1;
                object1[1, 1] = Convert.ToDouble(y12.Text);
                object1[1, 1] = -(object1[1, 1] - ny01) * pxy1;
                object2[1, 0] = Convert.ToDouble(x22.Text);
                object2[1, 0] = -(object2[1, 0] - nx02) * pxx2;
                object2[1, 1] = Convert.ToDouble(y22.Text);
                object2[1, 1] = -(object2[1, 1] - ny02) * pxy2;
            }
            else
            {
                nullcounter++;
                missing = 2;
            }
            
            if ((x13.Text.Length != 0) && (x23.Text.Length != 0)
                && (y13.Text.Length != 0) && (y23.Text.Length != 0))
            {
                object1[2, 0] = Convert.ToDouble(x13.Text);
                object1[2, 0] = -(object1[2, 0] - nx01) * pxx1;
                object1[2, 1] = Convert.ToDouble(y13.Text);
                object1[2, 1] = -(object1[2, 1] - ny01) * pxy1;
                object2[2, 0] = Convert.ToDouble(x23.Text);
                object2[2, 0] = -(object2[2, 0] - nx02) * pxx2;
                object2[2, 1] = Convert.ToDouble(y23.Text);
                object2[2, 1] = -(object2[2, 1] - ny02) * pxy2;
            }
            else
            {
                nullcounter++;
                missing = 3;
            }

            if ((x14.Text.Length != 0) && (x24.Text.Length != 0)
                && (y14.Text.Length != 0) && (y24.Text.Length != 0))
            {
                object1[3, 0] = Convert.ToDouble(x14.Text);
                object1[3, 0] = -(object1[3, 0] - nx01) * pxx1;
                object1[3, 1] = Convert.ToDouble(y14.Text);
                object1[3, 1] = -(object1[3, 1] - ny01) * pxy1;
                object2[3, 0] = Convert.ToDouble(x24.Text);
                object2[3, 0] = -(object2[3, 0] - nx02) * pxx2;
                object2[3, 1] = Convert.ToDouble(y24.Text);
                object2[3, 1] = -(object2[3, 1] - ny02) * pxy2;
            }
            else
            {
                nullcounter++;
                missing = 4;
            }
            
            if(nullcounter > 1)
                MessageBox.Show("Missing or wrong filled fields.","Error");
            else
            {
                ObjectCoordinatesCamera.koordynaty(T, object1, object2, missing, nullcounter, zf1, zf2, Tk1, Tk2);
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        displayT.Text += T[i, j].ToString("0.0000") + "  ";
                    }
                    displayT.Text += "\n";
                }
            }

            using (StreamWriter writer = new StreamWriter("ObjectT.txt"))
            {
                writer.Write(displayT.Text);
            }
        }

        bool isDone;
        string _filenmame;
        string line;
        string[] words;
        double[,,] objcoords;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            objcoords = new double[2, 4, 2];

            isDone = false;
            while (!isDone)
            {
                PerformButtonClick(Load1);
                PerformButtonClick(Calibrate1);
                PerformButtonClick(Load2);
                PerformButtonClick(Calibrate2);

                //11 = yellow
                //12 = green
                //13 = pink
                //14 = blue

                for (int cameraindex = 0; cameraindex < 2; cameraindex++)
                {
                    for (int paramindex = 1; paramindex < 5; paramindex++)
                    {
                        _filenmame = "set_cam" + cameraindex.ToString() + "_paramset" + paramindex.ToString() + ".txt";
                        using (System.IO.StreamReader reader = new System.IO.StreamReader(_filenmame))
                        {
                            for (int k = 0; k < 2; k++)
                            {
                                //wordCounter = 0;
                                line = reader.ReadLine();
                                words = line.Split('\t');
                                objcoords[cameraindex, paramindex - 1, k] = Double.Parse(words[0]);
                            }
                        }
                    }
                }

                x11.Text = objcoords[0, 0, 0].ToString();
                x12.Text = objcoords[0, 1, 0].ToString();
                x13.Text = objcoords[0, 2, 0].ToString();
                x14.Text = objcoords[0, 3, 0].ToString();
                y11.Text = objcoords[0, 0, 1].ToString();
                y12.Text = objcoords[0, 1, 1].ToString();
                y13.Text = objcoords[0, 2, 1].ToString();
                y14.Text = objcoords[0, 3, 1].ToString();

                x21.Text = objcoords[1, 0, 0].ToString();
                x22.Text = objcoords[1, 1, 0].ToString();
                x23.Text = objcoords[1, 2, 0].ToString();
                x24.Text = objcoords[1, 3, 0].ToString();
                y21.Text = objcoords[1, 0, 1].ToString();
                y22.Text = objcoords[1, 1, 1].ToString();
                y23.Text = objcoords[1, 2, 1].ToString();
                y24.Text = objcoords[1, 3, 1].ToString();

                //PerformButtonClick(Calculate);
                Calculate_Click(sender, e);

                isDone = true;
            }


        }

        delegate void PerformButtonClickCallback(Button button);
        private void PerformButtonClick(Button button)
        {
            if (button.InvokeRequired)
            {
                PerformButtonClickCallback d = new PerformButtonClickCallback(PerformButtonClick);
                this.Invoke(d, new object[] { button });
            }
            else
            {
                button.PerformClick();
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(mainFrm.canClose)
                this.Close();
        }
    }
}
