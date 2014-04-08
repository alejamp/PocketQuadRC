using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace TestConsole.Utils
{
    public partial class SerialPicker : UserControl
    {
        public SerialPicker()
        {
            InitializeComponent();
        }

        private void SerialPicker_Load(object sender, EventArgs e)
        {
            LoadAvailableComPorts();
        }

        private void LoadAvailableComPorts()
        {
            this.cmbPortName.Items.AddRange(SerialPort.GetPortNames());
        }

        public int BaudRate {
            get { return int.Parse(cmbBaudRate.Text); }
        }

        public string PortName
        {
            get { return this.cmbPortName.Text; }
        }
    }
}
