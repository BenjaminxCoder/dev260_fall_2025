using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab9_BST
{
    /// <summary>
    /// Lab 9: Binary Search Tree - Student Version with Interactive Menu
    /// 
    /// This program demonstrates BST fundamentals through a company employee
    /// management system. Students implement core BST operations while
    /// experiencing real-world applications of tree data structures.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("🌳 Welcome to Lab 9: Binary Search Tree in C#");
            Console.WriteLine("=============================================");
            Console.WriteLine("Today we'll explore BST through employee management scenarios!\n");

            var employeeSystem = new EmployeeManagementSystem();
            
            // Load demo data for interactive testing
            // LabSupport.LoadDemoData(employeeSystem);
            
            // Start the interactive menu system
            employeeSystem.RunInteractiveMenu();
        }
    }
}