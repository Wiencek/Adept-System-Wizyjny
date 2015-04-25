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
using System.Windows;
namespace AdeptLAB
{
    public partial class Form1 : Form
    {
        int connected = 0;                          //Zmienna monitorująca połączenie
        float krok = 0;                             //Krok przy ruchach na osiach X,Y,Z
        Communications con = new Communications();  //Klasa Komunikacji
        MiscControl ruch = new MiscControl();       //Klasa Kontroli
        Programs prog = new Programs();             //Klasa wykorzystywania V+
        Array ko = new float[6];                    //Tablica na współrzędne wewnętrzne
        Array ws = new float[6];                    //Tablica na współrzędne zewnętrzne
       
        public Form1()
        {
            InitializeComponent();
            
        }

        //nawiązywanie połączenia z Adeptem - Przyciść Connect
        private void button1_Click(object sender, EventArgs e)
        {
            int stat=5;
           
           con.Open("UDP",0,"172.16.150.130",0,""); //Nawiązywanie połączenia - adres IP wpisany na sztywno
           con.RequestEvents(1, out stat);          //Sprawdzanie, czy połączenie zostało nawiązane
            if(stat==0)
            {
                label1.Text = "Connected";          //Wyświetlenie udanego połączenia się
                label4.Text = con.ControllerIPAddress;  //Adres IP z którym się łączyliśmy
                connected = 1;                      //Zmiana zmiennej monitorującej na Połączony
            }
            else
            {
                label1.Text = "Not connected";      //Wyświetlenie, że nie jesteśmy połączeni
                label4.Text = "";                   //Brak adresu IP
                con.Close();                        //Zamknięcie połączenia
                connected = 0;                      //Zmiana zmiennej monitorującej
            }


        }
        //Odczytywanie zmienny - Przycisk Read
        private void button1_Click_1(object sender, EventArgs e)
        {
            Status stan=new Status();               //Klasa Status

            int joi;                                //Zmienna - ilość Członów
            if (connected == 1)                     //Powtarza się kilka razy - sprwdzamy, czy jesteśmy połączeni - inaczej wyskakują błędy
            {
                stan.Where(con, 0, out joi, out ws, out ko);    //Odczytanie pozycji robota
                label2.Text = "Joints";
                label3.Text = "World";
                for (int j = 1; j <= 6; j++)
                {
                    label2.Text += "\n" + ko.GetValue(j).ToString();    //Wyświetlanie współrzędnych wewnętrznych
                    label3.Text += "\n" + ws.GetValue(j).ToString();    //Wyświetlanie współrzędnych zewnętrznych
                }
            }
        }
        //Przycisk Rozłączenia - powtarza się sekwencja z łączenia gdy nawiązanie połączenia się nie uda
        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "Not Connected";
            label4.Text = "";
            con.Close();
            connected = 0;
        }
        //Ustawianie zmiennej Lokacja "loc1" - Przycisk SetL
        private void button3_Click(object sender, EventArgs e)
        {
            if (connected == 1)
            {
                int flaga                           //Zmienna błędu
                float w1, w2, w3, w4, w5, w6;       //Zmienne współrzędne
                w1 = float.Parse(textBox1.Text);    //Odczytywanie wartości dla zmiennej 'x'
                w2 = float.Parse(textBox2.Text);    //... dla zmiennej 'y'
                w3 = float.Parse(textBox3.Text);    //... 'z'
                w4 = float.Parse(textBox4.Text);    //'rx'
                w5 = float.Parse(textBox5.Text);    //'ry'
                w6 = float.Parse(textBox6.Text);    //'rz'
                ruch.SetL(con, "loc1",w1,w2,w3,w4,w5,w6, out flaga); //Wysyłanie zmiennej do sterownika
            }
        }
        //Timer - odczytywanie położenia co określony czas
        //Działa jak zwykłe odczytanie tylko co jakiś czas
        private void timer1_Tick(object sender, EventArgs e)
        {
            Status stan = new Status();

            int joi;
            if (connected == 1)
            {
                stan.Where(con, 0, out joi, out ws, out ko);
                label2.Text = "Joints";
                label3.Text = "World";
               
                for (int j = 1; j <= 6; j++)
                {
                    label2.Text += "\n" + ko.GetValue(j).ToString();
                    label3.Text += "\n" + ws.GetValue(j).ToString();
                }
                //Wyznaczony Sześcian dozwolony - W kolejności Z,Y,X
                if (float.Parse(ws.GetValue(3).ToString()) < -140 || float.Parse(ws.GetValue(3).ToString()) > 400)
                {
                    int pStatus;
                    ruch.SwitchOff(con, "POWER", out pStatus);
                }
                if (float.Parse(ws.GetValue(2).ToString()) < -250|| float.Parse(ws.GetValue(2).ToString()) > 250)
                {
                    int pStatus;
                    ruch.SwitchOff(con, "POWER", out pStatus);
                }
                if (float.Parse(ws.GetValue(1).ToString()) < 250 || float.Parse(ws.GetValue(1).ToString()) > 540)
                {
                    int pStatus;
                    ruch.SwitchOff(con, "POWER", out pStatus);
                }



            }
        }
        //Rozpoczęcie działania Timera - start timer
        private void buttonStart_Click_1(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
        //Zatrzymanie działania timera - stop timer
        private void buttonCancel_Click_1(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
        //Zmiana szybkości w sterowniku - speed
        //UWAGA - pierwsze wskazanie zawsze jest 0 - nie szczytuje prędkości w momencie uruchomienia programu
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            int nStatus;

            if (connected == 1)
            {
                ruch.Speed(con, hScrollBar1.Value, out nStatus);    //Wskazanie scrolla zmienia prędkość
            }
        }
        //Wyłączanie prądku - (pseudo)panic button
        private void button4_Click(object sender, EventArgs e)
        {
            int pStatus;
            ruch.SwitchOff(con,"POWER",out pStatus);    //Wyłączenie przycisku POWER
        }
        //Umożliienie włączenia prądu - enable power
        private void button5_Click(object sender, EventArgs e)
        {
            int pStatus;
            ruch.SwitchOn(con, "POWER", out pStatus);   //Włączenie przycisku POWER
        }
        //Odczytywanie wartości 'krok'
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            krok = float.Parse(textBox7.Text);
        }
        //Przemieszczenie się na pozycję 'loc1'
        //'loc1' jest aktualną pozycję przemieszczoną o 'krok' w kierunku...
        //UWAGA - wymaga jednej linijki napisanej w V+
        //"MOVE loc1"
        //Funkcja musi być zapisana jako 'ruch'
        //... +X
        private void button6_Click(object sender, EventArgs e)
        {
            int stat = 5;
            float w1, w2, w3, w4, w5, w6;
            Status stan=new Status();
            //Funkcja odczytuje aktualną pozycję jak przycisk READ
            int joi;
            if (connected == 1)
            {
                stan.Where(con, 0, out joi, out ws, out ko);
                w1 = float.Parse(ws.GetValue(1).ToString());
                w2 = float.Parse(ws.GetValue(2).ToString());
                w3 = float.Parse(ws.GetValue(3).ToString());
                w4 = float.Parse(ws.GetValue(4).ToString());
                w5 = float.Parse(ws.GetValue(5).ToString());
                w6 = float.Parse(ws.GetValue(6).ToString());
                ruch.SetL(con, "loc1", w1 + krok, w2, w3, w4, w5, w6, out stat);
                prog.Execute(con, "ruch()", 0, out stat);       //Wywołanie funkcji 'ruch'
            }
        }
        //... -X
        private void button9_Click(object sender, EventArgs e)
        {
            int stat = 5;
            float w1, w2, w3, w4, w5, w6;
            Status stan = new Status();

            int joi;
            if (connected == 1)
            {
                stan.Where(con, 0, out joi, out ws, out ko);
                w1 = float.Parse(ws.GetValue(1).ToString());
                w2 = float.Parse(ws.GetValue(2).ToString());
                w3 = float.Parse(ws.GetValue(3).ToString());
                w4 = float.Parse(ws.GetValue(4).ToString());
                w5 = float.Parse(ws.GetValue(5).ToString());
                w6 = float.Parse(ws.GetValue(6).ToString());
                ruch.SetL(con, "loc1", w1 - krok, w2, w3, w4, w5, w6, out stat);
                prog.Execute(con, "ruch()", 0, out stat);
            }
        }
        //... +Y
        private void button7_Click(object sender, EventArgs e)
        {
            int stat = 5;
            float w1, w2, w3, w4, w5, w6;
            Status stan = new Status();

            int joi;
            if (connected == 1)
            {
                stan.Where(con, 0, out joi, out ws, out ko);
                w1 = float.Parse(ws.GetValue(1).ToString());
                w2 = float.Parse(ws.GetValue(2).ToString());
                w3 = float.Parse(ws.GetValue(3).ToString());
                w4 = float.Parse(ws.GetValue(4).ToString());
                w5 = float.Parse(ws.GetValue(5).ToString());
                w6 = float.Parse(ws.GetValue(6).ToString());
                ruch.SetL(con, "loc1", w1, w2 + krok, w3, w4, w5, w6, out stat);
                prog.Execute(con, "ruch()", 0, out stat);
            }
        }
        //... -Y
        private void button10_Click(object sender, EventArgs e)
        {
            int stat = 5;
            float w1, w2, w3, w4, w5, w6;
            Status stan = new Status();

            int joi;
            if (connected == 1)
            {
                stan.Where(con, 0, out joi, out ws, out ko);
                w1 = float.Parse(ws.GetValue(1).ToString());
                w2 = float.Parse(ws.GetValue(2).ToString());
                w3 = float.Parse(ws.GetValue(3).ToString());
                w4 = float.Parse(ws.GetValue(4).ToString());
                w5 = float.Parse(ws.GetValue(5).ToString());
                w6 = float.Parse(ws.GetValue(6).ToString());
                ruch.SetL(con, "loc1", w1, w2 - krok, w3, w4, w5, w6, out stat);
                prog.Execute(con, "ruch()", 0, out stat);
            }
        }
        //... +Z
        private void button8_Click(object sender, EventArgs e)
        {
            int stat = 5;
            float w1, w2, w3, w4, w5, w6;
            Status stan = new Status();

            int joi;
            if (connected == 1)
            {
                stan.Where(con, 0, out joi, out ws, out ko);
                w1 = float.Parse(ws.GetValue(1).ToString());
                w2 = float.Parse(ws.GetValue(2).ToString());
                w3 = float.Parse(ws.GetValue(3).ToString());
                w4 = float.Parse(ws.GetValue(4).ToString());
                w5 = float.Parse(ws.GetValue(5).ToString());
                w6 = float.Parse(ws.GetValue(6).ToString());
                ruch.SetL(con, "loc1", w1, w2, w3 + krok, w4, w5, w6, out stat);
                prog.Execute(con, "ruch()", 0, out stat);
            }
        }
        //... -Z
        private void button11_Click(object sender, EventArgs e)
        {
            int stat = 5;
            float w1, w2, w3, w4, w5, w6;
            Status stan = new Status();

            int joi;
            if (connected == 1)
            {
                stan.Where(con, 0, out joi, out ws, out ko);
                w1 = float.Parse(ws.GetValue(1).ToString());
                w2 = float.Parse(ws.GetValue(2).ToString());
                w3 = float.Parse(ws.GetValue(3).ToString());
                w4 = float.Parse(ws.GetValue(4).ToString());
                w5 = float.Parse(ws.GetValue(5).ToString());
                w6 = float.Parse(ws.GetValue(6).ToString());
                ruch.SetL(con, "loc1", w1, w2, w3 - krok, w4, w5, w6, out stat);
                prog.Execute(con, "ruch()", 0, out stat);
            }
        }
        //Przemieszczenie się na pozycję 'loc1' - ostatnio ustawioną
        //UWAGA - wymaga jednej linijki napisanej w V+
        //"MOVE loc1"
        //Funkcja musi być zapisana jako 'ruch'
        private void button12_Click(object sender, EventArgs e)
        {
            int stat = 0;
            if (connected == 1)
            {
                prog.Execute(con, "ruch()", 0, out stat);       //Wywołanie funkcji 'ruch'
            }
        }
    }
}
