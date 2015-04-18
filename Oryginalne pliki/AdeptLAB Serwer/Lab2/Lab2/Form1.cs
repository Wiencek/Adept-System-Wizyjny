using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ActiveV2Lib;

namespace Lab2
{
    public partial class Form1 : Form
    {
        Communications mCommunications = new ActiveV2Lib.Communications();
        MiscControl msc = new ActiveV2Lib.MiscControl();
        public Form1()
        {
            InitializeComponent();
             
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                int status;
                
                mCommunications.Open("UDP", 0,"172.16.150.130", 0, "" );
                mCommunications.RequestEvents(1, out status);
                if( status == 0)
                {
                    if( MessageBox.Show(this, "This V+ co", "Connect", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
 
                    {
                        mCommunications.RequestEvents(0, out status);
                        textBox1.Text = "Poloczono";
                    }
                    else
                    {
                        mCommunications.Close();
                        return;
                    }
                }
                DialogResult = DialogResult.OK;
                // Close();
            }
            catch (System.Runtime.InteropServices.COMException Ex)
            {
                ActiveV2Lib.ErrorHandler HError = new ActiveV2Lib.ErrorHandler();
                string sMsg;

                HError.GetErrorString(Ex.ErrorCode, out sMsg);
                MessageBox.Show(this, sMsg, "Connection Error");
                mCommunications = null;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(this, Ex.Message, "Connection Error");
                mCommunications = null;
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            Status stat = new ActiveV2Lib.Status();            
            int stats;      
            int pNumberOfJoints;
          
            System.Array aWorldCoord;
            System.Array aJointCoord;

            msc.SetR(mCommunications, "ster", 0, out stats);

            stat.Where(mCommunications, 0, out pNumberOfJoints, out aWorldCoord, out aJointCoord);
            textBox1.Text = pNumberOfJoints.ToString();

            J1.Text = aJointCoord.GetValue(1).ToString();
            J2.Text = aJointCoord.GetValue(2).ToString();
            J3.Text = aJointCoord.GetValue(3).ToString();

            J4.Text = aJointCoord.GetValue(4).ToString();
            J5.Text = aJointCoord.GetValue(5).ToString();
            J6.Text = aJointCoord.GetValue(6).ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            int stat;
            msc.SetR(mCommunications, "ster", 1, out stat);
           // msc.SetL(mCommunications, "LOC1", 520, 20, 30, 0, 90, 0, out stat);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int stat;
            msc.SetR(mCommunications, "ster", 2, out stat);
        }
    }
}
