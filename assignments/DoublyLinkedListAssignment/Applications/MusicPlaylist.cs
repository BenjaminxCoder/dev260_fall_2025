using System;
using System.Linq;
using Week4DoublyLinkedLists.Core;

namespace Week4DoublyLinkedLists.Applications
{
    /// <summary>
    /// Represents a song in the music playlist
    /// Contains all metadata about a musical track
    /// </summary>
    public class Song
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public TimeSpan Duration { get; set; }
        public string Album { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        
        public Song(string title, string artist, TimeSpan duration, string album = "", int year = 0, string genre = "")
        {
            Title = title;
            Artist = artist;
            Duration = duration;
            Album = album;
            Year = year;
            Genre = genre;
        }
        
        public override string ToString()
        {
            return $"{Title} by {Artist} ({Duration:mm\\:ss})";
        }
        
        public string ToDetailedString()
        {
            return $"{Title} - {Artist} [{Album}, {Year}] ({Duration:mm\\:ss}) [{Genre}]";
        }
    }
    
    /// <summary>
    /// Music playlist manager using doubly linked list for efficient navigation
    /// Demonstrates practical application of doubly linked lists
    /// 
    /// PART B IMPLEMENTATION GUIDE:
    /// Step 8: Song class (already provided above)
    /// Step 9: Playlist core structure (implement below)
    /// Step 10: Playlist management (AddSong, RemoveSong, Navigation)
    /// Step 11: Display and basic management
    /// </summary>
    public class MusicPlaylist
    {
        #region Step 9: Playlist Core Structure - TODO: Students implement these properties
        
        private DoublyLinkedList<Song> playlist;
        private Node<Song>? currentSong;     // Currently selected song node
        
        // Basic playlist properties
        public string Name { get; set; }
        public int TotalSongs => playlist.Count;
        public bool HasSongs => playlist.Count > 0;
        public Song? CurrentSong => currentSong?.Data;
        
        /// <summary>
        /// Initialize a new music playlist
        /// </summary>
        /// <param name="name">Name of the playlist</param>
        public MusicPlaylist(string name = "My Playlist")
        {
            Name = name;
            playlist = new DoublyLinkedList<Song>();
            currentSong = null;
        }
        
        #endregion
        
        #region Step 10: Playlist Management - TODO: Students implement these step by step
        
        #region Step 10a: Adding Songs (5 points)
        
        /// <summary>
        /// Add a song to the end of the playlist
        /// Time Complexity: O(1) due to doubly linked list tail pointer
        /// 📚 Reference: https://www.geeksforgeeks.org/applications-of-linked-list-data-structure/
        /// </summary>
        /// <param name="song">Song to add</param>
        public void AddSong(Song song)
        {
            // TODO: Step 10a - Implement adding song to end of playlist
            // 1. Validate that song is not null
            // 2. Use your DoublyLinkedList's AddLast method
            // 3. If this is the first song, set it as current song
            // 📖 Assignment Reference: Step 10a in Part B
            
            throw new NotImplementedException("TODO: Step 10a - Implement AddSong method");
        }
        
        /// <summary>
        /// Add a song at a specific position in the playlist
        /// Time Complexity: O(n) for position-based insertion
        /// 📚 Reference: https://www.geeksforgeeks.org/applications-of-linked-list-data-structure/
        /// </summary>
        /// <param name="position">Position to insert at (0-based)</param>
        /// <param name="song">Song to add</param>
        public void AddSongAt(int position, Song song)
        {
            // TODO: Step 10a - Implement adding song at specific position
            // 1. Validate position is within valid range (0 to TotalSongs)
            // 2. Validate that song is not null
            // 3. Use your DoublyLinkedList's Insert method
            // 4. If this is the first song, set it as current song
            // 📖 Assignment Reference: Step 10a in Part B
            
            throw new NotImplementedException("TODO: Step 10a - Implement AddSongAt method");
        }
        
        #endregion
        
        #region Step 10b: Removing Songs (5 points)
        
        /// <summary>
        /// Remove a specific song from the playlist
        /// Time Complexity: O(n) due to search operation
        /// 📚 Reference: https://www.geeksforgeeks.org/applications-of-linked-list-data-structure/
        /// </summary>
        /// <param name="song">Song to remove</param>
        /// <returns>True if song was found and removed</returns>
        public bool RemoveSong(Song song)
        {
            // TODO: Step 10b - Implement removing specific song
            // 1. Validate that song is not null
            // 2. Find the song in the playlist using your DoublyLinkedList's Find method
            // 3. If the song being removed is the current song, handle current song update
            // 4. Use your DoublyLinkedList's Remove method
            // 5. Return true if removed, false if not found
            // 📖 Assignment Reference: Step 10b in Part B
            
            throw new NotImplementedException("TODO: Step 10b - Implement RemoveSong method");
        }
        
        /// <summary>
        /// Remove song at a specific position
        /// Time Complexity: O(n) for position-based removal
        /// 📚 Reference: https://www.geeksforgeeks.org/applications-of-linked-list-data-structure/
        /// </summary>
        /// <param name="position">Position to remove (0-based)</param>
        /// <returns>True if song was removed successfully</returns>
        public bool RemoveSongAt(int position)
        {
            // TODO: Step 10b - Implement removing song at position
            // 1. Validate position is within valid range (0 to TotalSongs-1)
            // 2. Get the node at that position to check if it's the current song
            // 3. If removing current song, update current song reference
            // 4. Use your DoublyLinkedList's RemoveAt method
            // 5. Return true if removed successfully
            // 📖 Assignment Reference: Step 10b in Part B
            
            throw new NotImplementedException("TODO: Step 10b - Implement RemoveSongAt method");
        }
        
