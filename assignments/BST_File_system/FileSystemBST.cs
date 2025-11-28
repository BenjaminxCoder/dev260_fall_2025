using System;
using System.Collections.Generic;
using System.Linq;

namespace FileSystemNavigator
{
    /// <summary>
    /// Binary Search Tree implementation for File System Navigation
    /// 
    /// STUDENT ASSIGNMENT: Implement the TODO methods in this class
    /// This class demonstrates BST concepts through a practical file system simulation
    /// 
    /// Learning Objectives:
    /// - Apply BST operations to hierarchical data
    /// - Implement complex search and filtering operations  
    /// - Practice file system concepts through tree structures
    /// - Build practical navigation and management tools
    /// </summary>
    public class FileSystemBST
    {
        private TreeNode? root;
        private int operationCount;
        private DateTime sessionStart;

        public FileSystemBST()
        {
            root = null;
            operationCount = 0;
            sessionStart = DateTime.Now;
            
            Console.WriteLine("🗂️  File System Navigator Initialized!");
            Console.WriteLine("📁 BST-based file system ready for operations.\n");
        }

        // ============================================
        // 🚀 STUDENT TODO METHODS - IMPLEMENT THESE
        // ============================================

        /// <summary>
        /// TODO #1: Create a new file in the file system
        /// 
        /// Requirements:
        /// - Insert file into BST maintaining proper ordering
        /// - Use file name for BST comparison (case-insensitive)
        /// - Handle duplicate file names (return false if exists)
        /// - Set appropriate file metadata (size, dates, extension)
        /// 
        /// BST Learning: Insertion with custom comparison logic
        /// Real-World: File creation in operating systems
        /// </summary>
        /// <param name="fileName">Name of file to create (e.g., "readme.txt")</param>
        /// <param name="size">File size in bytes (default 1024)</param>
        /// <returns>True if file created successfully, false if already exists</returns>
        public bool CreateFile(string fileName, long size = 1024)
        {
            operationCount++;
            if (string.IsNullOrWhiteSpace(fileName)) return false;
            if (size < 0) return false;
            var name = fileName.Trim();
            // Reject if any node (file) with this name already exists
            if (SearchNode(root, name) != null) return false;
            var nodeData = new FileNode(name, FileType.File, size);
            root = InsertNode(root, nodeData);
            return true;
        }

        /// <summary>
        /// TODO #2: Create a new directory in the file system
        /// 
        /// Requirements:
        /// - Insert directory into BST with FileType.Directory
        /// - Directories should sort before files with same name
        /// - Set size to 0 for directories (automatic in FileNode constructor)
        /// - Handle duplicate directory names
        /// 
        /// BST Learning: Custom comparison for different node types
        /// Real-World: Directory creation and organization
        /// </summary>
        /// <param name="directoryName">Name of directory to create (e.g., "Documents")</param>
        /// <returns>True if directory created successfully, false if already exists</returns>
        public bool CreateDirectory(string directoryName)
        {
            operationCount++;
            if (string.IsNullOrWhiteSpace(directoryName)) return false;
            var name = directoryName.Trim();
            // Reject if any node (directory) with this name already exists
            if (SearchNode(root, name) != null) return false;
            var nodeData = new FileNode(name, FileType.Directory);
            root = InsertNode(root, nodeData);
            return true;
        }

        /// <summary>
        /// TODO #3: Find a specific file by exact name
        /// 
        /// Requirements:
        /// - Search BST efficiently using file name as key
        /// - Case-insensitive search
        /// - Return FileNode if found, null if not found
        /// - Use recursive BST search pattern
        /// 
        /// BST Learning: O(log n) search operations
        /// Real-World: File lookup in operating systems
        /// </summary>
        /// <param name="fileName">Name of file to find (not full path)</param>
        /// <returns>FileNode if found, null otherwise</returns>
        public FileNode? FindFile(string fileName)
        {
            operationCount++;
            if (string.IsNullOrWhiteSpace(fileName)) return null;
            var name = fileName.Trim();
            return SearchNodeByType(root, name, FileType.File);
        }

        /// <summary>
        /// TODO #4: Find all files with a specific extension
        /// 
        /// Requirements:
        /// - Traverse entire BST collecting files with matching extension
        /// - Case-insensitive extension comparison (.txt = .TXT)
        /// - Return List of FileNode objects
        /// - Use in-order traversal for consistent ordering
        /// 
        /// BST Learning: Tree traversal with filtering
        /// Real-World: File type searches (find all .cs files)
        /// </summary>
        /// <param name="extension">File extension to search for (.txt, .cs, etc.)</param>
        /// <returns>List of files with matching extension</returns>
        public List<FileNode> FindFilesByExtension(string extension)
        {
            operationCount++;
            var results = new List<FileNode>();
            if (string.IsNullOrWhiteSpace(extension)) return results;
            var ext = NormalizeExtension(extension);
            TraverseAndCollect(root, results, f => f.Type == FileType.File && string.Equals(f.Extension, ext, StringComparison.OrdinalIgnoreCase));
            return results;
        }

