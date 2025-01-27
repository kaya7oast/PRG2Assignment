//==========================================================
// Student Number	: S10267204
// Student Name	: Eden Wong
// Partner Name	: Luo Tian Rui
//==========================================================
// features working on: 2, 3, 5, 6 & 9
using S10267204_PRG2Assignment;
using System.Collections.Immutable;
using System.Runtime.Serialization;

//FEATURE 1 Load files (airlines and boarding gates)
Dictionary<string, Airline> airlinesDic = new Dictionary<string, Airline>();
void InitializeAirlines()
// you should name them initialize___dict for future reference as there are many things called airlines and boarding gates already
{
    string[] airlineLines = File.ReadAllLines("airlines.csv");

    for (int i = 1; i < airlineLines.Length; i++)
    {
        var columns = airlineLines[i].Split(',');
        var airline = new Airline(columns[0], columns[1]);
        airlinesDic[columns[1]] = airline;
    }
}

Dictionary<string, BoardingGate> boardingGatesDic = new Dictionary<string, BoardingGate>();
void InitializeBoarding_gates()
{
    string[] gateLines = File.ReadAllLines("boardinggates.csv");

    for (int i = 1; i < gateLines.Length; i++)
    {
        var columns = gateLines[i].Split(',');
        var gate = new BoardingGate(columns[0], bool.Parse(columns[1]), bool.Parse(columns[2]), bool.Parse(columns[3]));
        boardingGatesDic[columns[0]] = gate;
    }
}


//FEATURE 2 Load files (flights)
Dictionary<string, Flight> FlightDic = new Dictionary<string, Flight>();
Dictionary<string, string> flightSRCs = new Dictionary<string, string>();
void InitializeFlightDic()
{
    StreamReader sr = new StreamReader("flights.csv");
    string? s = sr.ReadLine();
    string[] formats = { "h:mm tt", "htt" };
    while ((s = sr.ReadLine()) != null)
    {
        string SRC = "";
        string[] info = s.Split(",");
        string flight_number = info[0];
        string origin = info[1];
        string destination = info[2];
        string ETA = info[3];
        string checkSRC = info[4];
        if (checkSRC != "")
        {
            SRC = checkSRC;
        }
        DateTime expectedTime;
        if (DateTime.TryParseExact(ETA,
            formats,
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None,
            out expectedTime))
        {
            FlightDic.Add(flight_number, new Flight(flight_number, origin, destination, expectedTime));
            flightSRCs.Add(flight_number, SRC);
        }
        else
        {
            Console.WriteLine($"Failed to parse time for flight {flight_number}: {ETA}");
        }
    }
}
//FEATURE 3 display info
void DisplayFlightInfo()
{
    foreach (Flight flight in FlightDic.Values)
    {
        Console.WriteLine(flight);
    }
}

