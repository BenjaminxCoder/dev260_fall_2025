namespace Assignment6
{
    /// <summary>
    /// Entry point for the Game Matchmaking System
    /// 
    /// INSTRUCTOR NOTE: This is the main entry point for your application.
    /// While it looks simple, it demonstrates several important programming concepts
    /// that you should understand as you work on this assignment.
    /// </summary>
    class Program
    {
        // INSTRUCTOR NOTE: The Main method is where your program starts execution.
        // The 'static' keyword means this method belongs to the class itself,
        // not to any particular instance of the class. This is required for entry points.
        static void Main(string[] args)
        {
            // INSTRUCTOR NOTE: We use a try-catch block here for "top-level" error handling.
            // This is a safety net that catches any unhandled exceptions that might
            // bubble up from deeper in the application.
            try
            {
                // Create the main application controller
                // INSTRUCTOR NOTE: GameNavigator is our "controller" class that manages
                // the user interface and coordinates between different parts of the system.
                // This follows the Single Responsibility Principle - Program.cs just
                // starts the app, GameNavigator handles the UI and flow.
                var navigator = new GameNavigator();
                
                // Start the main application loop
                // INSTRUCTOR NOTE: StartApplication() contains the main menu loop
                // and handles all user interactions. By calling it here, we transfer
                // control to the GameNavigator class.
                navigator.StartApplication();
            }
            catch (Exception ex)
            {
                // INSTRUCTOR NOTE: If something goes catastrophically wrong anywhere
                // in the application, we'll end up here. This provides a graceful
                // way to show the error and exit, rather than crashing abruptly.
                
                Console.WriteLine($"❌ Fatal error: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                
                // INSTRUCTOR NOTE: After showing the error, the program will naturally
                // exit when this method ends. The user gets a chance to read the error
                // message before the console window closes.
            }
        }
    }
}