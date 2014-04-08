using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PocketQuadRC.MSP.Messages
{
    public class MSP_IncomingMessage {
        public byte code;
    }


    [Serializable]
    public class MSP_ATTITUDE : MSP_IncomingMessage
    {
        // Range [-180;180] (precision: 1/10 degree)
        public float roll;
        // Range [-90;90] (precision: 1/10 degree)
        public float pitch;
        // Range [-180;180] (precision 1 degree)
        public float heading;
    }
}
