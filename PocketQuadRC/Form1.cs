using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArPilot.Labs._3Dcuboid;
using ArUXV.Math;

namespace PocketQuadRC
{
    public partial class Form1 : Form
    {
        private MSP.MSPClient client = new MSP.MSPClient();
        public _3DCuboidControl box1 { get; set; }

        private float rollOffsetAngle = 0;
        private float pitchOffsetAngle = 0;
        private float headOffsetAngle = 0;
        private float thOffset = 1000;
        private float thScale = 1;

        private float rollAngle = 0;
        private float pitchAngle = 0;
        private float headAngle = 0;

        private int rollChannelSetPoint = 1500;
        private int pitchChannelSetPoint = 1500;
        private int rollChannelMin = 1000;
        private int pitchChannelMin = 1000;
        private int rollChannelMax = 2000;
        private int pitchChannelMax = 2000;

        // 8 RC Channels
        // ROLL/PITCH/YAW/THROTTLE/AUX1/AUX2/AUX3AUX4
        private ushort[] channels = new ushort[] {0,0,0,0,0,0,0,0} ;

        public Form1()
        {
            InitializeComponent();
            System.Windows.Forms.Form.CheckForIllegalCrossThreadCalls = false;
        }

        private void btnPitch1_Click(object sender, EventArgs e)
        {
            //this.client.MSP_SET_RAW_RC(0,0,0,0,2000,0,0,0);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            client.Begin(serialPicker1.PortName, serialPicker1.BaudRate);

            var scale = 57.295f;

            client.OnDataReceived = (msg) => {
                if (msg is MSP.Messages.MSP_ATTITUDE)
                {
                    MSP.Messages.MSP_ATTITUDE aux = (MSP.Messages.MSP_ATTITUDE)msg;

                    rollAngle = - (float)aux.roll / scale ;
                    pitchAngle = (float)aux.pitch / scale ;
                    headAngle = -(float)aux.heading / scale ;

                    txtRoll.Text = rollAngle.ToString();
                    txtPitch.Text = pitchAngle.ToString();
                    txtHeading.Text = headAngle.ToString();

                    Quaternion q = new Quaternion();
                    q.FromEulerAngles(pitchAngle + pitchOffsetAngle, rollAngle + rollOffsetAngle, headAngle + headOffsetAngle);
                    box1.RotationMatrix = q.Conjugate().ToRotationMatrix();
                }
            };
            hz50.Enabled = true;
        }

        private void btnTestRequest_Click(object sender, EventArgs e)
        {
            client.Request_MSP_ATTITUDE();
        }

        private void btnReqStatus_Click(object sender, EventArgs e)
        {
            client.SendByte(108); // Status
        }

        private void hz20_Tick(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                client.Request_MSP_ATTITUDE();

            if (checkBox3.Checked)
                this.client.MSP_SET_RAW_RC(channels[0], channels[1], channels[2], channels[3], channels[4], channels[5], channels[6], channels[7]);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            box1 = new _3DCuboidControl(new string[] { "Right.png", "Left.png", "Front.png", "Back.png", "Top.png", "Bottom.png" });
            box1.Dock = DockStyle.Fill;
            groupBox1.Controls.Add(box1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rollOffsetAngle = -rollAngle;
            pitchOffsetAngle = -pitchAngle;
            headOffsetAngle = -headAngle;
        }

        private void tbThrottle_ValueChanged(object sender, EventArgs e)
        {
            var t = (float)tbThrottle.Value * thScale + thOffset;
            channels[3] = (ushort)t;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            hz50.Enabled = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Enabled = checkBox3.Checked;
        }

        private void chkARM_CheckedChanged(object sender, EventArgs e)
        {
            if (chkARM.Checked)
                channels[4] = 2000;
            else
                channels[4] = 1000;
        }

        private void mouseStick1_MouseStickMoved(object sender, NSMouseStick.MouseStickEventArgs e)
        {
            // Roll
            var rollDelta = (rollChannelMax - rollChannelMin)/2;
            var pitchDelta = (pitchChannelMax - pitchChannelMin)/2;
            channels[0] = (ushort)(rollChannelSetPoint + e.xMag * (float)rollDelta);
            channels[1] = (ushort)(pitchChannelSetPoint + e.yMag * (float)pitchDelta);

            txtRollChannel.Text = channels[0].ToString();
            txtPitchChannel.Text = channels[1].ToString();
        }
    }
}
