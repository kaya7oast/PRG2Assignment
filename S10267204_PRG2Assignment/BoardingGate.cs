using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267204_PRG2Assignment
{
    internal class BoardingGate
    {
        public string Gate { get; set; }
        public bool DDJB { get; set; }
        public bool CFFT { get; set; }
        public bool LWTT { get; set; }

        public BoardingGate(string gate, bool ddjb, bool cfft, bool lwtt)
        {
            Gate = gate;
            DDJB = ddjb;
            CFFT = cfft;
            LWTT = lwtt;
        }

        public override string ToString()
        {
            return $"Gate: {Gate,-4} DDJB: {DDJB,-6} CFFT: {CFFT,-6} LWTT: {LWTT,-6}";
        }
    }
}
