using System;
using System.Collections.Generic;
using System.Linq;

namespace FileSystemNavigator
{
    /// <summary>
    /// Interactive menu navigator for the BST File System Navigator application.
    /// Provides a user-friendly interface for all file system operations.
    /// 
    /// INSTRUCTOR NOTE: This class demonstrates several important software engineering patterns:
    /// 1. MVC Pattern: This acts as the "View/Controller" managing user interaction
    /// 2. Separation of Concerns: UI logic is completely separate from business logic (FileSystemBST)
    /// 3. Command Pattern: User input is parsed into commands that route to specific handlers
    /// 4. Professional Error Handling: Graceful handling of unimplemented methods and exceptions
    /// 5. User Experience Design: Clear prompts, help system, and educational feedback
    /// 
    /// This class calls YOUR FileSystemBST methods to test each TODO implementation.
    /// Study how it provides comprehensive testing and feedback for student learning.
    /// </summary>
    public class FileSystemNavigator
    {
        // INSTRUCTOR NOTE: Using 'readonly' ensures the FileSystemBST reference can't be changed
        // after construction, which prevents bugs and makes the code more predictable.
        // This demonstrates the "Composition" pattern - Navigator HAS-A FileSystemBST.
        private readonly FileSystemBST fileSystem;
        
        // INSTRUCTOR NOTE: Simple boolean flag to control the main application loop.
        // This is a common pattern for interactive console applications.
        private bool isRunning;
        
        // INSTRUCTOR NOTE: Constructor demonstrates good dependency injection practices.
        // We require a FileSystemBST instance rather than creating one inside this class.
        // This makes testing easier and follows the "Dependency Inversion Principle".
        public FileSystemNavigator(FileSystemBST fileSystem)
        {
            // Always validate constructor parameters to prevent null reference exceptions
            this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
            this.isRunning = true;
        }
        
