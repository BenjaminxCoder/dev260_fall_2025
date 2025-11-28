using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab9_BST
{
    /// <summary>
    /// Supporting class for Lab 9 Binary Search Tree Operations
    /// Contains menu handlers, UI logic, and demo data setup functions
    /// 
    /// INSTRUCTOR NOTE: This class demonstrates several key software engineering principles:
    /// 
    /// 1. Separation of Concerns: UI logic is completely separate from business logic
    /// 2. Static Utility Pattern: All methods are static since this is pure infrastructure
    /// 3. Delegation Pattern: This class delegates to the EmployeeManagementSystem for actual work
    /// 4. User Experience Focus: Comprehensive error handling and clear feedback
    /// 
    /// Study this code to understand how a professional console application
    /// should structure its user interface. Notice how it calls YOUR BST methods!
    /// </summary>
    public static class LabSupport
    {
        // ============================================
        // 🎪 INTERACTIVE MENU SYSTEM
        // ============================================

        /// <summary>
        /// Main application loop that manages the interactive lab experience
        /// 
        /// INSTRUCTOR NOTE: This is the heart of the user interface - a standard pattern
        /// for console applications. Notice the structure:
        /// 1. Welcome message and context setting
        /// 2. Infinite loop with menu display and input handling
        /// 3. Comprehensive try-catch for error handling and learning feedback
        /// 4. Graceful exit with session statistics
        /// 
        /// This pattern allows developers to experiment freely without crashes
        /// while providing immediate feedback on their TODO implementations.
        /// </summary>
        /// <param name="system">The EmployeeManagementSystem instance containing developer implementations</param>
        public static void RunInteractiveMenu(EmployeeManagementSystem system)
        {
            // INSTRUCTOR NOTE: Always start with a clear welcome that sets expectations
            // and provides context for users about what they will learn
            Console.WriteLine("🌳 Welcome to the Binary Search Tree Lab!");
            Console.WriteLine("This system demonstrates how BST powers efficient employee management.\n");

            // INSTRUCTOR NOTE: The main application loop - keep running until user exits
            // This allows developers to test their implementations multiple times
            while (true)
            {
                DisplayMainMenu(system);
                var choice = Console.ReadLine()?.Trim();
                Console.WriteLine();

                // INSTRUCTOR NOTE: Wrap all developer code execution in try-catch
                // This prevents crashes from NotImplementedException and provides learning feedback
                try
                {
                    // INSTRUCTOR NOTE: Support both numbers and words for accessibility
                    // Users can type "1" or "insert" - both work the same way
                    switch (choice)
                    {
                        case "1": case "insert": HandleInsertEmployee(system); break;
                        case "2": case "search": HandleSearchEmployee(system); break;
                        case "3": case "show": case "display": case "all": HandleInOrderTraversal(system); break;
                        case "4": case "min": HandleFindMinimum(system); break;
                        case "5": case "max": HandleFindMaximum(system); break;
                        case "6": case "count": HandleCountEmployees(system); break;
                        case "7": case "tree": HandleDisplayTree(system); break;
                        case "8": case "stats": HandleShowStats(system); break;
                        case "0":
                        case "exit":
                            // INSTRUCTOR NOTE: Provide meaningful session summary on exit
                            var stats = system.GetSystemStats();
                            Console.WriteLine("👋 Thanks for exploring Binary Search Trees in C#!");
                            Console.WriteLine($"📊 You performed {stats.TotalOperations} tree operations during this session!");
                            return;
                        default:
                            Console.WriteLine("❌ Invalid option. Try again!");
                            break;
                    }
                }
                catch (NotImplementedException ex)
                {
                    // INSTRUCTOR NOTE: Special handling for TODO methods - this is educational feedback!
                    // When developers haven't implemented a method yet, guide them constructively
                    Console.WriteLine($"🚧 {ex.Message}");
                    Console.WriteLine("💡 This feature will work once you implement the TODO method!\n");
                }
                catch (Exception ex)
                {
                    // INSTRUCTOR NOTE: Handle any other errors gracefully for a smooth learning experience
                    Console.WriteLine($"❌ Error: {ex.Message}\n");
                }

                // INSTRUCTOR NOTE: Pause for user feedback and clear screen for clean experience
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        /// <summary>
        /// Display the main menu with current system statistics
        /// 
        /// INSTRUCTOR NOTE: This method demonstrates good UI design principles:
        /// 1. Clear visual hierarchy with ASCII box drawing
        /// 2. Real-time status information (operation count, employee count, tree height)
        /// 3. Multiple input methods for accessibility
        /// 4. Consistent formatting that's easy to scan
        /// 
        /// The menu updates dynamically to show developers progress through the lab.
        /// </summary>
        /// <param name="system">System instance to get current statistics from</param>
        private static void DisplayMainMenu(EmployeeManagementSystem system)
        {
            // INSTRUCTOR NOTE: Get live statistics from the developers's BST implementation
            // This provides immediate feedback on their progress and system state
            var stats = system.GetSystemStats();
            
            // INSTRUCTOR NOTE: ASCII box drawing creates a professional console interface
            // that's easy to read and navigate during live coding sessions
            Console.WriteLine("┌─ Binary Search Tree Lab Menu ──────────────────────┐");
            Console.WriteLine("│ 1. Insert Employee    │ 2. Search Employee      │");
            Console.WriteLine("│ 3. Show All (Sorted)  │ 4. Find Minimum         │");
            Console.WriteLine("│ 5. Find Maximum       │ 6. Count Employees      │");
            Console.WriteLine("│ 7. Display Tree       │ 8. Show Stats           │");
            Console.WriteLine("│ 0. Exit                                            │");
            Console.WriteLine("└────────────────────────────────────────────────────┘");
            
            // INSTRUCTOR NOTE: Show dynamic status to help developers track their progress
            try
            {
                var employeeCount = stats.IsEmpty ? 0 : stats.EmployeeCount;
                Console.WriteLine($"Operations: {stats.TotalOperations} | Employees: {employeeCount} | Height: {stats.TreeHeight}");
            }
            catch
            {
                Console.WriteLine($"Operations: {stats.TotalOperations} | Demo Data Loaded ✅");
            }
            Console.WriteLine();
            Console.Write("Choose operation (number or name): ");
        }

        // ============================================
        // 🎭 MENU HANDLERS
        // ============================================

        /// <summary>
        /// Demonstrate employee insertion into BST
        /// 
        /// INSTRUCTOR NOTE: This method showcases BST insertion with user interaction.
        /// It calls the developer's Insert method and provides immediate visual feedback.
        /// 
        /// Key learning concepts:
        /// - BST maintains ordering property automatically
        /// - Insertion position depends on comparison with existing nodes
        /// - Real-world application (adding employees to database)
        /// </summary>
        /// <param name="system">System instance containing developer's Insert implementation</param>
        public static void HandleInsertEmployee(EmployeeManagementSystem system)
        {
            Console.WriteLine("➕ Insert Employee Demo");
            Console.WriteLine("======================");

            // INSTRUCTOR NOTE: Get employee details from user input
            Console.Write("Enter Employee ID (number): ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("❌ Invalid ID. Please enter a number.");
                return;
            }

            Console.Write("Enter Employee Name: ");
            string name = Console.ReadLine() ?? "Unknown";

            Console.Write("Enter Department (optional): ");
            string department = Console.ReadLine() ?? "General";
            if (string.IsNullOrWhiteSpace(department))
                department = "General";

            var employee = new Employee(id, name, department);
            
            Console.WriteLine($"\n📝 Adding employee: {employee}");
            
            // INSTRUCTOR NOTE: This calls the developer's Insert method!
            system.Insert(employee);
            Console.WriteLine("✅ Employee added to BST successfully!");
            
            // INSTRUCTOR NOTE: Show tree structure after insertion for immediate feedback
            Console.WriteLine("\n🌳 Current tree structure:");
            system.DisplayTree();
        }

        /// <summary>
        /// Demonstrate employee search using BST navigation
        /// 
        /// INSTRUCTOR NOTE: This method shows BST's O(log n) search performance.
        /// It demonstrates the core value proposition of tree structures.
        /// 
        /// Key learning concepts:
        /// - O(log n) search performance vs O(n) linear search
        /// - BST navigation based on comparison operations
        /// - Practical search applications
        /// </summary>
        /// <param name="system">System instance containing developer's Search implementation</param>
        public static void HandleSearchEmployee(EmployeeManagementSystem system)
        {
            Console.WriteLine("🔍 Search Employee Demo");
            Console.WriteLine("=======================");

            Console.Write("Enter Employee ID to search for: ");
            if (!int.TryParse(Console.ReadLine(), out int searchId))
            {
                Console.WriteLine("❌ Invalid ID. Please enter a number.");
                return;
            }

            Console.WriteLine($"\n🔍 Searching for Employee ID {searchId}...");
            
            // INSTRUCTOR NOTE: This calls the developer's Search method!
            var foundEmployee = system.Search(searchId);
            
            if (foundEmployee != null)
            {
                Console.WriteLine("✅ Employee FOUND!");
                Console.WriteLine($"📝 Details: {foundEmployee}");
            }
            else
            {
                Console.WriteLine("❌ Employee NOT FOUND");
                Console.WriteLine("💡 The BST search navigated the tree efficiently!");
            }
        }

        /// <summary>
        /// Demonstrate in-order traversal showing automatic sorting
        /// 
        /// INSTRUCTOR NOTE: This method showcases the "magic" of BST - automatic sorting
        /// through in-order traversal. It's often a "wow moment" for developers.
        /// 
        /// Key learning concepts:
        /// - In-order traversal: Left -> Root -> Right
        /// - Automatic sorted output from tree structure
        /// - No explicit sorting algorithm needed
        /// </summary>
        /// <param name="system">System instance containing developer's InOrderTraversal implementation</param>
        public static void HandleInOrderTraversal(EmployeeManagementSystem system)
        {
            Console.WriteLine("📊 In-Order Traversal Demo");
            Console.WriteLine("==========================");
            Console.WriteLine("Watch the magic - employees appear in sorted order by ID!");
            Console.WriteLine();

            // INSTRUCTOR NOTE: This calls the developer's InOrderTraversal method!
            system.InOrderTraversal();
            
            Console.WriteLine("\n🎉 Notice how employees are automatically sorted by ID!");
            Console.WriteLine("💡 This is the power of BST in-order traversal!");
        }

        /// <summary>
        /// Demonstrate finding minimum employee (leftmost node)
        /// 
        /// INSTRUCTOR NOTE: This method teaches tree navigation patterns.
        /// Finding minimum demonstrates how tree structure enables efficient operations.
        /// 
        /// Key learning concepts:
        /// - Leftmost node contains minimum value in BST
        /// - Tree navigation algorithms
        /// - Efficient min/max finding
        /// </summary>
        /// <param name="system">System instance containing developer's FindMinimum implementation</param>
        public static void HandleFindMinimum(EmployeeManagementSystem system)
        {
            Console.WriteLine("📉 Find Minimum Employee Demo");
            Console.WriteLine("=============================");
            Console.WriteLine("Finding employee with lowest ID (leftmost node)...");
            Console.WriteLine();

            // INSTRUCTOR NOTE: This calls the developer's FindMinimum method!
            var minEmployee = system.FindMinimum();
            
            if (minEmployee != null)
            {
                Console.WriteLine("✅ Minimum employee found!");
                Console.WriteLine($"📝 Employee with lowest ID: {minEmployee}");
                Console.WriteLine("💡 Found by navigating to leftmost tree node!");
            }
            else
            {
                Console.WriteLine("❌ No employees in system");
            }
        }

        /// <summary>
        /// Demonstrate finding maximum employee (rightmost node)
        /// 
        /// INSTRUCTOR NOTE: This method complements FindMinimum and shows
        /// symmetric navigation patterns in BST.
        /// 
        /// Key learning concepts:
        /// - Rightmost node contains maximum value in BST
        /// - Symmetric algorithms for min/max operations
        /// - Tree navigation in both directions
        /// </summary>
        /// <param name="system">System instance containing developer's FindMaximum implementation</param>
        public static void HandleFindMaximum(EmployeeManagementSystem system)
        {
            Console.WriteLine("📈 Find Maximum Employee Demo");
            Console.WriteLine("=============================");
            Console.WriteLine("Finding employee with highest ID (rightmost node)...");
            Console.WriteLine();

            // INSTRUCTOR NOTE: This calls the developer's FindMaximum method!
            var maxEmployee = system.FindMaximum();
            
            if (maxEmployee != null)
            {
                Console.WriteLine("✅ Maximum employee found!");
                Console.WriteLine($"📝 Employee with highest ID: {maxEmployee}");
                Console.WriteLine("💡 Found by navigating to rightmost tree node!");
            }
            else
            {
                Console.WriteLine("❌ No employees in system");
            }
        }

        /// <summary>
        /// Demonstrate counting employees using recursive tree traversal
        /// 
        /// INSTRUCTOR NOTE: This method teaches recursive thinking for trees.
        /// Counting is a fundamental recursive algorithm that builds intuition.
        /// 
        /// Key learning concepts:
        /// - Recursive algorithms for tree operations
        /// - Base case handling (empty tree)
        /// - Combining results from subtrees
        /// </summary>
        /// <param name="system">System instance containing developer's Count implementation</param>
        public static void HandleCountEmployees(EmployeeManagementSystem system)
        {
            Console.WriteLine("🔢 Count Employees Demo");
            Console.WriteLine("=======================");
            Console.WriteLine("Counting all employees using recursive tree traversal...");
            Console.WriteLine();

            // INSTRUCTOR NOTE: This calls the developer's Count method!
            int totalEmployees = system.Count();
            
            Console.WriteLine($"📊 Total Employees: {totalEmployees}");
            Console.WriteLine("💡 Counted using recursive tree traversal!");
            
            if (totalEmployees > 0)
            {
                var stats = system.GetSystemStats();
                Console.WriteLine($"🌳 Tree Height: {stats.TreeHeight} levels");
                Console.WriteLine($"⚡ Search Performance: ~{Math.Log2(totalEmployees):F0} comparisons maximum");
            }
        }

        /// <summary>
        /// Demonstrate tree visualization for understanding structure
        /// 
        /// INSTRUCTOR NOTE: This method helps developers visualize the tree structure
        /// they've built through their insertions. Visual feedback is crucial for learning.
        /// 
        /// Key learning concepts:
        /// - Tree structure visualization
        /// - Parent-child relationships
        /// - BST ordering property verification
        /// </summary>
        /// <param name="system">System instance with DisplayTree implementation</param>
        public static void HandleDisplayTree(EmployeeManagementSystem system)
        {
            Console.WriteLine("🌳 Tree Structure Visualization");
            Console.WriteLine("===============================");
            Console.WriteLine("Current BST structure:");
            Console.WriteLine();

            // INSTRUCTOR NOTE: This uses the provided DisplayTree method
            system.DisplayTree();
            
            Console.WriteLine("\n💡 Notice the BST property: left < parent < right");
            Console.WriteLine("📐 Tree layout shows parent-child relationships clearly!");
        }

        /// <summary>
        /// Display comprehensive system statistics and BST reference
        /// 
        /// INSTRUCTOR NOTE: This method serves multiple educational purposes:
        /// 1. Shows developers their progress through the lab
        /// 2. Provides a quick reference for BST operations
        /// 3. Demonstrates system state tracking
        /// 4. Reinforces Big O notation concepts
        /// 
        /// It's both a progress indicator and a learning reference tool.
        /// </summary>
        /// <param name="system">System instance containing system statistics</param>
        public static void HandleShowStats(EmployeeManagementSystem system)
        {
            Console.WriteLine("📊 System Statistics");
            Console.WriteLine("====================");

            // INSTRUCTOR NOTE: Get comprehensive statistics from the developer's implementation
            var stats = system.GetSystemStats();

            // INSTRUCTOR NOTE: Display session progress and system state information
            Console.WriteLine($"🎯 Total BST Operations: {stats.TotalOperations}");
            Console.WriteLine($"⏱️ Lab Session Time: {stats.Uptime.ToString("hh\\\\:mm\\\\:ss")}");
            
            try
            {
                Console.WriteLine($"👥 Total Employees: {stats.EmployeeCount}");
                Console.WriteLine($"🌳 Tree Height: {stats.TreeHeight} levels");
                
                if (stats.EmployeeCount > 0)
                {
                    var maxComparisons = Math.Log2(stats.EmployeeCount);
                    Console.WriteLine($"⚡ Max Search Steps: ~{maxComparisons:F0} comparisons");
                }
            }
            catch
            {
                Console.WriteLine("👥 Employee count requires implemented Count() method");
            }

            // INSTRUCTOR NOTE: Provide a quick reference guide for BST operations
            // This reinforces the key concepts developers are learning
            Console.WriteLine("\n🌳 BST Operations Reference:");
            Console.WriteLine("  ✅ Insert() - Add employee maintaining BST property");
            Console.WriteLine("  ✅ Search() - Find employee using O(log n) navigation");
            Console.WriteLine("  ✅ InOrderTraversal() - Display employees in sorted order");
            Console.WriteLine("  ✅ FindMinimum() - Get leftmost node (smallest ID)");
            Console.WriteLine("  ✅ FindMaximum() - Get rightmost node (largest ID)");
            Console.WriteLine("  ✅ Count() - Recursive count of all nodes");
            Console.WriteLine("  ✅ DisplayTree() - Visualize tree structure");
        }

        // ============================================
        // 🎭 DEMO DATA SETUP
        // ============================================

        /// <summary>
        /// Initialize all demo data for the lab session
        /// 
        /// INSTRUCTOR NOTE: Demo data is crucial for effective lab sessions. This method
        /// sets up realistic test scenarios that allow developers to immediately see their
        /// implementations in action without having to manually create test data.
        /// 
        /// The data is designed to showcase specific BST behaviors:
        /// - Varied employee IDs that create interesting tree structures
        /// - Realistic employee information for context
        /// - IDs chosen to demonstrate BST ordering property
        /// </summary>
        /// <param name="system">System instance to populate with demo data</param>
        public static void LoadDemoData(EmployeeManagementSystem system)
        {
            // INSTRUCTOR NOTE: Create demo employees that will form an interesting BST
            // IDs chosen to create a reasonably balanced tree for demonstration
            var demoEmployees = CreateDemoEmployees();
            var totalInserted = 0;
            
            Console.WriteLine("📊 Loading demo employee data...");
            
            // INSTRUCTOR NOTE: Insert employees one by one
            // This could be done in bulk, but individual insertions show the process
            foreach (var employee in demoEmployees)
            {
                try
                {
                    system.Insert(employee);
                    totalInserted++;
                }
                catch (NotImplementedException)
                {
                    Console.WriteLine("🚧 Insert method not implemented yet. Demo data loading halted.");
                    break;
                }
            }
            
            if (totalInserted == demoEmployees.Count)
            {
                Console.WriteLine("✅ All demo employees inserted successfully.");
            } else {
                Console.WriteLine($"⚠️ Only {totalInserted} out of {demoEmployees.Count} employees inserted.");
            }
        }

        /// <summary>
        /// Create demo employee data with strategic IDs for BST demonstration
        /// 
        /// INSTRUCTOR NOTE: This data is carefully designed to create meaningful
        /// BST demonstrations:
        /// 
        /// Employee ID Strategy:
        /// - Root: 50 (central value)
        /// - Left subtree: 25, 15, 35, 10, 20, 30, 40
        /// - Right subtree: 75, 65, 85, 60, 70, 80, 90
        /// 
        /// This creates a reasonably balanced tree that clearly shows BST properties
        /// and allows for interesting search/traversal demonstrations.
        /// </summary>
        /// <returns>List of demo employees with strategic IDs</returns>
        private static List<Employee> CreateDemoEmployees()
        {
            return new List<Employee>
            {
                // INSTRUCTOR NOTE: Root and major branches
                new Employee(50, "Alice Johnson", "Engineering"),
                new Employee(25, "Bob Smith", "Marketing"), 
                new Employee(75, "Charlie Brown", "Sales"),
                
                // INSTRUCTOR NOTE: Left subtree employees
                new Employee(15, "Diana Ross", "HR"),
                new Employee(35, "Edward Lee", "Engineering"),
                new Employee(10, "Fiona Apple", "Intern"),
                new Employee(20, "George Martin", "Finance"),
                new Employee(30, "Helen Troy", "Marketing"),
                new Employee(40, "Ivan Petrov", "Engineering"),
                
                // INSTRUCTOR NOTE: Right subtree employees  
                new Employee(65, "Julia Roberts", "Sales"),
                new Employee(85, "Kevin Bacon", "Management"),
                new Employee(60, "Laura Croft", "Security"),
                new Employee(70, "Michael Scott", "Sales"),
                new Employee(80, "Nancy Drew", "Legal"),
                new Employee(90, "Oscar Wilde", "Communications")
            };
        }
    }
}