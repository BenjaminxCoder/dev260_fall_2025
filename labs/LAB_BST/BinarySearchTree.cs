using System;
using System.Collections.Generic;
using System.Linq;

// ============================================
// 📚 QUICK REFERENCE GUIDE
// ============================================

/*
🌳 BINARY SEARCH TREE CHEAT SHEET:

BST Property:
- Left subtree values < Root value < Right subtree values
- This property enables O(log n) search performance!

Tree Construction:
var bst = new BinarySearchTree();
bst.Insert(50);  // Root node
bst.Insert(30);  // Goes left (30 < 50)
bst.Insert(70);  // Goes right (70 > 50)

Tree Search - O(log n):
bool found = bst.Search(30);  // Navigate tree efficiently

Tree Traversal:
bst.InOrderTraversal();  // Outputs: sorted sequence automatically!
// Left -> Root -> Right = 20, 30, 40, 50, 60, 70, 80

Tree Analysis:
int minimum = bst.FindMinimum();    // Leftmost node
int maximum = bst.FindMaximum();    // Rightmost node
int count = bst.Count();            // Total nodes

🚀 WHY BINARY SEARCH TREES ROCK:
- O(log n) search vs O(n) linear search
- Automatic sorting through in-order traversal
- Perfect for dynamic data with frequent searches
- Natural hierarchical organization
- Foundation for advanced tree structures

🌐 REAL-WORLD USES:
- Database indexing systems
- File system organization
- Expression parsing in compilers
- Game engine spatial partitioning
- Symbol tables in programming languages
- Priority queues and scheduling systems
*/

namespace Lab9_BST
{
    /// <summary>
    /// Lab 9: Binary Search Tree - Employee Management System
    /// 
    /// This lab demonstrates BST fundamentals through employee management scenarios!
    /// You'll build systems that work like real database indexing and search APIs.
    /// 
    /// 🌟 Real-World Connection:
    /// Every time you search for an employee in HR systems or browse organized
    /// file directories, you're seeing BST operations in action!
    /// </summary>
    public class EmployeeManagementSystem
    {
        // 🌳 THE CORE: BST for O(log n) operations and automatic organization
        // This is exactly how employee databases and search systems work!
        private TreeNode? root;
        
        // 📊 System tracking (real systems do this too!)
        private int totalOperations = 0;
        private DateTime systemStartTime = DateTime.Now;
        
        public EmployeeManagementSystem()
        {
            root = null;
            Console.WriteLine("🏢 Employee Management System Initialized!");
            Console.WriteLine("📊 System ready for BST operations.\n");
        }
        
        // ============================================
        // 🚀 YOUR MISSION: IMPLEMENT THESE METHODS
        // ============================================
        
        /// <summary>
        /// TODO #1: Insert employee into BST maintaining ordering property
        /// 
        /// Real-World Connection: This is like adding new hires to company database
        /// 
        /// Requirements:
        /// - Insert employee based on EmployeeId (the key for BST ordering)
        /// - Maintain BST property: left < root < right
        /// - Use recursive approach with helper method
        /// - Handle empty tree case (first employee)
        /// 
        /// 🔑 Key Learning: BST property enables fast search operations!
        /// </summary>
        public void Insert(Employee employee)
        {
            totalOperations++;
            // Use recursive helper to insert and preserve links
            root = InsertRecursive(root, employee);
        }
        
        /// <summary>
        /// TODO #2: Search for employee by ID using BST navigation
        /// 
        /// Real-World Connection: This is like HR system finding employee records instantly
        /// 
        /// Requirements:
        /// - Search for employee using EmployeeId as key
        /// - Navigate tree: go left if id < current, right if id > current
        /// - Return the Employee object if found, null if not found
        /// - Use recursive approach
        /// 
        /// 🚀 Key Learning: O(log n) search vs O(n) linear search!
        /// </summary>
        public Employee? Search(int employeeId)
        {
            totalOperations++;
            // Use recursive helper for O(log n) average search
            return SearchRecursive(root, employeeId);
        }
        
