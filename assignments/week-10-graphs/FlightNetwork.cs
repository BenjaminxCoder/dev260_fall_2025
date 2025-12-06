using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment10
{
    /// <summary>
    /// Represents a flight route network using a graph data structure.
    /// Uses adjacency list representation for efficient storage and traversal.
    /// </summary>
    public class FlightNetwork
    {
        // Graph vertices: Dictionary of airport codes to Airport objects
        private Dictionary<string, Airport> airports;

        // Graph edges: Adjacency list mapping origin airport codes to lists of outgoing flights
        private Dictionary<string, List<Flight>> routes;

        // Airport code to city name mapping
        private static readonly Dictionary<string, string> airportCities = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "SEA", "Seattle" },
            { "PDX", "Portland" },
            { "SFO", "San Francisco" },
            { "LAX", "Los Angeles" },
            { "LAS", "Las Vegas" },
            { "PHX", "Phoenix" },
            { "DEN", "Denver" },
            { "DFW", "Dallas" },
            { "IAH", "Houston" },
            { "ORD", "Chicago" },
            { "MSP", "Minneapolis" },
            { "DTW", "Detroit" },
            { "ATL", "Atlanta" },
            { "MIA", "Miami" },
            { "JFK", "New York" },
            { "BOS", "Boston" }
        };

        /// <summary>
        /// Initializes a new empty flight network graph
        /// </summary>
        public FlightNetwork()
        {
            airports = new Dictionary<string, Airport>(StringComparer.OrdinalIgnoreCase);
            routes = new Dictionary<string, List<Flight>>(StringComparer.OrdinalIgnoreCase);
        }

        #region Graph Construction Methods (Implement During Lab)

        /// <summary>
        /// TODO LAB #1: Add Airport (Vertex) to Graph
        /// 
        /// Add an airport as a vertex in the graph data structure.
        /// Requirements:
        /// - Validate the airport parameter (check for null)
        /// - Validate the airport code (check for null or whitespace)
        /// - Convert airport code to uppercase for consistency
        /// - Add airport to the airports dictionary (avoid duplicates)
        /// - Initialize empty adjacency list in routes dictionary for this airport
        /// 
        /// Key Concepts:
        /// - Vertices in a graph represent entities (airports)
        /// - Dictionary provides O(1) lookup by airport code
        /// - Each vertex needs an adjacency list initialized (even if empty)
        /// - Case-insensitive comparison using ToUpperInvariant()
        /// </summary>
        /// <param name="airport">Airport object to add</param>
        public void AddAirport(Airport airport)
        {
            // Validate input
            if (airport == null || string.IsNullOrWhiteSpace(airport.Code))
            {
                Console.WriteLine("AddAirport: invalid airport or code.");
                return;
            }

            // Normalize code for case-insensitive consistency
            var code = airport.Code.ToUpperInvariant();

            // Normalize the airport object's Code as well
            airport.Code = code;

            // Insert airport vertex if not already present
            if (!airports.ContainsKey(code))
            {
                airports[code] = airport;
            }

            // Ensure an adjacency list exists for this vertex
            if (!routes.ContainsKey(code))
            {
                routes[code] = new List<Flight>();
            }
        }

        /// <summary>
        /// TODO LAB #2: Add Flight (Directed Edge) to Graph
        /// 
        /// Add a flight as a directed edge in the graph.
        /// Requirements:
        /// - Validate the flight parameter and its origin/destination
        /// - Convert airport codes to uppercase
        /// - Ensure both origin and destination airports exist (create if needed)
        /// - Add the flight to the origin airport's adjacency list
        /// 
        /// Key Concepts:
        /// - Edges in a graph represent relationships (flights between airports)
        /// - Directed edge: flight goes FROM origin TO destination (one-way)
        /// - Adjacency list: routes[origin] contains all flights FROM that airport
        /// - Auto-create airports if they don't exist (using airportCities mapping)
        /// </summary>
        /// <param name="flight">Flight object to add</param>
        public void AddFlight(Flight flight)
        {
            // Validate flight and required fields
            if (flight == null ||
                string.IsNullOrWhiteSpace(flight.Origin) ||
                string.IsNullOrWhiteSpace(flight.Destination))
            {
                Console.WriteLine("AddFlight: invalid flight or codes.");
                return;
            }

            // Normalize codes
            var origin = flight.Origin.ToUpperInvariant();
            var dest   = flight.Destination.ToUpperInvariant();

            // Ensure airports exist
            if (!airports.ContainsKey(origin))
            {
                var city = airportCities.TryGetValue(origin, out var oc) ? oc : origin;
                AddAirport(new Airport { Code = origin, City = city });
            }
            if (!airports.ContainsKey(dest))
            {
                var city = airportCities.TryGetValue(dest, out var dc) ? dc : dest;
                AddAirport(new Airport { Code = dest, City = city });
            }

            // Ensure adjacency list for origin
            if (!routes.ContainsKey(origin))
                routes[origin] = new List<Flight>();

            // Store normalized codes back onto the flight
            flight.Origin = origin;
            flight.Destination = dest;

            // Add directed edge
            routes[origin].Add(flight);
        }

        /// <summary>
        /// TODO LAB #3: Load Flight Data from CSV File
        /// 
        /// Parse a CSV file and populate the graph with flights.
        /// CSV Format: Origin,Destination,Airline,Duration,Cost
        /// Requirements:
        /// - Check if file exists (throw FileNotFoundException if not)
        /// - Read all lines from the file
        /// - Skip the header row (first line)
        /// - Parse each data row and extract flight information
        /// - Create Flight objects and add them to the graph
        /// - Handle parsing errors gracefully
        /// - Display summary of loaded flights
        /// 
        /// Key Concepts:
        /// - File I/O with File.ReadAllLines()
        /// - CSV parsing with string.Split(',')
        /// - Error handling with try-catch
        /// - Graph construction from external data
        /// </summary>
        /// <param name="filename">Path to the CSV file</param>
        public void LoadFlightsFromCSV(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename) || !File.Exists(filename))
                throw new FileNotFoundException($"CSV file not found: {filename}");

            var lines = File.ReadAllLines(filename);
            if (lines == null || lines.Length == 0)
            {
                Console.WriteLine("LoadFlightsFromCSV: file is empty.");
                return;
            }

            int loaded = 0;

            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i]?.Trim();
                if (string.IsNullOrWhiteSpace(line)) continue;

                try
                {
                    var parts = line.Split(',');
                    if (parts.Length < 5) continue;

                    var origin      = parts[0].Trim();
                    var destination = parts[1].Trim();
                    var airline     = parts[2].Trim();

                    if (!int.TryParse(parts[3].Trim(), out var duration))
                        continue;

                    if (!decimal.TryParse(parts[4].Trim(), out var cost))
                        continue;

                    var flight = new Flight
                    {
                        Origin = origin,
                        Destination = destination,
                        Airline = airline,
                        Duration = duration,
                        Cost = cost
                    };

                    AddFlight(flight);
                    loaded++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"LoadFlightsFromCSV: skipping line {i + 1}. Error: {ex.Message}");
                }
            }

            Console.WriteLine($"Loaded {loaded} flights from {filename}.");
        }

        /// <summary>
        /// TODO LAB #4: Display All Airports in Network
        /// 
        /// Display a formatted list of all airports with connection counts.
        /// Requirements:
        /// - Check if there are any airports in the network
        /// - Display a header with total count
        /// - List all airports sorted alphabetically by code
        /// - Show airport code, city, and number of outgoing flights
        /// - Format output for readability
        /// 
        /// Key Concepts:
        /// - Graph traversal (iterating over vertices)
        /// - LINQ OrderBy() for sorting
        /// - String formatting with alignment (-5, -20 for left-align)
        /// - Counting edges (degree) for each vertex
        /// </summary>
        public void DisplayAllAirports()
        {
            if (airports.Count == 0)
            {
                Console.WriteLine("No airports in the network.");
                return;
            }

            Console.WriteLine($"\nAirports in network: {airports.Count}");
            Console.WriteLine($"{"Code",-6}{"City",-20}{"Outgoing",-10}");

            foreach (var a in airports.Values.OrderBy(a => a.Code))
            {
                var code = a.Code.ToUpperInvariant();
                int outDeg = routes.TryGetValue(code, out var list) ? list.Count : 0;
                Console.WriteLine($"{code,-6}{(a.City ?? ""),-20}{outDeg,-10}");
            }
        }

        /// <summary>
        /// TODO LAB #5: Get Airport by Code
        /// 
        /// Retrieve an airport from the graph by its code.
        /// Requirements:
        /// - Validate the code parameter
        /// - Convert code to uppercase for case-insensitive lookup
        /// - Return the Airport object if found
        /// - Return null if code is invalid or airport not found
        /// 
        /// Key Concepts:
        /// - Dictionary lookup provides O(1) retrieval
        /// - Null safety and validation
        /// - Case-insensitive search using ToUpperInvariant()
        /// - Ternary operator for concise conditional return
        /// </summary>
        /// <param name="code">Airport code</param>
        /// <returns>Airport object or null if not found</returns>
        public Airport? GetAirport(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return null;
            var upper = code.ToUpperInvariant();
            return airports.ContainsKey(upper) ? airports[upper] : null;
        }

        #endregion

        #region Basic Search Operations (Student Implementation)

        /// <summary>
        /// TODO #1: Find Direct Flights Between Airports
        /// 
        /// Find all direct flight options between two airports.
        /// Requirements:
        /// - Validate that origin and destination are not null or empty
        /// - Convert airport codes to uppercase for consistent comparison
        /// - Check if the origin airport exists in the routes dictionary
        /// - Filter the flights from origin to find those going to destination
        /// - Return a list of matching Flight objects (empty list if none exist)
        /// 
        /// Key Concepts:
        /// - Adjacency list lookup - routes[origin] gives all outgoing flights
        /// - LINQ Where() for filtering based on destination
        /// - Case-insensitive string comparison
        /// </summary>
        /// <param name="origin">Departure airport code</param>
        /// <param name="destination">Arrival airport code</param>
        /// <returns>List of direct flights, or empty list if none exist</returns>
        public List<Flight> FindDirectFlights(string origin, string destination)
        {
            if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(destination))
                return new List<Flight>();

            var o = origin.ToUpperInvariant();
            var d = destination.ToUpperInvariant();

            if (!routes.ContainsKey(o)) return new List<Flight>();

            return routes[o]
                .Where(f => string.Equals(f.Destination, d, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        /// <summary>
        /// TODO #2: Get All Direct Destinations from Airport
        /// 
        /// Get a sorted list of all airports reachable via direct flights from the origin.
        /// Requirements:
        /// - Validate the origin airport code
        /// - Get all flights from the origin airport
        /// - Extract unique destination airport codes
        /// - Sort the destinations alphabetically
        /// - Return the sorted list
        /// 
        /// Key Concepts:
        /// - Adjacency list traversal - examining all edges from a vertex
        /// - LINQ Select() to extract destination codes from Flight objects
        /// - Distinct() to eliminate duplicate destinations
        /// - OrderBy() for alphabetical sorting
        /// </summary>
        /// <param name="origin">Departure airport code</param>
        /// <returns>Sorted list of reachable airport codes</returns>
        public List<string> GetDestinationsFrom(string origin)
        {
            if (string.IsNullOrWhiteSpace(origin)) return new List<string>();
            var o = origin.ToUpperInvariant();

            if (!routes.ContainsKey(o)) return new List<string>();

            return routes[o]
                .Select(f => f.Destination.ToUpperInvariant())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(x => x)
                .ToList();
        }

        /// <summary>
        /// TODO #3: Find Cheapest Direct Flight
        /// 
        /// Find the lowest-cost direct flight between two airports.
        /// Requirements:
        /// - Use FindDirectFlights() to get all direct flight options
        /// - Return null if no direct flights exist
        /// - Find and return the flight with the minimum cost
        /// 
        /// Key Concepts:
        /// - Code reuse - leverage existing methods
        /// - LINQ OrderBy() for sorting by cost
        /// - First() to get the minimum element
        /// </summary>
        /// <param name="origin">Departure airport code</param>
        /// <param name="destination">Arrival airport code</param>
        /// <returns>Cheapest flight, or null if no direct flight exists</returns>
        public Flight? FindCheapestDirectFlight(string origin, string destination)
        {
            var options = FindDirectFlights(origin, destination);
            if (options == null || options.Count == 0) return null;

#if NET6_0_OR_GREATER
            return options.OrderBy(f => f.Cost).First();
#else
            return options.OrderBy(f => f.Cost).FirstOrDefault();
#endif
        }

        #endregion

        #region BFS Pathfinding (Student Implementation)

        /// <summary>
        /// TODO #4: Find Any Valid Route Using BFS
        /// 
        /// Use breadth-first search to find any valid route between two airports.
        /// Requirements:
        /// - Validate inputs and check that both airports exist in the graph
        /// - Handle special case where origin equals destination
        /// - Implement BFS using a Queue for exploration
        /// - Track visited airports with a HashSet to avoid cycles
        /// - Track parent relationships to reconstruct the path
        /// - Return the path from origin to destination, or null if no route exists
        /// 
        /// Key Concepts:
        /// - BFS explores level-by-level (closest airports first)
        /// - Queue ensures FIFO processing (breadth-first order)
        /// - Parent tracking enables path reconstruction
        /// - HashSet prevents revisiting airports (cycle detection)
        /// 
        /// Algorithm Steps:
        /// 1. Initialize: Queue with origin, visited set, parent dictionary
        /// 2. Loop: Dequeue current airport
        /// 3. Check: If current == destination, reconstruct and return path
        /// 4. Explore: For each outgoing flight from current
        /// 5. Visit: If neighbor unvisited, mark visited, record parent, enqueue
        /// 6. Repeat until queue empty or destination found
        /// </summary>
        /// <param name="origin">Starting airport code</param>
        /// <param name="destination">Ending airport code</param>
        /// <returns>List of airport codes in route order, or null if no route exists</returns>
        public List<string>? FindRoute(string origin, string destination)
        {
            if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(destination))
                return null;

            var start = origin.ToUpperInvariant();
            var end   = destination.ToUpperInvariant();

            if (!airports.ContainsKey(start) || !airports.ContainsKey(end))
                return null;

            if (start == end) return new List<string> { start };

            var q = new Queue<string>();
            var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var parent = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            q.Enqueue(start);
            visited.Add(start);

            while (q.Count > 0)
            {
                var current = q.Dequeue();
                if (!routes.ContainsKey(current)) continue;

                if (current == end)
                    return ReconstructPath(parent, start, end);

                foreach (var flight in routes[current])
                {
                    var next = flight.Destination.ToUpperInvariant();
                    if (visited.Contains(next)) continue;

                    visited.Add(next);
                    parent[next] = current;
                    q.Enqueue(next);
                }
            }

            return null;
        }

        /// <summary>
        /// TODO #5: Find Shortest Route by Number of Stops
        /// 
        /// Find the route with the fewest number of stops (airports) using BFS.
        /// Requirements:
        /// - BFS naturally finds shortest path in unweighted graphs
        /// - Each edge (flight) has equal weight (one hop)
        /// - Can reuse FindRoute() since BFS guarantees shortest hop-count
        /// 
        /// Key Concepts:
        /// - BFS guarantees shortest path in unweighted graphs
        /// - Level-by-level exploration finds minimum hops automatically
        /// - This is different from FindCheapestRoute which considers edge weights (cost)
        /// </summary>
        /// <param name="origin">Starting airport code</param>
        /// <param name="destination">Ending airport code</param>
        /// <returns>List of airport codes representing shortest route, or null if no route exists</returns>
        public List<string>? FindShortestRoute(string origin, string destination)
        {
            // BFS already yields fewest hops
            return FindRoute(origin, destination);
        }

        #endregion

        #region Dijkstra's Algorithm (Student Implementation)

        /// <summary>
        /// TODO #6: Find Cheapest Route by Total Cost Using Dijkstra's Algorithm
        /// 
        /// Use Dijkstra's algorithm to find the route with the lowest total cost.
        /// Requirements:
        /// - Validate inputs and handle special cases
        /// - Use PriorityQueue to always explore lowest-cost path first
        /// - Track shortest known distance to each airport
        /// - Update distances when shorter path is found (relaxation)
        /// - Track parent relationships for path reconstruction
        /// - Return cheapest path or null if no route exists
        /// 
        /// Key Concepts:
        /// - Dijkstra's finds optimal path in weighted graphs (considers edge costs)
        /// - PriorityQueue ensures we explore minimum-cost paths first
        /// - Distance tracking prevents exploring worse paths
        /// - Relaxation: if (newCost < knownCost) update distance and parent
        /// - Different from BFS which only counts hops (unweighted)
        /// 
        /// Algorithm Steps:
        /// 1. Initialize: All distances to infinity, origin to 0
        /// 2. Create: PriorityQueue, parent dictionary, visited set
        /// 3. Enqueue: origin with cost 0
        /// 4. Loop: While queue not empty
        /// 5. Dequeue: Airport with minimum total cost
        /// 6. Skip: If already visited (duplicate in queue)
        /// 7. Check: If current == destination, reconstruct path
        /// 8. Explore: For each outgoing flight
        /// 9. Calculate: newCost = current distance + flight cost
        /// 10. Relax: If newCost < neighbor distance, update and enqueue
        /// </summary>
        /// <param name="origin">Starting airport code</param>
        /// <param name="destination">Ending airport code</param>
        /// <returns>List of airport codes representing cheapest route, or null if no route exists</returns>
        public List<string>? FindCheapestRoute(string origin, string destination)
        {
            if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(destination))
                return null;

            var start = origin.ToUpperInvariant();
            var end   = destination.ToUpperInvariant();

            if (!airports.ContainsKey(start) || !airports.ContainsKey(end))
                return null;

            if (start == end) return new List<string> { start };

            var dist = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
            var parent = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // Initialize distances
            foreach (var code in airports.Keys)
                dist[code.ToUpperInvariant()] = decimal.MaxValue;
            dist[start] = 0m;

            var pq = new PriorityQueue<string, decimal>();
            pq.Enqueue(start, 0m);

            while (pq.Count > 0)
            {
                var current = pq.Dequeue();
                if (visited.Contains(current)) continue;
                visited.Add(current);

                if (current == end)
                    return ReconstructPath(parent, start, end);

                if (!routes.ContainsKey(current)) continue;

                foreach (var flight in routes[current])
                {
                    var next = flight.Destination.ToUpperInvariant();
                    var newCost = dist[current] + flight.Cost;

                    if (newCost < dist.GetValueOrDefault(next, decimal.MaxValue))
                    {
                        dist[next] = newCost;
                        parent[next] = current;
                        pq.Enqueue(next, newCost);
                    }
                }
            }

            return null;
        }

        #endregion

        #region Multi-Criteria Search (Student Implementation)

        /// <summary>
        /// TODO #7: Find All Routes Meeting Constraints (EXTRA CREDIT - Advanced)
        /// 
        /// Find all routes that satisfy both maximum stops and maximum cost constraints.
        /// Requirements:
        /// - Validate inputs (null checks, airport existence)
        /// - Use DFS with backtracking to explore all possible paths
        /// - Prune paths that exceed maxStops or maxCost (optimization)
        /// - Track visited airports to prevent cycles
        /// - Collect all valid routes that reach destination within constraints
        /// - Return list of route lists (each route is list of airport codes)
        /// 
        /// Key Concepts:
        /// - DFS explores deeply before backtracking (vs BFS which explores level-by-level)
        /// - Backtracking: undo choices to explore alternative paths
        /// - Pruning: stop exploring paths that can't possibly succeed
        /// - This finds ALL solutions, not just one optimal solution
        /// 
        /// Algorithm Strategy:
        /// 1. Create result list and validate inputs
        /// 2. Initialize starting path with origin, mark as visited
        /// 3. Call recursive helper method DFSWithConstraints
        /// 4. Helper method:
        ///    - Base case: if at destination, save path copy
        ///    - Prune: if stops >= maxStops, return
        ///    - Explore: for each outgoing flight
        ///    - Calculate: newCost = currentCost + flight.Cost
        ///    - Prune: if newCost > maxCost or neighbor visited, skip
        ///    - Recurse: add neighbor to path, mark visited, call helper
        ///    - Backtrack: remove neighbor from path and visited set
        /// </summary>
        /// <param name="origin">Starting airport code</param>
        /// <param name="destination">Ending airport code</param>
        /// <param name="maxStops">Maximum number of stops allowed</param>
        /// <param name="maxCost">Maximum total cost allowed</param>
        /// <returns>List of valid routes, each route is a list of airport codes</returns>
        public List<List<string>> FindRoutesByCriteria(string origin, string destination, int maxStops, decimal maxCost)
        {
            var results = new List<List<string>>();

            if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(destination))
                return results;

            var start = origin.ToUpperInvariant();
            var end   = destination.ToUpperInvariant();

            if (!airports.ContainsKey(start) || !airports.ContainsKey(end))
                return results;

            if (maxStops < 0 || maxCost < 0) return results;

            var path = new List<string> { start };
            var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { start };

            DFSWithConstraints(start, end, maxStops, maxCost, 0m, path, visited, results);
            return results;
        }

        /// <summary>
        /// TODO #7 (continued): Helper Method for DFS with Backtracking
        /// 
        /// Recursive helper that explores all paths within constraints.
        /// This is a private method that implements the core DFS logic.
        /// </summary>
        private void DFSWithConstraints(string current, string destination, int maxStops, decimal maxCost,
            decimal currentCost, List<string> currentPath, HashSet<string> visited, List<List<string>> validRoutes)
        {
            // Base case: reached destination within constraints
            if (current.Equals(destination, StringComparison.OrdinalIgnoreCase))
            {
                validRoutes.Add(new List<string>(currentPath));
                return;
            }

            // Prune by max stops 
            if (currentPath.Count - 1 >= maxStops)
                return;

            if (!routes.ContainsKey(current)) return;

            foreach (var flight in routes[current])
            {
                var next = flight.Destination.ToUpperInvariant();
                var newCost = currentCost + flight.Cost;

                // Prune by cost or cycles
                if (newCost > maxCost || visited.Contains(next))
                    continue;

                // Choose
                visited.Add(next);
                currentPath.Add(next);

                // Explore
                DFSWithConstraints(next, destination, maxStops, maxCost, newCost, currentPath, visited, validRoutes);

                // Backtrack
                currentPath.RemoveAt(currentPath.Count - 1);
                visited.Remove(next);
            }
        }

        #endregion

        #region Network Analysis (Student Implementation)

        /// <summary>
        /// TODO #8: Find Hub Airports (Most Connected)
        /// 
        /// Find the airports with the most outgoing flight connections.
        /// Requirements:
        /// - Calculate the degree (number of outgoing flights) for each airport
        /// - Sort airports by degree in descending order
        /// - Return the top N airport codes
        /// - Handle edge case where topN <= 0
        /// 
        /// Key Concepts:
        /// - Vertex degree: number of edges (flights) from a vertex (airport)
        /// - Hub identification: high-degree vertices are central to network
        /// - LINQ for sorting and limiting results
        /// - In directed graphs, this measures out-degree specifically
        /// </summary>
        /// <param name="topN">Number of top airports to return</param>
        /// <returns>List of airport codes sorted by connection count (descending)</returns>
        public List<string> FindHubAirports(int topN)
        {
            if (topN <= 0) return new List<string>();

            return routes
                .OrderByDescending(kvp => kvp.Value?.Count ?? 0)
                .Take(topN)
                .Select(kvp => kvp.Key.ToUpperInvariant())
                .ToList();
        }

        /// <summary>
        /// TODO #9: Calculate Comprehensive Network Statistics
        /// 
        /// Calculate and format detailed statistics about the flight network.
        /// Requirements:
        /// - Count total airports and flights
        /// - Calculate average connections per airport
        /// - Find most and least connected airports
        /// - Calculate average flight cost and duration
        /// - Format results in a readable multi-line string
        /// - Handle empty network gracefully
        /// 
        /// Key Concepts:
        /// - Aggregate operations across graph structure
        /// - LINQ for calculations (Sum, Average, Min, Max)
        /// - StringBuilder for efficient string concatenation
        /// - Graph metrics provide insights into network structure
        /// </summary>
        /// <returns>Formatted string with network metrics</returns>
        public string CalculateNetworkStatistics()
        {
            if (airports.Count == 0)
                return "No airports in the network.";

            int totalAirports = airports.Count;
            int totalFlights = routes.Values.Sum(list => list.Count);

            double avgConnections = routes.Count == 0 ? 0.0 : (double)totalFlights / routes.Count;

            int maxConnections = routes.Count == 0 ? 0 : routes.Max(kvp => kvp.Value.Count);
            int minConnections = routes.Count == 0 ? 0 : routes.Min(kvp => kvp.Value.Count);

            var mostConnected = routes.Where(kvp => kvp.Value.Count == maxConnections).Select(kvp => kvp.Key).OrderBy(x => x).ToList();
            var leastConnected = routes.Where(kvp => kvp.Value.Count == minConnections).Select(kvp => kvp.Key).OrderBy(x => x).ToList();

            var allFlights = routes.Values.SelectMany(x => x).ToList();

            decimal avgCost = allFlights.Count > 0 ? allFlights.Average(f => f.Cost) : 0m;
            double avgDurationMin = allFlights.Count > 0 ? allFlights.Average(f => f.Duration) : 0.0;
            double avgDurationHours = avgDurationMin / 60.0;

            var sb = new System.Text.StringBuilder();
            sb.AppendLine("=== Flight Network Statistics ===");
            sb.AppendLine($"Total Airports: {totalAirports}");
            sb.AppendLine($"Total Flights: {totalFlights}");
            sb.AppendLine($"Average Outgoing Connections per Airport: {avgConnections:F2}");
            sb.AppendLine($"Max Outgoing Connections: {maxConnections} [{string.Join(", ", mostConnected)}]");
            sb.AppendLine($"Min Outgoing Connections: {minConnections} [{string.Join(", ", leastConnected)}]");
            sb.AppendLine($"Average Flight Cost: ${avgCost:F2}");
            sb.AppendLine($"Average Flight Duration: {avgDurationMin:F2} minutes ({avgDurationHours:F2} hours)");

            return sb.ToString();
        }

        /// <summary>
        /// TODO #10: Find Isolated Airports
        /// 
        /// Find airports that have no incoming or outgoing flights.
        /// Requirements:
        /// - Build set of airports that have incoming flights (destinations)
        /// - Check each airport for both outgoing and incoming connections
        /// - An airport is isolated if it has NEITHER incoming NOR outgoing flights
        /// - Return sorted list of isolated airport codes
        /// 
        /// Key Concepts:
        /// - Graph connectivity analysis
        /// - In-degree vs out-degree in directed graphs
        /// - HashSet for efficient membership testing
        /// - Network health diagnostics
        /// </summary>
        /// <returns>List of isolated airport codes</returns>
        public List<string> FindIsolatedAirports()
        {
            var isolated = new List<string>();
            var hasIncoming = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // Collect all airports with incoming flights
            foreach (var list in routes.Values)
                foreach (var f in list)
                    hasIncoming.Add(f.Destination.ToUpperInvariant());

            // Evaluate each known airport
            foreach (var code in airports.Keys)
            {
                var upper = code.ToUpperInvariant();
                bool hasOutgoing = routes.ContainsKey(upper) && routes[upper].Count > 0;
                bool incoming = hasIncoming.Contains(upper);

                if (!hasOutgoing && !incoming)
                    isolated.Add(upper);
            }

            return isolated.OrderBy(x => x).ToList();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Helper method to reconstruct a path from a parent map
        /// Used by BFS and Dijkstra's algorithms
        /// </summary>
        /// <param name="parents">Dictionary mapping each airport to its parent in the path</param>
        /// <param name="start">Starting airport code</param>
        /// <param name="end">Ending airport code</param>
        /// <returns>List of airport codes from start to end</returns>
        protected List<string> ReconstructPath(Dictionary<string, string> parents, string start, string end)
        {
            List<string> path = new List<string>();
            string current = end;

            while (current != start)
            {
                path.Add(current);
                
                if (!parents.ContainsKey(current))
                {
                    // Path reconstruction failed - no route exists
                    return new List<string>();
                }
                
                current = parents[current];
            }

            path.Add(start);
            path.Reverse();
            return path;
        }

        /// <summary>
        /// Gets the total cost of a route by summing flight costs
        /// </summary>
        /// <param name="route">List of airport codes in route order</param>
        /// <returns>Total cost, or -1 if route is invalid</returns>
        public decimal GetRouteCost(List<string> route)
        {
            if (route == null || route.Count < 2)
                return -1;

            decimal totalCost = 0;

            for (int i = 0; i < route.Count - 1; i++)
            {
                string from = route[i];
                string to = route[i + 1];

                Flight? cheapestFlight = FindCheapestDirectFlight(from, to);
                
                if (cheapestFlight == null)
                    return -1; 

                totalCost += cheapestFlight.Cost;
            }

            return totalCost;
        }

        /// <summary>
        /// Displays a route with detailed flight information
        /// </summary>
        /// <param name="route">List of airport codes in route order</param>
        public void DisplayRoute(List<string> route)
        {
            if (route == null || route.Count == 0)
            {
                Console.WriteLine("No route to display.");
                return;
            }

            Console.WriteLine($"\nRoute: {string.Join(" → ", route)}");
            Console.WriteLine($"Total stops: {route.Count - 1}");

            if (route.Count < 2)
                return;

            Console.WriteLine("\nFlight Details:");
            decimal totalCost = 0;
            int totalDuration = 0;

            for (int i = 0; i < route.Count - 1; i++)
            {
                string from = route[i];
                string to = route[i + 1];

                Flight? cheapestFlight = FindCheapestDirectFlight(from, to);
                
                if (cheapestFlight != null)
                {
                    Console.WriteLine($"  {i + 1}. {cheapestFlight}");
                    totalCost += cheapestFlight.Cost;
                    totalDuration += cheapestFlight.Duration;
                }
            }

            Console.WriteLine($"\nTotal Cost: ${totalCost:F2}");
            Console.WriteLine($"Total Duration: {totalDuration} minutes ({totalDuration / 60}h {totalDuration % 60}m)");
        }

        #endregion
    }
}
