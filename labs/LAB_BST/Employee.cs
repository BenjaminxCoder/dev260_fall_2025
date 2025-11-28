using System;

namespace Lab9_BST
{
    /// <summary>
    /// Employee class representing company personnel in the BST system
    /// 
    /// This class demonstrates how real-world objects can be organized
    /// using tree data structures for efficient searching and management.
    /// </summary>
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        
        public Employee(int employeeId, string name, string department = "General")
        {
            EmployeeId = employeeId;
            Name = name;
            Department = department;
        }
        
        public override string ToString()
        {
            return $"ID: {EmployeeId}, Name: {Name}, Dept: {Department}";
        }
    }
}