        /// <summary>
        /// TODO #3: Perform in-order traversal to display employees in sorted order
        /// 
        /// Real-World Connection: This is like generating sorted employee reports
        /// 
        /// Requirements:
        /// - Traverse tree in order: Left -> Root -> Right
        /// - Display employees sorted by EmployeeId automatically
        /// - Use recursive approach
        /// - Print each employee's details
        /// 
        /// 🎯 Key Learning: In-order traversal gives sorted output for free!
        /// </summary>
        public void InOrderTraversal()
        {
            totalOperations++;
            Console.WriteLine("👥 Employee Directory (sorted by ID):");

            if (root == null)
            {
                Console.WriteLine("   (No employees in system)");
                return;
            }

            InOrderRecursive(root);
        }
        
        /// <summary>
        /// TODO #4: Find employee with minimum ID (leftmost node)
        /// 
        /// Real-World Connection: This is like finding the newest hire or lowest badge number
        /// 
        /// Requirements:
        /// - Navigate to leftmost node in tree
        /// - Return the Employee with smallest EmployeeId
        /// - Handle empty tree case
        /// - Use iterative or recursive approach
        /// 
        /// 📊 Key Learning: Tree structure makes min/max finding efficient!
        /// </summary>
        public Employee? FindMinimum()
        {
            totalOperations++;

            if (root == null) return null;

            var curr = root;
            while (curr.Left != null)
            {
                curr = curr.Left;
            }
            return curr.Employee;
        }
        
        /// <summary>
        /// TODO #5: Find employee with maximum ID (rightmost node)
        /// 
        /// Real-World Connection: This is like finding the most senior employee or highest badge number
        /// 
        /// Requirements:
        /// - Navigate to rightmost node in tree
        /// - Return the Employee with largest EmployeeId
        /// - Handle empty tree case
        /// - Use iterative or recursive approach
        /// 
        /// 🏆 Key Learning: Symmetric to minimum - shows tree navigation patterns!
        /// </summary>
        public Employee? FindMaximum()
        {
            totalOperations++;

            if (root == null) return null;

            var curr = root;
            while (curr.Right != null)
            {
                curr = curr.Right;
            }
            return curr.Employee;
        }
        
        /// <summary>
        /// TODO #6: Count total employees in system using recursion
        /// 
        /// Real-World Connection: This is like generating headcount reports for management
        /// 
        /// Requirements:
        /// - Count all nodes in the tree recursively
        /// - Return total number of employees
        /// - Handle empty tree case (return 0)
        /// - Use recursive approach
        /// 
        /// 📈 Key Learning: Recursive thinking for tree algorithms!
        /// </summary>
        public int Count()
        {
            totalOperations++;
            return CountRecursive(root);
        }
        
        // ============================================
        // 🔧 HELPER METHODS FOR TODO IMPLEMENTATION
        // ============================================
        
        private TreeNode? InsertRecursive(TreeNode? node, Employee employee)
        {
            // Base case: empty spot found — create and return new node
            if (node == null)
            {
                return new TreeNode(employee);
            }

            // Compare by EmployeeId to maintain BST property
            if (employee.EmployeeId < node.Employee.EmployeeId)
            {
                node.Left = InsertRecursive(node.Left, employee);
            }
            else if (employee.EmployeeId > node.Employee.EmployeeId)
            {
                node.Right = InsertRecursive(node.Right, employee);
            }
            else
            {
                // Duplicate key: do not insert a new node; keep tree unchanged
            }

            // Return current node so parent links remain intact
            return node;
        }
        
