﻿//==========================================================
// Student Number	: S10267204
// Student Name	: Eden Wong
// Partner Name	: Luo Tian Rui
//==========================================================
// features working on: 2, 3, 5, 6 & 9
using S10267204_PRG2Assignment;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
int count = 0;
//FEATURE 1 Load files (airlines and boarding gates)
Dictionary<string, Airline> airlinesDic = new Dictionary<string, Airline>();
void InitializeAirlines()
{
    Console.WriteLine("Loading Airlines...");
    string[] airlineLines = File.ReadAllLines("airlines.csv");
    count = 0;
    for (int i = 1; i < airlineLines.Length; i++)
    {
        var columns = airlineLines[i].Split(',');
        var airline = new Airline(columns[0], columns[1]);
        airlinesDic[columns[1]] = airline;
        count++;
    }
    Console.WriteLine($"{count} airlines loaded!");
}

Dictionary<string, BoardingGate> boardingGatesDic = new Dictionary<string, BoardingGate>();
void InitializeBoarding_gates()
{
    Console.WriteLine("Loading Boarding Gates...");
    string[] gateLines = File.ReadAllLines("boardinggates.csv");
    count = 0;
    for (int i = 1; i < gateLines.Length; i++)
    {
        var columns = gateLines[i].Split(',');
        var gate = new BoardingGate(columns[0], bool.Parse(columns[1]), bool.Parse(columns[2]), bool.Parse(columns[3]));
        boardingGatesDic[columns[0]] = gate;
        count++;
    }
    Console.WriteLine($"{count} Boarding Gates loaded!");
}


