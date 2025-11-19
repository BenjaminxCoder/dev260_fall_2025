using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment8
{
    /// <summary>
    /// Interactive menu navigator for the Spell Checker application.
    /// Provides a user-friendly interface for all spell checking operations.
    /// 
    /// INSTRUCTOR NOTE: This class demonstrates several important software engineering patterns:
    /// 1. MVC Pattern: This acts as the "View/Controller" managing user interaction
    /// 2. Separation of Concerns: UI logic is completely separate from business logic (SpellChecker)
    /// 3. Command Pattern: User input is parsed into commands that route to specific handlers
    /// 4. Professional Error Handling: Graceful handling of unimplemented methods and exceptions
    /// 5. User Experience Design: Clear prompts, help system, and educational feedback
    /// 
    /// This class calls YOUR SpellChecker methods to test each TODO implementation.
    /// Study how it provides comprehensive testing and feedback for student learning.
    /// </summary>
    public class SpellCheckerNavigator
    {
        // INSTRUCTOR NOTE: Using 'readonly' ensures the SpellChecker reference can't be changed
        // after construction, which prevents bugs and makes the code more predictable.
        // This demonstrates the "Composition" pattern - Navigator HAS-A SpellChecker.
        private readonly SpellChecker spellChecker;
        
        // INSTRUCTOR NOTE: Simple boolean flag to control the main application loop.
        // This is a common pattern for interactive console applications.
        private bool isRunning;
        
        // INSTRUCTOR NOTE: Constructor demonstrates good dependency injection practices.
        // We require a SpellChecker instance rather than creating one inside this class.
        // This makes testing easier and follows the "Dependency Inversion Principle".
        public SpellCheckerNavigator(SpellChecker spellChecker)
        {
            // Always validate constructor parameters to prevent null reference exceptions
            this.spellChecker = spellChecker ?? throw new ArgumentNullException(nameof(spellChecker));
            this.isRunning = true;
        }
        
        /// <summary>
        /// Main application loop that handles user input and coordinates spell checking operations.
        /// 
        /// INSTRUCTOR NOTE: This method demonstrates the classic "Read-Evaluate-Print Loop" (REPL)
        /// pattern used in many interactive applications. The structure is:
        /// 1. Welcome message and setup
        /// 2. Main loop with menu display and input handling
        /// 3. Comprehensive exception handling to prevent crashes
        /// 4. Clear screen and menu redisplay after each operation
        /// 
        /// </summary>
        public void Run()
        {
            // INSTRUCTOR NOTE: Clear welcome message sets educational context
            Console.WriteLine("📚 Spell Checker & Vocabulary Explorer");
            Console.WriteLine("======================================");
            Console.WriteLine("Welcome to the interactive spell checking application!");
            Console.WriteLine("Test your HashSet<T> implementations with real text analysis.");
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
                    ProcessCommand(choice.Trim());
                }
                catch (Exception ex)
                {
                    // INSTRUCTOR NOTE: Global exception handler prevents crashes and provides
                    // helpful feedback. This is crucial for student learning environments.
                    Console.WriteLine($"❌ Error: {ex.Message}");
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
        /// Notice how we support both numbers (1-6) for TODO methods AND descriptive names
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
            // Each numbered case (1-6) corresponds to a specific TODO method for easy testing.
            switch (command)
            {
                case "1":
                case "load":
                case "dictionary":
                    // INSTRUCTOR NOTE: Test TODO #1 - Dictionary loading with HashSet operations
                    HandleLoadDictionaryCommand();
                    break;
                    
                case "2":
                case "analyze":
                    // INSTRUCTOR NOTE: Test TODO #2 - Text file analysis and tokenization
                    HandleAnalyzeCommand(args);
                    break;
                    
                case "3":
                case "categorize":
                    // INSTRUCTOR NOTE: Test TODO #3 - Word categorization using HashSet.Contains()
                    HandleCategorizeCommand();
                    break;
                    
                case "4":
                case "check":
                    // INSTRUCTOR NOTE: Test TODO #4 - Individual word checking with occurrence counting
                    HandleCheckCommand(args);
                    break;
                    
                case "5":
                case "misspelled":
                    // INSTRUCTOR NOTE: Test TODO #5 - Misspelled word retrieval and formatting
                    HandleListMisspelledCommand();
                    break;
                    
                case "6":
                case "unique":
                    // INSTRUCTOR NOTE: Test TODO #6 - Unique word sampling and display
                    HandleListUniqueCommand();
                    break;
                    
                case "7":
                case "stats":
                    // INSTRUCTOR NOTE: Summary information about current analysis state
                    HandleStatsCommand();
                    break;
                    
                case "8":
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
        // 3. Real-time status information (dictionary and analysis state)
        // 4. Consistent formatting and layout
        private void DisplayMainMenu()
        {
            // INSTRUCTOR NOTE: These calls to YOUR SpellChecker provide live data
            int dictionarySize = spellChecker.DictionarySize;
            bool hasAnalysis = spellChecker.HasAnalyzedText;
            string fileName = hasAnalysis ? spellChecker.CurrentFileName : "None";

            // INSTRUCTOR NOTE: ASCII box drawing creates a professional console interface
            Console.WriteLine("┌─ Spell Checker & Vocabulary Explorer ────────────┐");
            Console.WriteLine("│ 1. Load Dictionary │ 2. Analyze Text  │ 3. Categorize │");
            Console.WriteLine("│ 4. Check Word      │ 5. Misspelled    │ 6. Unique     │");
            Console.WriteLine("│ 7. Statistics      │ 8. Exit          │               │");
            Console.WriteLine("└───────────────────────────────────────────────────┘");
            Console.WriteLine($"Dictionary: {(dictionarySize > 0 ? $"{dictionarySize:N0} words" : "Not loaded")} | Analysis: {fileName}");
            Console.Write("\nChoose operation (number or name): ");
        }

        /// <summary>
        /// Display a good application goodbye experience with useful summary information.
        /// 
        /// INSTRUCTOR NOTE: A good application always provides a proper goodbye experience
        /// with useful summary information and encouragement for learning.
        /// </summary>
        private void ShowGoodbye()
        {
            Console.WriteLine("\n📚 Thank you for using the Spell Checker & Vocabulary Explorer!");
            Console.WriteLine("=============================================================");
            
            // INSTRUCTOR NOTE: Show final statistics using YOUR SpellChecker properties
            Console.WriteLine($"Dictionary: {spellChecker.DictionarySize:N0} words loaded");
            if (spellChecker.HasAnalyzedText)
            {
                var stats = spellChecker.GetTextStats();
                Console.WriteLine($"Last Analysis: '{spellChecker.CurrentFileName}'");
                Console.WriteLine($"  - Total words: {stats.totalWords:N0}");
                Console.WriteLine($"  - Unique words: {stats.uniqueWords:N0}");
                Console.WriteLine($"  - Correctly spelled: {stats.correctWords:N0}");
                Console.WriteLine($"  - Misspelled: {stats.misspelledWords:N0}");
            }
            
            // INSTRUCTOR NOTE: Connect the assignment to real-world applications
            Console.WriteLine("\n🎯 You've learned HashSet operations and text analysis!");
            Console.WriteLine("These concepts are used in real applications like spell checkers,");
            Console.WriteLine("search engines, and text processing systems.");
            Console.WriteLine("\nGood luck with your coding journey! 🚀");
        }
        
        /// <summary>
        /// Handle the load dictionary command - TODO #1 test.
        /// 
        /// INSTRUCTOR NOTE: This method demonstrates several important patterns:
        /// 1. Clear educational feedback with visual success/failure indicators
        /// 2. Exception handling for both NotImplementedException and runtime errors
        /// 3. Informative error messages that guide student troubleshooting
        /// 4. Statistics display to show the scale of data processing
        /// 
        /// The method uses try-catch to handle both unimplemented TODOs and actual
        /// runtime errors, providing different feedback for each scenario.
        /// </summary>
        private void HandleLoadDictionaryCommand()
        {
            Console.WriteLine("\n=== Testing TODO #1: LoadDictionary() ===");
            Console.WriteLine("Attempting to load dictionary from 'dictionary.txt'...");
            
            try
            {
                // INSTRUCTOR NOTE: Test the student's LoadDictionary implementation
                // The boolean return helps to understand success vs. failure
                if (spellChecker.LoadDictionary("dictionary.txt"))
                {
                    Console.WriteLine($"✓ SUCCESS: Dictionary loaded successfully!");
                    Console.WriteLine($"  - Words in dictionary: {spellChecker.DictionarySize:N0}");
                    Console.WriteLine("  - Ready for text analysis operations");
                }
                else
                {
                    // INSTRUCTOR NOTE: Guide user toward common issues
                    Console.WriteLine("✗ FAILED: Dictionary could not be loaded.");
                    Console.WriteLine("  - Check that 'dictionary.txt' exists in the project directory");
                    Console.WriteLine("  - Verify your LoadDictionary() implementation");
                }
            }
            catch (NotImplementedException)
            {
                // INSTRUCTOR NOTE: Friendly reminder for unimplemented TODO methods
                Console.WriteLine("⚠️  TODO #1 not yet implemented!");
                Console.WriteLine("   Complete the LoadDictionary() method in SpellChecker.cs");
            }
            catch (Exception ex)
            {
                // INSTRUCTOR NOTE: Help debug actual implementation errors
                Console.WriteLine($"✗ ERROR: {ex.Message}");
                Console.WriteLine("  Check your LoadDictionary() implementation for bugs");
            }
            
            Console.WriteLine();
        }
        
        /// <summary>
        /// Handle the analyze command to load and process a text file - TODO #2 test.
        /// 
        /// INSTRUCTOR NOTE: This method demonstrates several key educational concepts:
        /// 1. Parameter validation and user guidance for command syntax
        /// 2. Integration testing between file I/O and HashSet operations
        /// 3. Educational feedback with statistics and next-step suggestions
        /// 4. Error handling for both missing implementations and runtime issues
        /// 
        /// Notice how this builds on TODO #1 and prepares for TODO #3-6.
        /// </summary>
        private void HandleAnalyzeCommand(string[] args)
        {
            Console.WriteLine("\n=== Testing TODO #2: AnalyzeTextFile() ===");
            
            // INSTRUCTOR NOTE: Parameter validation with helpful usage examples
            if (args.Length == 0)
            {
                Console.WriteLine("Please specify a filename to analyze.");
                Console.WriteLine("Usage: 2 <filename>  or  analyze <filename>");
                Console.WriteLine("Examples:");
                Console.WriteLine("  2 sample_text_1.txt");
                Console.WriteLine("  analyze sample_text_2.txt\n");
                return;
            }
            
            string filename = args[0];
            Console.WriteLine($"Attempting to analyze '{filename}'...");
            
            try
            {
                // INSTRUCTOR NOTE: Test the student's AnalyzeTextFile implementation
                if (spellChecker.AnalyzeTextFile(filename))
                {
                    Console.WriteLine($"✓ SUCCESS: Text file analyzed successfully!");
                    
                    // INSTRUCTOR NOTE: Show immediate value from the analysis
                    // This helps to understand what the code accomplished
                    var stats = spellChecker.GetTextStats();
                    Console.WriteLine($"  - Total words found: {stats.totalWords:N0}");
                    Console.WriteLine($"  - Unique words found: {stats.uniqueWords:N0}");
                    Console.WriteLine($"  - File: '{filename}' is now loaded for further analysis");
                    
                    // INSTRUCTOR NOTE: Guide toward the next logical steps
                    Console.WriteLine("\n💡 Next steps:");
                    Console.WriteLine("  - Type '3' to categorize words (TODO #3)");
                    Console.WriteLine("  - Type 'stats' for detailed information");
                }
                else
                {
                    // INSTRUCTOR NOTE: Help troubleshoot common file issues
                    Console.WriteLine($"✗ FAILED: Could not analyze '{filename}'");
                    Console.WriteLine("  - Check that the file exists in the project directory");
                    Console.WriteLine("  - Verify your AnalyzeTextFile() implementation");
                }
            }
            catch (NotImplementedException)
            {
                // INSTRUCTOR NOTE: Clear guidance for incomplete implementations
                Console.WriteLine("⚠️  TODO #2 not yet implemented!");
                Console.WriteLine("   Complete the AnalyzeTextFile() method in SpellChecker.cs");
            }
            catch (Exception ex)
            {
                // INSTRUCTOR NOTE: Debug help for implementation errors
                Console.WriteLine($"✗ ERROR: {ex.Message}");
                Console.WriteLine("  Check your AnalyzeTextFile() implementation for bugs");
            }
            
            Console.WriteLine();
        }
        
        /// <summary>
        /// Handle the categorize command - TODO #3 test.
        /// 
        /// INSTRUCTOR NOTE: This method demonstrates prerequisite checking and advanced analytics:
        /// 1. Validates that prerequisites (dictionary and text) are loaded before processing
        /// 2. Shows how HashSet operations can provide instant spelling analysis
        /// 3. Calculates and displays meaningful statistics (accuracy percentage)
        /// 4. Provides clear next-step guidance for continued exploration
        /// 
        /// This is where you can see the power of HashSet for fast membership testing.
        /// </summary>
        private void HandleCategorizeCommand()
        {
            Console.WriteLine("\n=== Testing TODO #3: CategorizeWords() ===");
            
            // INSTRUCTOR NOTE: Check prerequisites in logical order
            // This teaches about dependency relationships in data processing
            if (!spellChecker.HasAnalyzedText)
            {
                Console.WriteLine("⚠️  No text file has been analyzed yet!");
                Console.WriteLine("   Use command '2 <filename>' to analyze a text file first.");
                Console.WriteLine("   Example: 2 sample_text_1.txt\n");
                return;
            }
            
            if (spellChecker.DictionarySize == 0)
            {
                Console.WriteLine("⚠️  No dictionary loaded yet!");
                Console.WriteLine("   Use command '1' to load the dictionary first.\n");
                return;
            }
            
            Console.WriteLine("Attempting to categorize words...");
            
            try
            {
                // INSTRUCTOR NOTE: Test the core HashSet membership operation
                spellChecker.CategorizeWords();
                Console.WriteLine("✓ SUCCESS: Words categorized successfully!");
                
                // INSTRUCTOR NOTE: Show the immediate value of the analysis
                // These statistics demonstrate the effectiveness of HashSet operations
                var stats = spellChecker.GetTextStats();
                Console.WriteLine($"  - Correctly spelled: {stats.correctWords:N0} words");
                Console.WriteLine($"  - Misspelled: {stats.misspelledWords:N0} words");
                
                // INSTRUCTOR NOTE: Calculate accuracy percentage for educational insight
                if (stats.uniqueWords > 0)
                {
                    double accuracy = (double)stats.correctWords / stats.uniqueWords * 100;
                    Console.WriteLine($"  - Spelling accuracy: {accuracy:F1}%");
                }
                
                // INSTRUCTOR NOTE: Guide toward the next logical steps
                Console.WriteLine("\n💡 Next steps:");
                Console.WriteLine("  - Type '5' to see misspelled words (TODO #5)");
                Console.WriteLine("  - Type '6' to see unique words sample (TODO #6)");
                Console.WriteLine("  - Type 'stats' for comprehensive analysis");
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("⚠️  TODO #3 not yet implemented!");
                Console.WriteLine("   Complete the CategorizeWords() method in SpellChecker.cs");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ ERROR: {ex.Message}");
                Console.WriteLine("  Check your CategorizeWords() implementation for bugs");
            }
            
            Console.WriteLine();
        }
        
        /// <summary>
        /// Handle the check command to validate individual words - TODO #4 test.
        /// 
        /// INSTRUCTOR NOTE: This method demonstrates individual word lookup operations:
        /// 1. Parameter validation with helpful usage examples
        /// 2. HashSet Contains() operation for fast dictionary lookup
        /// 3. Integration between dictionary and text analysis data
        /// 4. Conditional display based on available data (graceful degradation)
        /// 
        /// You see both dictionary checking and frequency analysis in action.
        /// </summary>
        private void HandleCheckCommand(string[] args)
        {
            Console.WriteLine("\n=== Testing TODO #4: CheckWord() ===");
            
            // INSTRUCTOR NOTE: Parameter validation with clear examples
            if (args.Length == 0)
            {
                Console.WriteLine("Please specify a word to check.");
                Console.WriteLine("Usage: 4 <word>  or  check <word>");
                Console.WriteLine("Examples:");
                Console.WriteLine("  4 hello");
                Console.WriteLine("  check programming\n");
                return;
            }
            
            string word = args[0];
            Console.WriteLine($"Checking word: '{word}'");
            
            try
            {
                // INSTRUCTOR NOTE: Test individual word lookup - core HashSet operation
                var result = spellChecker.CheckWord(word);
                
                Console.WriteLine("✓ SUCCESS: Word check completed!");
                Console.WriteLine($"  - In dictionary: {(result.inDictionary ? "✓ YES" : "✗ NO")}");
                
                // INSTRUCTOR NOTE: Show integration between different data structures
                // This demonstrates how HashSet and Dictionary work together
                if (spellChecker.HasAnalyzedText)
                {
                    Console.WriteLine($"  - Found in analyzed text: {(result.inText ? "✓ YES" : "✗ NO")}");
                    if (result.inText)
                    {
                        Console.WriteLine($"  - Occurrences in text: {result.occurrences}");
                    }
                }
                else
                {
                    // INSTRUCTOR NOTE: Graceful handling when prerequisites aren't met
                    Console.WriteLine("  - Text analysis: No file analyzed yet");
                    Console.WriteLine("    (Use '2 <filename>' to analyze text for occurrence counting)");
                }
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("⚠️  TODO #4 not yet implemented!");
                Console.WriteLine("   Complete the CheckWord() method in SpellChecker.cs");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ ERROR: {ex.Message}");
                Console.WriteLine("  Check your CheckWord() implementation for bugs");
            }
            
            Console.WriteLine();
        }
        
        /// <summary>
        /// Handle the list-misspelled command to show spelling errors - TODO #5 test.
        /// 
        /// INSTRUCTOR NOTE: This method demonstrates collection processing and user-friendly output:
        /// 1. Prerequisite checking for proper workflow
        /// 2. HashSet enumeration and presentation strategies
        /// 3. Dynamic column formatting based on console width
        /// 4. Positive feedback when no errors are found
        /// 5. Truncation handling for large result sets
        /// 
        /// You see how to present HashSet data in readable, professional formats.
        /// </summary>
        private void HandleListMisspelledCommand()
        {
            Console.WriteLine("\n=== Testing TODO #5: GetMisspelledWords() ===");
            
            // INSTRUCTOR NOTE: Ensure prerequisite analysis has been completed
            if (!spellChecker.HasAnalyzedText)
            {
                Console.WriteLine("⚠️  No text file has been analyzed yet!");
                Console.WriteLine("   Use '2 <filename>' to analyze a text file first.\n");
                return;
            }
            
            Console.WriteLine("Retrieving misspelled words...");
            
            try
            {
                // INSTRUCTOR NOTE: Test HashSet enumeration and filtering
                var misspelledWords = spellChecker.GetMisspelledWords();
                
                Console.WriteLine("✓ SUCCESS: Misspelled words retrieved!");
                
                // INSTRUCTOR NOTE: Handle empty results with positive reinforcement
                if (misspelledWords.Count == 0)
                {
                    Console.WriteLine("🎉 Excellent! No misspelled words found in the analyzed text.");
                }
                else
                {
                    // INSTRUCTOR NOTE: Professional data presentation with dynamic formatting
                    Console.WriteLine($"  - Found {misspelledWords.Count} misspelled words:");
                    Console.WriteLine(new string('=', 50));
                    
                    // INSTRUCTOR NOTE: Responsive column layout based on console width
                    // This teaches you to consider user experience in console apps
                    int columns = Math.Min(4, Math.Max(1, Console.WindowWidth / 15));
                    for (int i = 0; i < misspelledWords.Count; i++)
                    {
                        if (i > 0 && i % columns == 0)
                            Console.WriteLine();
                            
                        Console.Write($"{misspelledWords[i],-15}");
                    }
                    Console.WriteLine("\n" + new string('=', 50));
                    
                    // INSTRUCTOR NOTE: Handle large datasets gracefully
                    if (misspelledWords.Count >= 50)
                    {
                        Console.WriteLine("Note: Only showing first 50 misspelled words. There may be more.");
                    }
                }
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("⚠️  TODO #5 not yet implemented!");
                Console.WriteLine("   Complete the GetMisspelledWords() method in SpellChecker.cs");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ ERROR: {ex.Message}");
                Console.WriteLine("  Check your GetMisspelledWords() implementation for bugs");
            }
            
            Console.WriteLine();
        }
        
        /// <summary>
        /// Handle the list-unique command to show sample unique words - TODO #6 test.
        /// 
        /// INSTRUCTOR NOTE: This method demonstrates HashSet sampling and data presentation:
        /// 1. Shows how to extract representative samples from large datasets
        /// 2. Combines statistics with actual data for comprehensive insight
        /// 3. Uses responsive formatting for professional console output
        /// 4. Provides context about total dataset size vs. sample shown
        /// 
        /// You learn about practical data sampling and user-friendly presentation.
        /// </summary>
        private void HandleListUniqueCommand()
        {
            Console.WriteLine("\n=== Testing TODO #6: GetUniqueWordsSample() ===");
            
            // INSTRUCTOR NOTE: Prerequisite validation for proper workflow
            if (!spellChecker.HasAnalyzedText)
            {
                Console.WriteLine("⚠️  No text file has been analyzed yet!");
                Console.WriteLine("   Use '2 <filename>' to analyze a text file first.\n");
                return;
            }
            
            Console.WriteLine("Retrieving unique words sample...");
            
            try
            {
                // INSTRUCTOR NOTE: Test HashSet sampling and statistics integration
                var stats = spellChecker.GetTextStats();
                var uniqueWordsSample = spellChecker.GetUniqueWordsSample();
                
                Console.WriteLine("✓ SUCCESS: Unique words sample retrieved!");
                
                // INSTRUCTOR NOTE: Show context - total count vs. sample size
                Console.WriteLine($"  - Total unique words found: {stats.uniqueWords:N0}");
                Console.WriteLine($"  - Showing sample of {uniqueWordsSample.Count} words:");
                Console.WriteLine(new string('=', 50));
                
                // INSTRUCTOR NOTE: Professional column-based display layout
                // This demonstrates responsive UI design in console applications
                int columns = Math.Min(4, Math.Max(1, Console.WindowWidth / 15));
                for (int i = 0; i < uniqueWordsSample.Count; i++)
                {
                    if (i > 0 && i % columns == 0)
                        Console.WriteLine();
                        
                    Console.Write($"{uniqueWordsSample[i],-15}");
                }
                Console.WriteLine("\n" + new string('=', 50));
                
                // INSTRUCTOR NOTE: Educational context about sampling vs. full dataset
                if (stats.uniqueWords > 20)
                {
                    Console.WriteLine($"Note: Showing sample of 20 from {stats.uniqueWords:N0} total unique words.");
                }
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("⚠️  TODO #6 not yet implemented!");
                Console.WriteLine("   Complete the GetUniqueWordsSample() method in SpellChecker.cs");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ ERROR: {ex.Message}");
                Console.WriteLine("  Check your GetUniqueWordsSample() implementation for bugs");
            }
            
            Console.WriteLine();
        }
        
        /// <summary>
        /// Handle the stats command to show comprehensive analysis statistics.
        /// 
        /// INSTRUCTOR NOTE: This method demonstrates comprehensive data analysis presentation:
        /// 1. Shows both raw statistics and calculated metrics (accuracy percentage)
        /// 2. Provides educational context about HashSet performance characteristics
        /// 3. Uses proper number formatting for professional output
        /// 4. Gracefully handles cases where analysis hasn't been performed
        /// 
        /// You see the value of your HashSet implementation through concrete metrics.
        /// </summary>
        private void HandleStatsCommand()
        {
            Console.WriteLine($"\n=== Spell Checker Statistics ===");
            Console.WriteLine($"Dictionary: {spellChecker.DictionarySize:N0} words loaded");
            
            // INSTRUCTOR NOTE: Handle case where no text analysis has been performed
            if (!spellChecker.HasAnalyzedText)
            {
                Console.WriteLine("Text Analysis: No file analyzed yet");
                Console.WriteLine("\nUse 'analyze <filename>' to load a text file for analysis.\n");
                return;
            }
            
            // INSTRUCTOR NOTE: Display comprehensive statistics from all TODO implementations
            var stats = spellChecker.GetTextStats();
            
            Console.WriteLine($"Current File: {spellChecker.CurrentFileName}");
            Console.WriteLine();
            Console.WriteLine("Text Analysis Results:");
            Console.WriteLine($"  Total words: {stats.totalWords:N0}");
            Console.WriteLine($"  Unique words: {stats.uniqueWords:N0}");
            Console.WriteLine($"  Correctly spelled: {stats.correctWords:N0}");
            Console.WriteLine($"  Misspelled: {stats.misspelledWords:N0}");
            
            // INSTRUCTOR NOTE: Calculate and display meaningful accuracy metrics
            if (stats.uniqueWords > 0)
            {
                double accuracyRate = (double)stats.correctWords / stats.uniqueWords * 100;
                Console.WriteLine($"  Spelling accuracy: {accuracyRate:F1}%");
            }
            
            Console.WriteLine();
            
            // INSTRUCTOR NOTE: Educational context about HashSet performance benefits
            // This reinforces the theoretical concepts through practical application
            Console.WriteLine("HashSet Performance Benefits:");
            Console.WriteLine($"  Dictionary lookups: O(1) constant time");
            Console.WriteLine($"  Word uniqueness: Automatic duplicate removal");
            Console.WriteLine($"  Memory efficient: No duplicate storage");
            Console.WriteLine();
        }
    }
}