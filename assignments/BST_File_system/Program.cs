using System;

namespace FileSystemNavigator
{
    /// <summary>
    /// Main entry point for the BST File System Navigator application.
    /// 
    /// INSTRUCTOR NOTE: This demonstrates the MVC pattern where:
    /// - Model: FileSystemBST (business logic and data)
    /// - View/Controller: FileSystemNavigator (user interface)
    /// - Main: Application bootstrapping and error handling
    /// 
    /// The clean separation makes the code maintainable and testable.
    /// Students can focus on BST algorithms without UI complexity.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("🗂️  BST File System Navigator - Assignment 9");
            Console.WriteLine("=============================================");
            Console.WriteLine("Testing Binary Search Tree implementations with file operations\\n");
            
            try
            {
                // INSTRUCTOR NOTE: Dependency injection pattern
                // Create the model (FileSystemBST) first, then inject it into the view/controller
                var fileSystem = new FileSystemBST();
                var navigator = new FileSystemNavigator(fileSystem);
                
                // Start the interactive application
                navigator.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Fatal Application Error: {ex.Message}");
                Console.WriteLine("\n🔧 Debug Information:");
                Console.WriteLine($"   Error Type: {ex.GetType().Name}");
                Console.WriteLine($"   Stack Trace: {ex.StackTrace}");
                
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
        }
    }
}