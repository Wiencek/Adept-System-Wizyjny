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
        CalParams _calParams;
        int PreviousClaibrationDataSet;
        string _filenmame;

        public MainForm()
        {
            InitializeComponent();
            CalibrationData = new int[2, 5, 8] { { { 6, 6, -1, 20, -1, 40, 1, 60 }, { 5, 6, -1, 20, -1, 40, 1, 60 }, { 6, 6, -1, 20, -1, 40, 1, 60 }, { 6, 6, -1, 20, -1, 40, 1, 60 }, { 6, 6, -1, 20, -1, 40, 1, 60 } }, { { 6, 6, -1, 20, -1, 40, 1, 60 }, { 6, 6, -1, 20, -1, 40, 1, 60 }, { 6, 6, -1, 20, -1, 40, 1, 60 }, { 6, 6, -1, 20, -1, 40, 1, 60 }, { 6, 6, -1, 20, -1, 40, 1, 60 } } }; ;
            _calParams = new CalParams(CalibrationData);
            PreviousClaibrationDataSet = -1;
        }

        public CalParams calParams
        {
            get { return _calParams; }
        }

        public string filename
        {
            get { return _filenmame; }
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
    }

    public class CalParams
    {
        private int[,,] _calParams = null;

        public CalParams()
        {
        }

        public CalParams(int[,,] startupParams)
        {
            _calParams = startupParams;
        }

        public int this[int i1, int i2, int i3]
        {
            get { return _calParams[i1, i2, i3]; }
            set { _calParams[i1, i2, i3] = value; }
        }

    }
}