        /// <summary>
        /// TODO #5: Find all files within a size range
        /// 
        /// Requirements:
        /// - Search for files between minSize and maxSize (inclusive)
        /// - Only include FileType.File items (not directories)
        /// - Return files sorted by name (in-order traversal)
        /// - Handle edge cases (minSize > maxSize)
        /// 
        /// BST Learning: Range queries and filtered traversal
        /// Real-World: Find large files for cleanup, small files for compression
        /// </summary>
        /// <param name="minSize">Minimum file size in bytes</param>
        /// <param name="maxSize">Maximum file size in bytes</param>
        /// <returns>List of files within size range</returns>
        public List<FileNode> FindFilesBySize(long minSize, long maxSize)
        {
            operationCount++;
            var results = new List<FileNode>();
            if (minSize < 0 || maxSize < 0) return results;
            if (minSize > maxSize) return results;
            TraverseAndCollect(root, results, f => f.Type == FileType.File && f.Size >= minSize && f.Size <= maxSize);
            return results;
        }

        /// <summary>
        /// TODO #6: Find the N largest files in the system
        /// 
        /// Requirements:
        /// - Collect all files and sort by size (descending)
        /// - Return top N largest files
        /// - Handle case where N > total file count
        /// - Only include FileType.File items
        /// 
        /// BST Learning: Tree traversal with post-processing
        /// Real-World: Disk cleanup utilities, storage analysis
        /// </summary>
        /// <param name="count">Number of largest files to return</param>
        /// <returns>List of largest files, sorted by size descending</returns>
        public List<FileNode> FindLargestFiles(int count)
        {
            operationCount++;
            if (count <= 0) return new List<FileNode>();
            var all = new List<FileNode>();
            TraverseAndCollect(root, all, f => f.Type == FileType.File);
            return all
                .OrderByDescending(f => f.Size)
                .ThenBy(f => f.Name, StringComparer.OrdinalIgnoreCase)
                .Take(count)
                .ToList();
        }

        /// <summary>
        /// TODO #7: Calculate total size of all files and directories
        /// 
        /// Requirements:
        /// - Traverse entire BST and sum all file sizes
        /// - Include both files and directories in count
        /// - Use recursive traversal approach
        /// - Return total size in bytes
        /// 
        /// BST Learning: Aggregation through tree traversal
        /// Real-World: Disk usage analysis, storage reporting
        /// </summary>
        /// <returns>Total size of all files in bytes</returns>
        public long CalculateTotalSize()
        {
            operationCount++;
            return SumSizes(root);
        }

        /// <summary>
        /// TODO #8: Delete a file or directory from the system
        /// 
        /// Requirements:
        /// - Remove item from BST maintaining tree structure
        /// - Handle all three deletion cases (no children, one child, two children)
        /// - Return true if deleted, false if not found
        /// - Use standard BST deletion algorithm
        /// 
        /// BST Learning: Complex deletion maintaining tree structure
        /// Real-World: File deletion in operating systems
        /// </summary>
        /// <param name="fileName">Name of file or directory to delete</param>
        /// <returns>True if deleted successfully, false if not found</returns>
        public bool DeleteItem(string fileName)
        {
            operationCount++;
            if (string.IsNullOrWhiteSpace(fileName)) return false;
            var name = fileName.Trim();
            bool deleted;
            (root, deleted) = DeleteNode(root, new FileNode(name, FileType.File));
            if (deleted) return true;
            (root, deleted) = DeleteNode(root, new FileNode(name, FileType.Directory));
            return deleted;
        }

        // ============================================
        // 🔧 HELPER METHODS FOR TODO IMPLEMENTATION
        // ============================================
        
        /// <summary>
        /// Helper method for BST insertion
        /// Students should use this in CreateFile and CreateDirectory
        /// </summary>
        private TreeNode? InsertNode(TreeNode? node, FileNode fileData)
        {
            if (node == null) return new TreeNode(new FileNode(fileData.Name, fileData.Type, fileData.Size));
            int cmp = CompareFileNodes(fileData, node.FileData);
            if (cmp == 0)
            {
                // Duplicate by ordering rules: do not insert, return existing node
                return node;
            }
            else if (cmp < 0)
            {
                node.Left = InsertNode(node.Left, fileData);
            }
            else
            {
                node.Right = InsertNode(node.Right, fileData);
            }
            return node;
        }