        /// <summary>
        /// Main application loop that handles user input and coordinates file system operations.
        /// 
        /// INSTRUCTOR NOTE: This method demonstrates the classic "Read-Evaluate-Print Loop" (REPL)
        /// pattern used in many interactive applications. The structure is:
        /// 1. Welcome message and setup
        /// 2. Main loop with menu display and input handling
        /// 3. Comprehensive exception handling to prevent crashes
        /// 4. Clear screen and menu redisplay after each operation
        /// </summary>
        public void Run()
        {
            // INSTRUCTOR NOTE: Clear welcome message sets educational context
            Console.WriteLine("🗂️  BST File System Navigator");
            Console.WriteLine("============================");
            Console.WriteLine("Welcome to the interactive file system application!");
            Console.WriteLine("Test your Binary Search Tree implementations with real file operations.");
            Console.WriteLine();

            // INSTRUCTOR NOTE: The main application loop - keeps running until isRunning becomes false
            while (isRunning)
            {
                DisplayMainMenu();
                string choice = Console.ReadLine()?.ToLower() ?? "";

                Console.WriteLine($"You selected: {choice}");
                
                // INSTRUCTOR NOTE: Wrap all user operations in try-catch to prevent crashes
                // from invalid input or business logic errors
                try
                {
                    ProcessCommand(choice);
                }
                catch (NotImplementedException ex)
                {
                    Console.WriteLine($"\n❌ Method not implemented yet: {ex.Message}");
                    Console.WriteLine("💡 Implement the TODO method to use this feature.\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ Error: {ex.Message}\n");
                }

                // INSTRUCTOR NOTE: Pause and clear screen for better user experience
                if (isRunning)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
        
        /// <summary>
        /// Process user commands and route to appropriate handlers.
        /// 
        /// INSTRUCTOR NOTE: This method demonstrates the "Command Pattern" - we parse the user input
        /// into a command and arguments, then route to specific handler methods. This keeps the
        /// code organized and makes it easy to add new commands later.
        /// 
        /// Notice how we support both numbers (1-8) for TODO methods AND descriptive names
        /// for better accessibility. Each TODO method has its own dedicated handler for
        /// comprehensive testing and educational feedback.
        /// </summary>
        private void ProcessCommand(string input)
        {
            // INSTRUCTOR NOTE: Split the input into command and arguments
            // RemoveEmptyEntries prevents issues with multiple spaces
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) return;
            
            // INSTRUCTOR NOTE: Commands are case-insensitive for better user experience
            string command = parts[0].ToLower();
            // INSTRUCTOR NOTE: Skip(1) gets everything after the first element (the arguments)
            string[] args = parts.Skip(1).ToArray();
            
            // INSTRUCTOR NOTE: Switch statement provides clean command routing.
            // Each numbered case (1-8) corresponds to a specific TODO method for easy testing.
            switch (command)
            {
                case "1":
                case "create":
                case "file":
                    // INSTRUCTOR NOTE: Test TODO #1 - File creation with BST insertion
                    HandleCreateFileCommand(args);
                    break;
                    
                case "2":
                case "mkdir":
                case "directory":
                    // INSTRUCTOR NOTE: Test TODO #2 - Directory creation with custom comparison
                    HandleCreateDirectoryCommand(args);
                    break;
                    
                case "3":
                case "find":
                case "search":
                    // INSTRUCTOR NOTE: Test TODO #3 - File search with O(log n) BST navigation
                    HandleFindFileCommand(args);
                    break;
                    
                case "4":
                case "extension":
                case "filter":
                    // INSTRUCTOR NOTE: Test TODO #4 - Extension filtering with tree traversal
                    HandleFindByExtensionCommand(args);
                    break;
                    
                case "5":
                case "size":
                case "range":
                    // INSTRUCTOR NOTE: Test TODO #5 - Size range queries with BST traversal
                    HandleFindBySizeCommand(args);
                    break;
                    
                case "6":
                case "largest":
                case "biggest":
                    // INSTRUCTOR NOTE: Test TODO #6 - Top-N analysis combining traversal + sorting
                    HandleFindLargestCommand(args);
                    break;
                    
                case "7":
                case "total":
                case "calculate":
                    // INSTRUCTOR NOTE: Test TODO #7 - Aggregation through recursive tree traversal
                    HandleCalculateTotalCommand();
                    break;
                    
                case "8":
                case "delete":
                case "remove":
                    // INSTRUCTOR NOTE: Test TODO #8 - Complex BST deletion with structure maintenance
                    HandleDeleteCommand(args);
                    break;
                    
                case "9":
                case "tree":
                case "display":
                    // INSTRUCTOR NOTE: Tree visualization for debugging and understanding
                    HandleDisplayTreeCommand();
                    break;
                    
                case "10":
                case "stats":
                case "statistics":
                    // INSTRUCTOR NOTE: System statistics and performance metrics
                    HandleStatsCommand();
                    break;
                    
                case "11":
                case "sample":
                case "demo":
                    // INSTRUCTOR NOTE: Load sample data for immediate testing
                    HandleLoadSampleCommand();
                    break;
                    
                case "12":
                case "exit":
                case "quit":
                    // INSTRUCTOR NOTE: Graceful exit - sets the loop flag to false
                    isRunning = false;
                    ShowGoodbye();
                    break;
                    
                default:
                    // INSTRUCTOR NOTE: Always handle invalid input gracefully with helpful feedback
                    Console.WriteLine($"❌ Unknown command: '{command}'. Please try again.\n");
                    break;
            }
        }
        
        /// <summary>
        /// Display the main menu with all available TODO implementations.
        /// </summary>
        // INSTRUCTOR NOTE: This method demonstrates good UI design principles:
        // 1. Clear visual hierarchy with borders and sections
        // 2. Multiple input methods (numbers OR words) for accessibility
        // 3. Real-time status information (file system state)
        // 4. Consistent formatting and layout
        private void DisplayMainMenu()
        {
            // INSTRUCTOR NOTE: These calls to YOUR FileSystemBST provide live data
            bool isEmpty = fileSystem.IsEmpty();
            var stats = fileSystem.GetStatistics();
            
            // INSTRUCTOR NOTE: ASCII box drawing creates a professional console interface
            Console.WriteLine("┌─ BST File System Navigator ───────────────────────────┐");
            Console.WriteLine("│ 1. Create File    │ 2. Create Dir     │ 3. Find File  │");
            Console.WriteLine("│ 4. Find by Ext    │ 5. Find by Size   │ 6. Largest    │");
            Console.WriteLine("│ 7. Total Size     │ 8. Delete Item    │ 9. Show Tree  │");
            Console.WriteLine("│ 10. Statistics    │ 11. Load Sample   │ 12. Exit      │");
            Console.WriteLine("└───────────────────────────────────────────────────────┘");
            
            // INSTRUCTOR NOTE: Dynamic status display shows current system state
            Console.WriteLine($"📊 Status: {stats.TotalFiles} files, {stats.TotalDirectories} directories");
            Console.WriteLine($"💾 Storage: {stats.FormatSize(stats.TotalSize)}");
            Console.WriteLine($"⚡ Operations: {stats.TotalOperations}");
            Console.WriteLine();
            
            // INSTRUCTOR NOTE: Help text guides new users
            Console.WriteLine("💡 Tip: Use numbers (1-12) or keywords (create, find, etc.)");
            Console.Write("Enter your choice: ");
        }

        /// <summary>
        /// Display a good application goodbye experience with useful summary information.
        /// 
        /// INSTRUCTOR NOTE: A good application always provides a proper goodbye experience
        /// with useful summary information and encouragement for learning.
        /// </summary>
        private void ShowGoodbye()
        {
            Console.WriteLine("\n🎓 Thank you for using BST File System Navigator!");
            Console.WriteLine("=================================================");
            
            var stats = fileSystem.GetStatistics();
            if (!fileSystem.IsEmpty())
            {
                Console.WriteLine($"📊 Session Summary:");
                Console.WriteLine($"   Files Created: {stats.TotalFiles}");
                Console.WriteLine($"   Directories Created: {stats.TotalDirectories}");
                Console.WriteLine($"   Total Operations: {stats.TotalOperations}");
                Console.WriteLine($"   Session Duration: {stats.SessionDuration:mm\\\\:ss}");
            }
            
            Console.WriteLine("\n🚀 You've practiced essential Binary Search Tree concepts!");
            Console.WriteLine("   These algorithms power file systems, databases, and search engines.");
            Console.WriteLine("   Keep practicing recursive thinking - it's fundamental to computer science!");
        }
        
        /// <summary>
        /// Handle the create file command - TODO #1 test.
        /// 
        /// INSTRUCTOR NOTE: This method demonstrates file creation with BST insertion.
        /// Shows how to handle user input validation and provide educational feedback.
        /// </summary>
        private void HandleCreateFileCommand(string[] args)
        {
            string fileName;
            long size = 1024; // default size
            
            if (args.Length == 0)
            {
                Console.Write("Enter file name (e.g., readme.txt, app.cs): ");
                fileName = Console.ReadLine()?.Trim() ?? "";
                
                if (string.IsNullOrEmpty(fileName))
                {
                    Console.WriteLine("❌ Invalid file name provided.");
                    return;
                }
                
                Console.Write("Enter file size in bytes (default 1024): ");
                var sizeInput = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(sizeInput) && long.TryParse(sizeInput, out long parsedSize))
                {
                    size = parsedSize;
                }
            }
            else
            {
                fileName = args[0];
                if (args.Length > 1 && long.TryParse(args[1], out long parsedSize))
                {
                    size = parsedSize;
                }
            }
            
            Console.WriteLine();
            
            bool success = fileSystem.CreateFile(fileName, size);
            
            if (success)
            {
                Console.WriteLine($"✅ File created successfully: {fileName}");
                Console.WriteLine("🌳 BST insertion completed - file added maintaining tree order!");
            }
            else
            {
                Console.WriteLine($"❌ Failed to create file: {fileName}");
                Console.WriteLine("💡 This usually means the file already exists in the BST.");
            }
        }
        
