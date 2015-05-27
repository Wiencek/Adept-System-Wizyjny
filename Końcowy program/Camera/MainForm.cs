using System;
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
    public partial class MainForm : Form
    {
        int[,,] CalibrationData;
        string[,] LoginData;
        bool isLoginChangeAllowed;
        CalParams _calParams;
        int PreviousClaibrationDataSet;
        string _filenmame;

        public MainForm()
        {
            InitializeComponent();
            CalibrationData = new int[2, 5, 8] { { { 3, 4, 1, 170, -1, 110, -1, 40 }, { 9, 9, 1, 180, 1, 150, -1, 80 }, { 9, 9, -1, 100, 1, 90, -1, 110 }, { 9, 9, 1, 190, -1, 60, 1, 40 }, { 9, 9, -1, 170, 1, 100, 1, 130 } }, 
                                                 { { 3, 4, 1, 130, -1, 150, -1, 50 }, { 9, 9, 1, 180, 1, 150, -1, 80 }, { 9, 9, -1, 60, 1, 40, -1, 110 }, { 9, 9, 1, 190, -1, 60, 1, 40 }, { 9, 9, -1, 170, 1, 100, 1, 130 } } }; ;
            LoginData = new string[2, 3] { { "http://192.168.1.70/mjpg/video.mjpg", "admin", "1234" }, { "http://192.168.1.215/mjpg/video.mjpg", "admin", "1234" } };
            _calParams = new CalParams(CalibrationData, LoginData);
            PreviousClaibrationDataSet = -1;
            isLoginChangeAllowed = false;

            DataSetSelect.DropDownStyle = ComboBoxStyle.DropDownList;
            DataSetSelect.SelectedIndex = 0;

        }

        public CalParams calParams
        {
            get { return _calParams; }
        }

        public string filename
        {
            get { return _filenmame; }
        }

        private void LoginDataChange(object sender, EventArgs e)
        {
            if (isLoginChangeAllowed)
            {
                _calParams.LoginData[0, 0] = Address1.Text;
                _calParams.LoginData[0, 1] = Login1.Text;
                _calParams.LoginData[0, 2] = Password1.Text;
                _calParams.LoginData[1, 0] = Address2.Text;
                _calParams.LoginData[1, 1] = Login2.Text;
                _calParams.LoginData[1, 2] = Password2.Text;
            }
        }

        private void UpdateCalibrationData(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox) sender;

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
            }

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
        private void Start_Button_Click(object sender, EventArgs e)
        {
            for (int cameraindex = 0; cameraindex < 2; cameraindex++)
            {
                for (int paramindex = 0; paramindex < 5; paramindex++)
                {
                    _filenmame = "set_cam" + cameraindex.ToString() + "_paramset" + paramindex.ToString() + ".txt";
                    frmCamera = new Camera(this, cameraindex, paramindex);
                    frmCamera.ShowDialog();
                }
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {

            Address1.Text = _calParams.LoginData[0, 0];
            Login1.Text = _calParams.LoginData[0, 1];
            Password1.Text = _calParams.LoginData[0, 2];
            Address2.Text = _calParams.LoginData[1, 0];
            Login2.Text = _calParams.LoginData[1, 1];
            Password2.Text = _calParams.LoginData[1, 2];
            isLoginChangeAllowed = true;
        }
    }

    public class CalParams
    {
        private int[,,] _calParams = null;
        private string[,] _logindata = null;

        public CalParams()
        {
        }

        public CalParams(int[,,] startupParams, string[,] startupLoginData)
        {
            _calParams = startupParams;
            _logindata = startupLoginData;
        }

        public string[,] LoginData
        {
            get { return _logindata; }
            set { _logindata = value; }
        }

        public int this[int cameranum, int datanum, int i]
        {
            get { return _calParams[cameranum, datanum, i]; }
            set { _calParams[cameranum, datanum, i] = value; }
        }

    }
}