        private Employee? SearchRecursive(TreeNode? node, int employeeId)
        {
            // Base case: not found
            if (node == null) return null;

            // Match
            if (employeeId == node.Employee.EmployeeId)
            {
                return node.Employee;
            }

            // Navigate left or right by key comparison
            if (employeeId < node.Employee.EmployeeId)
            {
                return SearchRecursive(node.Left, employeeId);
            }

            return SearchRecursive(node.Right, employeeId);
        }
        
        private void InOrderRecursive(TreeNode? node)
        {
            if (node == null) return;

            InOrderRecursive(node.Left);

            // Print current employee in a readable line
            var emp = node.Employee;
            Console.WriteLine($"   ID:{emp.EmployeeId} | Name:{emp.Name} | Dept:{emp.Department}");

            InOrderRecursive(node.Right);
        }
        
        private int CountRecursive(TreeNode? node)
        {
            if (node == null) return 0;
            return 1 + CountRecursive(node.Left) + CountRecursive(node.Right);
        }
        
        // ============================================
        // 🎯 UTILITY METHODS (PROVIDED)
        // ============================================
        
        /// <summary>
        /// Check if the BST contains any employees
        /// 
        /// STUDENT NOTE: This is a simple helper method that's very useful for validation.
        /// Many BST operations need to check if the tree is empty before proceeding.
        /// 
        /// Key learning concepts:
        /// - Empty tree condition (root == null)
        /// - Basic tree state checking
        /// - Foundation for error handling in other methods
        /// </summary>
        /// <returns>True if tree is empty, false if it contains employees</returns>
        public bool IsEmpty()
        {
            // STUDENT NOTE: A tree is empty when the root node is null
            // This is the simplest possible tree operation!
            return root == null;
        }
        
        /// <summary>
        /// Display the entire tree structure visually
        /// 
        /// STUDENT NOTE: This method helps you visualize your BST as you build it!
        /// It shows the parent-child relationships and helps you verify that
        /// your Insert() method is maintaining the BST property correctly.
        /// 
        /// Key learning concepts:
        /// - Tree visualization for debugging
        /// - Recursive tree traversal patterns
        /// - Understanding tree structure through visual feedback
        /// </summary>
        public void DisplayTree()
        {
            Console.WriteLine("🌳 Tree Structur Visualization:");
            
            // STUDENT NOTE: Always handle the empty tree case first
            if (root == null)
            {
                Console.WriteLine("   (Empty tree)");
                return;
            }
            
            // STUDENT NOTE: Start recursive displays
            Console.WriteLine("\n📊 Enhanced Tree Structure:");
            DisplayTreeEnhanced(root, "", true, true);
            
            Console.WriteLine("\n🎯 Level-by-Level View:");
            DisplayTreeByLevels();
        }
        