        /// <summary>
        /// Helper method for BST searching
        /// Students should use this in FindFile
        /// </summary>
        private FileNode? SearchNode(TreeNode? node, string fileName)
        {
            if (node == null || string.IsNullOrWhiteSpace(fileName)) return null;
            var name = fileName.Trim();
            var found = SearchNodeByType(node, name, FileType.Directory);
            if (found != null) return found;
            return SearchNodeByType(node, name, FileType.File);
        }

        /// <summary>
        /// Helper method for collecting nodes during traversal
        /// Students should use this for FindFilesByExtension, FindFilesBySize, etc.
        /// </summary>
        private void TraverseAndCollect(TreeNode? node, List<FileNode> collection, Func<FileNode, bool> filter)
        {
            if (node == null) return;
            TraverseAndCollect(node.Left, collection, filter);
            if (filter(node.FileData)) collection.Add(node.FileData);
            TraverseAndCollect(node.Right, collection, filter);

        }
        // ======== Minimal Private Helpers for BST/FileSystem ========
        private string NormalizeExtension(string ext)
        {
            var e = ext.Trim().ToLowerInvariant();
            if (!e.StartsWith(".")) e = "." + e;
            return e;
        }

        private FileNode? SearchNodeByType(TreeNode? node, string fileName, FileType type)
        {
            if (node == null) return null;
            var probe = new FileNode(fileName, type);
            int cmp = CompareFileNodes(probe, node.FileData);
            if (cmp == 0)
            {
                return node.FileData;
            }
            else if (cmp < 0)
            {
                return SearchNodeByType(node.Left, fileName, type);
            }
            else
            {
                return SearchNodeByType(node.Right, fileName, type);
            }
        }

        private long SumSizes(TreeNode? node)
        {
            if (node == null) return 0L;
            long self = node.FileData.Type == FileType.File ? node.FileData.Size : 0L;
            return self + SumSizes(node.Left) + SumSizes(node.Right);
        }

        private (TreeNode? node, bool deleted) DeleteNode(TreeNode? node, FileNode probe)
        {
            if (node == null) return (null, false);
            int cmp = CompareFileNodes(probe, node.FileData);
            if (cmp < 0)
            {
                var res = DeleteNode(node.Left, probe);
                node.Left = res.node;
                return (node, res.deleted);
            }
            if (cmp > 0)
            {
                var res = DeleteNode(node.Right, probe);
                node.Right = res.node;
                return (node, res.deleted);
            }

            // Found node
            if (node.Left == null && node.Right == null) return (null, true);
            if (node.Left == null) return (node.Right, true);
            if (node.Right == null) return (node.Left, true);

            // Two children: replace with inorder successor
            var successor = MinNode(node.Right);
            node.FileData = new FileNode(successor.FileData.Name, successor.FileData.Type, successor.FileData.Size);
            // Preserve extension/created if FileNode exposes them via constructor; if not, existing constructor should infer extension from name
            var resRight = DeleteNode(node.Right, successor.FileData);
            node.Right = resRight.node;
            return (node, true);
        }

        private TreeNode MinNode(TreeNode node)
        {
            var cur = node;
            while (cur.Left != null) cur = cur.Left;
            return cur;
        }

        /// <summary>
        /// Custom comparison method for file system ordering
        /// Directories come before files, then alphabetical by name
        /// </summary>
        private int CompareFileNodes(FileNode a, FileNode b)
        {
            // Directories sort before files
            if (a.Type != b.Type)
                return a.Type == FileType.Directory ? -1 : 1;
            
            // Then alphabetical by name (case-insensitive)
            return string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase);
        }

        // ============================================
        // 🎯 PROVIDED UTILITY METHODS
        // ============================================

        /// <summary>
        /// Display the file system tree structure visually
        /// Helps students visualize their BST structure
        /// </summary>
        public void DisplayTree()
        {
            Console.WriteLine("🌳 File System Tree Structure:");
            Console.WriteLine("================================");
            
            if (root == null)
            {
                Console.WriteLine("   (Empty file system)");
                return;
            }
            DisplayTreeEnhanced(root, "", true, true);
            Console.WriteLine("================================\n");
            Console.WriteLine("🌲 Horizontal Level-by-Level View:");
            DisplayTreeByLevels();
        }

