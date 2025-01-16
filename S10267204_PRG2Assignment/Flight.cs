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
        public string ArrivalDepatureTiming { get; set; }
        public string SpecialRequestCode { get; set; }

        public Flight (string flightNumber, string origin, string destination, string arrivalDepatureTiming, string specialRequestCode)
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            ArrivalDepatureTiming = arrivalDepatureTiming;
            SpecialRequestCode = specialRequestCode;
        }

        public override string ToString()
        {
            return $"Flight Number: {FlightNumber,-7} Origin: {Origin,-18} Destination: {Destination,-18} Arrival/Depature Time: {ArrivalDepatureTiming,-9} Special Request Code: {SpecialRequestCode,-4}";
        }
    }
}
