# Assignment 10: Flight Route Network Navigator - Implementation Notes

**Name:** Berry Walker Jr.

## Graph Data Structure Understanding

### How adjacency list representation works for flight networks
The flight network uses a `Dictionary<string, List<Flight>>` as an adjacency list. Each key is an airport code, and the value is a list of all outgoing flights. This structure is extremely efficient for real flight networks because:

- Airport lookup is **O(1)** via dictionary key access.
- Only airports that actually have outgoing flights are stored.
- The graph is **sparse** (16 airports, 52 flights), so adjacency lists avoid the wasted memory of a full adjacency matrix (`16x16 = 256` slots, many of them empty).
- Traversing outgoing flights is **O(k)** where k is the number of routes from that origin, which is small compared to total edges.

Overall, adjacency lists are the correct approach for modeling real transportation networks.

### Difference between BFS and Dijkstra's algorithms
**BFS** finds the shortest path in terms of number of steps. It assumes all edges have equal weight. BFS expands neighbors level-by-level, guaranteeing the shortest hop count.

**Dijkstra’s Algorithm** finds the shortest path based on **total cost**, taking into account edge weights. It uses a priority queue to always explore the *currently cheapest known* route. This guarantees the optimal cost path for graphs with non-negative weights.

Use BFS → shortest hops.  
Use Dijkstra → cheapest route.

## Challenges and Solutions

### Biggest challenge faced
The most difficult part was implementing Dijkstra’s algorithm correctly, especially:

- Managing the priority queue
- Doing relaxation correctly
- Tracking distances and parent pointers
- Ensuring nodes weren’t revisited incorrectly

It required much more careful handling than BFS.

### How you solved it
I broke the problem down into small steps:

- Reviewed pseudocode examples of Dijkstra from Microsoft Docs and GeeksForGeeks.
- Debugged step-by-step with small graphs.
- Drew sample paths on paper to see how parent pointers should update.
- Used breakpoints to inspect `dist[current]`, queue states, and parent map changes.

Once I validated the relaxation pattern, everything clicked.

### Most confusing concept
Understanding **parent map updates during relaxation**.  
You only update the parent when a *cheaper* path is found, not every time you discover a node. This is critical for correct path reconstruction and was initially confusing.

## Algorithm Implementation Details

### BFS Implementation (FindRoute and FindShortestRoute)
The BFS solution used:

- A `Queue<string>` to explore airports in breadth-first order.
- A `HashSet<string>` to track visited airports and avoid revisits.
- A `Dictionary<string, string>` to track each node’s parent.
- Early exit when the destination is found.
- Reconstructing the final path by backtracking through the parent map.

Because BFS explores in rings, it guarantees the **fewest hops** path.

### Dijkstra's Implementation (FindCheapestRoute)
The Dijkstra implementation used:

- A `PriorityQueue<string, decimal>` to always dequeue the lowest-cost airport.
- A `Dictionary<string, decimal>` to store current shortest-cost estimates.
- A `Dictionary<string, string>` for parent pointers.
- A `HashSet<string>` for visited nodes.
- The relaxation rule:
  - `if newCost < dist[next]: update dist[next], parent[next] = current, enqueue(next)`

Once the destination is dequeued, the path is reconstructed from the parent map.

### Path Reconstruction Logic
The reconstruction pattern was:

- Start at the destination.
- Follow `parent[node]` backward until reaching the origin.
- Store each node in a list.
- Reverse the list to get origin → destination order.

This works for BFS, Dijkstra, and DFS-based algorithms.

## Code Quality

### What you're most proud of in your implementation
I stayed consistent with:

- Uppercasing all airport codes
- Clean adjacency list structures
- Correct BFS and Dijkstra implementations
- Good error handling for CSV parsing and invalid input
- Efficient use of dictionaries and sets for O(1) lookups

The result feels robust and production-like.

### What you would improve if you had more time
If time allowed, I would:

- Add A* search for even faster weighted routing.
- Add caching for repeated queries (memoization).
- Build a flight network visualization tool.
- Improve the menu system for a smoother UX.

## Real-World Applications

### How this relates to actual routing systems
This assignment directly mirrors real systems:

- **Google Flights / Kayak / Expedia** → Dijkstra for cost optimization.
- **Google Maps** → Modified Dijkstra / A*.
- **Social networks** → BFS for shortest connection paths.
- **Airline routing networks** → adjacency lists + pathfinding.
- **Internet routing protocols (OSPF, IS-IS)** → Dijkstra’s algorithm.

What we implemented is a simplified version of real transportation and networking systems.

### What you learned about graph algorithms
Key takeaways:

- BFS and Dijkstra solve fundamentally different optimization problems.
- Graphs model relationships more naturally than lists or trees.
- Adjacency lists scale well and are memory efficient.
- Parent maps are essential for reconstructing paths after traversal.

This assignment unlocked a deeper understanding of graph traversal.

## Testing and Verification

### Test cases you created
Test scenarios included:

- Direct flight queries: SEA→LAX, SEA→SFO
- BFS shortest path: SEA→MIA
- Dijkstra cheapest path: SEA→MIA
- Origin = Destination: SEA→SEA
- Invalid airports: XXX→SEA
- Disconnected paths (via manual test flights)
- Hub detection (Top 5 airports)
- Isolated airports after removing flights

### Interesting findings from testing
- Cheapest path was not always the fewest stops.
- BFS tended to pass through large hubs.
- Dijkstra sometimes avoided hubs if the flights were overpriced.
- ORD and ATL consistently showed up as hubs.
- Some multi-stop routes were cheaper than direct routes.

## Optional Challenge
**Not implemented — focused on core requirements.**

## Time Spent

**Total time:** 12 hours

**Breakdown:**
- Understanding graph concepts: 2 hour  
- Basic search operations: 2 hour  
- BFS pathfinding: 2 hour  
- Dijkstra's algorithm: 2 hours  
- Network analysis methods: 2 hour  
- Testing: 1 hour  
- Debugging: 45 minutes  
- Writing notes: 15 minutes  

**Most time-consuming part:**  
Dijkstra’s priority queue and relaxation logic.

## Key Takeaways

### Most important lesson learned
Shortest path and cheapest path are completely different problems.  
The algorithm must match the goal.

### How this changed your understanding of data structures
Graphs revealed how real systems (flights, maps, networks) truly operate.  
Compared to arrays/lists/trees, graphs offer much more expressive modeling and require algorithmic thinking to navigate.
