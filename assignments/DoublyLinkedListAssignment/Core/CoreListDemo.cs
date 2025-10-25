using System;
using System.Diagnostics;
using Week4DoublyLinkedLists.Core;

namespace Week4DoublyLinkedLists.Core
{
    /// <summary>
    /// Interactive demonstration of core doubly linked list operations
    /// Allows users to test and visualize list functionality
    /// </summary>
    public class CoreListDemo
    {
        private DoublyLinkedList<int> list;
        
        public CoreListDemo()
        {
            list = new DoublyLinkedList<int>();
        }
        
        /// <summary>
        /// Run interactive demonstration of core list operations
        /// Follows the step-by-step assignment structure
        /// </summary>
        public void RunInteractiveDemo()
        {
            Console.WriteLine("=== PART A: CORE DOUBLY LINKED LIST OPERATIONS ===");
            Console.WriteLine();
            Console.WriteLine("This demo follows the step-by-step assignment structure.");
            Console.WriteLine("Test each step as you implement it to ensure correctness!");
            Console.WriteLine();
            Console.WriteLine("💡 Tip: Start with Steps 1-2 (Node and basic structure),");
            Console.WriteLine("   then proceed through Steps 3-7 in order.");
            Console.WriteLine();
            
            bool keepRunning = true;
            
            while (keepRunning)
            {
                DisplayCurrentList();
                DisplayStepBasedMenu();
                
                string choice = Console.ReadLine() ?? "";
                Console.WriteLine();
                
                try
                {
                    switch (choice.ToLower())
                    {
                        case "3":
                            TestStep3_AddOperations();
                            break;
                        case "4":
                            TestStep4_TraversalOperations();
                            break;
                        case "5":
                            TestStep5_SearchOperations();
                            break;
                        case "6":
                            TestStep6_RemoveOperations();
                            break;
                        case "7":
                            TestStep7_AdvancedOperations();
                            break;
                        case "a":
                            TestAllBasicOperations();
                            break;
                        case "p":
                            RunPerformanceTest();
                            break;
                        case "r":
                            ResetList();
                            break;
                        case "q":
                            keepRunning = false;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please select from the menu options.");
                            break;
                    }
                }
                catch (NotImplementedException ex)
                {
                    Console.WriteLine($"📝 TODO: {ex.Message}");
                    Console.WriteLine("💡 Implement this step first, then test it!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error: {ex.Message}");
                    Console.WriteLine("🔍 Check your implementation for bugs.");
                }
                
                if (keepRunning)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.WriteLine();
                }
            }
        }
        
        /// <summary>
        /// Display the current state of the list
        /// </summary>
        private void DisplayCurrentList()
        {
            Console.WriteLine("=== CURRENT LIST STATE ===");
            list.DisplayInfo();
            Console.WriteLine();
        }
        
        /// <summary>
        /// Display the step-based demo menu aligned with assignment structure
        /// </summary>
        private void DisplayStepBasedMenu()
        {
            Console.WriteLine("=== ASSIGNMENT STEPS TESTING MENU ===");
            Console.WriteLine("Test each step as you implement it:");
            Console.WriteLine();
            Console.WriteLine("📖 IMPLEMENTATION STEPS:");
            Console.WriteLine("  Steps 1-2: Node & basic structure (implement first)");
            Console.WriteLine();
            Console.WriteLine("🧪 TESTING OPTIONS:");
            Console.WriteLine("  3  → Test Step 3: Add Methods (AddFirst, AddLast, Insert)");
            Console.WriteLine("  4  → Test Step 4: Traversal Methods (Forward, Backward, ToArray)");
            Console.WriteLine("  5  → Test Step 5: Search Methods (Contains, Find, IndexOf)");
            Console.WriteLine("  6  → Test Step 6: Remove Methods (RemoveFirst, RemoveLast, etc.)");
            Console.WriteLine("  7  → Test Step 7: Advanced Operations (Reverse, Clear)");
            Console.WriteLine();
            Console.WriteLine("🚀 COMPREHENSIVE TESTING:");
            Console.WriteLine("  A  → Test All Basic Operations (Steps 3-6)");
            Console.WriteLine("  P  → Performance Test");
            Console.WriteLine();
            Console.WriteLine("🛠️  UTILITIES:");
            Console.WriteLine("  R  → Reset List");
            Console.WriteLine("  Q  → Return to Main Menu");
            Console.WriteLine();
            Console.Write("Enter your choice: ");
        }
        
