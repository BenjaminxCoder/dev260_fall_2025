namespace Assignment6
{
    /// <summary>
    /// Represents a player in the matchmaking system with stats and preferences
    /// 
    /// INSTRUCTOR NOTE: This is a complete implementation for you to study and understand.
    /// Notice how we encapsulate all player data and behavior in one cohesive class.
    /// This follows the Single Responsibility Principle - the Player class is responsible
    /// for managing everything related to an individual player.
    /// </summary>
    public class Player
    {
        // INSTRUCTOR NOTE: These properties represent the core data we need to track
        // for each player. Notice how we use public properties with getters/setters
        // for easy access while maintaining encapsulation.
        
        public string Username { get; set; }        // Unique identifier for the player
        public int SkillRating { get; set; }        // 1-10 skill level for matchmaking
        public int Wins { get; set; }               // Total victories
        public int Losses { get; set; }             // Total defeats
        public GameMode PreferredMode { get; set; }  // Which queue they like to play
        
        // INSTRUCTOR NOTE: These DateTime properties help us track timing:
        // - JoinedQueue: When they entered the current queue (for wait time calculations)
        // - LastMatchTime: When they last played (could be used for activity tracking)
        public DateTime JoinedQueue { get; set; }
        public DateTime LastMatchTime { get; set; }

        // INSTRUCTOR NOTE: This constructor demonstrates good validation practices.
        // Always validate input parameters to prevent invalid state!
        // Notice how we use meaningful error messages and reasonable default values.
        public Player(string username, int skillRating, GameMode preferredMode = GameMode.Casual)
        {
            // Validate username - can't be null, empty, or just whitespace
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty");
            
            // Validate skill rating - must be within our 1-10 scale
            if (skillRating < 1 || skillRating > 10)
                throw new ArgumentException("Skill rating must be between 1 and 10");

            // Set the validated values
            Username = username;
            SkillRating = skillRating;
            PreferredMode = preferredMode;  // Default parameter makes this optional
            
            // Initialize stats to zero for new players
            Wins = 0;
            Losses = 0;
            
            // DateTime.MinValue is a good "null" value for DateTime
            // It clearly indicates "never happened" or "not set"
            LastMatchTime = DateTime.MinValue;
        }

        /// <summary>
        /// Calculate win rate as a percentage
        /// 
        /// INSTRUCTOR NOTE: This is a "computed property" - it calculates a value
        /// based on other properties rather than storing it separately.
        /// This ensures the win rate is always accurate and up-to-date.
        /// Notice the ternary operator: condition ? true_value : false_value
        /// </summary>
        public double WinRate => TotalMatches > 0 ? (double)Wins / TotalMatches * 100 : 0;

        /// <summary>
        /// Total number of matches played
        /// 
        /// INSTRUCTOR NOTE: Another computed property. This makes our code more readable
        /// and reduces the chance of calculation errors elsewhere in the code.
        /// </summary>
        public int TotalMatches => Wins + Losses;

        /// <summary>
        /// Update skill rating after a match (minimum 1, maximum 10)
        /// 
        /// INSTRUCTOR NOTE: This method demonstrates several important concepts:
        /// 1. Encapsulation - we control how skill rating changes
        /// 2. Bounds checking - using Math.Min/Max to enforce limits
        /// 3. Side effects - this method updates multiple properties at once
        /// 4. State management - keeping track of when the last match occurred
        /// </summary>
        public void UpdateSkillRating(bool won)
        {
            if (won)
            {
                // Winner gains 1 skill point, but can't exceed maximum of 10
                SkillRating = Math.Min(10, SkillRating + 1);
                Wins++;
            }
            else
            {
                // Loser loses 1 skill point, but can't go below minimum of 1
                SkillRating = Math.Max(1, SkillRating - 1);
                Losses++;
            }
            
            // Always update the timestamp regardless of win/loss
            LastMatchTime = DateTime.Now;
        }

        /// <summary>
        /// Get formatted wait time in queue
        /// 
        /// INSTRUCTOR NOTE: This method shows how to work with TimeSpan for duration calculations.
        /// Notice how we handle the special case where the player isn't in queue,
        /// and how we format the output differently based on the duration.
        /// </summary>
        public string GetQueueTime()
        {
            // Check if player is actually in a queue
            if (JoinedQueue == DateTime.MinValue)
                return "Not in queue";
            
            // Calculate how long they've been waiting
            var waitTime = DateTime.Now - JoinedQueue;
            
            // Format the display based on duration (seconds vs minutes)
            return waitTime.TotalMinutes < 1 
                ? $"{waitTime.Seconds}s"                    // Short waits: just seconds
                : $"{waitTime.Minutes}m {waitTime.Seconds}s"; // Longer waits: minutes + seconds
        }

        /// <summary>
        /// Mark player as joining a queue
        /// 
        /// INSTRUCTOR NOTE: Simple but important method for state management.
        /// This timestamp will be used to calculate wait times and potentially
        /// for queue timeout logic in your MatchmakingSystem implementation.
        /// </summary>
        public void JoinQueue()
        {
            JoinedQueue = DateTime.Now;
        }

        /// <summary>
        /// Mark player as leaving queue
        /// 
        /// INSTRUCTOR NOTE: We reset to DateTime.MinValue to indicate "not in queue".
        /// This is a common pattern for representing "null" dates without using nullable types.
        /// </summary>
        public void LeaveQueue()
        {
            JoinedQueue = DateTime.MinValue;
        }

        // INSTRUCTOR NOTE: ToString() override is crucial for debugging and display.
        // This provides a concise, readable representation of the player.
        // The GameNavigator uses this when showing queue contents.
        public override string ToString()
        {
            return $"{Username} (Skill: {SkillRating}, W/L: {Wins}/{Losses})";
        }

        /// <summary>
        /// Get detailed player information for display
        /// 
        /// INSTRUCTOR NOTE: This method demonstrates C# 11's raw string literals (""").
        /// Raw strings are perfect for multi-line formatted text like this.
        /// Notice how we use conditional formatting for the "Last Match" field.
        /// </summary>
        public string ToDetailedString()
        {
            return $"""
                Player: {Username}
                Skill Rating: {SkillRating}/10
                Record: {Wins}W - {Losses}L ({WinRate:F1}% win rate)
                Total Matches: {TotalMatches}
                Preferred Mode: {PreferredMode}
                Last Match: {(LastMatchTime == DateTime.MinValue ? "Never" : LastMatchTime.ToString("MM/dd HH:mm"))}
                """;
        }
    }

    /// <summary>
    /// Game modes available for matchmaking
    /// 
    /// INSTRUCTOR NOTE: This enum defines the three different matchmaking strategies
    /// you'll need to implement in your MatchmakingSystem class:
    /// 
    /// - Casual: Pure FIFO (First In, First Out) - just like a queue at the store
    /// - Ranked: Skill-based matching - only match players within ±2 skill levels
    /// - QuickPlay: Hybrid approach - try skill matching first, but broaden if needed
    /// 
    /// Each mode represents a different balance between match quality and wait time.
    /// </summary>
    public enum GameMode
    {
        Casual,    // Any skill level, FIFO matching (fastest queues)
        Ranked,    // Skill-based matching (±2 levels) (highest quality matches)
        QuickPlay  // Prefer skill matching, but allow broader matching for speed (balanced)
    }
}