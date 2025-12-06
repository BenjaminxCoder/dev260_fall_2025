using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment10
{
    /// <summary>
    /// Main entry point for the Flight Route Network Navigator application.
    /// This application demonstrates Graph data structures through interactive flight route planning.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Flight Route Network Navigator ===");
            Console.WriteLine("This application uses Graph data structures for route finding and network analysis.\n");

            try
            {
                // Initialize the flight network system
                var flightNetwork = new FlightNetwork();
                
                // Load flight data from CSV file
                Console.WriteLine("Loading flight data from CSV file...");
                flightNetwork.LoadFlightsFromCSV("flights.csv");
                
                Console.WriteLine(); // Extra line for spacing
                
                // Start the interactive navigator immediately
                var navigator = new FlightNetworkNavigator(flightNetwork);
                navigator.Run();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Error: Could not find the flight data file.");
                Console.WriteLine($"Details: {ex.Message}");
                Console.WriteLine("\nMake sure 'flights.csv' is in the same directory as the program.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
