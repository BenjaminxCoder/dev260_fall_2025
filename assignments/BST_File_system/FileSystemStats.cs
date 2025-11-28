using System;

namespace FileSystemNavigator
{
    /// <summary>
    /// Statistics about file system usage and operations
    /// Used for reporting and analytics
    /// </summary>
    public class FileSystemStats
    {
        public int TotalFiles { get; set; }
        public int TotalDirectories { get; set; }
        public long TotalSize { get; set; }
        public int TotalOperations { get; set; }
        public TimeSpan SessionDuration { get; set; }
        public string LargestFile { get; set; } = "";
        public long LargestFileSize { get; set; }
        public string MostCommonExtension { get; set; } = "";

        public int TotalItems => TotalFiles + TotalDirectories;

        public string FormatSize(long bytes)
        {
            if (bytes < 1024) return $"{bytes} B";
            if (bytes < 1024 * 1024) return $"{bytes / 1024:F1} KB";
            if (bytes < 1024 * 1024 * 1024) return $"{bytes / (1024 * 1024):F1} MB";
            return $"{bytes / (1024 * 1024 * 1024):F1} GB";
        }

        public override string ToString()
        {
            return $"""
                📊 File System Statistics
                ========================
                Total Items: {TotalItems:N0} ({TotalDirectories:N0} directories, {TotalFiles:N0} files)
                Total Size: {FormatSize(TotalSize)}
                Operations Performed: {TotalOperations:N0}
                Session Duration: {SessionDuration:mm\\:ss}
                Largest File: {LargestFile} ({FormatSize(LargestFileSize)})
                Most Common Extension: {MostCommonExtension}
                """;
        }
    }
}