//FEATURE 2 Load files (flights)
Dictionary<string, Flight> FlightDic = new Dictionary<string, Flight>();
Dictionary<string, string> flightSRCs = new Dictionary<string, string>();
void InitializeFlightDic()
{
    Console.WriteLine("Loading Flights...");
    StreamReader sr = new StreamReader("flights.csv");
    string? s = sr.ReadLine();
    string[] formats = { "h:mm tt", "htt" };
    count = 0;
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
            count++;
        }
        else
        {
            Console.WriteLine($"Failed to parse time for flight {flight_number}: {ETA}");
        }
    }
    Console.WriteLine($"{count} Flights loaded!");
}
//FEATURE 3 display info
void DisplayFlightInfo()
{
    Console.WriteLine("==================================================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("==================================================================");
    Console.WriteLine($"{"Flight Number:",-15}{"Airline Name:",-19}{"Origin:",-19}{"Destination:",-17}{"Arrival/Depature Time:",-24}{"Status:",-8}");
    foreach (Flight flight in FlightDic.Values)
    {
        foreach (Airline airline in airlinesDic.Values)
        {
            if (flight.FlightNumber.Contains(airline.Name))
            {
                Console.WriteLine($"{flight.FlightNumber,-15}{airline.Code,-19}{flight.Origin,-19}{flight.Destination,-17}{flight.ExpectedTime,-24}{flight.Status,-8}");
            }
        }
    }
    Console.WriteLine();
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
    Console.WriteLine();
}

// FEATURE 5 assign boarding gate to a flight
void AssignBGtoFlight()
{
    string searched_flightNO = "";
    Flight Searched_Flight = null;
    while (true)
    {
        Console.WriteLine("Please input flight no.: ");
        searched_flightNO = Console.ReadLine();
        try
        {
            Searched_Flight = FlightDic[searched_flightNO];
            break;
        }
        catch (KeyNotFoundException)
        {
            Console.Clear();
            Console.WriteLine($"{searched_flightNO} is an invalid flight number, Please try Again\n");
        }
    }
    while (true)
    {
        BoardingGate Searched_Gate = null;
        string selected_BoardingGate = "";
        while (true)
        {
            Console.WriteLine("Please input boarding gate: ");
            selected_BoardingGate = Console.ReadLine();
            try
            {
                Searched_Gate = boardingGatesDic[selected_BoardingGate];
                break;
            }
            catch (KeyNotFoundException)
            {
                Console.Clear();
                Console.WriteLine($"{selected_BoardingGate} is an invalid Boarding Gate, Please try Again\n");
            }
        }
        if (Searched_Gate.Flight == null)
        {
            Searched_Gate.Flight = Searched_Flight;
            if (flightSRCs[searched_flightNO] != "")
            {
                Console.WriteLine($"\nFlight Number: {searched_flightNO}\nOrigin: {Searched_Flight.Origin}\nDestination: {Searched_Flight.Destination}\nExpected Time: {Searched_Flight.ExpectedTime}\nSpecial Request Code: {flightSRCs[searched_flightNO]}\nBoarding Gate: {selected_BoardingGate}\nSupports DDJB: {Searched_Gate.SupportsDDJB}\nSupports CFFT: {Searched_Gate.SupportsCFFT}\nSupports LWTT: {Searched_Gate.SupportsLWTT}\n");
            }
            else
            {
                Console.WriteLine($"\nFlight Number: {searched_flightNO}\nOrigin: {Searched_Flight.Origin}\nDestination: {Searched_Flight.Destination}\nExpected Time: {Searched_Flight.ExpectedTime}\nBoarding Gate: {selected_BoardingGate}\nSupports DDJB: {Searched_Gate.SupportsDDJB}\nSupports CFFT: {Searched_Gate.SupportsCFFT}\nSupports LWTT: {Searched_Gate.SupportsLWTT}\n");
            }
            Console.WriteLine("would you like to update the Status of the Flight? [Y] \nor set the Status of the Flight to the default of “On Time” and continue to the next step [N]: ");
            string user_answer = Console.ReadLine().ToLower();
            if (user_answer == "y")
            {
                Console.WriteLine("Would you like the status to be: \n1. Delayed \n2. Boarding\n3. On Time");
                int selected_status = Convert.ToInt16(Console.ReadLine());
                switch (selected_status)
                {
                    case 1:
                        Searched_Flight.Status = "Delayed";
                        break;
                    case 2:
                        Searched_Flight.Status = "Boarding";
                        break;
                    case 3:
                        Searched_Flight.Status = "On Time";
                        break;
                    default:
                        Console.WriteLine("Invalid Input, Please try again.");
                        break;
                }
            }
            else if (user_answer == "n") 
            {
                Searched_Flight.Status = "On Time";
            }
            else
            {
                Console.WriteLine("Invalid Input, please try again.");
            }
            Console.WriteLine($"Flight {searched_flightNO} succesfully assigned to {selected_BoardingGate}, Status: {Searched_Flight.Status}\n");
            break;
        }
        else
        {
            Console.WriteLine("Boarding Gate already has scheduled flight please pick again.\n");
        }
    }
}


// FEATURE 6 create a new flight
void CreateFlight()
{
    string[] details = { "", "" };
    string new_Flight_No = "";
    string new_Origin = "";
    string new_Destination = "";
    string new_Status = "";
    while (true)
    {
        Console.Write("Enter flight details (Flight Number, Origin, Destination and Status [“Delayed”, “Boarding”, or “On Time”]) [seperate info with ',']: ");
        details = Console.ReadLine().Split(",");
        try
        {
            new_Flight_No = details[0];
            new_Origin = details[1];
            new_Destination = details[2];
            new_Status = details[3];
            break;
        }
        catch (IndexOutOfRangeException)
        {
            Console.Clear();
            Console.WriteLine("You did not input enough details. Try again.\n");
        }
    }
    string[] formats = { "h:mm tt", "htt" };
    string new_ETA = "";
    DateTime expectedTime = DateTime.Now;
    while (true)
    {
        Console.WriteLine("Enter Flight's Estimated Time Arrival [hh:mm tt] (e.g., '10:30 PM'): ");
        new_ETA = Console.ReadLine();
        try
        {
            string datetimeformat = "hh:mm tt";
            expectedTime = DateTime.ParseExact(new_ETA, datetimeformat, System.Globalization.CultureInfo.InvariantCulture);
            break;
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid time format. Please enter the time in 'hh:mm tt' format (e.g., '10:30 PM').\n");
        }
    }
    Console.WriteLine("Please enter any additional information (i.e Special Request Code) [Type 'None' if no addtional info added]: ");
    string additional_details = Console.ReadLine().ToLower();
    string new_SRC = "";
    if (additional_details != "none")
    {
        new_SRC = additional_details;
    }
    Flight New_Flight = new Flight(new_Flight_No, new_Origin, new_Destination, expectedTime, new_Status);
    string new_flight_details = $"{new_Flight_No},{new_Origin},{new_Destination},{new_ETA},{new_Status}";
    try
    {
        using (StreamWriter sfw = new StreamWriter("flights.csv", append: true))
        {
            sfw.WriteLine(new_flight_details);
        }
        Console.WriteLine($"Flight {New_Flight.FlightNumber} has been added!");
    }
    catch (IOException ex)
    {
        Console.WriteLine("Error writing to file: " + ex.Message);
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

//FEATURE 7
void ListAndDisplayFlightAndAirlineDetails()
{

    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Airline Name",-20}{"Airline Code"}");

    foreach (var airline in airlinesDic.Values)
    {
        Console.WriteLine($"{airline.Code,-20}{airline.Name}");
    }

    Console.WriteLine("\nEnter Airline Code: ");
    string airlineCode = Console.ReadLine().ToUpper();

    Airline selectedAirline = airlinesDic[airlineCode];

    foreach (Flight flight in FlightDic.Values)
    {
        if (flight.FlightNumber.Contains(airlineCode))
        {
            selectedAirline.AddFlight(flight);
        }
    }

    Console.WriteLine("\n=============================================");
    Console.WriteLine($"List of Flights for {selectedAirline.Name}");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Flight Number",-20}{"Origin",-20}{"Destination",-20}{"Expected Time"}");

    foreach (var flight in selectedAirline.Flights.Values)
    {
        string expectedTime = flight.ExpectedTime.ToString("dd/MM/yyyy hh:mm tt");
        Console.WriteLine($"{flight.FlightNumber,-20}{flight.Origin,-20}{flight.Destination,-20}{expectedTime}");
    }
    Console.WriteLine();
}

//FEATURE 8 Modify or Delete Flights
void ModifyOrDeleteFlight()
{
    InitializeFlightDic();
    InitializeBoarding_gates();
    InitializeAirlines();

    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Airline Code",-20}{"Airline Name"}");

    foreach (var airline in airlinesDic.Values)
    {
        Console.WriteLine($"{airline.Code,-20}{airline.Name}");
    }

    Console.WriteLine("\nEnter Airline Code");
    string airlineCode = Console.ReadLine().ToUpper();

    if (!airlinesDic.ContainsKey(airlineCode))
    {
        Console.WriteLine("Invalid Airline Code. Exiting...");
        return;
    }

    Airline selectedAirline = airlinesDic[airlineCode];

    foreach (Flight flight in FlightDic.Values)
    {
        if (flight.FlightNumber.Contains(airlineCode))
        {
            selectedAirline.AddFlight(flight);
        }
    }

    if (selectedAirline.Flights.Count == 0)
    {
        Console.WriteLine("No flights available for this airline.");
        return;
    }

    Console.WriteLine("\n=============================================");
    Console.WriteLine($"List of Flights for {selectedAirline.Name}");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Flight Number",-20}{"Origin",-20}{"Destination",-20}{"Expected Time"}");

    foreach (var flight in selectedAirline.Flights.Values)
    {
        string expectedTime = flight.ExpectedTime.ToString("dd/MM/yyyy hh:mm tt");
        Console.WriteLine($"{flight.FlightNumber,-20}{flight.Origin,-20}{flight.Destination,-20}{expectedTime}");
    }

    Console.WriteLine("\nWould you like to: \n[1]Modify an existing Flight\n[2]Delete an existing Flight");
    string action = Console.ReadLine();

    if (action == "1")
    {
        Console.WriteLine("\nEnter Flight Number to Modify");
        string flightNumber = Console.ReadLine().ToUpper();

        if (!selectedAirline.Flights.ContainsKey(flightNumber))
        {
            Console.WriteLine("Flight not found. Exiting...");
            return;
        }

        Flight selectedFlight = selectedAirline.Flights[flightNumber];

        Console.WriteLine("What would you like to modify?\n1. Origin\n2. Destination\n3. Expected Time\n4. Status\n5. Special Request Code");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("Enter new Origin: ");
                selectedFlight.Origin = Console.ReadLine();
                break;
            case "2":
                Console.Write("Enter new Destination: ");
                selectedFlight.Destination = Console.ReadLine();
                break;
            case "3":
                Console.Write("Enter new Expected Time (dd/MM/yyyy hh:mm tt): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime newTime))
                {
                    selectedFlight.ExpectedTime = newTime;
                }
                else
                {
                    Console.WriteLine("Invalid time format. No changes made.");
                }
                break;
            case "4":
                Console.Write("Enter new Status: ");
                selectedFlight.Status = Console.ReadLine();
                break;
            case "5":
                Console.Write("Enter new Special Request Code: ");
                string flightNumberSRC = selectedFlight.FlightNumber;
                if (flightSRCs.ContainsKey(flightNumberSRC))
                {
                    flightSRCs[flightNumberSRC] = Console.ReadLine();
                }
                else
                {
                    flightSRCs.Add(flightNumberSRC, Console.ReadLine());
                }
                break;
            case "6":
                Console.Write("Enter new Boarding Gate: ");
                string newGate = Console.ReadLine();
                if (boardingGatesDic != null && boardingGatesDic.ContainsKey(newGate))
                {
                    BoardingGate gate = boardingGatesDic[newGate];
                    if (gate.Flight == null)
                    {
                        gate.Flight = selectedFlight;
                    }
                    else
                    {
                        Console.WriteLine("The selected gate is already assigned to another flight. No changes made.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Boarding Gate. No changes made.");
                }
                break;
            default:
                Console.WriteLine("Invalid choice. No changes made.");
                break;
        }
    }

    else if (action == "2")
    {
        Console.WriteLine("\nEnter Flight Number to Delete");
        string flightNumber = Console.ReadLine().ToUpper();

        if (!selectedAirline.Flights.ContainsKey(flightNumber))
        {
            Console.WriteLine("Flight not found. Exiting...");
            return;
        }

        Console.WriteLine("Are you sure you want to delete this flight? (Y/N): ");
        string confirm = Console.ReadLine().ToUpper();

        if (confirm == "Y")
        {
            selectedAirline.RemoveFlight(flightNumber);
            Console.WriteLine("Flight deleted successfully");
        }

        else
        {
            Console.WriteLine("Deletion cancelled.)");
        }
    }

    else
    {
        Console.WriteLine("Invalid choice. Exiting...");
        return;
    }

    Console.WriteLine("\n=============================================");
    Console.WriteLine($"Updated List of Flights for {selectedAirline.Name}");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Flight Number",-20}{"Origin",-20}{"Destination",-20}{"Expected Time"}");

    foreach (var flight in selectedAirline.Flights.Values)
    {
        string expectedTime = flight.ExpectedTime.ToString("dd/MM/yyy hh:mm tt");
        Console.WriteLine($"{flight.FlightNumber,-20}{flight.Origin,-20}{flight.Destination,-20}{expectedTime,-20}");
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
    Console.WriteLine();
}

