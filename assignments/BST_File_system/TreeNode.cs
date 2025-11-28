using System;

namespace FileSystemNavigator
{
    /// <summary>
    /// Tree node for the Binary Search Tree structure
    /// Each node contains a FileNode and references to left/right children
    /// </summary>
    public class TreeNode
    {
        public FileNode FileData { get; set; }
        public TreeNode? Left { get; set; }
        public TreeNode? Right { get; set; }

        public TreeNode(FileNode fileData)
        {
            FileData = fileData;
            Left = null;
            Right = null;
        }

        public override string ToString()
        {
            return FileData.ToString();
        }
    }
}