        /// <summary>
        /// Enhanced tree display with better visual formatting and clear parent-child relationships
        /// </summary>
        private void DisplayTreeEnhanced(TreeNode? node, string prefix, bool isLast, bool isRoot)
        {
            if (node == null) return;

            // Display current node with enhanced formatting
            string connector = isRoot ? "🌟 " : (isLast ? "└── " : "├── ");
            string nodeInfo = $"{node.FileData.Name}{(node.FileData.Type == FileType.Directory ? "/" : $" ({FormatSize(node.FileData.Size)})")}";
            
            Console.WriteLine(prefix + connector + nodeInfo);

            // Update prefix for children
            string childPrefix = prefix + (isRoot ? "" : (isLast ? "    " : "│   "));

            // Display children with clear Left/Right indicators
            bool hasLeft = node.Left != null;
            bool hasRight = node.Right != null;

            if (hasRight)
            {
                Console.WriteLine(childPrefix + "│");
                Console.WriteLine(childPrefix + "├─(R)─┐");
                DisplayTreeEnhanced(node.Right, childPrefix + "│     ", !hasLeft, false);
            }

            if (hasLeft)
            {
                Console.WriteLine(childPrefix + "│");
                Console.WriteLine(childPrefix + "└─(L)─┐");
                DisplayTreeEnhanced(node.Left, childPrefix + "      ", true, false);
            }
        }

        /// <summary>
        /// Display tree in a horizontal level-by-level format
        /// </summary>
        private void DisplayTreeByLevels()
        {
            if (root == null) return;

            var queue = new Queue<(TreeNode?, int)>();
            queue.Enqueue((root, 0));
            int currentLevel = -1;

            while (queue.Count > 0)
            {
                var (node, level) = queue.Dequeue();
                
                if (level > currentLevel)
                {
                    if (currentLevel >= 0) Console.WriteLine();
                    Console.Write($"Level {level}: ");
                    currentLevel = level;
                }

                if (node != null)
                {
                    Console.Write($"[{node.FileData.Name}{(node.FileData.Type == FileType.Directory ? "/" : "")}] ");
                    queue.Enqueue((node.Left, level + 1));
                    queue.Enqueue((node.Right, level + 1));
                }
                else
                {
                    Console.Write("[null] ");
                }
            }
            Console.WriteLine();
        }


        private string FormatSize(long bytes)
        {
            if (bytes < 1024) return $"{bytes}B";
            if (bytes < 1024 * 1024) return $"{bytes / 1024}KB";
            return $"{bytes / (1024 * 1024)}MB";
        }

        /// <summary>
        /// Get comprehensive statistics about the file system
        /// </summary>
        public FileSystemStats GetStatistics()
        {
            var stats = new FileSystemStats
            {
                TotalOperations = operationCount,
                SessionDuration = DateTime.Now - sessionStart
            };

            if (root != null)
            {
                CalculateStats(root, stats);
            }

            return stats;
        }

        private void CalculateStats(TreeNode? node, FileSystemStats stats)
        {
            if (node == null) return;

            var file = node.FileData;
            if (file.Type == FileType.File)
            {
                stats.TotalFiles++;
                stats.TotalSize += file.Size;
                
                if (file.Size > stats.LargestFileSize)
                {
                    stats.LargestFileSize = file.Size;
                    stats.LargestFile = file.Name;
                }
            }
            else
            {
                stats.TotalDirectories++;
            }

            CalculateStats(node.Left, stats);
            CalculateStats(node.Right, stats);
        }

        /// <summary>
        /// Check if the file system is empty
        /// </summary>
        public bool IsEmpty() => root == null;

        /// <summary>
        /// Load sample data for testing and demonstration
        /// </summary>
        public void LoadSampleData()
        {
            Console.WriteLine("📁 Loading sample file system data...");
            
            // Sample directories
            var sampleDirs = new[]
            {
                "Documents", "Pictures", "Videos", "Music", "Downloads",
                "Projects", "Code", "Images", "Archive"
            };

            // Sample files with extensions and sizes
            var sampleFiles = new[]
            {
                ("readme.txt", 2048L), ("config.json", 1024L), ("app.cs", 5120L),
                ("photo.jpg", 2048000L), ("song.mp3", 4096000L), ("video.mp4", 52428800L),
                ("document.pdf", 1048576L), ("presentation.pptx", 3145728L),
                ("spreadsheet.xlsx", 512000L), ("archive.zip", 10485760L)
            };

            try
            {
                // Create directories
                foreach (var dir in sampleDirs.Take(6))
                {
                    CreateDirectory(dir);
                }

                // Create files
                foreach (var (fileName, size) in sampleFiles.Take(8))
                {
                    CreateFile(fileName, size);
                }

                Console.WriteLine("✅ Sample data loaded successfully!");
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("⚠️  Cannot load sample data - TODO methods not implemented yet");
            }
        }
    }
}