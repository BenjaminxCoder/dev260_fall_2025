using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment10
{
    /// <summary>
    /// Interactive menu-driven navigator for the Flight Network application.
    /// Provides user interface for all graph operations and pathfinding algorithms.
    /// </summary>
    public class FlightNetworkNavigator
    {
        private FlightNetwork network;

        /// <summary>
        /// Initializes the navigator with a flight network instance
        /// </summary>
        /// <param name="flightNetwork">The flight network to navigate</param>
        public FlightNetworkNavigator(FlightNetwork flightNetwork)
        {
            network = flightNetwork ?? throw new ArgumentNullException(nameof(flightNetwork));
        }

        /// <summary>
        /// Main menu loop - runs until user chooses to exit
        /// </summary>
        public void Run()
        {
            bool running = true;

            while (running)
            {
                DisplayMainMenu();
                string choice = Console.ReadLine()?.Trim() ?? "";

                switch (choice)
                {
                    case "1":
                        DisplayAllAirports();
                        break;
                    case "2":
                        FindDirectFlightsMenu();
                        break;
                    case "3":
                        GetDestinationsFromMenu();
                        break;
                    case "4":
                        FindCheapestDirectFlightMenu();
                        break;
                    case "5":
                        FindRouteMenu();
                        break;
                    case "6":
                        FindShortestRouteMenu();
                        break;
                    case "7":
                        FindCheapestRouteMenu();
                        break;
                    case "8":
                        CompareRoutesMenu();
                        break;
                    case "9":
                        FindRoutesByCriteriaMenu();
                        break;
                    case "10":
                        FindHubAirportsMenu();
                        break;
                    case "11":
                        DisplayNetworkStatistics();
                        break;
                    case "12":
                        FindIsolatedAirportsMenu();
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("\nThank you for using Flight Route Network Navigator!");
                        break;
                    default:
                        Console.WriteLine("\nInvalid choice. Please try again.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Displays the main menu options
        /// </summary>
        private void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║        FLIGHT ROUTE NETWORK NAVIGATOR - MAIN MENU         ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("  BASIC OPERATIONS");
            Console.WriteLine("  ─────────────────────────────────────────────────────────");
            Console.WriteLine("  1.  Display All Airports");
            Console.WriteLine("  2.  Find Direct Flights Between Airports");
            Console.WriteLine("  3.  Get All Destinations from Airport");
            Console.WriteLine("  4.  Find Cheapest Direct Flight");
            Console.WriteLine();
            Console.WriteLine("  PATHFINDING ALGORITHMS");
            Console.WriteLine("  ─────────────────────────────────────────────────────────");
            Console.WriteLine("  5.  Find Any Route (BFS)");
            Console.WriteLine("  6.  Find Shortest Route by Stops (BFS)");
            Console.WriteLine("  7.  Find Cheapest Route by Cost (Dijkstra's)");
            Console.WriteLine("  8.  Compare All Route Options");
            Console.WriteLine();
            Console.WriteLine("  ADVANCED SEARCH");
            Console.WriteLine("  ─────────────────────────────────────────────────────────");
            Console.WriteLine("  9.  Find Routes by Criteria (Max Stops & Cost)");
            Console.WriteLine();
            Console.WriteLine("  NETWORK ANALYSIS");
            Console.WriteLine("  ─────────────────────────────────────────────────────────");
            Console.WriteLine("  10. Find Hub Airports");
            Console.WriteLine("  11. Display Network Statistics");
            Console.WriteLine("  12. Find Isolated Airports");
            Console.WriteLine();
            Console.WriteLine("  0.  Exit");
            Console.WriteLine();
            Console.Write("Enter your choice: ");
        }

        #region Menu Methods

        private void DisplayAllAirports()
        {
            network.DisplayAllAirports();
        }

        private void FindDirectFlightsMenu()
        {
            Console.WriteLine("\n=== FIND DIRECT FLIGHTS ===");
            
            Console.Write("Enter origin airport code (e.g., SEA): ");
            string origin = Console.ReadLine()?.Trim().ToUpper() ?? "";
            
            Console.Write("Enter destination airport code (e.g., LAX): ");
            string destination = Console.ReadLine()?.Trim().ToUpper() ?? "";

            if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(destination))
            {
                Console.WriteLine("Invalid input. Please enter valid airport codes.");
                return;
            }

            List<Flight> flights = network.FindDirectFlights(origin, destination);

            if (flights.Count == 0)
            {
                Console.WriteLine($"\nNo direct flights found from {origin} to {destination}.");
            }
            else
            {
                Console.WriteLine($"\nFound {flights.Count} direct flight(s) from {origin} to {destination}:");
                Console.WriteLine();
                for (int i = 0; i < flights.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {flights[i]}");
                }
            }
        }

        private void GetDestinationsFromMenu()
        {
            Console.WriteLine("\n=== GET DESTINATIONS FROM AIRPORT ===");
            
            Console.Write("Enter airport code (e.g., SEA): ");
            string origin = Console.ReadLine()?.Trim().ToUpper() ?? "";

            if (string.IsNullOrWhiteSpace(origin))
            {
                Console.WriteLine("Invalid input. Please enter a valid airport code.");
                return;
            }

            List<string> destinations = network.GetDestinationsFrom(origin);

            if (destinations.Count == 0)
            {
                Console.WriteLine($"\nNo destinations found from {origin}.");
            }
            else
            {
                Console.WriteLine($"\nDestinations reachable from {origin} ({destinations.Count} total):");
                Console.WriteLine(string.Join(", ", destinations));
            }
        }

        private void FindCheapestDirectFlightMenu()
        {
            Console.WriteLine("\n=== FIND CHEAPEST DIRECT FLIGHT ===");
            
            Console.Write("Enter origin airport code (e.g., SEA): ");
            string origin = Console.ReadLine()?.Trim().ToUpper() ?? "";
            
            Console.Write("Enter destination airport code (e.g., LAX): ");
            string destination = Console.ReadLine()?.Trim().ToUpper() ?? "";

            if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(destination))
            {
                Console.WriteLine("Invalid input. Please enter valid airport codes.");
                return;
            }

            Flight? cheapest = network.FindCheapestDirectFlight(origin, destination);

            if (cheapest == null)
            {
                Console.WriteLine($"\nNo direct flights found from {origin} to {destination}.");
            }
            else
            {
                Console.WriteLine($"\nCheapest direct flight from {origin} to {destination}:");
                Console.WriteLine(cheapest.ToDetailedString());
            }
        }

        private void FindRouteMenu()
        {
            Console.WriteLine("\n=== FIND ANY ROUTE (BFS) ===");
            
            Console.Write("Enter origin airport code (e.g., SEA): ");
            string origin = Console.ReadLine()?.Trim().ToUpper() ?? "";
            
            Console.Write("Enter destination airport code (e.g., JFK): ");
            string destination = Console.ReadLine()?.Trim().ToUpper() ?? "";

            if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(destination))
            {
                Console.WriteLine("Invalid input. Please enter valid airport codes.");
                return;
            }

            List<string>? route = network.FindRoute(origin, destination);

            if (route == null)
            {
                Console.WriteLine($"\nNo route found from {origin} to {destination}.");
            }
            else
            {
                network.DisplayRoute(route);
            }
        }

        private void FindShortestRouteMenu()
        {
            Console.WriteLine("\n=== FIND SHORTEST ROUTE BY STOPS (BFS) ===");
            
            Console.Write("Enter origin airport code (e.g., SEA): ");
            string origin = Console.ReadLine()?.Trim().ToUpper() ?? "";
            
            Console.Write("Enter destination airport code (e.g., JFK): ");
            string destination = Console.ReadLine()?.Trim().ToUpper() ?? "";

            if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(destination))
            {
                Console.WriteLine("Invalid input. Please enter valid airport codes.");
                return;
            }

            List<string>? route = network.FindShortestRoute(origin, destination);

            if (route == null)
            {
                Console.WriteLine($"\nNo route found from {origin} to {destination}.");
            }
            else
            {
                Console.WriteLine("\nShortest route by number of stops:");
                network.DisplayRoute(route);
            }
        }

        private void FindCheapestRouteMenu()
        {
            Console.WriteLine("\n=== FIND CHEAPEST ROUTE BY COST (DIJKSTRA'S) ===");
            
            Console.Write("Enter origin airport code (e.g., SEA): ");
            string origin = Console.ReadLine()?.Trim().ToUpper() ?? "";
            
            Console.Write("Enter destination airport code (e.g., JFK): ");
            string destination = Console.ReadLine()?.Trim().ToUpper() ?? "";

            if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(destination))
            {
                Console.WriteLine("Invalid input. Please enter valid airport codes.");
                return;
            }

            List<string>? route = network.FindCheapestRoute(origin, destination);

            if (route == null)
            {
                Console.WriteLine($"\nNo route found from {origin} to {destination}.");
            }
            else
            {
                Console.WriteLine("\nCheapest route by total cost:");
                network.DisplayRoute(route);
            }
        }

        private void CompareRoutesMenu()
        {
            Console.WriteLine("\n=== COMPARE ALL ROUTE OPTIONS ===");
            
            Console.Write("Enter origin airport code (e.g., SEA): ");
            string origin = Console.ReadLine()?.Trim().ToUpper() ?? "";
            
            Console.Write("Enter destination airport code (e.g., JFK): ");
            string destination = Console.ReadLine()?.Trim().ToUpper() ?? "";

            if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(destination))
            {
                Console.WriteLine("Invalid input. Please enter valid airport codes.");
                return;
            }

            Console.WriteLine($"\n╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  ROUTE COMPARISON: {origin} → {destination}");
            Console.WriteLine($"╚════════════════════════════════════════════════════════════╝");

            // Find shortest route by stops
            List<string>? shortestRoute = network.FindShortestRoute(origin, destination);
            
            // Find cheapest route by cost
            List<string>? cheapestRoute = network.FindCheapestRoute(origin, destination);

            if (shortestRoute == null && cheapestRoute == null)
            {
                Console.WriteLine($"\nNo routes found from {origin} to {destination}.");
                return;
            }

            if (shortestRoute != null)
            {
                Console.WriteLine("\n--- OPTION 1: FEWEST STOPS (BFS) ---");
                network.DisplayRoute(shortestRoute);
            }

            if (cheapestRoute != null)
            {
                Console.WriteLine("\n--- OPTION 2: LOWEST COST (DIJKSTRA'S) ---");
                network.DisplayRoute(cheapestRoute);
            }

            // Compare if both routes exist
            if (shortestRoute != null && cheapestRoute != null)
            {
                Console.WriteLine("\n--- COMPARISON ---");
                decimal shortestCost = network.GetRouteCost(shortestRoute);
                decimal cheapestCost = network.GetRouteCost(cheapestRoute);
                
                if (shortestRoute.Count == cheapestRoute.Count && shortestCost == cheapestCost)
                {
                    Console.WriteLine("✓ Both routes are identical - optimal in both stops and cost!");
                }
                else
                {
                    Console.WriteLine($"Stop difference: {Math.Abs(shortestRoute.Count - cheapestRoute.Count)} stop(s)");
                    Console.WriteLine($"Cost difference: ${Math.Abs(shortestCost - cheapestCost):F2}");
                    
                    if (shortestRoute.Count < cheapestRoute.Count)
                    {
                        Console.WriteLine($"→ Shortest route saves {cheapestRoute.Count - shortestRoute.Count} stop(s) but costs ${shortestCost - cheapestCost:F2} more");
                    }
                    else if (cheapestRoute.Count < shortestRoute.Count)
                    {
                        Console.WriteLine($"→ Cheapest route saves ${shortestCost - cheapestCost:F2} but requires {cheapestRoute.Count - shortestRoute.Count} more stop(s)");
                    }
                    else
                    {
                        Console.WriteLine($"→ Same number of stops, cheapest route saves ${shortestCost - cheapestCost:F2}");
                    }
                }
            }
        }

        private void FindRoutesByCriteriaMenu()
        {
            Console.WriteLine("\n=== FIND ROUTES BY CRITERIA ===");
            
            Console.Write("Enter origin airport code (e.g., SEA): ");
            string origin = Console.ReadLine()?.Trim().ToUpper() ?? "";
            
            Console.Write("Enter destination airport code (e.g., JFK): ");
            string destination = Console.ReadLine()?.Trim().ToUpper() ?? "";

            if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(destination))
            {
                Console.WriteLine("Invalid input. Please enter valid airport codes.");
                return;
            }

            Console.Write("Enter maximum number of stops (e.g., 3): ");
            if (!int.TryParse(Console.ReadLine(), out int maxStops) || maxStops < 0)
            {
                Console.WriteLine("Invalid number of stops.");
                return;
            }

            Console.Write("Enter maximum total cost (e.g., 500): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal maxCost) || maxCost <= 0)
            {
                Console.WriteLine("Invalid cost amount.");
                return;
            }

            List<List<string>> routes = network.FindRoutesByCriteria(origin, destination, maxStops, maxCost);

            if (routes.Count == 0)
            {
                Console.WriteLine($"\nNo routes found from {origin} to {destination} within {maxStops} stops and ${maxCost:F2}.");
            }
            else
            {
                Console.WriteLine($"\nFound {routes.Count} route(s) within constraints:");
                for (int i = 0; i < routes.Count; i++)
                {
                    Console.WriteLine($"\n--- Route {i + 1} ---");
                    network.DisplayRoute(routes[i]);
                }
            }
        }

        private void FindHubAirportsMenu()
        {
            Console.WriteLine("\n=== FIND HUB AIRPORTS ===");
            
            Console.Write("Enter number of top hubs to display (e.g., 5): ");
            if (!int.TryParse(Console.ReadLine(), out int topN) || topN <= 0)
            {
                Console.WriteLine("Invalid number. Using default of 5.");
                topN = 5;
            }

            List<string> hubs = network.FindHubAirports(topN);

            if (hubs.Count == 0)
            {
                Console.WriteLine("\nNo airports found in the network.");
            }
            else
            {
                Console.WriteLine($"\nTop {hubs.Count} Hub Airport(s) by Connection Count:");
                Console.WriteLine();
                for (int i = 0; i < hubs.Count; i++)
                {
                    var airport = network.GetAirport(hubs[i]);
                    var destinations = network.GetDestinationsFrom(hubs[i]);
                    Console.WriteLine($"{i + 1}. {hubs[i]} - {destinations.Count} destination(s)");
                }
            }
        }

        private void DisplayNetworkStatistics()
        {
            string stats = network.CalculateNetworkStatistics();
            Console.WriteLine(stats);
        }

        private void FindIsolatedAirportsMenu()
        {
            Console.WriteLine("\n=== FIND ISOLATED AIRPORTS ===");
            
            List<string> isolated = network.FindIsolatedAirports();

            if (isolated.Count == 0)
            {
                Console.WriteLine("\nNo isolated airports found. All airports have at least one connection.");
            }
            else
            {
                Console.WriteLine($"\nFound {isolated.Count} isolated airport(s) with no connections:");
                foreach (string code in isolated)
                {
                    var airport = network.GetAirport(code);
                    if (airport != null)
                    {
                        Console.WriteLine($"  - {airport}");
                    }
                }
            }
        }

        #endregion
    }
}
