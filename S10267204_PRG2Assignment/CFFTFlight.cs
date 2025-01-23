using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267204_PRG2Assignment
{
    internal class CFFTFlight : Flight
    {
        public CFFTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status = "Null") : base(flightNumber, origin, destination, expectedTime, status) { }

        public override string ToString()
        {
            return $"Flight Number: {FlightNumber,-7} Origin: {Origin,-18} Destination: {Destination,-17} Arrival/Depature Time: {ExpectedTime,-23} Status: {Status,-6}";
        }
    }
}