// FEATURE 4 display boarding gates in terminal 5 + extra info
void DisplayBoardingGatesInfo()
{
    Console.WriteLine("==================================================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("==================================================================");
    Console.WriteLine("Gate Name      DDJB                   CFFT                   LWTT");
    foreach (var gate in boardingGatesDic.Values)
    {
        Console.WriteLine($"{gate.GateName,-15}{gate.SupportsDDJB,-23}{gate.SupportsCFFT,-23}{gate.SupportsLWTT,-15}");
    }
}

// FEATURE 5 assign boarding gate to a flight
void AssignBGtoFlight()
{
    Console.WriteLine("Please input flight no.: ");
    string searched_flightNO = Console.ReadLine();
    if (flightSRCs[searched_flightNO] != "")
    {
        Console.WriteLine($"{FlightDic[searched_flightNO]} SRC:{flightSRCs[searched_flightNO]}");
    }
    else
    {
        Console.WriteLine($"{FlightDic[searched_flightNO]}");
    }
    while (true)
    {
        Console.WriteLine("Please input boarding gate: ");
        string selected_BoardingGate = Console.ReadLine();
        if (boardingGatesDic[selected_BoardingGate].Flight == null)
        {
            boardingGatesDic[selected_BoardingGate].Flight = FlightDic[searched_flightNO];
            if (flightSRCs[searched_flightNO] != "")
            {
                Console.WriteLine($"{FlightDic[searched_flightNO]}\tSRC: {flightSRCs[searched_flightNO]}\nBoarding Gate: {selected_BoardingGate}");
            }
            else
            {
                Console.WriteLine($"{FlightDic[searched_flightNO]}\nBoarding Gate: {selected_BoardingGate}");
            }
            Console.WriteLine("would you like to update the Status of the Flight, with a new Status of any of the following options: “Delayed” or “Boarding” [Y] \nor set the Status of the Flight to the default of “On Time” and continue to the next step [N]: ");
            string user_answer = Console.ReadLine().ToLower();
            if (user_answer != "n")
            {
                Console.WriteLine("Would you like the status to be: “Delayed”, “Boarding”, or “On Time”: ");
                string selected_status = Console.ReadLine();
                switch (selected_status)
                {
                    case "Delayed":
                        FlightDic[searched_flightNO].Status = "Delayed";
                        break;
                    case "Boarding":
                        FlightDic[searched_flightNO].Status = "Boarding";
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        break;

                }

            }
            else
            {
                FlightDic[searched_flightNO].Status = "On Time";
            }
            Console.WriteLine($"Flight {searched_flightNO} succesfully assigned to {selected_BoardingGate}, Status: {FlightDic[searched_flightNO].Status}");
            break;
        }
        else
        {
            Console.WriteLine("Boarding Gate already has scheduled flight please pick again.");
        }
    }
}


// FEATURE 6 create a new flight
void CreateFlight()
{
    Console.Write("Enter flight details (Flight Number, Origin, Destination and Status [“Delayed”, “Boarding”, or “On Time”]) [seperate info with ',']: ");
    string[] details = Console.ReadLine().Split(",");
    string new_Flight_No = details[0];
    string new_Origin = details[1];
    string new_Destination = details[2];
    string new_Status = details[3];
    string[] formats = { "h:mm tt", "htt" };
    Console.WriteLine("Enter Flight's Estimated Time Arrival [hh:mm tt]: ");
    string new_ETA = Console.ReadLine();
    string datetimeformat = "hh:mm tt";
    DateTime expectedTime = DateTime.ParseExact(new_ETA, datetimeformat, System.Globalization.CultureInfo.InvariantCulture);
    Console.WriteLine("Please enter any additional information (i.e Special Request Code) [Type 'None' if no addtional info added]: ");
    string additional_details = Console.ReadLine();
    string new_SRC = "";
    if (additional_details != "None")
    {
        new_SRC = additional_details;
    }
    Flight New_Flight = new Flight(new_Flight_No, new_Origin, new_Destination, expectedTime, new_Status);
    string new_flight_details = $"{new_Flight_No},{new_Origin},{new_Destination},{new_ETA},{new_SRC}";
    using (StreamWriter sw = new StreamWriter("flights.csv", append: true))
    {
        sw.WriteLine(new_flight_details);
    }
}
void CreateFlights()
{
    int count = 1;
    while (true)
    {
        CreateFlight();
        Console.WriteLine("Would you like to add another Flight? [Y/N]: ");
        string reply = Console.ReadLine().ToLower();
        if (reply != "y")
        {
            Console.WriteLine($"{count} new flight added.");
            break;
        }
        else
        {
            count++;
        }
    }
}

//FEATURE 9 Display scheduled flights in chronological order
void DisplayInChronologicalOrder()
{
    var sortedFlights = FlightDic.Values.OrderBy(flight => flight.ExpectedTime).ToList();

    foreach (var flight in sortedFlights)
    {
        var assignedGate = boardingGatesDic.Values.FirstOrDefault(gate => gate.Flight != null && gate.Flight.FlightNumber == flight.FlightNumber);

        if (flightSRCs[flight.FlightNumber] == "")
        {
            Console.Write($"{flight}");
        }
        else
        {
            Console.Write($"{flight} SRC:{flightSRCs[flight.FlightNumber],-5}");
        }
        if (assignedGate != null)
        {
            Console.Write($"\nBoarding Gate: {assignedGate.GateName}");
        }
        Console.WriteLine();
    }
}


InitializeFlightDic();
InitializeBoarding_gates();
InitializeAirlines();
DisplayInChronologicalOrder();
AssignBGtoFlight();
Console.Clear();
DisplayInChronologicalOrder();