        /// <summary>
        /// Handle the create directory command - TODO #2 test.
        /// 
        /// INSTRUCTOR NOTE: This method tests directory creation with custom comparison logic.
        /// Demonstrates how directories sort before files in the BST ordering.
        /// </summary>
        private void HandleCreateDirectoryCommand(string[] args)
        {
            string dirName;
            
            if (args.Length == 0)
            {
                Console.Write("Enter directory name (e.g., Documents, Projects): ");
                dirName = Console.ReadLine()?.Trim() ?? "";
            }
            else
            {
                dirName = args[0];
            }
            
            if (string.IsNullOrEmpty(dirName))
            {
                Console.WriteLine("❌ Invalid directory name provided.");
                return;
            }
            
            Console.WriteLine();
            
            bool success = fileSystem.CreateDirectory(dirName);
            
            if (success)
            {
                Console.WriteLine($"✅ Directory created successfully: {dirName}");
                Console.WriteLine("📂 BST insertion with custom comparison - directories before files!");
            }
            else
            {
                Console.WriteLine($"❌ Failed to create directory: {dirName}");
                Console.WriteLine("💡 This usually means the directory already exists in the BST.");
            }
        }
        
        /// <summary>
        /// Handle the find file command - TODO #3 test.
        /// 
        /// INSTRUCTOR NOTE: This method demonstrates O(log n) file search using BST navigation.
        /// Shows the performance advantage of tree search over linear search.
        /// </summary>
        private void HandleFindFileCommand(string[] args)
        {
            string fileName;
            
            if (args.Length == 0)
            {
                Console.Write("Enter file name to search for: ");
                fileName = Console.ReadLine()?.Trim() ?? "";
            }
            else
            {
                fileName = args[0];
            }
            
            if (string.IsNullOrEmpty(fileName))
            {
                Console.WriteLine("❌ Invalid file name provided.");
                return;
            }
            
            Console.WriteLine();
            
            var result = fileSystem.FindFile(fileName);
            
            if (result != null)
            {
                Console.WriteLine($"✅ File found: {result}");
                Console.WriteLine("⚡ O(log n) BST search completed successfully!");
            }
            else
            {
                Console.WriteLine($"❌ File not found: {fileName}");
                Console.WriteLine("🔍 BST search traversed the tree but no matching file exists.");
            }
        }
        