        /// <summary>
        /// Enhanced tree display with better visual formatting and clear parent-child relationships
        /// 
        /// STUDENT NOTE: This is an advanced example of recursive tree traversal for visualization!
        /// Study this method to understand how recursion can create complex visual outputs.
        /// The method uses ASCII art to show the actual tree structure with branches and connections.
        /// 
        /// Key learning concepts:
        /// - Recursive tree traversal with state tracking (prefix, position)
        /// - String manipulation for visual formatting
        /// - Parent-child relationship visualization
        /// - How tree structure translates to visual representation
        /// - Base case handling (null nodes) in recursive algorithms
        /// 
        /// Real-World Connection:
        /// This type of visualization is used in debugging tools, file system browsers,
        /// and database query plan displays. Understanding tree structure visually
        /// helps developers optimize algorithms and troubleshoot issues.
        /// </summary>
        /// <param name="node">Current node being displayed (null for empty subtrees)</param>
        /// <param name="prefix">String prefix for proper indentation and connection lines</param>
        /// <param name="isLast">True if this node is the last child of its parent</param>
        /// <param name="isRoot">True if this is the root node (top of tree)</param>
        private void DisplayTreeEnhanced(TreeNode? node, string prefix, bool isLast, bool isRoot)
        {
            // STUDENT NOTE: Base case - empty node means we've reached the end of a branch
            // This is the same pattern you'll use in your TODO recursive methods!
            if (node == null) return;

            // STUDENT NOTE: Choose the right connector symbol based on position in tree
            // Root gets special star, others get tree branch characters
            string connector = isRoot ? "🌟 " : (isLast ? "└── " : "├── ");
            string nodeInfo = $"ID:{node.Employee.EmployeeId} ({node.Employee.Name})";
            
            // STUDENT NOTE: Display current node with proper indentation and connection
            Console.WriteLine(prefix + connector + nodeInfo);

            // STUDENT NOTE: Calculate prefix for child nodes to maintain proper tree structure
            // This ensures children align correctly under their parent
            string childPrefix = prefix + (isRoot ? "" : (isLast ? "    " : "│   "));

            // STUDENT NOTE: Check which children exist to determine display order
            // We need to know this to draw the connecting lines correctly
            bool hasLeft = node.Left != null;
            bool hasRight = node.Right != null;

            // STUDENT NOTE: Display right subtree first (appears at top in vertical layout)
            // This creates a more natural left-to-right reading flow
            if (hasRight)
            {
                Console.WriteLine(childPrefix + "│");                    // Vertical connection line
                Console.WriteLine(childPrefix + "├─(R)─┐");              // Right branch indicator
                // STUDENT NOTE: Recursive call! Same pattern as your TODO methods
                DisplayTreeEnhanced(node.Right, childPrefix + "│     ", !hasLeft, false);
            }

            // STUDENT NOTE: Display left subtree second (appears at bottom in vertical layout)
            // Notice how recursion naturally handles the entire subtree structure
            if (hasLeft)
            {
                Console.WriteLine(childPrefix + "│");                    // Vertical connection line
                Console.WriteLine(childPrefix + "└─(L)─┐");              // Left branch indicator
                // STUDENT NOTE: Another recursive call - this is the power of recursion!
                DisplayTreeEnhanced(node.Left, childPrefix + "      ", true, false);
            }
        }

        /// <summary>
        /// Display tree in a horizontal level-by-level format using breadth-first traversal
        /// 
        /// While your TODO methods use recursive depth-first traversal (going deep first),
        /// this method uses iterative breadth-first traversal (going wide first).
        /// 
        /// Key learning concepts:
        /// - Breadth-First Search (BFS) vs Depth-First Search (DFS) algorithms
        /// - Queue data structure for level-order processing
        /// - Iterative tree traversal (no recursion needed!)
        /// - Level-by-level tree analysis and display
        /// - Tuple usage for tracking node-level pairs
        /// 
        /// Algorithm Pattern:
        /// 1. Start with root at level 0 in queue
        /// 2. Process all nodes at current level
        /// 3. Add their children to queue for next level
        /// 4. Repeat until queue is empty
        /// 
        /// Real-World Connection:
        /// This BFS pattern is used in shortest-path algorithms, network analysis,
        /// social media "degrees of separation", and game AI pathfinding.
        /// </summary>
        private void DisplayTreeByLevels()
        {
            // STUDENT NOTE: Handle empty tree case first (same pattern as your TODO methods)
            if (root == null) return;

            // STUDENT NOTE: Queue stores (node, level) pairs for breadth-first processing
            // This is different from recursion - we're using an explicit queue!
            var queue = new Queue<(TreeNode?, int)>();
            queue.Enqueue((root, 0));              // Start with root at level 0
            int currentLevel = -1;                  // Track which level we're displaying

            // STUDENT NOTE: Continue until queue is empty (all nodes processed)
            // This is the iterative equivalent of recursive base case checking
            while (queue.Count > 0)
            {
                // STUDENT NOTE: Dequeue next node and its level from front of queue
                // Tuple destructuring makes this elegant and readable
                var (node, level) = queue.Dequeue();
                
                // STUDENT NOTE: Check if we're starting a new level of the tree
                // This creates the "Level 0:", "Level 1:", etc. headers
                if (level > currentLevel)
                {
                    if (currentLevel >= 0) Console.WriteLine();    // End previous level
                    Console.Write($"Level {level}: ");             // Start new level
                    currentLevel = level;
                }

                // STUDENT NOTE: Display current node and enqueue its children
                if (node != null)
                {
                    Console.Write($"[{node.Employee.EmployeeId}] ");
                    // STUDENT NOTE: Add children to queue for processing at next level
                    // This is how BFS "discovers" nodes level by level
                    queue.Enqueue((node.Left, level + 1));
                    queue.Enqueue((node.Right, level + 1));
                }
                else
                {
                    // STUDENT NOTE: Show null nodes to maintain tree structure clarity
                    Console.Write("[null] ");
                }
            }
            Console.WriteLine();    // Final newline after all levels
        }
        
