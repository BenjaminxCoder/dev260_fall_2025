namespace Assignment6
{
    /// <summary>
    /// Handles the interactive menu and user interface for the matchmaking system
    /// 
    /// INSTRUCTOR NOTE: This class is a complete implementation that demonstrates
    /// several important software engineering patterns and concepts:
    /// 
    /// 1. MVC Pattern: This is the "View/Controller" that manages user interaction
    /// 2. Separation of Concerns: UI logic is separate from business logic
    /// 3. Error Handling: Comprehensive try-catch blocks and input validation
    /// 4. User Experience: Clear menus, feedback, and error messages
    /// 
    /// Study this code to understand how a professional console application
    /// should be structured. Notice how it calls YOUR MatchmakingSystem methods!
    /// </summary>
    public class GameNavigator
    {
        // INSTRUCTOR NOTE: This is the core business logic component.
        // GameNavigator is responsible for UI, MatchmakingSystem handles the queues.
        // This demonstrates the "Composition" pattern - GameNavigator HAS-A MatchmakingSystem.
        private MatchmakingSystem matchmaking = new MatchmakingSystem();

        /// <summary>
        /// Start the interactive matchmaking application
        /// 
        /// INSTRUCTOR NOTE: This is the main application loop - a common pattern
        /// in console applications. Notice the structure:
        /// 1. Welcome message and setup
        /// 2. Main loop with menu display and input handling
        /// 3. Try-catch for error handling
        /// 4. Graceful exit with cleanup/summary
        /// </summary>
        public void StartApplication()
        {
            // INSTRUCTOR NOTE: Always start with a clear welcome message
            // that tells users what they're using and what to expect
            Console.WriteLine("🎮 Game Matchmaking System");
            Console.WriteLine("==========================");
            Console.WriteLine("Welcome to the online matchmaking simulator!");
            Console.WriteLine();

            // INSTRUCTOR NOTE: Create demo data so users can immediately test
            // the system without having to manually create everything first
            CreateDemoPlayers();

            // INSTRUCTOR NOTE: The main application loop - keep running until user exits
            bool running = true;
            while (running)
            {
                DisplayMainMenu();
                string choice = Console.ReadLine()?.ToLower() ?? "";

                Console.WriteLine($"You selected: {choice}");
                // INSTRUCTOR NOTE: Wrap all user operations in try-catch to prevent crashes
                // from invalid input or business logic errors
                try
                {
                    switch (choice)
                    {
                        case "1":
                        case "create":
                            HandleCreatePlayer();
                            break;
                        case "2":
                        case "queue":
                            HandleJoinQueue();
                            break;
                        case "3":
                        case "match":
                            HandleCreateMatches();
                            break;
                        case "4":
                        case "status":
                            // INSTRUCTOR NOTE: Direct call to YOUR MatchmakingSystem method
                            matchmaking.DisplayQueueStatus();
                            break;
                        case "5":
                        case "stats":
                            HandlePlayerStats();
                            break;
                        case "6":
                        case "history":
                            HandleMatchHistory();
                            break;
                        case "7":
                        case "system":
                            // INSTRUCTOR NOTE: Another direct call to YOUR implementation
                            Console.WriteLine(matchmaking.GetSystemStats());
                            break;
                        case "8":
                        case "exit":
                            running = false;
                            ShowGoodbye();
                            break;
                        default:
                            Console.WriteLine("❌ Invalid choice. Please try again.\n");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    // INSTRUCTOR NOTE: Always provide meaningful error messages to users
                    Console.WriteLine($"❌ Error: {ex.Message}\n");
                }

                // INSTRUCTOR NOTE: Pause and clear screen for better user experience
                if (running)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        // INSTRUCTOR NOTE: This method demonstrates good UI design principles:
        // 1. Clear visual hierarchy with borders and sections
        // 2. Multiple input methods (numbers OR words) for accessibility
        // 3. Real-time status information (player count, match count)
        // 4. Consistent formatting and layout
        private void DisplayMainMenu()
        {
            // INSTRUCTOR NOTE: These calls to YOUR MatchmakingSystem provide live data
            var allPlayers = matchmaking.GetAllPlayers();
            int totalPlayers = allPlayers.Count;
            int totalMatches = matchmaking.GetMatchHistory().Count;

            // INSTRUCTOR NOTE: ASCII box drawing creates a professional console interface
            Console.WriteLine("┌─ Game Matchmaking System ────────────────────────┐");
            Console.WriteLine("│ 1. Create Player  │ 2. Join Queue   │ 3. Match  │");
            Console.WriteLine("│ 4. Queue Status   │ 5. Player Stats │ 6. History│");
            Console.WriteLine("│ 7. System Stats   │ 8. Exit         │           │");
            Console.WriteLine("└───────────────────────────────────────────────────┘");
            Console.WriteLine($"Players: {totalPlayers} | Matches: {totalMatches}");
            Console.Write("\nChoose operation (number or name): ");
        }

        // INSTRUCTOR NOTE: This method demonstrates the complete user input cycle:
        // 1. Clear prompts and instructions
        // 2. Input validation with meaningful error messages
        // 3. Default values and error recovery
        // 4. Confirmation of successful operations
        // 5. Integration with business logic (YOUR MatchmakingSystem.CreatePlayer method)
        private void HandleCreatePlayer()
        {
            Console.WriteLine("\n🎮 Create New Player");
            Console.WriteLine("===================");

            // INSTRUCTOR NOTE: Always validate user input and provide clear error messages
            Console.Write("Enter username: ");
            string? username = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("❌ Username cannot be empty.");
                return;
            }

            // INSTRUCTOR NOTE: Use TryParse for safe numeric input handling
            Console.Write("Enter skill rating (1-10): ");
            if (!int.TryParse(Console.ReadLine(), out int skillRating) || skillRating < 1 || skillRating > 10)
            {
                Console.WriteLine("❌ Skill rating must be a number between 1 and 10.");
                return;
            }

            // INSTRUCTOR NOTE: Provide clear choices with explanations for each option
            Console.WriteLine("Select preferred game mode:");
            Console.WriteLine("1. Casual   - Any skill level, quick matches");
            Console.WriteLine("2. Ranked   - Skill-based competitive matches");
            Console.WriteLine("3. QuickPlay - Fast matching with loose skill requirements");
            Console.Write("Choose mode (1-3): ");

            // INSTRUCTOR NOTE: Use default values when input is invalid rather than failing
            GameMode preferredMode = GameMode.Casual;
            string? modeChoice = Console.ReadLine();
            switch (modeChoice)
            {
                case "1": preferredMode = GameMode.Casual; break;
                case "2": preferredMode = GameMode.Ranked; break;
                case "3": preferredMode = GameMode.QuickPlay; break;
                default:
                    Console.WriteLine("Invalid choice, defaulting to Casual mode.");
                    break;
            }

            try
            {
                // INSTRUCTOR NOTE: This calls YOUR MatchmakingSystem.CreatePlayer method!
                var player = matchmaking.CreatePlayer(username, skillRating, preferredMode);
                Console.WriteLine($"✅ Player '{player.Username}' created successfully!");
                Console.WriteLine($"📊 Skill: {skillRating}/10 | Preferred Mode: {preferredMode}");
            }
            catch (ArgumentException ex)
            {
                // INSTRUCTOR NOTE: Handle business logic errors gracefully
                Console.WriteLine($"❌ {ex.Message}");
            }
        }

        // INSTRUCTOR NOTE: This method shows how to handle complex multi-step operations:
        // 1. Check prerequisites (are there players?)
        // 2. Display available options dynamically
        // 3. Validate selections against available data
        // 4. Provide feedback and estimates for user experience
        private void HandleJoinQueue()
        {
            Console.WriteLine("\n⏳ Join Matchmaking Queue");
            Console.WriteLine("========================");

            // INSTRUCTOR NOTE: Always check prerequisites before proceeding
            var players = matchmaking.GetAllPlayers();
            if (players.Count == 0)
            {
                Console.WriteLine("❌ No players available. Create players first.");
                return;
            }

            // INSTRUCTOR NOTE: Display available options dynamically based on current state
            Console.WriteLine("Available players:");
            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {players[i]}");
            }

            // INSTRUCTOR NOTE: Validate array/list indexes to prevent crashes
            Console.Write("\nSelect player (number): ");
            if (!int.TryParse(Console.ReadLine(), out int playerIndex) || 
                playerIndex < 1 || playerIndex > players.Count)
            {
                Console.WriteLine("❌ Invalid player selection.");
                return;
            }

            var selectedPlayer = players[playerIndex - 1];

            // INSTRUCTOR NOTE: Consistent UI patterns make the app easier to learn
            Console.WriteLine("\nSelect game mode:");
            Console.WriteLine("1. Casual   - Any skill level matches");
            Console.WriteLine("2. Ranked   - Skill-based matches (±2 levels)");
            Console.WriteLine("3. QuickPlay - Fast matching");
            Console.Write("Choose mode (1-3): ");

            GameMode mode = GameMode.Casual;
            string? modeChoice = Console.ReadLine();
            switch (modeChoice)
            {
                case "1": mode = GameMode.Casual; break;
                case "2": mode = GameMode.Ranked; break;
                case "3": mode = GameMode.QuickPlay; break;
                default:
                    Console.WriteLine("Invalid choice, defaulting to Casual mode.");
                    break;
            }

            try
            {
                // INSTRUCTOR NOTE: These are YOUR MatchmakingSystem methods!
                matchmaking.AddToQueue(selectedPlayer, mode);
                string estimate = matchmaking.GetQueueEstimate(mode);
                Console.WriteLine($"✅ {selectedPlayer.Username} added to {mode} queue!");
                Console.WriteLine($"⏱️ Estimated wait time: {estimate}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error adding to queue: {ex.Message}");
            }
        }

        // INSTRUCTOR NOTE: This method demonstrates automated batch processing:
        // 1. Process multiple queues systematically
        // 2. Handle partial success scenarios gracefully
        // 3. Provide detailed feedback on what happened
        // 4. Guide users on next steps when no matches are possible
        private void HandleCreateMatches()
        {
            Console.WriteLine("\n🎯 Process Matchmaking");
            Console.WriteLine("=====================");

            // INSTRUCTOR NOTE: Process all three game modes systematically
            GameMode[] modes = { GameMode.Casual, GameMode.Ranked, GameMode.QuickPlay };
            int totalMatches = 0;

            foreach (var mode in modes)
            {
                Console.WriteLine($"\n🔄 Processing {mode} queue...");
                
                int modeMatches = 0;
                // INSTRUCTOR NOTE: Keep trying to create matches until no more are possible
                while (true)
                {
                    try
                    {
                        // INSTRUCTOR NOTE: These are YOUR key MatchmakingSystem methods!
                        var match = matchmaking.TryCreateMatch(mode);
                        if (match == null)  // No more matches possible in this mode
                            break;

                        matchmaking.ProcessMatch(match);
                        modeMatches++;
                        totalMatches++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Error creating {mode} match: {ex.Message}");
                        break;
                    }
                }

                // INSTRUCTOR NOTE: Provide meaningful feedback for each queue
                if (modeMatches == 0)
                {
                    Console.WriteLine($"ℹ️ No matches possible in {mode} queue");
                }
                else
                {
                    Console.WriteLine($"✅ Created {modeMatches} {mode} match(es)");
                }
            }

            // INSTRUCTOR NOTE: Summary and guidance for next steps
            Console.WriteLine($"\n🏆 Total matches created: {totalMatches}");
            if (totalMatches == 0)
            {
                Console.WriteLine("💡 Tip: Add more players to queues to create matches!");
            }
        }

        // INSTRUCTOR NOTE: Pattern for selecting from dynamic lists - reusable approach
        private void HandlePlayerStats()
        {
            Console.WriteLine("\n📊 Player Statistics");
            Console.WriteLine("===================");

            var players = matchmaking.GetAllPlayers();
            if (players.Count == 0)
            {
                Console.WriteLine("❌ No players available.");
                return;
            }

            Console.WriteLine("Select player:");
            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {players[i]}");
            }

            Console.Write("\nSelect player (number): ");
            if (!int.TryParse(Console.ReadLine(), out int playerIndex) || 
                playerIndex < 1 || playerIndex > players.Count)
            {
                Console.WriteLine("❌ Invalid player selection.");
                return;
            }

            var selectedPlayer = players[playerIndex - 1];
            try
            {
                // INSTRUCTOR NOTE: This calls YOUR MatchmakingSystem.DisplayPlayerStats method!
                matchmaking.DisplayPlayerStats(selectedPlayer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error displaying stats: {ex.Message}");
            }
        }

        // INSTRUCTOR NOTE: This method demonstrates data presentation patterns:
        // 1. Handle empty data gracefully
        // 2. Limit display to reasonable amounts (pagination concept)
        // 3. Show most recent items first (reverse chronological)
        // 4. Provide summary information for context
        private void HandleMatchHistory()
        {
            Console.WriteLine("\n📜 Match History");
            Console.WriteLine("===============");

            // INSTRUCTOR NOTE: This calls YOUR MatchmakingSystem.GetMatchHistory method!
            var matches = matchmaking.GetMatchHistory();
            if (matches.Count == 0)
            {
                Console.WriteLine("ℹ️ No matches have been played yet.");
                return;
            }

            Console.WriteLine($"Total matches: {matches.Count}\n");

            // INSTRUCTOR NOTE: Limit display to prevent information overload
            // TakeLast(10) gets the newest 10, Reverse() shows newest first
            var recentMatches = matches.TakeLast(10).Reverse();
            Console.WriteLine("Recent matches:");
            foreach (var match in recentMatches)
            {
                Console.WriteLine($"📋 {match.GetSummary()}");
            }

            // INSTRUCTOR NOTE: Inform users about additional data availability
            if (matches.Count > 10)
            {
                Console.WriteLine($"\n... and {matches.Count - 10} earlier matches");
            }
        }

        // INSTRUCTOR NOTE: Demo data creation is crucial for development and testing.
        // This method shows how to set up realistic test scenarios that cover
        // different use cases your MatchmakingSystem needs to handle.
        private void CreateDemoPlayers()
        {
            try
            {
                // INSTRUCTOR NOTE: Create diverse demo players that test different scenarios:
                // - Different skill levels (2-9) to test skill-based matching
                // - Different preferred modes to test all queue types
                // - Variety of names for clear identification during testing
                matchmaking.CreatePlayer("ProGamer", 9, GameMode.Ranked);
                matchmaking.CreatePlayer("Newbie", 2, GameMode.Casual);
                matchmaking.CreatePlayer("Average", 5, GameMode.QuickPlay);
                matchmaking.CreatePlayer("Skilled", 7, GameMode.Ranked);
                matchmaking.CreatePlayer("Beginner", 3, GameMode.Casual);
                matchmaking.CreatePlayer("Expert", 8, GameMode.Ranked);
                
                Console.WriteLine("✅ Demo players created for testing!");
                Console.WriteLine("💡 You can create additional players or use the existing ones.\n");
            }
            catch (Exception ex)
            {
                // INSTRUCTOR NOTE: Demo data creation shouldn't crash the app
                Console.WriteLine($"⚠️ Warning: Could not create demo players: {ex.Message}");
            }
        }

        // INSTRUCTOR NOTE: A good application always provides a proper goodbye experience
        // with useful summary information and encouragement for learning.
        private void ShowGoodbye()
        {
            Console.WriteLine("\n🎮 Thank you for using the Game Matchmaking System!");
            Console.WriteLine("================================================");
            
            // INSTRUCTOR NOTE: Show final statistics using YOUR GetSystemStats method
            var stats = matchmaking.GetSystemStats();
            Console.WriteLine(stats);
            
            // INSTRUCTOR NOTE: Connect the assignment to real-world applications
            Console.WriteLine("\n🎯 You've learned queue management and matchmaking algorithms!");
            Console.WriteLine("These concepts are used in real games like League of Legends, Overwatch,");
            Console.WriteLine("and many other online multiplayer systems.");
            Console.WriteLine("\nGood luck with your coding journey! 🚀");
        }
    }
}