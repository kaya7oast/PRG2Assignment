using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267204_PRG2Assignment
{
    internal class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }

        public BoardingGate(string gateName, bool supportsCFFT, bool supportsDDJB, bool supportsLWTT)
        {
            GateName = gateName;
            SupportsCFFT = supportsCFFT;
            SupportsDDJB = supportsDDJB;
            SupportsLWTT = supportsLWTT;
            Flight = null;
        }

        public double CalculateFees()
        {
            return 0.0;
        }

        public override string ToString()
        {
            return $"Gate: {GateName}, SupportsCFFT: {SupportsCFFT}, SupportsDDJB: {SupportsDDJB}, SupportsLWTT: {SupportsLWTT}, Assigned Flight: {Flight.FlightNumber}";
        }
    }
}
