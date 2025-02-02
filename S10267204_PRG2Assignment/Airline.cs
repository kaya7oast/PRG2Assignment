using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//==========================================================
// Student Number	: S10266956
// Student Name	: Luo TianRui
// Partner Name	: Eden Wong Tian Le
//==========================================================

namespace S10267204_PRG2Assignment
{
    internal class Airline
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public Dictionary<string, Flight> Flights;

        public Airline(string code, string name)
        {
            Code = code;
            Name = name;
            Flights = new Dictionary<string, Flight>();
        }

        public bool AddFlight(Flight flight)
        {
            if (!Flights.ContainsKey(flight.FlightNumber))
            {
                Flights.Add(flight.FlightNumber, flight);
                return true;
            }
            return false;
        }

        public bool RemoveFlight(string flightNumber)
        {
            return Flights.Remove(flightNumber);
        }

        public double CalculateFees()
        {
            return 0.0;
        }

        public override string ToString()
        {
            return $"Airline: {Name}, Code: {Code}, Flights: {Flights.Count}";
        }
    }
}
