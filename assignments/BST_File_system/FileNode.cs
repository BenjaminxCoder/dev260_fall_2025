using System;

namespace FileSystemNavigator
{
    /// <summary>
    /// Represents a file or directory in the file system
    /// Simplified design focusing on BST learning objectives
    /// </summary>
    public class FileNode
    {
        public string Name { get; set; }
        public FileType Type { get; set; }
        public long Size { get; set; }  // Size in bytes
        public DateTime CreatedDate { get; set; }
        public string Extension { get; set; }

        public FileNode(string name, FileType type)
        {
            Name = name;
            Type = type;
            CreatedDate = DateTime.Now;
            Size = type == FileType.Directory ? 0 : 1024; // Default 1KB for files
            Extension = type == FileType.File ? GetFileExtension(name) : "";
        }

        public FileNode(string name, FileType type, long size) : this(name, type)
        {
            Size = size;
        }

        private string GetFileExtension(string fileName)
        {
            var lastDotIndex = fileName.LastIndexOf('.');
            return lastDotIndex >= 0 ? fileName.Substring(lastDotIndex) : "";
        }

        public override string ToString()
        {
            var sizeDisplay = Type == FileType.Directory ? "<DIR>" : FormatSize(Size);
            return $"{Name,-25} {Type,-10} {sizeDisplay,-10} {CreatedDate:MM/dd/yyyy}";
        }

        private string FormatSize(long bytes)
        {
            if (bytes < 1024) return $"{bytes}B";
            if (bytes < 1024 * 1024) return $"{bytes / 1024}KB";
            return $"{bytes / (1024 * 1024)}MB";
        }
    }

    /// <summary>
    /// Enumeration for file system item types
    /// </summary>
    public enum FileType
    {
        File,
        Directory
    }
}