        /// <summary>
        /// Test Step 3: Add Operations (AddFirst, AddLast, Insert)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/introduction-and-insertion-in-a-doubly-linked-list/
        /// </summary>
        private void TestStep3_AddOperations()
        {
            Console.WriteLine("=== TESTING STEP 3: ADD OPERATIONS ===");
            Console.WriteLine("📖 Assignment Reference: Step 3a-3c");
            Console.WriteLine();
            
            Console.WriteLine("Testing AddFirst (Step 3a):");
            Console.Write("Enter value to add to beginning: ");
            if (int.TryParse(Console.ReadLine(), out int value1))
            {
                try
                {
                    list.AddFirst(value1);
                    Console.WriteLine($"✅ Added {value1} to beginning");
                    list.DisplayForward();
                }
                catch (NotImplementedException ex)
                {
                    Console.WriteLine($"📝 TODO: {ex.Message}");
                    Console.WriteLine("💡 Implement AddFirst method in Step 3a first!");
                }
            }
            
            Console.WriteLine("\nTesting AddLast (Step 3b):");
            Console.Write("Enter value to add to end: ");
            if (int.TryParse(Console.ReadLine(), out int value2))
            {
                try
                {
                    list.AddLast(value2);
                    Console.WriteLine($"✅ Added {value2} to end");
                    list.DisplayForward();
                }
                catch (NotImplementedException ex)
                {
                    Console.WriteLine($"📝 TODO: {ex.Message}");
                    Console.WriteLine("💡 Implement AddLast method in Step 3b first!");
                }
            }
            
            Console.WriteLine("\nTesting Insert at position (Step 3c):");
            if (!list.IsEmpty)
            {
                Console.Write($"Enter index (0 to {list.Count}): ");
                if (int.TryParse(Console.ReadLine(), out int index))
                {
                    Console.Write("Enter value to insert: ");
                    if (int.TryParse(Console.ReadLine(), out int value3))
                    {
                        try
                        {
                            list.Insert(index, value3);
                            Console.WriteLine($"✅ Inserted {value3} at index {index}");
                            list.DisplayForward();
                        }
                        catch (NotImplementedException ex)
                        {
                            Console.WriteLine($"📝 TODO: {ex.Message}");
                            Console.WriteLine("💡 Implement Insert method in Step 3c first!");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("💡 Add some elements first to test Insert");
                Console.WriteLine("📝 Note: AddFirst and AddLast need to be implemented first");
            }
            
            Console.WriteLine("\n🎯 Step 3 Complete! Your add operations are working.");
        }
        
        /// <summary>
        /// Test Step 4: Traversal Operations (DisplayForward, DisplayBackward, ToArray)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
        /// </summary>
        private void TestStep4_TraversalOperations()
        {
            Console.WriteLine("=== TESTING STEP 4: TRAVERSAL OPERATIONS ===");
            Console.WriteLine("📖 Assignment Reference: Step 4a-4c");
            Console.WriteLine();
            
            if (list.IsEmpty)
            {
                Console.WriteLine("⚠️  List is empty. Add some elements first to test traversal.");
                Console.WriteLine("💡 Tip: Implement and test Step 3 (Add Methods) first!");
                return;
            }
            
            Console.WriteLine("Testing Forward Traversal (Step 4a):");
            try
            {
                list.DisplayForward();
            }
            catch (NotImplementedException ex)
            {
                Console.WriteLine($"📝 TODO: {ex.Message}");
                Console.WriteLine("💡 Implement DisplayForward method in Step 4a first!");
            }
            
            Console.WriteLine("\nTesting Backward Traversal (Step 4b):");
            Console.WriteLine("💡 This demonstrates the power of doubly linked lists!");
            try
            {
                list.DisplayBackward();
            }
            catch (NotImplementedException ex)
            {
                Console.WriteLine($"📝 TODO: {ex.Message}");
                Console.WriteLine("💡 Implement DisplayBackward method in Step 4b first!");
            }
            
            Console.WriteLine("\nTesting ToArray conversion (Step 4c):");
            try
            {
                var array = list.ToArray();
                Console.Write("Array result: [");
                for (int i = 0; i < array.Length; i++)
                {
                    Console.Write(array[i]);
                    if (i < array.Length - 1) Console.Write(", ");
                }
                Console.WriteLine("]");
            }
            catch (NotImplementedException ex)
            {
                Console.WriteLine($"📝 TODO: {ex.Message}");
                Console.WriteLine("💡 Implement ToArray method in Step 4c first!");
            }
            
            Console.WriteLine("\nTesting IEnumerable (foreach) support:");
            Console.Write("Foreach result: ");
            foreach (var item in list)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
            
            Console.WriteLine("\n🎯 Step 4 Complete! Your traversal methods are working.");
        }
        
        /// <summary>
        /// Test Step 5: Search Operations (Contains, Find, IndexOf)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/search-an-element-in-a-doubly-linked-list/
        /// </summary>
        private void TestStep5_SearchOperations()
        {
            Console.WriteLine("=== TESTING STEP 5: SEARCH OPERATIONS ===");
            Console.WriteLine("📖 Assignment Reference: Step 5a-5c");
            Console.WriteLine();
            
            if (list.IsEmpty)
            {
                Console.WriteLine("⚠️  List is empty. Add some elements first to test search.");
                return;
            }
            
            Console.Write("Enter value to search for: ");
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                // Test Contains (Step 5a)
                Console.WriteLine($"\nTesting Contains (Step 5a):");
                try
                {
                    bool contains = list.Contains(value);
                    Console.WriteLine($"Contains({value}): {(contains ? "✅ Found" : "❌ Not found")}");
                }
                catch (NotImplementedException)
                {
                    Console.WriteLine("📝 TODO: Implement Contains method in Step 5a");
                }
                
                // Test Find (Step 5b)
                Console.WriteLine($"\nTesting Find (Step 5b):");
                try
                {
                    var node = list.Find(value);
                    if (node != null)
                    {
                        Console.WriteLine($"Find({value}): ✅ Found node with value {node.Data}");
                    }
                    else
                    {
                        Console.WriteLine($"Find({value}): ❌ Not found");
                    }
                }
                catch (NotImplementedException)
                {
                    Console.WriteLine("📝 TODO: Implement Find method in Step 5b");
                }
                
                // Test IndexOf (Step 5c)
                Console.WriteLine($"\nTesting IndexOf (Step 5c):");
                try
                {
                    int index = list.IndexOf(value);
                    if (index >= 0)
                    {
                        Console.WriteLine($"IndexOf({value}): ✅ Found at index {index}");
                    }
                    else
                    {
                        Console.WriteLine($"IndexOf({value}): ❌ Not found (returned -1)");
                    }
                }
                catch (NotImplementedException)
                {
                    Console.WriteLine("📝 TODO: Implement IndexOf method in Step 5c");
                }
            }
            
            Console.WriteLine("\n🎯 Step 5 Complete! Your search operations are working.");
        }
        
        /// <summary>
        /// Test search operations
        /// </summary>
        private void TestSearchOperations()
        {
            Console.WriteLine("=== TESTING SEARCH OPERATIONS ===");
            Console.WriteLine();
            
            Console.Write("Enter value to search for: ");
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                var node = list.Find(value);
                bool contains = list.Contains(value);
                
                Console.WriteLine($"Find({value}): {(node != null ? $"Found at node with value {node.Data}" : "Not found")}");
                Console.WriteLine($"Contains({value}): {contains}");
            }
        }
        
        /// <summary>
        /// Test Step 6: Remove Operations (RemoveFirst, RemoveLast, Remove, RemoveAt)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/
        /// </summary>
        private void TestStep6_RemoveOperations()
        {
            Console.WriteLine("=== TESTING STEP 6: REMOVE OPERATIONS ===");
            Console.WriteLine("📖 Assignment Reference: Step 6a-6d");
            Console.WriteLine();
            
            if (list.IsEmpty)
            {
                Console.WriteLine("⚠️  List is empty. Add some elements first to test removal.");
                return;
            }
            
            Console.WriteLine("Choose remove operation to test:");
            Console.WriteLine("1. RemoveFirst (Step 6a)");
            Console.WriteLine("2. RemoveLast (Step 6b)");
            Console.WriteLine("3. Remove by value (Step 6c)");
            Console.WriteLine("4. RemoveAt index (Step 6d)");
            
            string choice = Console.ReadLine() ?? "";
            
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Testing RemoveFirst (Step 6a):");
                    try
                    {
                        int firstValue = list.RemoveFirst();
                        Console.WriteLine($"✅ Removed first element: {firstValue}");
                        list.DisplayForward();
                    }
                    catch (NotImplementedException)
                    {
                        Console.WriteLine("📝 TODO: Implement RemoveFirst method in Step 6a");
                    }
                    break;
                case "2":
                    Console.WriteLine("Testing RemoveLast (Step 6b):");
                    try
                    {
                        int lastValue = list.RemoveLast();
                        Console.WriteLine($"✅ Removed last element: {lastValue}");
                        list.DisplayForward();
                    }
                    catch (NotImplementedException)
                    {
                        Console.WriteLine("📝 TODO: Implement RemoveLast method in Step 6b");
                    }
                    break;
                case "3":
                    Console.WriteLine("Testing Remove by value (Step 6c):");
                    Console.Write("Enter value to remove: ");
                    if (int.TryParse(Console.ReadLine(), out int value))
                    {
                        try
                        {
                            bool removed = list.Remove(value);
                            Console.WriteLine(removed ? $"✅ Removed {value}" : $"❌ {value} not found");
                            list.DisplayForward();
                        }
                        catch (NotImplementedException)
                        {
                            Console.WriteLine("📝 TODO: Implement Remove method in Step 6c");
                        }
                    }
                    break;
                case "4":
                    Console.WriteLine("Testing RemoveAt index (Step 6d):");
                    Console.Write($"Enter index (0 to {list.Count - 1}): ");
                    if (int.TryParse(Console.ReadLine(), out int index))
                    {
                        try
                        {
                            int removedValue = list.RemoveAt(index);
                            Console.WriteLine($"✅ Removed {removedValue} from index {index}");
                            list.DisplayForward();
                        }
                        catch (NotImplementedException)
                        {
                            Console.WriteLine("📝 TODO: Implement RemoveAt method in Step 6d");
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
            
            Console.WriteLine("\n🎯 Step 6 Complete! Your remove operations are working.");
        }
        
        /// <summary>
        /// Test Step 7: Advanced Operations (Clear, Reverse)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/reverse-a-doubly-linked-list/
        /// </summary>
        private void TestStep7_AdvancedOperations()
        {
            Console.WriteLine("=== TESTING STEP 7: ADVANCED OPERATIONS ===");
            Console.WriteLine("📖 Assignment Reference: Step 7a-7b");
            Console.WriteLine();
            
            Console.WriteLine("Choose advanced operation to test:");
            Console.WriteLine("1. Reverse list (Step 7b)");
            Console.WriteLine("2. Clear list (Step 7a)");
            
            string choice = Console.ReadLine() ?? "";
            
            switch (choice)
            {
                case "1":
                    if (list.IsEmpty)
                    {
                        Console.WriteLine("⚠️  List is empty. Add some elements first to test reverse.");
                        return;
                    }
                    
                    Console.WriteLine("Testing Reverse (Step 7b):");
                    Console.WriteLine("Before reverse:");
                    list.DisplayForward();
                    
                    try
                    {
                        list.Reverse();
                        Console.WriteLine("After reverse:");
                        list.DisplayForward();
                        Console.WriteLine("✅ Reverse operation successful!");
                        Console.WriteLine("💡 This demonstrates the power of doubly linked lists - easy reversal!");
                    }
                    catch (NotImplementedException)
                    {
                        Console.WriteLine("📝 TODO: Implement Reverse method in Step 7b");
                    }
                    break;
                    
                case "2":
                    Console.WriteLine("Testing Clear (Step 7a):");
                    Console.WriteLine($"Before clear - Count: {list.Count}");
                    
                    try
                    {
                        list.Clear();
                        Console.WriteLine("After clear:");
                        list.DisplayInfo();
                        Console.WriteLine("✅ Clear operation successful!");
                    }
                    catch (NotImplementedException)
                    {
                        Console.WriteLine("📝 TODO: Implement Clear method in Step 7a");
                    }
                    break;
                    
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
            
            Console.WriteLine("\n🎯 Step 7 Complete! Your advanced operations are working.");
        }
        
        /// <summary>
        /// Test all basic operations in sequence - comprehensive testing
        /// </summary>
        private void TestAllBasicOperations()
        {
            Console.WriteLine("=== COMPREHENSIVE TESTING: ALL BASIC OPERATIONS ===");
            Console.WriteLine("Testing Steps 3-6 in sequence...");
            Console.WriteLine();
            
            try
            {
                // Test Step 3: Add operations
                Console.WriteLine("🔸 Testing Add Operations (Step 3):");
                list.AddFirst(10);
                list.AddLast(20);
                list.AddFirst(5);
                list.Insert(2, 15);
                Console.WriteLine("Added: 5 (first), 10, 15 (insert), 20 (last)");
                list.DisplayForward();
                
                // Test Step 4: Traversal
                Console.WriteLine("\n🔸 Testing Traversal (Step 4):");
                Console.WriteLine("Forward and backward should show same elements:");
                list.DisplayForward();
                list.DisplayBackward();
                
                // Test Step 5: Search
                Console.WriteLine("\n🔸 Testing Search Operations (Step 5):");
                Console.WriteLine($"Contains 15: {list.Contains(15)}");
                Console.WriteLine($"Contains 99: {list.Contains(99)}");
                Console.WriteLine($"IndexOf 20: {list.IndexOf(20)}");
                
                // Test Step 6: Remove
                Console.WriteLine("\n🔸 Testing Remove Operations (Step 6):");
                list.Remove(15);
                Console.WriteLine("Removed 15 by value");
                list.RemoveFirst();
                Console.WriteLine("Removed first element");
                list.DisplayForward();
                
                Console.WriteLine("\n✅ All basic operations tested successfully!");
                Console.WriteLine("🎉 Your doubly linked list implementation is working!");
                
            }
            catch (NotImplementedException ex)
            {
                Console.WriteLine($"📝 TODO: {ex.Message}");
                Console.WriteLine("💡 Complete the missing steps first, then run this test again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error during testing: {ex.Message}");
                Console.WriteLine("🔍 Check your implementation for bugs.");
            }
        }
        
        /// <summary>
        /// Run performance test comparing operations
        /// </summary>
        private void RunPerformanceTest()
        {
            Console.WriteLine("=== PERFORMANCE TEST ===");
            Console.WriteLine();
            Console.WriteLine("Testing performance of various operations...");
            Console.WriteLine();
            
            var testList = new DoublyLinkedList<int>();
            var sw = new Stopwatch();
            
            // Test insertions
            Console.WriteLine("Testing 10,000 insertions at end:");
            sw.Start();
            for (int i = 0; i < 10000; i++)
            {
                testList.Add(i);
            }
            sw.Stop();
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");
            
            // Test search
            sw.Restart();
            testList.Contains(5000);
            sw.Stop();
            Console.WriteLine($"Search for middle element: {sw.ElapsedTicks} ticks");
            
            // Test removal
            sw.Restart();
            testList.RemoveFirst();
            sw.Stop();
            Console.WriteLine($"Remove first: {sw.ElapsedTicks} ticks");
            
            sw.Restart();
            testList.RemoveLast();
            sw.Stop();
            Console.WriteLine($"Remove last: {sw.ElapsedTicks} ticks");
            
            Console.WriteLine($"Final list size: {testList.Count}");
        }
        
        /// <summary>
        /// Reset the list to empty state
        /// </summary>
        private void ResetList()
        {
            list.Clear();
            Console.WriteLine("List has been reset to empty state.");
        }
    }
}