//Aditional Feature - Eden
void BulkProcessUnassignedFlights()
{
    Queue<Flight> unassignedFlights = new Queue<Flight>();
    List<BoardingGate> unassignedGates = new List<BoardingGate>();

    foreach (var flight in FlightDic.Values)
    {
        if (!boardingGatesDic.Values.Any(gate => gate.Flight?.FlightNumber == flight.FlightNumber))
        {
            unassignedFlights.Enqueue(flight);
        }
    }

    foreach (BoardingGate gate in boardingGatesDic.Values)
    {
        if (gate.Flight == null)
        {
            unassignedGates.Add(gate);
        }
    }
    int unassignedflights = unassignedFlights.Count;
    int unassignedgates = unassignedGates.Count;
    Console.WriteLine($"\nTotal number of unassigned flights: {unassignedflights}");
    Console.WriteLine($"Total number of unassigned boarding gates: {unassignedgates}");
    
    int processedFlights = 0;
    int processedGates = 0;

    while (unassignedFlights.Count > 0 && unassignedGates.Count > 0)
    {
        Flight flight = unassignedFlights.Dequeue();
        BoardingGate? selectedGate = null;

        string specialRequestCode = flightSRCs[flight.FlightNumber];
        if (specialRequestCode != "")
        {
            selectedGate = unassignedGates.FirstOrDefault(gate => (specialRequestCode == "DDJB" && gate.SupportsDDJB) || (specialRequestCode == "CFFT" && gate.SupportsCFFT) || (specialRequestCode == "LWTT" && gate.SupportsLWTT));
        }

        if (selectedGate == null)
        {
            selectedGate = unassignedGates.FirstOrDefault(gate =>!gate.SupportsDDJB && !gate.SupportsCFFT && !gate.SupportsLWTT);
        }

        // Assign gate if one is found
        if (selectedGate != null)
        {
            selectedGate.Flight = flight;
            unassignedGates.Remove(selectedGate);
            processedFlights++;
            processedGates++;
        }
    }

    Console.WriteLine($"\nTotal flights processed and assigned: {processedFlights}");
    Console.WriteLine($"Total gates processed and assigned: {processedGates}");
    Console.WriteLine();
    int totalFlights = FlightDic.Count;
    int totalGates = boardingGatesDic.Count;
    Console.WriteLine($"Percentage of flights assigned: {((double)processedFlights / unassignedflights) * 100:0.00}%");
    Console.WriteLine($"Percentage of gates assigned: {((double)processedGates / unassignedgates) * 100:0.00}%");
}

