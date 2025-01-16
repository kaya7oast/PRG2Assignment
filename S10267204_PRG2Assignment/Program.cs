//==========================================================
// Student Number	: S10267204
// Student Name	: Eden Wong
// Partner Name	: Tian Rui
//==========================================================
// features working on: 2, 3, 5, 6 & 9
using S10267204_PRG2Assignment;

//FEATURE 1 Load files (airlines and boarding gates)

/*
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
    // no need to display Airlines this is in another feature
    foreach (var airline in airlinesDic.Values)
    {
        Console.WriteLine(airline);
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
*/

//FEATURE 2 Load files (flights)
Dictionary<string,Flight> FlightDic = new Dictionary<string,Flight>();
void InitializeFlightDic()
{
    StreamReader sr = new StreamReader("flights.csv");
    string? s = sr.ReadLine();
    while ((s = sr.ReadLine()) != null)
    {
        string[] info = s.Split(",");
        string flight_number = info[0];
        string origin = info[1];
        string destination = info[2];
        string ETA = info[3];
        string SRC = info[4];

        FlightDic.Add(flight_number, new Flight(flight_number, origin, destination, ETA, SRC));
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

// FEATURE 5 assign boarding gate to a flight



// FEATURE 6 create a new flight
void CreateFlight()
{
    Console.Write("Enter flight details (Flight Number, Origin, Destination, and Expected Departure/Arrival Time) [seperate info with ',']: ");
    string[] details = Console.ReadLine().Split(",");
    string new_Flight_No = details[0];
    string new_Origin = details[1];
    string new_Destination = details[2];
    string new_ETA = details[3];
    Console.WriteLine("Would you like to enter any additional information (i.e Special Request Code) [Type 'No' if no addtional info added]: ");
    string additional_details = Console.ReadLine();
    string new_SRC = "";
    if (additional_details != "No")
    {
        new_SRC = additional_details;
    }
    Flight New_Flight = new Flight(new_Flight_No, new_Origin, new_Destination, new_ETA, new_SRC);
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

//FEATURE 9 

/*
InitializeBoarding_gates();
InitializeFlightDic();
InitializeAirlines();
DisplayFlightInfo();
DisplayBoardingGateInfo();
*/