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

using ActiveV2Lib;

namespace CompleteProgram
{
    public partial class MainForm : Form
    {
        int[,,] CalibrationData;
        string[,] LoginData;
        int[,] CamPosData;
        bool isStartupParamsChangeAllowed;
        CalParams _calParams;
        int PreviousClaibrationDataSet;
        string _filenmame;

        public MainForm()
        {
            InitializeComponent();
            CalibrationData = new int[2, 5, 8];
            CalibrationData[0, 0, 0] = 4;
            CalibrationData[0, 0, 1] = 4;
            CalibrationData[0, 0, 2] = 1;
            CalibrationData[0, 0, 3] = 120;
            CalibrationData[0, 0, 4] = -1;
            CalibrationData[0, 0, 5] = 90;
            CalibrationData[0, 0, 6] = -1;
            CalibrationData[0, 0, 7] = 40;
            CalibrationData[0, 1, 0] = 9;
            CalibrationData[0, 1, 1] = 9;
            CalibrationData[0, 1, 2] = 1;
            CalibrationData[0, 1, 3] = 180;
            CalibrationData[0, 1, 4] = 1;
            CalibrationData[0, 1, 5] = 150;
            CalibrationData[0, 1, 6] = -1;
            CalibrationData[0, 1, 7] = 80;
            CalibrationData[0, 2, 0] = 9;
            CalibrationData[0, 2, 1] = 9;
            CalibrationData[0, 2, 2] = -1;
            CalibrationData[0, 2, 3] = 100;
            CalibrationData[0, 2, 4] = 1;
            CalibrationData[0, 2, 5] = 90;
            CalibrationData[0, 2, 6] = -1;
            CalibrationData[0, 2, 7] = 110;
            CalibrationData[0, 3, 0] = 9;
            CalibrationData[0, 3, 1] = 9;
            CalibrationData[0, 3, 2] = 1;
            CalibrationData[0, 3, 3] = 70;
            CalibrationData[0, 3, 4] = -1;
            CalibrationData[0, 3, 5] = 50;
            CalibrationData[0, 3, 6] = 1;
            CalibrationData[0, 3, 7] = 20;
            CalibrationData[0, 4, 0] = 9;
            CalibrationData[0, 4, 1] = 9;
            CalibrationData[0, 4, 2] = -1;
            CalibrationData[0, 4, 3] = 170;
            CalibrationData[0, 4, 4] = 1;
            CalibrationData[0, 4, 5] = 100;
            CalibrationData[0, 4, 6] = 1;
            CalibrationData[0, 4, 7] = 110;
            CalibrationData[1, 0, 0] = 4;
            CalibrationData[1, 0, 1] = 4;
            CalibrationData[1, 0, 2] = 1;
            CalibrationData[1, 0, 3] = 80;
            CalibrationData[1, 0, 4] = -1;
            CalibrationData[1, 0, 5] = 40;
            CalibrationData[1, 0, 6] = -1;
            CalibrationData[1, 0, 7] = 40;
            CalibrationData[1, 1, 0] = 9;
            CalibrationData[1, 1, 1] = 9;
            CalibrationData[1, 1, 2] = 1;
            CalibrationData[1, 1, 3] = 180;
            CalibrationData[1, 1, 4] = 1;
            CalibrationData[1, 1, 5] = 150;
            CalibrationData[1, 1, 6] = -1;
            CalibrationData[1, 1, 7] = 80;
            CalibrationData[1, 2, 0] = 9;
            CalibrationData[1, 2, 1] = 9;
            CalibrationData[1, 2, 2] = -1;
            CalibrationData[1, 2, 3] = 70;
            CalibrationData[1, 2, 4] = 1;
            CalibrationData[1, 2, 5] = 50;
            CalibrationData[1, 2, 6] = -1;
            CalibrationData[1, 2, 7] = 70;
            CalibrationData[1, 3, 0] = 9;
            CalibrationData[1, 3, 1] = 9;
            CalibrationData[1, 3, 2] = 1;
            CalibrationData[1, 3, 3] = 190;
            CalibrationData[1, 3, 4] = -1;
            CalibrationData[1, 3, 5] = 60;
            CalibrationData[1, 3, 6] = 1;
            CalibrationData[1, 3, 7] = 40;
            CalibrationData[1, 4, 0] = 9;
            CalibrationData[1, 4, 1] = 9;
            CalibrationData[1, 4, 2] = -1;
            CalibrationData[1, 4, 3] = 170;
            CalibrationData[1, 4, 4] = 1;
            CalibrationData[1, 4, 5] = 100;
            CalibrationData[1, 4, 6] = 1;
            CalibrationData[1, 4, 7] = 130;
            LoginData = new string[2, 3] { { "http://192.168.1.70/mjpg/video.mjpg", "admin", "1234" }, { "http://192.168.1.215/mjpg/video.mjpg", "admin", "1234" } };
            CamPosData = new int[2, 3] { {390, -20, 470}, {-460, -37, 470} };
            _calParams = new CalParams(CalibrationData, LoginData, CamPosData);

            PreviousClaibrationDataSet = -1;
            isStartupParamsChangeAllowed = false;

            DataSetSelect.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public CalParams calParams
        {
            get { return _calParams; }
        }

        public string filename
        {
            get { return _filenmame; }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Address1.Text = _calParams.LoginData[0, 0];
            Login1.Text = _calParams.LoginData[0, 1];
            Password1.Text = _calParams.LoginData[0, 2];
            Address2.Text = _calParams.LoginData[1, 0];
            Login2.Text = _calParams.LoginData[1, 1];
            Password2.Text = _calParams.LoginData[1, 2];
            cam1x.Text = _calParams.CamPos[0, 0].ToString();
            cam1y.Text = _calParams.CamPos[0, 1].ToString();
            cam1z.Text = _calParams.CamPos[0, 2].ToString();
            cam2x.Text = _calParams.CamPos[1, 0].ToString();
            cam2y.Text = _calParams.CamPos[1, 1].ToString();
            cam2z.Text = _calParams.CamPos[1, 2].ToString();
            DataSetSelect.SelectedIndex = 0;
            isStartupParamsChangeAllowed = true;
        }

        private void DataChange(object sender, EventArgs e)
        {
            if (isStartupParamsChangeAllowed)
            {
                _calParams.LoginData[0, 0] = Address1.Text;
                _calParams.LoginData[0, 1] = Login1.Text;
                _calParams.LoginData[0, 2] = Password1.Text;
                _calParams.LoginData[1, 0] = Address2.Text;
                _calParams.LoginData[1, 1] = Login2.Text;
                _calParams.LoginData[1, 2] = Password2.Text;
                _calParams.CamPos[0, 0] = int.Parse(cam1x.Text);
                _calParams.CamPos[0, 1] = int.Parse(cam1y.Text);
                _calParams.CamPos[0, 2] = int.Parse(cam1z.Text);
                _calParams.CamPos[1, 0] = int.Parse(cam2x.Text);
                _calParams.CamPos[1, 1] = int.Parse(cam2y.Text);
                _calParams.CamPos[1, 2] = int.Parse(cam2z.Text);

                if (PreviousClaibrationDataSet != -1)
                {
                    Int32.TryParse(MinH1.Text, out CalibrationData[0, PreviousClaibrationDataSet, 0]);
                    Int32.TryParse(MinW1.Text, out CalibrationData[0, PreviousClaibrationDataSet, 1]);
                    if (RL1.Checked == true)
                    {
                        CalibrationData[0, PreviousClaibrationDataSet, 2] = -1;
                    }
                    else
                    {
                        CalibrationData[0, PreviousClaibrationDataSet, 2] = 1;
                    }
                    Int32.TryParse(RValue1.Text, out CalibrationData[0, PreviousClaibrationDataSet, 3]);
                    if (GL1.Checked == true)
                    {
                        CalibrationData[0, PreviousClaibrationDataSet, 4] = -1;
                    }
                    else
                    {
                        CalibrationData[0, PreviousClaibrationDataSet, 4] = 1;
                    }
                    Int32.TryParse(GValue1.Text, out CalibrationData[0, PreviousClaibrationDataSet, 5]);
                    if (BL1.Checked == true)
                    {
                        CalibrationData[0, PreviousClaibrationDataSet, 6] = -1;
                    }
                    else
                    {
                        CalibrationData[0, PreviousClaibrationDataSet, 6] = 1;
                    }
                    Int32.TryParse(BValue1.Text, out CalibrationData[0, PreviousClaibrationDataSet, 7]);

                    Int32.TryParse(MinH2.Text, out CalibrationData[1, PreviousClaibrationDataSet, 0]);
                    Int32.TryParse(MinW2.Text, out CalibrationData[1, PreviousClaibrationDataSet, 1]);
                    if (RL2.Checked == true)
                    {
                        CalibrationData[1, PreviousClaibrationDataSet, 2] = -1;
                    }
                    else
                    {
                        CalibrationData[1, PreviousClaibrationDataSet, 2] = 1;
                    }
                    Int32.TryParse(RValue2.Text, out CalibrationData[1, PreviousClaibrationDataSet, 3]);
                    if (GL2.Checked == true)
                    {
                        CalibrationData[1, PreviousClaibrationDataSet, 4] = -1;
                    }
                    else
                    {
                        CalibrationData[1, PreviousClaibrationDataSet, 4] = 1;
                    }
                    Int32.TryParse(GValue2.Text, out CalibrationData[1, PreviousClaibrationDataSet, 5]);
                    if (BL2.Checked == true)
                    {
                        CalibrationData[1, PreviousClaibrationDataSet, 6] = -1;
                    }
                    else
                    {
                        CalibrationData[1, PreviousClaibrationDataSet, 6] = 1;
                    }
                    Int32.TryParse(BValue2.Text, out CalibrationData[1, PreviousClaibrationDataSet, 7]);

                    _calParams.CalParamsGetSet = CalibrationData;
                }
            }
        }

        private void UpdateCalibrationData(object sender, EventArgs e)
        {
            ComboBox combo = DataSetSelect;
            PreviousClaibrationDataSet = -1;
            MinH1.Text = CalibrationData[0, combo.SelectedIndex, 0].ToString();
            MinW1.Text = CalibrationData[0, combo.SelectedIndex, 1].ToString();
            if (CalibrationData[0, combo.SelectedIndex, 2] == -1)
            {
                RL1.Select();
            }
            else
            {
                RR1.Select();
            }
            RValue1.Text = CalibrationData[0, combo.SelectedIndex, 3].ToString();
            if (CalibrationData[0, combo.SelectedIndex, 4] == -1)
            {
                GL1.Select();
            }
            else
            {
                GR1.Select();
            }
            GValue1.Text = CalibrationData[0, combo.SelectedIndex, 5].ToString();
            if (CalibrationData[0, combo.SelectedIndex, 6] == -1)
            {
                BL1.Select();
            }
            else
            {
                BR1.Select();
            }
            BValue1.Text = CalibrationData[0, combo.SelectedIndex, 7].ToString();

            MinH2.Text = CalibrationData[1, combo.SelectedIndex, 0].ToString();
            MinW2.Text = CalibrationData[1, combo.SelectedIndex, 1].ToString();
            if (CalibrationData[1, combo.SelectedIndex, 2] == -1)
            {
                RL2.Select();
            }
            else
            {
                RR2.Select();
            }
            RValue2.Text = CalibrationData[1, combo.SelectedIndex, 3].ToString();
            if (CalibrationData[1, combo.SelectedIndex, 4] == -1)
            {
                GL2.Select();
            }
            else
            {
                GR2.Select();
            }
            GValue2.Text = CalibrationData[1, combo.SelectedIndex, 5].ToString();
            if (CalibrationData[1, combo.SelectedIndex, 6] == -1)
            {
                BL2.Select();
            }
            else
            {
                BR2.Select();
            }
            BValue2.Text = CalibrationData[1, combo.SelectedIndex, 7].ToString();

            PreviousClaibrationDataSet = combo.SelectedIndex;
        }

        Camera frmCamera = null;
        ObjectCoordinates frmObjCoords = null;
        System.IO.StreamReader reader_template;
        private void Start_Button_Click(object sender, EventArgs e)
        {
            for (int cameraindex = 0; cameraindex < 2; cameraindex++)
            {
                _filenmame = "set_cam" + cameraindex.ToString() + "_paramset" + "0" + ".txt";
                frmCamera = new Camera(this, cameraindex, 0, 1);
                frmCamera.ShowDialog();

                using (reader_template = new System.IO.StreamReader(_filenmame))
                {
                    string line = reader_template.ReadLine();
                    string[] words = line.Split('\t');
                    if (words.Length != 192)
                    {
                        MessageBox.Show("Błąd odczytu szablonu", "Error");
                        break;
                    }
                }
            }
        }

        System.IO.StreamReader reader;
        bool isEmpty;
        private void GetObjectPosButton_Click(object sender, EventArgs e)
        {
            isEmpty = false;
            
            MessageBox.Show("Please place object.", "Place Object");
            for (int cameraindex = 0; cameraindex < 2; cameraindex++)
            {
                for (int paramindex = 1; paramindex < 5; paramindex++)
                {
                    _filenmame = "set_cam" + cameraindex.ToString() + "_paramset" + paramindex.ToString() + ".txt";
                    frmCamera = new Camera(this, cameraindex, paramindex, 1);
                    frmCamera.ShowDialog();

                    using (reader = new System.IO.StreamReader(_filenmame))
                    {
                        if (reader.EndOfStream)
                        {
                            MessageBox.Show("Nie znaleziono punktu!", "Error");
                            isEmpty = true;
                            cameraindex = 3;
                            break;
                        }
                    }
                }
            }

            shwcordsform(sender, e);
        }

        private void ShowCam1Frm_Click(object sender, EventArgs e)
        {
            frmCamera = new Camera(this, 0, DataSetSelect.SelectedIndex);
            frmCamera.ShowDialog();
        }

        private void ShowCam2Frm_Click_1(object sender, EventArgs e)
        {
            frmCamera = new Camera(this, 1, DataSetSelect.SelectedIndex);
            frmCamera.ShowDialog();
        }

        private void shwcordsform(object sender, EventArgs e)
        {
            isEmpty = false;
            for (int cameraindex = 0; cameraindex < 2; cameraindex++)
            {
                for (int paramindex = 1; paramindex < 5; paramindex++)
                {
                    _filenmame = "set_cam" + cameraindex.ToString() + "_paramset" + paramindex.ToString() + ".txt";
                    using (reader = new System.IO.StreamReader(_filenmame))
                    {
                        if (reader.EndOfStream)
                        {
                            MessageBox.Show("Nie znaleziono punktu!", "Error");
                            isEmpty = true;
                            break;
                        }
                    }
                }
            }

            if (!isEmpty)
            {
                try
                {
                    _canClose = false;
                    frmObjCoords = new ObjectCoordinates(this, double.Parse(cam1x.Text), double.Parse(cam1y.Text), double.Parse(cam1z.Text),
                        double.Parse(cam2x.Text), double.Parse(cam2y.Text), double.Parse(cam2z.Text));
                    frmObjCoords.ShowDialog();
                    _canClose = true;
                }
                catch
                {

                }
            }
        }

        private void SaveParamsButton_Click(object sender, EventArgs e)
        {
            saveParamsFileDialog.Filter = "Text files (*.txt)|*.txt";
            DialogResult result = saveParamsFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveParamsFileDialog.FileName))
                {
                    int counter = 0;
                    foreach (string camlogindata in _calParams.LoginData)
                    {
                        counter++;
                        if (counter < _calParams.LoginData.Length)
                        {
                            writer.Write(camlogindata + "\t");
                        }
                        else
                        {
                            writer.Write(camlogindata);
                        }
                    }
                    writer.Write(writer.NewLine);
                    counter = 0;
                    foreach (int campos in _calParams.CamPos)
                    {
                        counter++;
                        if (counter < _calParams.CamPos.Length)
                        {
                            writer.Write(campos.ToString() + "\t");
                        }
                        else
                        {
                            writer.Write(campos.ToString());
                        }
                    }
                    writer.Write(writer.NewLine);
                    counter = 0;
                    foreach (int calparam in _calParams.CalParamsGetSet)
                    {
                        counter++;
                        if (counter < _calParams.CalParamsGetSet.Length)
                        {
                            writer.Write(calparam.ToString() + "\t");
                        }
                        else
                        {
                            writer.Write(calparam.ToString());
                        }
                    }
                }
            }
        }

        private void LoadParamsButton_Click(object sender, EventArgs e)
        {
            openParamsFileDialog.Filter = "Text files (*.txt)|*.txt";
            openParamsFileDialog.CheckFileExists = true;
            openParamsFileDialog.CheckPathExists = true;
            DialogResult result = openParamsFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(openParamsFileDialog.FileName))
                {
                    int counter = 0;
                    string line = reader.ReadLine();
                    string[] words = line.Split('\t');
                    for (int camnum = 0; camnum < _calParams.LoginData.GetLength(0); camnum++)
                    {
                        for (int i = 0; i < _calParams.LoginData.GetLength(1); i++)
                        {
                            _calParams.LoginData[camnum, i] = words[counter];
                            counter++;
                        }
                    }
                    counter = 0;
                    line = reader.ReadLine();
                    words = line.Split('\t');
                    for (int camnum = 0; camnum < _calParams.CamPos.GetLength(0); camnum++)
                    {
                        for (int i = 0; i < _calParams.CamPos.GetLength(1); i++)
                        {
                            _calParams.CamPos[camnum, i] = int.Parse(words[counter]);
                            counter++;
                        }
                    }
                    counter = 0;
                    line = reader.ReadLine();
                    words = line.Split('\t');
                    for (int camnum = 0; camnum < _calParams.CalParamsGetSet.GetLength(0); camnum++)
                    {
                        for (int paramtype = 0; paramtype < _calParams.CalParamsGetSet.GetLength(1); paramtype++)
                        {
                            for (int i = 0; i < _calParams.CalParamsGetSet.GetLength(2); i++)
                            {
                                _calParams.CalParamsGetSet[camnum, paramtype, i] = int.Parse(words[counter]);
                                counter++;
                            }
                        }
                    }

                    isStartupParamsChangeAllowed = false;
                    MainForm_Shown(sender, e);
                    UpdateCalibrationData(sender, e);
                }
            }
        }

        int connected = 0;                          //Zmienna monitorująca połączenie
        int stat = 0;
        Communications con = new Communications();  //Klasa Komunikacji
        MiscControl ruch = new MiscControl();       //Klasa Kontroli
        Programs prog = new Programs();             //Klasa wykorzystywania V+
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            stat = 5;

            con.Open("UDP", 0, "172.16.150.130", 0, ""); //Nawiązywanie połączenia - adres IP wpisany na sztywno
            con.RequestEvents(1, out stat);          //Sprawdzanie, czy połączenie zostało nawiązane
            if (stat == 0)
            {
                ConnectLabel.Text = "Connected";          //Wyświetlenie udanego połączenia się
                //label4.Text = con.ControllerIPAddress;  //Adres IP z którym się łączyliśmy
                connected = 1;                      //Zmiana zmiennej monitorującej na Połączony
            }
            else
            {
                ConnectLabel.Text = "Not connected";      //Wyświetlenie, że nie jesteśmy połączeni
                //label4.Text = "";                   //Brak adresu IP
                con.Close();                        //Zamknięcie połączenia
                connected = 0;                      //Zmiana zmiennej monitorującej
            }
        }

        private void PickUpObjectButton_Click(object sender, EventArgs e)
        {
            if (connected == 1)
            {
                int flaga;                          //Zmienna błędu
                stat = 5;
                float x, y, z, rx, ry, rz;      //Zmienne współrzędne

                //Obliczenie potrzebnych elementów macierzy do adepta
                double ex, ey, ez, dx, dy;
                double[,] Tobj = new double[4, 4];
                string separator = "  ";
                ex = 451.4;
                ey = 12.275;
                ez = -145.275;

                using (System.IO.StreamReader reader = new System.IO.StreamReader("ObjectT.txt"))
                {
                    int counter;
                    string line;
                    string[] words;
                    for (int n = 0; n < Tobj.GetLength(0); n++)
                    {
                        counter = 0;
                        line = reader.ReadLine();
                        words = line.Split(separator.ToCharArray());
                        for (int m = 0; m < Tobj.GetLength(1); m++)
                        {
                            Tobj[n, m] = double.Parse(words[counter]);
                            counter++;
                            counter++;
                        }
                    }
                }

                //Pierwsza wersja:
                dx = Tobj[0, 1] * ex + Tobj[0, 0] * ey + Tobj[0, 2] * ez;
                dy = Tobj[1, 1] * ex + Tobj[1, 0] * ey + Tobj[1, 2] * ez;
                //Druga wersja:
                //dx = Tobj[0, 0] * ex + Tobj[0, 1] * ey + Tobj[0, 2] * ez;
                //dy = -Tobj[1, 0] * ex + Tobj[1, 1] * ey + Tobj[1, 2] * ez;
                        
                //Koniec obliczania elementów macierzy do adepta
  
                //Wyliczenie współrzędnych
                x = (float)dx;  
                y = (float)dy;  
                z = -145;                                   //z = -145
                rx = float(Math.Acos(Tobj[1, 0]) + 90);     //rx = arccos(bx)
                ry = 90;                                    //ry = 90
                rz = -90;                                   //rz = -90


                ruch.SetL(con, "loc1", x, y, 200, rx, ry, rz, out flaga); //Wysyłanie zmiennej do sterownika

                while (stat != 1)
                {
                    prog.Execute(con, "ruch()", 0, out stat);
                }
                stat = 5;

                while (stat != 1)
                {
                    prog.Execute(con, "otw()", 1, out stat);       //Wywołanie funkcji 'otw'
                }
                stat = 5;

                ruch.SetL(con, "loc1", x, y, z, rx, ry, rz, out flaga); //Wysyłanie zmiennej do sterownika

                while (stat != 1)
                {
                    prog.Execute(con, "ruch()", 0, out stat);
                }
                stat = 5;

                while (stat != 1)
                {
                    prog.Execute(con, "zamk()", 0, out stat);       //Wywołanie funkcji 'zamk'
                }
                stat = 5;

                
                ruch.SetL(con, "loc1", x, y, 200, rx, ry, rz, out flaga); //Wysyłanie zmiennej do sterownika

                while (stat != 1)
                {
                    prog.Execute(con, "ruch()", 0, out stat);
                }
                stat = 5;
            }
        }

        private void ObjectDoAllButton_Click(object sender, EventArgs e)
        {
            GetObjectPosButton_Click(sender, e);
            PickUpObjectButton_Click(sender, e);
        }

        bool _canClose = true;
        public bool canClose
        {
            get { return _canClose; }
        }
    }

    public class CalParams
    {
        private string[,] _logindata = null;
        private int[,] _camPos = null;
        private int[,,] _calParams = null;
        
        public CalParams()
        {
        }

        public CalParams(int[,,] startupParams, string[,] startupLoginData, int[,] startupCamPos)
        {
            _logindata = startupLoginData;
            _camPos = startupCamPos;
            _calParams = startupParams;
        }

        public string[,] LoginData
        {
            get { return _logindata; }
            set { _logindata = value; }
        }

        public int[,] CamPos
        {
            get { return _camPos; }
            set { _camPos = value; }
        }

        public int[,,] CalParamsGetSet
        {
            get { return _calParams; }
            set { _calParams = value; }
        }

        public int this[int cameranum, int datanum, int i]
        {
            get { return _calParams[cameranum, datanum, i]; }
            set { _calParams[cameranum, datanum, i] = value; }
        }
    }
}