        /// <summary>
        /// Generate comprehensive statistics about the BST system
        /// 
        /// STUDENT NOTE: This method demonstrates how to gather information about
        /// your data structure. Notice how it calls YOUR Count() method - once you
        /// implement Count(), the employee count will work automatically!
        /// 
        /// Key learning concepts:
        /// - System monitoring and statistics gathering
        /// - Combining multiple data points into useful information
        /// - How completed methods depend on your TODO implementations
        /// </summary>
        /// <returns>SystemStats object containing current system information</returns>
        public SystemStats GetSystemStats()
        {
            // STUDENT NOTE: Calculate how long the system has been running
            var uptime = DateTime.Now - systemStartTime;
            
            // STUDENT NOTE: Create stats object with current system state
            // Pay attention: EmployeeCount uses YOUR Count() method!
            return new SystemStats
            {
                TotalOperations = totalOperations,
                Uptime = uptime,
                EmployeeCount = IsEmpty() ? 0 : Count(),  // Calls YOUR Count() method!
                TreeHeight = CalculateHeight(root),
                IsEmpty = IsEmpty()
            };
        }
        
        /// <summary>
        /// Calculate the height (depth) of the BST
        /// 
        /// STUDENT NOTE: This is another excellent example of recursive tree algorithms!
        /// Tree height is important for understanding BST performance - shorter trees
        /// mean faster searches. Study this recursive pattern carefully.
        /// 
        /// Key learning concepts:
        /// - Recursive tree measurement algorithms
        /// - Base case + recursive case pattern (same as your TODO methods!)
        /// - Tree height affects search performance (shorter = faster)
        /// </summary>
        /// <param name="node">Current node being measured</param>
        /// <returns>Height of tree rooted at this node</returns>
        private int CalculateHeight(TreeNode? node)
        {
            // STUDENT NOTE: Base case - empty tree has height 0
            if (node == null) return 0;
            
            // STUDENT NOTE: Recursive case - height is 1 + maximum height of children
            // This is the same recursive thinking you'll use in your TODO methods!
            return 1 + Math.Max(CalculateHeight(node.Left), CalculateHeight(node.Right));
        }
        
        /// <summary>
        /// Launch the interactive menu system for testing your implementations
        /// 
        /// STUDENT NOTE: This connects your BST implementation to the user interface!
        /// The LabSupport class handles all the menu logic and calls YOUR methods
        /// when you select different options. This separation keeps your BST code
        /// focused on algorithms while the UI handles user interaction.
        /// 
        /// Key learning concepts:
        /// - Separation of concerns (business logic vs. user interface)
        /// - How your implementations integrate with larger systems
        /// - Testing your code through interactive exploration
        /// </summary>
        public void RunInteractiveMenu()
        {
            // STUDENT NOTE: Delegate to LabSupport for menu handling
            // This keeps your BST class focused on tree operations only
            LabSupport.RunInteractiveMenu(this);
        }
    }
}