        /// <summary>
        /// Handle the find by extension command - TODO #4 test.
        /// 
        /// INSTRUCTOR NOTE: This method demonstrates tree traversal with filtering.
        /// Shows how to collect specific items during in-order traversal.
        /// </summary>
        private void HandleFindByExtensionCommand(string[] args)
        {
            string extension;
            
            if (args.Length == 0)
            {
                Console.Write("Enter file extension (e.g., .txt, .cs): ");
                extension = Console.ReadLine()?.Trim() ?? "";
            }
            else
            {
                extension = args[0];
            }
            
            if (string.IsNullOrEmpty(extension))
            {
                Console.WriteLine("❌ Invalid extension provided.");
                return;
            }
            
            // Add dot if not provided
            if (!extension.StartsWith("."))
            {
                extension = "." + extension;
            }
            
            Console.WriteLine();
            
            var results = fileSystem.FindFilesByExtension(extension);
            
            Console.WriteLine($"\n🔍 Found {results.Count} files with extension '{extension}':");
            Console.WriteLine("=" + new string('=', 60));
            
            if (results.Count == 0)
            {
                Console.WriteLine("   (No files found)");
                Console.WriteLine("🌳 Tree traversal completed - no files match the filter criteria.");
            }
            else
            {
                foreach (var file in results.Take(10)) // Show max 10 results
                {
                    Console.WriteLine($"   {file}");
                }
                
                if (results.Count > 10)
                {
                    Console.WriteLine($"   ... and {results.Count - 10} more files");
                }
                
                Console.WriteLine("🌳 In-order traversal with filtering completed successfully!");
            }
        }
        
