using System;

namespace Lab9_BST
{
    /// <summary>
    /// SystemStats class for Binary Search Tree implementation
    /// 
    /// This class encapsulates various statistics about the BST system,
    /// including operation counts, tree height, and employee count.
    /// It provides a snapshot of the system's performance and state.
    /// 
    /// Properties:
    /// - TotalOperations: Total number of operations performed on the BST
    /// - Uptime: Duration the system has been running
    /// - EmployeeCount: Number of employees currently stored in the BST
    /// - TreeHeight: Height of the BST
    /// - IsEmpty: Indicates whether the BST is empty
    /// </summary>
        public class SystemStats
    {
        public int TotalOperations { get; set; }
        public TimeSpan Uptime { get; set; }
        public int EmployeeCount { get; set; }
        public int TreeHeight { get; set; }
        public bool IsEmpty { get; set; }
    }
}