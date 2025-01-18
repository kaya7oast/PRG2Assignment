using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267204_PRG2Assignment
{
    internal class Airline
    {
        public string Name { get; set; }
        public string Code { get; set; }

        private Dictionary<string, Flight> flights;

        public Airline(string code, string name)
        {
            Code = code;
            Name = name;    
            flights = new Dictionary<string, Flight>();
        }

        public bool AddFlight(Flight flight)
        {
            if (!flights.ContainsKey(flight.FlightNumber))
            {
                flights.Add(flight.FlightNumber, flight);
                return true;
            }
            return false;
        }

        public bool RemoveFlight(string flightNumber)
        {
            return flights.Remove(flightNumber);
        }

        public double CalculateFees()
        {
            // Placeholder for fee calculation
            return 0.0;
        }

        public override string ToString()
        {
            return $"Airline: {Name}, Code: {Code}, Flights: {flights.Count}";
        }
    }
}