        /// <summary>
        /// Handle the find by size command - TODO #5 test.
        /// 
        /// INSTRUCTOR NOTE: This method demonstrates range queries using tree traversal.
        /// Shows how to filter data during traversal for efficient range operations.
        /// </summary>
        private void HandleFindBySizeCommand(string[] args)
        {
            long minSize, maxSize;
            
            if (args.Length < 2)
            {
                Console.Write("Enter minimum size in bytes: ");
                if (!long.TryParse(Console.ReadLine(), out minSize))
                {
                    Console.WriteLine("❌ Invalid minimum size provided.");
                    return;
                }
                
                Console.Write("Enter maximum size in bytes: ");
                if (!long.TryParse(Console.ReadLine(), out maxSize))
                {
                    Console.WriteLine("❌ Invalid maximum size provided.");
                    return;
                }
            }
            else
            {
                if (!long.TryParse(args[0], out minSize) || !long.TryParse(args[1], out maxSize))
                {
                    Console.WriteLine("❌ Invalid size parameters. Usage: size <min> <max>");
                    return;
                }
            }
            
            Console.WriteLine();
            
            var results = fileSystem.FindFilesBySize(minSize, maxSize);
            
            Console.WriteLine($"\n📏 Found {results.Count} files between {FormatSize(minSize)} and {FormatSize(maxSize)}:");
            Console.WriteLine("=" + new string('=', 70));
            
            if (results.Count == 0)
            {
                Console.WriteLine("   (No files found in size range)");
                Console.WriteLine("📊 Range query traversal completed - no files match the size criteria.");
            }
            else
            {
                foreach (var file in results.Take(10))
                {
                    Console.WriteLine($"   {file}");
                }
                
                if (results.Count > 10)
                {
                    Console.WriteLine($"   ... and {results.Count - 10} more files");
                }
                
                Console.WriteLine("📊 Range-based tree traversal completed successfully!");
            }
        }
        
        /// <summary>
        /// Handle the find largest files command - TODO #6 test.
        /// 
        /// INSTRUCTOR NOTE: This method demonstrates tree traversal with post-processing.
        /// Shows how to combine tree operations with sorting for top-N analysis.
        /// </summary>
        private void HandleFindLargestCommand(string[] args)
        {
            int count;
            
            if (args.Length == 0)
            {
                Console.Write("Enter number of largest files to find: ");
                if (!int.TryParse(Console.ReadLine(), out count) || count <= 0)
                {
                    Console.WriteLine("❌ Invalid count provided.");
                    return;
                }
            }
            else
            {
                if (!int.TryParse(args[0], out count) || count <= 0)
                {
                    Console.WriteLine("❌ Invalid count parameter. Usage: largest <count>");
                    return;
                }
            }
            
            Console.WriteLine();
            
            var results = fileSystem.FindLargestFiles(count);
            
            Console.WriteLine($"\n🏆 Top {Math.Min(count, results.Count)} largest files:");
            Console.WriteLine("=" + new string('=', 50));
            
            if (results.Count == 0)
            {
                Console.WriteLine("   (No files found)");
                Console.WriteLine("📈 Tree traversal + sorting completed - no files to rank.");
            }
            else
            {
                for (int i = 0; i < results.Count; i++)
                {
                    Console.WriteLine($"   #{i + 1}: {results[i]}");
                }
                
                Console.WriteLine("📈 Tree traversal with sorting completed successfully!");
            }
        }
        
