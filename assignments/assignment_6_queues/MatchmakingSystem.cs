namespace Assignment6
{
    /// <summary>
    /// Main matchmaking system managing queues and matches
    /// Students implement the core methods in this class
    /// </summary>
    public class MatchmakingSystem
    {
        // Data structures for managing the matchmaking system
        private Queue<Player> casualQueue = new Queue<Player>();
        private Queue<Player> rankedQueue = new Queue<Player>();
        private Queue<Player> quickPlayQueue = new Queue<Player>();
        private List<Player> allPlayers = new List<Player>();
        private List<Match> matchHistory = new List<Match>();

        // Track when each user entered a queue for display purposes
        private readonly Dictionary<string, DateTime> enqueuedAt = new(StringComparer.OrdinalIgnoreCase);

        // Statistics tracking
        private int totalMatches = 0;
        private DateTime systemStartTime = DateTime.Now;

        /// <summary>
        /// Create a new player and add to the system
        /// </summary>
        public Player CreatePlayer(string username, int skillRating, GameMode preferredMode = GameMode.Casual)
        {
            // Check for duplicate usernames
            if (allPlayers.Any(p => p.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException($"Player with username '{username}' already exists");
            }

            var player = new Player(username, skillRating, preferredMode);
            allPlayers.Add(player);
            return player;
        }

        /// <summary>
        /// Get all players in the system
        /// </summary>
        public List<Player> GetAllPlayers() => allPlayers.ToList();

        /// <summary>
        /// Get match history
        /// </summary>
        public List<Match> GetMatchHistory() => matchHistory.ToList();

        /// <summary>
        /// Get system statistics
        /// </summary>
        public string GetSystemStats()
        {
            var uptime = DateTime.Now - systemStartTime;
            var avgMatchQuality = matchHistory.Count > 0 
                ? matchHistory.Average(m => m.SkillDifference) 
                : 0;

            return $"""
                🎮 Matchmaking System Statistics
                ================================
                Total Players: {allPlayers.Count}
                Total Matches: {totalMatches}
                System Uptime: {uptime.ToString("hh\\:mm\\:ss")}
                
                Queue Status:
                - Casual: {casualQueue.Count} players
                - Ranked: {rankedQueue.Count} players  
                - QuickPlay: {quickPlayQueue.Count} players
                
                Match Quality:
                - Average Skill Difference: {avgMatchQuality:F1}
                - Recent Matches: {Math.Min(5, matchHistory.Count)}
                """;
        }

        // ============================================
        // STUDENT IMPLEMENTATION METHODS (TO DO)
        // ============================================

        /// <summary>
        /// TODO: Add a player to the appropriate queue based on game mode
        /// 
        /// Requirements:
        /// - Add player to correct queue (casualQueue, rankedQueue, or quickPlayQueue)
        /// - Call player.JoinQueue() to track queue time
        /// - Handle any validation needed
        /// </summary>
        public void AddToQueue(Player player, GameMode mode)
        {
            if (player is null) throw new ArgumentNullException(nameof(player));
            if (player.SkillRating < 1 || player.SkillRating > 10)
                throw new ArgumentOutOfRangeException(nameof(player.SkillRating), "Skill must be between 1 and 10.");

            // Prevent duplicates across all queues
            if (casualQueue.Contains(player) || rankedQueue.Contains(player) || quickPlayQueue.Contains(player))
                throw new InvalidOperationException("Player is already in a queue.");

            var queue = GetQueueByMode(mode);
            queue.Enqueue(player);
            player.JoinQueue();
            enqueuedAt[player.Username] = DateTime.Now;
        }

        /// <summary>
        /// TODO: Try to create a match from the specified queue
        /// 
        /// Requirements:
        /// - Return null if not enough players (need at least 2)
        /// - For Casual: Any two players can match (simple FIFO)
        /// - For Ranked: Only players within ±2 skill levels can match
        /// - For QuickPlay: Prefer skill matching, but allow any match if queue > 4 players
        /// - Remove matched players from queue and call LeaveQueue() on them
        /// - Return new Match object if successful
        /// </summary>
        public Match? TryCreateMatch(GameMode mode)
        {
            var queue = GetQueueByMode(mode);
            if (queue.Count < 2) return null;

            // Local helper: remove two specific players while preserving order of the rest
            static void RemovePairFromQueue(Queue<Player> q, Player a, Player b)
            {
                var temp = q.ToList();
                q.Clear();
                foreach (var p in temp)
                {
                    if (!ReferenceEquals(p, a) && !ReferenceEquals(p, b))
                        q.Enqueue(p);
                }
            }

            if (mode == GameMode.Casual)
            {
                var p1 = queue.Dequeue();
                var p2 = queue.Dequeue();
                p1.LeaveQueue();
                p2.LeaveQueue();
                enqueuedAt.Remove(p1.Username);
                enqueuedAt.Remove(p2.Username);
                return new Match(p1, p2, mode);
            }

            // Ranked and QuickPlay use head-of-queue as anchor
            var snapshot = queue.ToList();
            var head = snapshot[0];

            if (mode == GameMode.Ranked)
            {
                var idx = snapshot.FindIndex(1, other => CanMatchInRanked(head, other));
                if (idx > 0)
                {
                    var mate = snapshot[idx];
                    RemovePairFromQueue(queue, head, mate);
                    head.LeaveQueue();
                    mate.LeaveQueue();
                    enqueuedAt.Remove(head.Username);
                    enqueuedAt.Remove(mate.Username);
                    return new Match(head, mate, mode);
                }
                return null;
            }

            // QuickPlay: prefer skill match first, then relax to FIFO if busy (>4)
            if (mode == GameMode.QuickPlay)
            {
                var idx = snapshot.FindIndex(1, other => CanMatchInRanked(head, other));
                if (idx > 0)
                {
                    var mate = snapshot[idx];
                    RemovePairFromQueue(queue, head, mate);
                    head.LeaveQueue();
                    mate.LeaveQueue();
                    enqueuedAt.Remove(head.Username);
                    enqueuedAt.Remove(mate.Username);
                    return new Match(head, mate, mode);
                }

                if (queue.Count > 4)
                {
                    var p1 = queue.Dequeue();
                    var p2 = queue.Dequeue();
                    p1.LeaveQueue();
                    p2.LeaveQueue();
                    enqueuedAt.Remove(p1.Username);
                    enqueuedAt.Remove(p2.Username);
                    return new Match(p1, p2, mode);
                }

                return null;
            }

            return null;
        }

        /// <summary>
        /// TODO: Process a match by simulating outcome and updating statistics
        /// 
        /// Requirements:
        /// - Call match.SimulateOutcome() to determine winner
        /// - Add match to matchHistory
        /// - Increment totalMatches counter
        /// - Display match results to console
        /// </summary>
        public void ProcessMatch(Match match)
        {
            if (match is null) throw new ArgumentNullException(nameof(match));

            // Simulate outcome and update system stats
            match.SimulateOutcome();
            matchHistory.Add(match);
            totalMatches++;

            var winnerName = match.Winner?.Username ?? "Unknown";
            Console.WriteLine($"Match Result [{match.Mode}]: {match.Player1.Username} vs {match.Player2.Username} → Winner: {winnerName}");
            Console.WriteLine($"   Skill Δ: {match.SkillDifference} | Time: {DateTime.Now:T}");
        }

        /// <summary>
        /// TODO: Display current status of all queues with formatting
        /// 
        /// Requirements:
        /// - Show header "Current Queue Status"
        /// - For each queue (Casual, Ranked, QuickPlay):
        ///   - Show queue name and player count
        ///   - List players with position numbers and queue times
        ///   - Handle empty queues gracefully
        /// - Use proper formatting and emojis for readability
        /// </summary>
        public void DisplayQueueStatus()
        {
            Console.WriteLine("\n🎮 Current Queue Status");
            Console.WriteLine("=======================");

            void PrintQueue(string title, Queue<Player> queue)
            {
                Console.WriteLine($"\n{title} Queue: {queue.Count} player(s)");

                if (queue.Count == 0)
                {
                    Console.WriteLine("  — empty —");
                    return;
                }

                int position = 1;
                foreach (var player in queue)
                {
                    string timeInQueue = enqueuedAt.TryGetValue(player.Username, out var t)
                        ? (DateTime.Now - t).ToString("mm\\:ss")
                        : "n/a";

                    Console.WriteLine($"  {position}. {player.Username} (Skill {player.SkillRating}) ⏱ {timeInQueue}");
                    position++;
                }
            }

            PrintQueue("Casual", casualQueue);
            PrintQueue("Ranked", rankedQueue);
            PrintQueue("QuickPlay", quickPlayQueue);

            Console.WriteLine();
        }

        /// <summary>
        /// TODO: Display detailed statistics for a specific player
        /// 
        /// Requirements:
        /// - Use player.ToDetailedString() for basic info
        /// - Add queue status (in queue, estimated wait time)
        /// - Show recent match history for this player (last 3 matches)
        /// - Handle case where player has no matches
        /// </summary>
        public void DisplayPlayerStats(Player player)
        {
            if (player is null) throw new ArgumentNullException(nameof(player));

            Console.WriteLine(player.ToDetailedString());

            // Determine queue status
            string queueStatus;
            GameMode? modeInQueue = null;

            if (casualQueue.Contains(player))
            {
                queueStatus = "In Casual queue";
                modeInQueue = GameMode.Casual;
            }
            else if (rankedQueue.Contains(player))
            {
                queueStatus = "In Ranked queue";
                modeInQueue = GameMode.Ranked;
            }
            else if (quickPlayQueue.Contains(player))
            {
                queueStatus = "In QuickPlay queue";
                modeInQueue = GameMode.QuickPlay;
            }
            else
            {
                queueStatus = "Not in any queue";
            }

            Console.WriteLine($"Status: {queueStatus}");

            // Estimated wait time
            if (modeInQueue.HasValue)
            {
                string eta = GetQueueEstimate(modeInQueue.Value);
                Console.WriteLine($"Estimated Wait: {eta}");
            }

            // Recent matches (last 3)
            var recentMatches = matchHistory
                .Where(m => m.Player1 == player || m.Player2 == player)
                .Reverse()
                .Take(3)
                .ToList();

            if (recentMatches.Count == 0)
            {
                Console.WriteLine("Recent Matches: none\n");
                return;
            }

            Console.WriteLine("Recent Matches:");
            foreach (var match in recentMatches)
            {
                var opponent = match.Player1 == player ? match.Player2 : match.Player1;
                string result = match.Winner == player ? "Win" : "Loss";
                Console.WriteLine($"  [{match.Mode}] vs {opponent.Username} → {result} (Skill Δ {match.SkillDifference})");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// TODO: Calculate estimated wait time for a queue
        /// 
        /// Requirements:
        /// - Return "No wait" if queue has 2+ players
        /// - Return "Short wait" if queue has 1 player
        /// - Return "Long wait" if queue is empty
        /// - For Ranked: Consider skill distribution (harder to match = longer wait)
        /// </summary>
        public string GetQueueEstimate(GameMode mode)
        {
            var queue = GetQueueByMode(mode);
            int count = queue.Count;

            if (count == 0)
                return "Long wait";

            if (count >= 2)
            {
                if (mode == GameMode.Ranked && !HasRankedCompatiblePair(queue))
                    return "Long wait";

                return "No wait";
            }

            return mode == GameMode.Ranked ? "Long wait" : "Short wait";

            static bool HasRankedCompatiblePair(Queue<Player> candidates)
            {
                Span<int> seenBySkill = stackalloc int[11];

                foreach (var player in candidates)
                {
                    int minSkill = Math.Max(1, player.SkillRating - 2);
                    int maxSkill = Math.Min(10, player.SkillRating + 2);

                    for (int skill = minSkill; skill <= maxSkill; skill++)
                    {
                        if (seenBySkill[skill] > 0)
                            return true;
                    }

                    seenBySkill[player.SkillRating]++;
                }

                return false;
            }
        }

        // ============================================
        // HELPER METHODS (PROVIDED)
        // ============================================

        /// <summary>
        /// Helper: Check if two players can match in Ranked mode (±2 skill levels)
        /// </summary>
        private bool CanMatchInRanked(Player player1, Player player2)
        {
            return Math.Abs(player1.SkillRating - player2.SkillRating) <= 2;
        }

        /// <summary>
        /// Helper: Remove player from all queues (useful for cleanup)
        /// </summary>
        private void RemoveFromAllQueues(Player player)
        {
            // Create temporary lists to avoid modifying collections during iteration
            var casualPlayers = casualQueue.ToList();
            var rankedPlayers = rankedQueue.ToList();
            var quickPlayPlayers = quickPlayQueue.ToList();

            // Clear and rebuild queues without the specified player
            casualQueue.Clear();
            foreach (var p in casualPlayers.Where(p => p != player))
                casualQueue.Enqueue(p);

            rankedQueue.Clear();
            foreach (var p in rankedPlayers.Where(p => p != player))
                rankedQueue.Enqueue(p);

            quickPlayQueue.Clear();
            foreach (var p in quickPlayPlayers.Where(p => p != player))
                quickPlayQueue.Enqueue(p);

            player.LeaveQueue();
            enqueuedAt.Remove(player.Username);
        }

        /// <summary>
        /// Helper: Get queue by mode (useful for generic operations)
        /// </summary>
        private Queue<Player> GetQueueByMode(GameMode mode)
        {
            return mode switch
            {
                GameMode.Casual => casualQueue,
                GameMode.Ranked => rankedQueue,
                GameMode.QuickPlay => quickPlayQueue,
                _ => throw new ArgumentException($"Unknown game mode: {mode}")
            };
        }
    }
}
