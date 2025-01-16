using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267204_PRG2Assignment
{
    internal class Flight
    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }
        public Flight () { }

        public Flight (string flightNumber, string origin, string destination, DateTime expectedTime, string status = "Unknown")
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            ExpectedTime = expectedTime;
            Status = status;
        }

        public override string ToString()
        {
            return $"Flight Number: {FlightNumber,-7} Origin: {Origin,-17} Destination: {Destination,-17} Arrival/Depature Time: {ExpectedTime,-23} Status: {Status,-6}";
        }
    }
}