        /// <summary>
        /// Handle the calculate total size command - TODO #7 test.
        /// 
        /// INSTRUCTOR NOTE: This method demonstrates recursive aggregation through tree traversal.
        /// Shows how to calculate summary statistics using tree algorithms.
        /// </summary>
        private void HandleCalculateTotalCommand()
        {
            Console.WriteLine();
            
            long totalSize = fileSystem.CalculateTotalSize();
            
            Console.WriteLine($"📊 Total file system size: {FormatSize(totalSize)}");
            Console.WriteLine($"   Raw bytes: {totalSize:N0}");
            Console.WriteLine("🧮 Recursive tree traversal aggregation completed successfully!");
        }
        
        /// <summary>
        /// Handle the delete item command - TODO #8 test.
        /// 
        /// INSTRUCTOR NOTE: This method demonstrates complex BST deletion.
        /// Shows the most challenging BST operation with structure maintenance.
        /// </summary>
        private void HandleDeleteCommand(string[] args)
        {
            string itemName;
            
            if (args.Length == 0)
            {
                Console.Write("Enter name of file or directory to delete: ");
                itemName = Console.ReadLine()?.Trim() ?? "";
            }
            else
            {
                itemName = args[0];
            }
            
            if (string.IsNullOrEmpty(itemName))
            {
                Console.WriteLine("❌ Invalid item name provided.");
                return;
            }
            
            Console.WriteLine();
            
            bool success = fileSystem.DeleteItem(itemName);
            
            if (success)
            {
                Console.WriteLine($"✅ Item deleted successfully: {itemName}");
                Console.WriteLine("🗑️  Complex BST deletion completed - tree structure maintained!");
            }
            else
            {
                Console.WriteLine($"❌ Failed to delete item: {itemName}");
                Console.WriteLine("🔍 Item not found in BST - nothing to delete.");
            }
        }
        
        /// <summary>
        /// Handle the display tree command.
        /// 
        /// INSTRUCTOR NOTE: Tree visualization helps students understand BST structure.
        /// Critical for debugging and verifying proper BST ordering.
        /// </summary>
        private void HandleDisplayTreeCommand()
        {
            Console.WriteLine("\n🌳 Current File System Tree Structure:");
            Console.WriteLine("=====================================");
            fileSystem.DisplayTree();
            Console.WriteLine("\n💡 This shows your BST structure - directories before files, then alphabetical!");
        }
        
        /// <summary>
        /// Handle the statistics command.
        /// 
        /// INSTRUCTOR NOTE: System statistics show the results of BST operations.
        /// Demonstrates the practical value of tree-based organization.
        /// </summary>
        private void HandleStatsCommand()
        {
            Console.WriteLine("\n📊 File System Statistics:");
            Console.WriteLine("===========================");
            
            var stats = fileSystem.GetStatistics();
            Console.WriteLine(stats.ToString());
            
            if (stats.TotalFiles + stats.TotalDirectories > 0)
            {
                Console.WriteLine("\n⚡ Performance Benefits of BST:");
                Console.WriteLine($"   - O(log n) search time for {stats.TotalFiles + stats.TotalDirectories} items");
                Console.WriteLine("   - Automatic sorting maintained during insertions");
                Console.WriteLine("   - Efficient range queries and filtering operations");
            }
        }
        
        /// <summary>
        /// Handle the load sample data command.
        /// 
        /// INSTRUCTOR NOTE: Sample data provides immediate testing material.
        /// Students can experiment with operations without manual data entry.
        /// </summary>
        private void HandleLoadSampleCommand()
        {
            Console.WriteLine("\n🎲 Loading sample file system data...");
            fileSystem.LoadSampleData();
            Console.WriteLine("\n💡 Now you can test search, filter, and analysis operations!");
        }
        
        /// <summary>
        /// Helper method for formatting file sizes in human-readable format.
        /// </summary>
        private static string FormatSize(long bytes)
        {
            if (bytes < 1024) return $"{bytes} B";
            if (bytes < 1024 * 1024) return $"{bytes / 1024:F1} KB";
            if (bytes < 1024 * 1024 * 1024) return $"{bytes / (1024 * 1024):F1} MB";
            return $"{bytes / (1024 * 1024 * 1024):F1} GB";
        }
    }
}