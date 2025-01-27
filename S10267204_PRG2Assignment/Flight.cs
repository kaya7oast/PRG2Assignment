using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267204_PRG2Assignment
{
    internal class Flight : IComparable<Flight>
    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }

        public Flight(string flightNumber, string origin, string destination, DateTime expectedTime, string status = "Null")
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            ExpectedTime = expectedTime;
            Status = status;
        }

        public virtual double CalculateFees()
        {
            return 0.0;
        }
        public int CompareTo(Flight other)
        {
            if (other == null) return 1;
            return ExpectedTime.CompareTo(other.ExpectedTime);
        }

        public override string ToString()
        {
            return $"Flight Number: {FlightNumber,-7} Origin: {Origin,-18} Destination: {Destination,-16} Arrival/Depature Time: {ExpectedTime,-22} Status: {Status,-8}";
        }
    }
}
