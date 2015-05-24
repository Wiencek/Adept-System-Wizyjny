namespace CompleteProgram
{
    partial class Camera
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.R_value = new System.Windows.Forms.TextBox();
            this.G_value = new System.Windows.Forms.TextBox();
            this.B_value = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RMoreThan = new System.Windows.Forms.RadioButton();
            this.RLessThan = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.GMoreThan = new System.Windows.Forms.RadioButton();
            this.GLessThan = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.BMoreThan = new System.Windows.Forms.RadioButton();
            this.BLessThan = new System.Windows.Forms.RadioButton();
            this.MinH_value = new System.Windows.Forms.TextBox();
            this.MinW_value = new System.Windows.Forms.TextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxCameraAxis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxFrame)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
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
            this.Login.Location = new System.Drawing.Point(804, 55);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(214, 20);
            this.Login.TabIndex = 2;
            this.Login.Text = "admin";
            // 
            // Address
            // 
            this.Address.Location = new System.Drawing.Point(804, 29);
            this.Address.Name = "Address";
            this.Address.Size = new System.Drawing.Size(214, 20);
            this.Address.TabIndex = 3;
            this.Address.Text = "http://192.168.2.3/mjpg/video.mjpg";
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
            this.Password.Location = new System.Drawing.Point(804, 81);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(214, 20);
            this.Password.TabIndex = 7;
            this.Password.Text = "1234";
            // 
            // Connect_button
            // 
            this.Connect_button.Location = new System.Drawing.Point(1049, 29);
            this.Connect_button.Name = "Connect_button";
            this.Connect_button.Size = new System.Drawing.Size(175, 72);
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
            this.button1.Location = new System.Drawing.Point(1049, 164);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(175, 44);
            this.button1.TabIndex = 12;
            this.button1.Text = "Capture points";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PictureBoxFrame
            // 
            this.PictureBoxFrame.Location = new System.Drawing.Point(12, 12);
            this.PictureBoxFrame.Name = "PictureBoxFrame";
            this.PictureBoxFrame.Size = new System.Drawing.Size(640, 512);
            this.PictureBoxFrame.TabIndex = 13;
            this.PictureBoxFrame.TabStop = false;
            this.PictureBoxFrame.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1049, 111);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(175, 47);
            this.button2.TabIndex = 14;
            this.button2.Text = "Get Frame";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "1: 2",
            "2: 3"});
            this.listBox1.Location = new System.Drawing.Point(688, 225);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(200, 290);
            this.listBox1.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(912, 228);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "R";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(912, 264);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "G";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(912, 302);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "B";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(680, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "MinHeight";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(826, 178);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "MinWidth";
            // 
            // R_value
            // 
            this.R_value.Location = new System.Drawing.Point(1108, 225);
            this.R_value.Name = "R_value";
            this.R_value.Size = new System.Drawing.Size(100, 20);
            this.R_value.TabIndex = 21;
            // 
            // G_value
            // 
            this.G_value.Location = new System.Drawing.Point(1108, 261);
            this.G_value.Name = "G_value";
            this.G_value.Size = new System.Drawing.Size(100, 20);
            this.G_value.TabIndex = 22;
            // 
            // B_value
            // 
            this.B_value.Location = new System.Drawing.Point(1108, 299);
            this.B_value.Name = "B_value";
            this.B_value.Size = new System.Drawing.Size(100, 20);
            this.B_value.TabIndex = 23;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.RMoreThan);
            this.panel1.Controls.Add(this.RLessThan);
            this.panel1.Location = new System.Drawing.Point(933, 225);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(169, 20);
            this.panel1.TabIndex = 24;
            // 
            // RMoreThan
            // 
            this.RMoreThan.AutoSize = true;
            this.RMoreThan.Location = new System.Drawing.Point(107, 3);
            this.RMoreThan.Name = "RMoreThan";
            this.RMoreThan.Size = new System.Drawing.Size(31, 17);
            this.RMoreThan.TabIndex = 27;
            this.RMoreThan.TabStop = true;
            this.RMoreThan.Text = ">";
            this.RMoreThan.UseVisualStyleBackColor = true;
            // 
            // RLessThan
            // 
            this.RLessThan.AutoSize = true;
            this.RLessThan.Location = new System.Drawing.Point(34, 3);
            this.RLessThan.Name = "RLessThan";
            this.RLessThan.Size = new System.Drawing.Size(31, 17);
            this.RLessThan.TabIndex = 26;
            this.RLessThan.TabStop = true;
            this.RLessThan.Text = "<";
            this.RLessThan.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.GMoreThan);
            this.panel2.Controls.Add(this.GLessThan);
            this.panel2.Location = new System.Drawing.Point(933, 261);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(169, 20);
            this.panel2.TabIndex = 28;
            // 
            // GMoreThan
            // 
            this.GMoreThan.AutoSize = true;
            this.GMoreThan.Location = new System.Drawing.Point(107, 3);
            this.GMoreThan.Name = "GMoreThan";
            this.GMoreThan.Size = new System.Drawing.Size(31, 17);
            this.GMoreThan.TabIndex = 27;
            this.GMoreThan.TabStop = true;
            this.GMoreThan.Text = ">";
            this.GMoreThan.UseVisualStyleBackColor = true;
            // 
            // GLessThan
            // 
            this.GLessThan.AutoSize = true;
            this.GLessThan.Location = new System.Drawing.Point(34, 3);
            this.GLessThan.Name = "GLessThan";
            this.GLessThan.Size = new System.Drawing.Size(31, 17);
            this.GLessThan.TabIndex = 26;
            this.GLessThan.TabStop = true;
            this.GLessThan.Text = "<";
            this.GLessThan.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.BMoreThan);
            this.panel3.Controls.Add(this.BLessThan);
            this.panel3.Location = new System.Drawing.Point(933, 299);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(169, 20);
            this.panel3.TabIndex = 28;
            // 
            // BMoreThan
            // 
            this.BMoreThan.AutoSize = true;
            this.BMoreThan.Location = new System.Drawing.Point(107, 3);
            this.BMoreThan.Name = "BMoreThan";
            this.BMoreThan.Size = new System.Drawing.Size(31, 17);
            this.BMoreThan.TabIndex = 27;
            this.BMoreThan.TabStop = true;
            this.BMoreThan.Text = ">";
            this.BMoreThan.UseVisualStyleBackColor = true;
            // 
            // BLessThan
            // 
            this.BLessThan.AutoSize = true;
            this.BLessThan.Location = new System.Drawing.Point(34, 3);
            this.BLessThan.Name = "BLessThan";
            this.BLessThan.Size = new System.Drawing.Size(31, 17);
            this.BLessThan.TabIndex = 26;
            this.BLessThan.TabStop = true;
            this.BLessThan.Text = "<";
            this.BLessThan.UseVisualStyleBackColor = true;
            // 
            // MinH_value
            // 
            this.MinH_value.Location = new System.Drawing.Point(741, 175);
            this.MinH_value.Name = "MinH_value";
            this.MinH_value.Size = new System.Drawing.Size(75, 20);
            this.MinH_value.TabIndex = 29;
            // 
            // MinW_value
            // 
            this.MinW_value.Location = new System.Drawing.Point(884, 175);
            this.MinW_value.Name = "MinW_value";
            this.MinW_value.Size = new System.Drawing.Size(75, 20);
            this.MinW_value.TabIndex = 30;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(933, 492);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 31;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // Camera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1247, 541);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.MinW_value);
            this.Controls.Add(this.MinH_value);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.B_value);
            this.Controls.Add(this.G_value);
            this.Controls.Add(this.R_value);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listBox1);
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
            this.Name = "Camera";
            this.Text = "Camera coordinates";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Shown += new System.EventHandler(this.Camera_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxCameraAxis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxFrame)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
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
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox R_value;
        private System.Windows.Forms.TextBox G_value;
        private System.Windows.Forms.TextBox B_value;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton RMoreThan;
        private System.Windows.Forms.RadioButton RLessThan;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton GMoreThan;
        private System.Windows.Forms.RadioButton GLessThan;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton BMoreThan;
        private System.Windows.Forms.RadioButton BLessThan;
        private System.Windows.Forms.TextBox MinH_value;
        private System.Windows.Forms.TextBox MinW_value;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;

    }
}

