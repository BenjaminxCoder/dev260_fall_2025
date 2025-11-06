namespace Assignment6
{
    /// <summary>
    /// Represents a completed match between players
    /// 
    /// INSTRUCTOR NOTE: This class models what happens AFTER your MatchmakingSystem
    /// finds two players to match together. It handles the simulation of the actual
    /// game and tracks the results. This is a complete implementation for you to study.
    /// 
    /// Key concepts demonstrated here:
    /// - Probability calculations based on skill differences
    /// - Random outcome generation with weighted probabilities
    /// - Automatic player stat updates after matches
    /// - Quality metrics for evaluating matchmaking effectiveness
    /// </summary>
    public class Match
    {
        // INSTRUCTOR NOTE: These properties store all the essential match data.
        // Notice how we use nullable types (Player?) for Winner/Loser since
        // they're only set after the match is simulated.
        
        public Player Player1 { get; set; }         // First player in the match
        public Player Player2 { get; set; }         // Second player in the match
        public GameMode Mode { get; set; }          // Which queue they came from
        public Player? Winner { get; set; }         // Set after simulation
        public Player? Loser { get; set; }          // Set after simulation
        public DateTime MatchTime { get; set; }     // When the match occurred
        public double WinProbability { get; set; }  // Calculated win chance for winner
        public int SkillDifference { get; set; }    // Absolute skill gap between players

        // INSTRUCTOR NOTE: The constructor sets up the match but doesn't simulate it yet.
        // This separation allows us to create a Match object and then decide when to run
        // the simulation. Notice the null checking - defensive programming in action!
        public Match(Player player1, Player player2, GameMode mode)
        {
            // Validate that we have actual players (not null)
            Player1 = player1 ?? throw new ArgumentNullException(nameof(player1));
            Player2 = player2 ?? throw new ArgumentNullException(nameof(player2));
            
            Mode = mode;
            MatchTime = DateTime.Now;  // Record when this match was created
            
            // Calculate and store the skill gap for quality metrics
            SkillDifference = Math.Abs(player1.SkillRating - player2.SkillRating);
        }

        /// <summary>
        /// Simulate the match outcome based on skill difference
        /// 
        /// INSTRUCTOR NOTE: This method demonstrates several advanced programming concepts:
        /// 1. Probability calculations and mathematical modeling
        /// 2. Random number generation with weighted outcomes
        /// 3. Boundary/range clamping with Math.Min/Max
        /// 4. State management (updating multiple objects)
        /// 
        /// The algorithm creates realistic but still somewhat unpredictable outcomes.
        /// </summary>
        public void SimulateOutcome()
        {
            // INSTRUCTOR NOTE: Calculate Player1's advantage based on skill difference.
            // Each skill point difference gives a 10% advantage (0.1 multiplier).
            // This creates a linear relationship between skill gap and win probability.
            double skillAdvantage = (Player1.SkillRating - Player2.SkillRating) * 0.1;
            WinProbability = 0.5 + skillAdvantage;  // Start at 50/50, adjust by skill
            
            // INSTRUCTOR NOTE: Clamp the probability between 10% and 90%.
            // This ensures that even massive skill differences leave some chance
            // for upsets, keeping matches interesting and realistic.
            WinProbability = Math.Max(0.1, Math.Min(0.9, WinProbability));

            // INSTRUCTOR NOTE: Use Random to determine the actual outcome.
            // NextDouble() returns 0.0 to 1.0, so if it's less than our
            // calculated probability, Player1 wins.
            var random = new Random();
            bool player1Wins = random.NextDouble() < WinProbability;

            // Set winner and loser based on the random outcome
            if (player1Wins)
            {
                Winner = Player1;
                Loser = Player2;
            }
            else
            {
                Winner = Player2;
                Loser = Player1;
                // INSTRUCTOR NOTE: Flip the probability for display purposes
                // since Player2 actually won (shows their win probability)
                WinProbability = 1.0 - WinProbability;
            }

            // INSTRUCTOR NOTE: Automatically update both players' stats.
            // This ensures the skill ratings and win/loss records stay current.
            // The Player.UpdateSkillRating() method handles the details.
            Winner.UpdateSkillRating(true);   // Winner gains skill/wins
            Loser.UpdateSkillRating(false);   // Loser loses skill, gains loss
        }

        /// <summary>
        /// Get match quality rating based on skill difference
        /// 
        /// INSTRUCTOR NOTE: This method demonstrates switch expressions (C# 8 feature).
        /// It provides a simple way to evaluate how good your matchmaking algorithm is.
        /// Lower skill differences = higher quality matches = better player experience.
        /// </summary>
        public string GetMatchQuality()
        {
            return SkillDifference switch
            {
                0 => "Perfect",     // Identical skill levels
                1 => "Excellent",   // Very close match
                2 => "Good",        // Still quite balanced
                3 => "Fair",        // Noticeable difference
                4 => "Poor",        // Significant gap
                _ => "Very Poor"    // Large skill disparity
            };
        }

        /// <summary>
        /// Get competitive balance indicator
        /// 
        /// INSTRUCTOR NOTE: This calculates how "fair" the match was by looking at
        /// win probabilities. A 90% vs 10% match is unbalanced, while 60% vs 40%
        /// is much more competitive. This helps evaluate matchmaking effectiveness.
        /// </summary>
        public string GetCompetitiveBalance()
        {
            // Calculate the balance rating (higher = more balanced)
            // We take the lower probability and double it
            double balanceRating = Math.Min(WinProbability, 1.0 - WinProbability) * 2;
            
            return balanceRating switch
            {
                >= 0.8 => "Very Balanced",      // 40%+ chance for underdog
                >= 0.6 => "Balanced",           // 30%+ chance for underdog
                >= 0.4 => "Somewhat Balanced",  // 20%+ chance for underdog
                >= 0.2 => "Unbalanced",         // 10%+ chance for underdog
                _ => "Very Unbalanced"          // Less than 10% chance for underdog
            };
        }

        // INSTRUCTOR NOTE: ToString() provides a quick, readable summary of the match.
        // Notice the null checking - we handle both simulated and unsimulated matches.
        // This is used in various places throughout the UI for displaying match results.
        public override string ToString()
        {
            return Winner != null && Loser != null 
                ? $"{Winner.Username} defeated {Loser.Username} ({Mode})"
                : $"Match not yet processed ({Mode})";
        }

        /// <summary>
        /// Get detailed match information for display
        /// 
        /// INSTRUCTOR NOTE: This method shows advanced string formatting techniques:
        /// - Raw string literals (""") for multi-line text
        /// - Emoji for visual appeal in console apps
        /// - Conditional formatting based on object state
        /// - Multiple formatting patterns (:P1 for percentage, :HH:mm:ss for time)
        /// </summary>
        public string ToDetailedString()
        {
            // Handle case where match hasn't been simulated yet
            if (Winner == null || Loser == null)
                return "Match not yet processed";

            return $"""
                🎮 Match Result ({Mode})
                ========================
                🏆 Winner: {Winner.Username} (Skill: {Winner.SkillRating})
                😞 Loser: {Loser.Username} (Skill: {Loser.SkillRating})
                
                📊 Match Details:
                - Skill Difference: {SkillDifference}
                - Match Quality: {GetMatchQuality()}
                - Competitive Balance: {GetCompetitiveBalance()}
                - Win Probability: {WinProbability:P1}
                - Match Time: {MatchTime:HH:mm:ss}
                """;
        }

        /// <summary>
        /// Get summary string for match history
        /// 
        /// INSTRUCTOR NOTE: This provides a compact format for displaying many matches
        /// in a list or table. It's designed to be scannable and informative while
        /// taking up minimal screen space.
        /// </summary>
        public string GetSummary()
        {
            if (Winner == null || Loser == null)
                return $"{MatchTime:MM/dd HH:mm} | Match not processed | {Mode}";

            return $"{MatchTime:MM/dd HH:mm} | {Winner.Username} beat {Loser.Username} | {Mode} | Quality: {GetMatchQuality()}";
        }
    }
}