//ADDITIONAL FEATURE - TIAN RUI
void DisplayTotalFeePerAirline(Dictionary<string, Airline> airlinesDic, Dictionary<string, BoardingGate> boardingGatesDic)
{
    // Step 1: Check that all flights have been assigned boarding gates
    List<string> unassignedFlights = new List<string>();

    foreach (var airline in airlinesDic.Values)
    {
        foreach (var flight in airline.Flights.Values)
        {
            bool hasGate = boardingGatesDic.Values.Any(gate => gate.Flight == flight);
            if (!hasGate)
            {
                unassignedFlights.Add(flight.FlightNumber);
            }
        }
    }

    if (unassignedFlights.Count > 0)
    {
        Console.WriteLine("The following flights do not have assigned boarding gates:");
        foreach (string flightNumber in unassignedFlights)
        {
            Console.WriteLine($"- Flight {flightNumber}");
        }
        Console.WriteLine("Please assign all unassigned flights before running this feature again.");
        return;
    }

    // Step 2: Compute fees per airline
    double totalFees = 0;
    double totalDiscounts = 0;

    Console.WriteLine("=== Airline Fee Breakdown ===");

    foreach (var airline in airlinesDic.Values)
    {
        double airlineSubtotal = 0;
        double airlineDiscounts = 0;

        Console.WriteLine($"Checking Airline: {airline.Name} (Flights: {airline.Flights.Count})");

        foreach (var flight in airline.Flights.Values)
        {
            double flightFee = 300; // Base Boarding Gate Fee

            // Check for assigned boarding gate
            BoardingGate assignedGate = boardingGatesDic.Values.FirstOrDefault(g => g.Flight == flight);
            if (assignedGate == null)
            {
                Console.WriteLine($"Flight {flight.FlightNumber} has NO assigned boarding gate! Skipping fee calculation.");
                continue;
            }
            else
            {
                Console.WriteLine($"Flight {flight.FlightNumber} assigned to Gate {assignedGate.GateName}");
            }

            // SIN origin fee ($800) or destination fee ($500)
            if (flight.Origin == "SIN")
            {
                flightFee += 800;
                Console.WriteLine($"Flight {flight.FlightNumber} departs from SIN, adding $800");
            }
            if (flight.Destination == "SIN")
            {
                flightFee += 500;
                Console.WriteLine($"Flight {flight.FlightNumber} arrives at SIN, adding $500");
            }

            // Special Request Fee (if applicable)
            if (assignedGate.SupportsCFFT)
            {
                flightFee += 200;
                Console.WriteLine($"Flight {flight.FlightNumber} supports CFFT, adding $200");
            }
            if (assignedGate.SupportsDDJB)
            {
                flightFee += 150;
                Console.WriteLine($"Flight {flight.FlightNumber} supports DDJB, adding $150");
            }
            if (assignedGate.SupportsLWTT)
            {
                flightFee += 100;
                Console.WriteLine($"Flight {flight.FlightNumber} supports LWTT, adding $100");
            }

            airlineSubtotal += flightFee;
            Console.WriteLine($"Total fee for Flight {flight.FlightNumber}: ${flightFee}");
        }

        // Apply Promotional Discounts (if applicable)
        airlineDiscounts = CalculatePromotionalDiscount(airline, airlineSubtotal);

        // Final fee after discount
        double airlineTotal = airlineSubtotal - airlineDiscounts;
        totalFees += airlineSubtotal;
        totalDiscounts += airlineDiscounts;

        Console.WriteLine($"\n{airline.Name} (Code: {airline.Code})");
        Console.WriteLine($"Subtotal Fees: ${airlineSubtotal}");
        Console.WriteLine($"Discounts Applied: -${airlineDiscounts}");
        Console.WriteLine($"Final Fees to be Charged: ${airlineTotal}");
        Console.WriteLine("--------------------------------------");
    }

    // Step 3: Display Total Fees Summary
    Console.WriteLine("=== Terminal 5 Summary ===");
    Console.WriteLine($"Total Airline Fees Before Discounts: ${totalFees}");
    Console.WriteLine($"Total Discounts Applied: -${totalDiscounts}");
    Console.WriteLine($"Final Total Fees Collected: ${totalFees - totalDiscounts}");
    Console.WriteLine($"Percentage of Discounts: {((totalFees == 0) ? 0 : (totalDiscounts / totalFees) * 100):0.00}%");
}