        #endregion
        
        #region Step 10c: Navigation (5 points)
        
        /// <summary>
        /// Move to the next song in the playlist
        /// Time Complexity: O(1) due to doubly linked list Next pointer
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
        /// </summary>
        /// <returns>True if moved to next song, false if at end</returns>
        public bool Next()
        {
            // TODO: Step 10c - Implement moving to next song
            // 1. Check if there is a current song and if it has a Next node
            // 2. Update currentSong to the next node
            // 3. Return true if successful, false if at end of playlist
            // 📖 Assignment Reference: Step 10c in Part B
            // 💡 This demonstrates the power of doubly linked lists for navigation!
            
            throw new NotImplementedException("TODO: Step 10c - Implement Next method");
        }
        
        /// <summary>
        /// Move to the previous song in the playlist
        /// Time Complexity: O(1) due to doubly linked list Previous pointer
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
        /// </summary>
        /// <returns>True if moved to previous song, false if at beginning</returns>
        public bool Previous()
        {
            // TODO: Step 10c - Implement moving to previous song
            // 1. Check if there is a current song and if it has a Previous node
            // 2. Update currentSong to the previous node
            // 3. Return true if successful, false if at beginning of playlist
            // 📖 Assignment Reference: Step 10c in Part B
            // 💡 This demonstrates bidirectional navigation unique to doubly linked lists!
            
            throw new NotImplementedException("TODO: Step 10c - Implement Previous method");
        }
        
        /// <summary>
        /// Jump directly to a song at a specific position
        /// Time Complexity: O(n) for position-based access
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
        /// </summary>
        /// <param name="position">Position to jump to (0-based)</param>
        /// <returns>True if jump was successful</returns>
        public bool JumpToSong(int position)
        {
            // TODO: Step 10c - Implement jumping to specific position
            // 1. Validate position is within valid range (0 to TotalSongs-1)
            // 2. Traverse to the node at the specified position
            // 3. Update currentSong to that node
            // 4. Return true if successful, false for invalid position
            // 📖 Assignment Reference: Step 10c in Part B
            // 💡 Hint: You can traverse from head or use helper methods
            
            throw new NotImplementedException("TODO: Step 10c - Implement JumpToSong method");
        }
        
        #endregion
        
        #endregion
        
        #region Step 11: Display and Basic Management (10 points)
        
        /// <summary>
        /// Display the entire playlist with current song highlighted
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
        /// </summary>
        public void DisplayPlaylist()
        {
            // TODO: Step 11 - Implement playlist display
            // 1. Show playlist name and total song count
            // 2. If playlist is empty, show appropriate message
            // 3. Iterate through all songs using foreach (your DoublyLinkedList supports this)
            // 4. Mark the current song with an indicator (e.g., "► ")
            // 5. Show position numbers (1-based for user display)
            // 📖 Assignment Reference: Step 11 in Part B
            // 💡 Format: "  1. Song Title by Artist (3:45)" or "► 2. Current Song by Artist (4:12)"
            
            throw new NotImplementedException("TODO: Step 11 - Implement DisplayPlaylist method");
        }
        
        /// <summary>
        /// Display details of the currently selected song
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
        /// </summary>
        public void DisplayCurrentSong()
        {
            // TODO: Step 11 - Implement current song display
            // 1. Check if there is a current song selected
            // 2. If no current song, show appropriate message
            // 3. If current song exists, show detailed information:
            //    - Title, Artist, Album, Year, Duration, Genre
            //    - Current position in playlist (e.g., "Song 3 of 10")
            // 📖 Assignment Reference: Step 11 in Part B
            
            throw new NotImplementedException("TODO: Step 11 - Implement DisplayCurrentSong method");
        }
        
        /// <summary>
        /// Get the currently selected song
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
        /// </summary>
        /// <returns>Currently selected song, or null if no song selected</returns>
        public Song? GetCurrentSong()
        {
            // TODO: Step 11 - Implement getting current song
            // 1. Return the Data from the currentSong node
            // 2. Return null if no current song is selected
            // 📖 Assignment Reference: Step 11 in Part B
            // 💡 This is a simple getter, but important for the playlist interface
            
            throw new NotImplementedException("TODO: Step 11 - Implement GetCurrentSong method");
        }
        
        #endregion
        
        #region Helper Methods for Students
        
        /// <summary>
        /// Get the position of the current song (1-based for display)
        /// Useful for showing "Song X of Y" information
        /// </summary>
        /// <returns>Position of current song, or 0 if no current song</returns>
        public int GetCurrentPosition()
        {
            if (currentSong == null) return 0;
            
            int position = 1;
            var current = playlist.First;
            while (current != null && current != currentSong)
            {
                position++;
                current = current.Next;
            }
            return current == currentSong ? position : 0;
        }
        
        /// <summary>
        /// Calculate total duration of all songs in the playlist
        /// Demonstrates traversal and aggregate operations
        /// </summary>
        /// <returns>Total duration as TimeSpan</returns>
        public TimeSpan GetTotalDuration()
        {
            TimeSpan total = TimeSpan.Zero;
            foreach (var song in playlist)
            {
                total = total.Add(song.Duration);
            }
            return total;
        }
        
        #endregion
    }
}