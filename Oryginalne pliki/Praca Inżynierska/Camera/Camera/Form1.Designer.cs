namespace Camera
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.videoSourcePlayer1 = new AForge.Controls.VideoSourcePlayer();
            this.Login = new System.Windows.Forms.TextBox();
            this.Address = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
            this.Connect_button = new System.Windows.Forms.Button();
            this.CameraAxisCheckBox = new System.Windows.Forms.CheckBox();
            this.PictureBoxCameraAxis = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.PictureBoxFrame = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.button4 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.hScrollBar2 = new System.Windows.Forms.HScrollBar();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxCameraAxis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // videoSourcePlayer1
            // 
            this.videoSourcePlayer1.Location = new System.Drawing.Point(12, 12);
            this.videoSourcePlayer1.Name = "videoSourcePlayer1";
            this.videoSourcePlayer1.Size = new System.Drawing.Size(640, 512);
            this.videoSourcePlayer1.TabIndex = 0;
            this.videoSourcePlayer1.Text = "videoSourcePlayer1";
            this.videoSourcePlayer1.VideoSource = null;
            // 
            // Login
            // 
            this.Login.Location = new System.Drawing.Point(774, 55);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(214, 20);
            this.Login.TabIndex = 2;
            this.Login.Text = "admin";
            // 
            // Address
            // 
            this.Address.Location = new System.Drawing.Point(774, 29);
            this.Address.Name = "Address";
            this.Address.Size = new System.Drawing.Size(214, 20);
            this.Address.TabIndex = 3;
            this.Address.Text = "http://192.168.0.100/mjpg/video.mjpg";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(674, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Connection address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(674, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Login";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(674, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Password";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(774, 81);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(214, 20);
            this.Password.TabIndex = 7;
            this.Password.Text = "1234";
            // 
            // Connect_button
            // 
            this.Connect_button.Location = new System.Drawing.Point(994, 29);
            this.Connect_button.Name = "Connect_button";
            this.Connect_button.Size = new System.Drawing.Size(130, 30);
            this.Connect_button.TabIndex = 8;
            this.Connect_button.Text = "Connect";
            this.Connect_button.UseVisualStyleBackColor = true;
            this.Connect_button.Click += new System.EventHandler(this.Connect_button_Click);
            // 
            // CameraAxisCheckBox
            // 
            this.CameraAxisCheckBox.AutoSize = true;
            this.CameraAxisCheckBox.Location = new System.Drawing.Point(677, 111);
            this.CameraAxisCheckBox.Name = "CameraAxisCheckBox";
            this.CameraAxisCheckBox.Size = new System.Drawing.Size(83, 17);
            this.CameraAxisCheckBox.TabIndex = 10;
            this.CameraAxisCheckBox.Text = "Camera axis";
            this.CameraAxisCheckBox.UseVisualStyleBackColor = true;
            this.CameraAxisCheckBox.CheckedChanged += new System.EventHandler(this.CameraAxisCheckBox_CheckedChanged);
            // 
            // PictureBoxCameraAxis
            // 
            this.PictureBoxCameraAxis.BackColor = System.Drawing.Color.Transparent;
            this.PictureBoxCameraAxis.Location = new System.Drawing.Point(12, 12);
            this.PictureBoxCameraAxis.Name = "PictureBoxCameraAxis";
            this.PictureBoxCameraAxis.Size = new System.Drawing.Size(640, 512);
            this.PictureBoxCameraAxis.TabIndex = 11;
            this.PictureBoxCameraAxis.TabStop = false;
            this.PictureBoxCameraAxis.Visible = false;
            this.PictureBoxCameraAxis.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBoxCameraAxis_Paint);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(994, 71);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 30);
            this.button1.TabIndex = 12;
            this.button1.Text = "GetImage";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PictureBoxFrame
            // 
            this.PictureBoxFrame.ImageLocation = "";
            this.PictureBoxFrame.Location = new System.Drawing.Point(12, 12);
            this.PictureBoxFrame.Name = "PictureBoxFrame";
            this.PictureBoxFrame.Size = new System.Drawing.Size(640, 512);
            this.PictureBoxFrame.TabIndex = 13;
            this.PictureBoxFrame.TabStop = false;
            this.PictureBoxFrame.Visible = false;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(677, 257);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(152, 58);
            this.button2.TabIndex = 16;
            this.button2.Text = "Start Server";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(677, 321);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(447, 180);
            this.textBox1.TabIndex = 17;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(677, 163);
            this.hScrollBar1.Maximum = 255;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(152, 19);
            this.hScrollBar1.TabIndex = 20;
            this.hScrollBar1.Value = 123;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(886, 163);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 20);
            this.button4.TabIndex = 21;
            this.button4.Text = "Erode";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(679, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Treshold";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(883, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Morphological Operations";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(832, 163);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(46, 20);
            this.textBox2.TabIndex = 24;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(967, 164);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(46, 20);
            this.textBox3.TabIndex = 25;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1019, 196);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 21);
            this.button3.TabIndex = 26;
            this.button3.Text = "Save Settings";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(832, 196);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(46, 20);
            this.textBox4.TabIndex = 28;
            // 
            // hScrollBar2
            // 
            this.hScrollBar2.Location = new System.Drawing.Point(677, 196);
            this.hScrollBar2.Maximum = 40;
            this.hScrollBar2.Name = "hScrollBar2";
            this.hScrollBar2.Size = new System.Drawing.Size(152, 19);
            this.hScrollBar2.TabIndex = 27;
            this.hScrollBar2.Value = 16;
            this.hScrollBar2.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar2_Scroll);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(886, 196);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 20);
            this.button5.TabIndex = 29;
            this.button5.Text = "Dilate";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(967, 197);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(46, 20);
            this.textBox5.TabIndex = 30;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(967, 257);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(152, 58);
            this.button6.TabIndex = 31;
            this.button6.Text = "Exit";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(1020, 163);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(99, 21);
            this.button7.TabIndex = 32;
            this.button7.Text = "Test";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 538);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.hScrollBar2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.PictureBoxFrame);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.PictureBoxCameraAxis);
            this.Controls.Add(this.CameraAxisCheckBox);
            this.Controls.Add(this.Connect_button);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Address);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.videoSourcePlayer1);
            this.Name = "Form1";
            this.Text = "Vison System";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxCameraAxis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxFrame)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AForge.Controls.VideoSourcePlayer videoSourcePlayer1;
        private System.Windows.Forms.TextBox Login;
        private System.Windows.Forms.TextBox Address;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Button Connect_button;
        private System.Windows.Forms.CheckBox CameraAxisCheckBox;
        private System.Windows.Forms.PictureBox PictureBoxCameraAxis;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox PictureBoxFrame;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.HScrollBar hScrollBar2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;

    }
}