// Placeholder function for promotional discount calculations
double CalculatePromotionalDiscount(Airline airline, double subtotal)
{
    // Example: Apply a 10% discount for airlines with more than 5 flights
    if (airline.Flights.Count > 5)
    {
        Console.WriteLine($"Promotional discount applied to {airline.Name}: 10% of ${subtotal}");
        return subtotal * 0.1;
    }
    return 0;
}

void Main()
{
    InitializeFlightDic();
    InitializeBoarding_gates();
    InitializeAirlines();
    Console.WriteLine();
    bool program = true;
    while (program)
    {
        Console.WriteLine("=============================================\r\nWelcome to Changi Airport Terminal 5\r\n=============================================\r\n1. List All Flights\r\n2. List Boarding Gates\r\n3. Assign a Boarding Gate to a Flight\r\n4. Create Flight\r\n5. Display Airline Flights\r\n6. Modify Flight Details\r\n7. Display Flight Schedule\r\n8. Bulk Assign Flights To Gates\r\n9. Display Total Fees Per Airline\r\n0. Exit\n");
        Console.WriteLine("Please select your option: ");
        string user_input = Console.ReadLine();
        Console.WriteLine();
        try
        {
            int UI = Convert.ToInt16(user_input);
            switch (UI)
            {
                case 1:
                    DisplayFlightInfo();
                    break;

                case 2:
                    DisplayBoardingGatesInfo();
                    break;

                case 3:
                    AssignBGtoFlight();
                    break;

                case 4:
                    CreateFlights();
                    break;

                case 5:
                    ListAndDisplayFlightAndAirlineDetails(); 
                    break;

                case 6:
                    break;

                case 7:
                    DisplayInChronologicalOrder();
                    break;

                case 8:
                    BulkProcessUnassignedFlights();
                    Console.WriteLine();
                    break;

                case 9:
                    DisplayTotalFeePerAirline(airlinesDic, boardingGatesDic);
                    break;

                case 0:
                    Console.WriteLine("Goodbye!");
                    program = false;
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine($"{user_input}\nPlease Input a number from the menu\n");
                    break;
            }
        }
        catch (FormatException)
        {
            Console.Clear();
            Console.WriteLine($"{user_input}\nPlease input a number from the menu\n");
        }
    }
}
Main();

