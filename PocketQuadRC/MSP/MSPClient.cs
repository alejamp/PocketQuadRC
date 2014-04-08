using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace PocketQuadRC.MSP
{
    enum MSP_STATES { 
        Listening,
        BeginMessageReceived,
        MessageMarkReceived,
        IncomingMarkReceived,
        DataLengthReceived,
        CommandCodeReceived,
        PayloadReceived,
        Checking
    }

    //http://www.multiwii.com/forum/viewtopic.php?f=8&t=1516
    //http://www.multiwii.com/wiki/index.php?title=Multiwii_Serial_Protocol 
    public class MSPClient
    {
        public const byte CC_MSP_ATTITUDE = 108;

        private string portName = " COM1";
        private int baudRate = 57600;
        private SerialPort _serialPort = new SerialPort();
        private byte[] _reqFrameMark;
        private byte[] _resFrameMark;
        private MSP_STATES _currentState = MSP_STATES.Listening;
        private byte _currentMessageLength = 0;
        private List<byte> _currentMessageDataReceived = new List<byte>();
        private byte _currentCommandCode = 0;
        private byte[] _supportedCommandCodes = new byte[] { 
            CC_MSP_ATTITUDE 
        };

        public Action<Messages.MSP_IncomingMessage> OnDataReceived;

        public MSPClient()
        {
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            // 36, 77, 60
            _reqFrameMark = new byte[] { Convert.ToByte('$'), Convert.ToByte('M'), Convert.ToByte('<') };

            // 36, 77, 62
            _resFrameMark = new byte[] { Convert.ToByte('$'), Convert.ToByte('M'), Convert.ToByte('>') };
        }

        public void Begin(string portName, int baudRate = 57600)
        {
            this.portName = portName;
            this.baudRate = baudRate;

            _serialPort.PortName = portName;
            _serialPort.BaudRate = baudRate;
            _serialPort.Parity = Parity.None;
            _serialPort.StopBits = StopBits.One;
            _serialPort.DataBits = 8;
            _serialPort.Handshake = Handshake.None;
            _serialPort.Open();
        }


        // ROLL/PITCH/YAW/THROTTLE/AUX1/AUX2/AUX3AUX4
        // Range [1000;2000]
        // This request is used to inject RC channel via MSP. Each chan overrides legacy RX as long as it is refreshed at least every second. See UART radio projects for more details.
        // Command Code = 200
        public void MSP_SET_RAW_RC(UInt16 ch1, UInt16 ch2, UInt16 ch3, UInt16 ch4, UInt16 ch5, UInt16 ch6, UInt16 ch7, UInt16 ch8)
        {
            // Send
            List<byte> data = new List<byte>();
            data.AddRange(BitConverter.GetBytes(ch1));
            data.AddRange(BitConverter.GetBytes(ch2));
            data.AddRange(BitConverter.GetBytes(ch3));
            data.AddRange(BitConverter.GetBytes(ch4));
            data.AddRange(BitConverter.GetBytes(ch5));
            data.AddRange(BitConverter.GetBytes(ch6));
            data.AddRange(BitConverter.GetBytes(ch7));
            data.AddRange(BitConverter.GetBytes(ch8));

            SendMessage(BuildMessage(data.ToArray(), 200));
        }

        public void SendMessage(byte[] message)
        {
            _serialPort.Write(message, 0, message.Length);
        }

        public void RequestMessage(byte commandCode)
        {
            SendMessage(BuildMessage(null, commandCode));
        }

        public void Request_MSP_ATTITUDE()
        {
            RequestMessage(CC_MSP_ATTITUDE);
        }

        public void SendByte(byte b)
        {
            _serialPort.Write(new byte[]{b}, 0, 1);
        }

        // Data Frame:
        // $M>[data length][code][data][checksum]
        //1 octet '$'
        //1 octet 'M'
        //1 octet '<'
        //1 octet [data length]
        //1 octet [code]
        //several octets [data]
        //1 octet [checksum]
        private byte[] BuildMessage (byte[] data, byte commandCode)
        {
            List<byte> msg = new List<byte>();
            //msg.AddRange(_reqFrameMark);
            //msg.Add(commandCode);
            //msg.Add((data != null ? (byte)data.Length : (byte)0));
            //msg.AddRange(data != null ? data : new byte[] {0xFF});
            //msg.Add(data != null ? CalculateChecksum(data) : CalculateChecksum(new byte[] {0xFF}));
            byte size = (data != null ? (byte)data.Length : (byte)0);
            msg.AddRange(_reqFrameMark);
            msg.Add(size);
            msg.Add(commandCode);
            if (data!= null) msg.AddRange(data);
            msg.Add(CalculateChecksumReq(commandCode, size, data));

            return msg.ToArray();
        }

        private byte CalculateChecksumReq(byte cmdCode, byte size, byte[] data)
        {
            byte checksum = 0;
            checksum ^= size;
            checksum ^= cmdCode;

            if (data != null)
                foreach (var b in data)
                    checksum ^= b;

            return checksum;
        }


        private byte CalculateChecksum(byte[] data)
        {
            byte checksum = 0;
            foreach (var b in data)
                checksum ^= b;
            return checksum;
        }

        private bool isValidMessage(byte[] data, byte checksum)
        {
            return CalculateChecksum(data) == checksum;
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            //Console.WriteLine("Serial:" + _serialPort.ReadExisting());
            SerialPort com = (SerialPort)sender;

            while (com.BytesToRead > 0)
            {
                byte b = (byte)com.ReadByte();
                switch (_currentState)
                {
                    case MSP_STATES.Listening:
                        if (b == _resFrameMark[0]) _currentState = MSP_STATES.BeginMessageReceived;
                        break;
                    case MSP_STATES.BeginMessageReceived:
                        if (b == _resFrameMark[1])
                            _currentState = MSP_STATES.MessageMarkReceived;
                        else
                            _currentState = MSP_STATES.Listening;
                        break;
                    case MSP_STATES.MessageMarkReceived:
                        if (b == _resFrameMark[2])
                            _currentState = MSP_STATES.IncomingMarkReceived;
                        else
                            _currentState = MSP_STATES.Listening;
                        break;
                    case MSP_STATES.IncomingMarkReceived:
                        _currentMessageLength = b;
                        _currentState = MSP_STATES.DataLengthReceived;
                        break;
                    case MSP_STATES.DataLengthReceived:
                        if (_supportedCommandCodes.Contains<byte>(b))
                        {
                            _currentCommandCode = b;
                            _currentState = MSP_STATES.CommandCodeReceived;
                            _currentMessageDataReceived.Clear();
                        }
                        else
                            _currentState = MSP_STATES.Listening;
                        break;
                    case MSP_STATES.CommandCodeReceived:
                        _currentMessageDataReceived.Add(b);
                        if (_currentMessageDataReceived.Count == _currentMessageLength)
                            _currentState = MSP_STATES.PayloadReceived;
                        break;
                    case MSP_STATES.PayloadReceived:
                        //if (isValidMessage(_currentMessageDataReceived.ToArray(), b))
                            ProcessIncomingMessage(_currentCommandCode, _currentMessageDataReceived.ToArray());
                        _currentState = MSP_STATES.Listening;
                        break;
                    default:
                        break;
                }
            }
        }

        private void ProcessIncomingMessageEvent (Messages.MSP_IncomingMessage msg)
        {
            if (OnDataReceived != null) OnDataReceived(msg);
        }


        private void ProcessIncomingMessage(byte commandCode, byte[] data)
        {
            switch (commandCode)
            {
                case CC_MSP_ATTITUDE:
                    ProcessIncomingMessageEvent(new Messages.MSP_ATTITUDE() 
                    {
                        code = CC_MSP_ATTITUDE,
                        roll = (float)BitConverter.ToInt16(new byte[]{ data[0], data[1]}, 0) / 10f,
                        pitch = (float)BitConverter.ToInt16(new byte[] { data[2], data[3] }, 0) / 10f,
                        heading = (float)BitConverter.ToInt16(new byte[] { data[4], data[5] }, 0),
                    });
                    break;

                    
                default:
                    break;
            }
        }

    }
}
