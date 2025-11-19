using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment8
{
    /// <summary>
    /// Main entry point for the Spell Checker application.
    /// This application demonstrates HashSet usage through interactive spell checking.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Simple Spell Checker & Vocabulary Explorer ===");
            Console.WriteLine("This application uses HashSet<string> for fast word lookups and analysis.\n");

            try
            {
                // Initialize the spell checker system
                var spellChecker = new SpellChecker();
                
                // Start the interactive navigator immediately
                var navigator = new SpellCheckerNavigator(spellChecker);
                navigator.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}