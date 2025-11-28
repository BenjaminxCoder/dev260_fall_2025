using System;

namespace Lab9_BST
{
    /// <summary>
    /// TreeNode class for Binary Search Tree implementation
    /// 
    /// Each node contains:
    /// - An Employee object (the data)
    /// - Left child reference (for employees with smaller IDs)
    /// - Right child reference (for employees with larger IDs)
    /// 
    /// This structure enables the BST ordering property:
    /// Left subtree < Root < Right subtree
    /// </summary>
    public class TreeNode
    {
        public Employee Employee { get; set; }
        public TreeNode? Left { get; set; }
        public TreeNode? Right { get; set; }
        
        public TreeNode(Employee employee)
        {
            Employee = employee;
            Left = null;
            Right = null;
        }
    }
}