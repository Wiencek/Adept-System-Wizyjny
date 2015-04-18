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
namespace AdeptLAB
{
    public partial class Form1 : Form
    {
        Communications con = new Communications();
        string str;
       
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int stat=5;
           
           con.Open("UDP",0,"172.16.150.130",0,"");
           con.RequestEvents(1, out stat);
            if(stat==0)
            {
                label1.Text = "Connected";
                label2.Text = con.ControllerIPAddress;
            }
            else
            {
                label1.Text = "not connected";
                con.Close();
            }


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Status stan=new Status();

            
            //int i =stan.get_NumberOfRobots(con);
            //label2.Text = str;
           // if (i==1) label2.Text ="jeden";
           // stan.Here(con, 1, str);
            Array ko = new float[6];
            Array ws = new float[6];
            int joi;
            stan.Where(con, 0, out joi,out ws,out ko);
            for (int j = 1; j <= 6; j++)
            {
                label2.Text += "\n" + ko.GetValue(j).ToString();
                 
            }
        }
        
        

        private void button2_Click(object sender, EventArgs e)
        {
            con.Close();
        }
    }
}
