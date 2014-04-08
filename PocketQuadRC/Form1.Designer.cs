namespace PocketQuadRC
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
            this.components = new System.ComponentModel.Container();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtRoll = new System.Windows.Forms.TextBox();
            this.txtPitch = new System.Windows.Forms.TextBox();
            this.txtHeading = new System.Windows.Forms.TextBox();
            this.btnTestRequest = new System.Windows.Forms.Button();
            this.btnReqStatus = new System.Windows.Forms.Button();
            this.hz50 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tbThrottle = new System.Windows.Forms.TrackBar();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkARM = new System.Windows.Forms.CheckBox();
            this.mouseStick1 = new NSMouseStick.MouseStick();
            this.serialPicker1 = new TestConsole.Utils.SerialPicker();
            this.txtRollChannel = new System.Windows.Forms.TextBox();
            this.txtPitchChannel = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.tbThrottle)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(135, 76);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 5;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtRoll
            // 
            this.txtRoll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRoll.Location = new System.Drawing.Point(573, 560);
            this.txtRoll.Name = "txtRoll";
            this.txtRoll.Size = new System.Drawing.Size(100, 20);
            this.txtRoll.TabIndex = 6;
            // 
            // txtPitch
            // 
            this.txtPitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPitch.Location = new System.Drawing.Point(679, 560);
            this.txtPitch.Name = "txtPitch";
            this.txtPitch.Size = new System.Drawing.Size(100, 20);
            this.txtPitch.TabIndex = 7;
            // 
            // txtHeading
            // 
            this.txtHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHeading.Location = new System.Drawing.Point(785, 560);
            this.txtHeading.Name = "txtHeading";
            this.txtHeading.Size = new System.Drawing.Size(100, 20);
            this.txtHeading.TabIndex = 8;
            // 
            // btnTestRequest
            // 
            this.btnTestRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTestRequest.Location = new System.Drawing.Point(12, 397);
            this.btnTestRequest.Name = "btnTestRequest";
            this.btnTestRequest.Size = new System.Drawing.Size(100, 23);
            this.btnTestRequest.TabIndex = 9;
            this.btnTestRequest.Text = "MSP_ATTITUDE";
            this.btnTestRequest.UseVisualStyleBackColor = true;
            this.btnTestRequest.Click += new System.EventHandler(this.btnTestRequest_Click);
            // 
            // btnReqStatus
            // 
            this.btnReqStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReqStatus.Location = new System.Drawing.Point(123, 397);
            this.btnReqStatus.Name = "btnReqStatus";
            this.btnReqStatus.Size = new System.Drawing.Size(100, 23);
            this.btnReqStatus.TabIndex = 10;
            this.btnReqStatus.Text = "MSP_STATUS";
            this.btnReqStatus.UseVisualStyleBackColor = true;
            this.btnReqStatus.Click += new System.EventHandler(this.btnReqStatus_Click);
            // 
            // hz50
            // 
            this.hz50.Interval = 20;
            this.hz50.Tick += new System.EventHandler(this.hz20_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(337, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(548, 521);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Attitude";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(442, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Set Home";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(24, 80);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(88, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "50 hz update";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // tbThrottle
            // 
            this.tbThrottle.Location = new System.Drawing.Point(268, 61);
            this.tbThrottle.Maximum = 1000;
            this.tbThrottle.Name = "tbThrottle";
            this.tbThrottle.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbThrottle.Size = new System.Drawing.Size(45, 250);
            this.tbThrottle.TabIndex = 13;
            this.tbThrottle.TickFrequency = 100;
            this.tbThrottle.ValueChanged += new System.EventHandler(this.tbThrottle_ValueChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(337, 10);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(100, 17);
            this.checkBox2.TabIndex = 14;
            this.checkBox2.Text = "Attitude Update";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(21, 125);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(109, 17);
            this.checkBox3.TabIndex = 15;
            this.checkBox3.Text = "MSP RC Enabled";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.txtPitchChannel);
            this.groupBox2.Controls.Add(this.txtRollChannel);
            this.groupBox2.Controls.Add(this.mouseStick1);
            this.groupBox2.Controls.Add(this.chkARM);
            this.groupBox2.Controls.Add(this.btnTestRequest);
            this.groupBox2.Controls.Add(this.btnReqStatus);
            this.groupBox2.Controls.Add(this.tbThrottle);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(12, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(319, 426);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            // 
            // chkARM
            // 
            this.chkARM.AutoSize = true;
            this.chkARM.Location = new System.Drawing.Point(9, 31);
            this.chkARM.Name = "chkARM";
            this.chkARM.Size = new System.Drawing.Size(85, 17);
            this.chkARM.TabIndex = 14;
            this.chkARM.Text = "ARM Motors";
            this.chkARM.UseVisualStyleBackColor = true;
            this.chkARM.CheckedChanged += new System.EventHandler(this.chkARM_CheckedChanged);
            // 
            // mouseStick1
            // 
            this.mouseStick1.BackColor = System.Drawing.Color.Black;
            this.mouseStick1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.mouseStick1.LineEnd = System.Drawing.Drawing2D.LineCap.Round;
            this.mouseStick1.Location = new System.Drawing.Point(12, 61);
            this.mouseStick1.Name = "mouseStick1";
            this.mouseStick1.NeedleColor = System.Drawing.Color.Red;
            this.mouseStick1.NeedleWidth = 3;
            this.mouseStick1.Size = new System.Drawing.Size(250, 250);
            this.mouseStick1.TabIndex = 15;
            this.mouseStick1.Text = "mouseStick1";
            this.mouseStick1.MouseStickMoved += new NSMouseStick.MouseStickMovedEventHandler(this.mouseStick1_MouseStickMoved);
            // 
            // serialPicker1
            // 
            this.serialPicker1.Location = new System.Drawing.Point(1, 1);
            this.serialPicker1.Name = "serialPicker1";
            this.serialPicker1.Size = new System.Drawing.Size(227, 86);
            this.serialPicker1.TabIndex = 4;
            // 
            // txtRollChannel
            // 
            this.txtRollChannel.Location = new System.Drawing.Point(107, 16);
            this.txtRollChannel.Name = "txtRollChannel";
            this.txtRollChannel.Size = new System.Drawing.Size(100, 20);
            this.txtRollChannel.TabIndex = 16;
            // 
            // txtPitchChannel
            // 
            this.txtPitchChannel.Location = new System.Drawing.Point(213, 16);
            this.txtPitchChannel.Name = "txtPitchChannel";
            this.txtPitchChannel.Size = new System.Drawing.Size(100, 20);
            this.txtPitchChannel.TabIndex = 17;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 583);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtRoll);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.txtPitch);
            this.Controls.Add(this.txtHeading);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.serialPicker1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbThrottle)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TestConsole.Utils.SerialPicker serialPicker1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtRoll;
        private System.Windows.Forms.TextBox txtPitch;
        private System.Windows.Forms.TextBox txtHeading;
        private System.Windows.Forms.Button btnTestRequest;
        private System.Windows.Forms.Button btnReqStatus;
        private System.Windows.Forms.Timer hz50;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TrackBar tbThrottle;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkARM;
        private NSMouseStick.MouseStick mouseStick1;
        private System.Windows.Forms.TextBox txtPitchChannel;
        private System.Windows.Forms.TextBox txtRollChannel;
    }
}

