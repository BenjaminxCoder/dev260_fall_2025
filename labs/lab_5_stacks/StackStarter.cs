using System;
using System.Collections.Generic;

/*
=== QUICK REFERENCE GUIDE ===

Stack<T> Essential Operations:
- new Stack<string>()           // Create empty stack
- stack.Push(item)              // Add item to top (LIFO)
- stack.Pop()                   // Remove and return top item
- stack.Peek()                  // Look at top item (don't remove)
- stack.Clear()                 // Remove all items
- stack.Count                   // Get number of items

Safety Rules:
- ALWAYS check stack.Count > 0 before Pop() or Peek()
- Empty stack Pop() throws InvalidOperationException
- Empty stack Peek() throws InvalidOperationException

Common Patterns:
- Guard clause: if (stack.Count > 0) { ... }
- LIFO order: Last item pushed is first item popped
- Enumeration: foreach gives top-to-bottom order

Helpful icons!:
- ✅ Success
- ❌ Error
- 👀 Look
- 📋 Display out
- ℹ️ Information
- 📊 Stats
- 📝 Write
*/

namespace StackLab
{
    /// <summary>
    /// Student skeleton version - follow along with instructor to build this out!
    /// Uncomment the class name and Main method when ready to use this version.
    /// </summary>
    // class Program  // Uncomment this line when ready to use
    class StudentSkeleton
    {

        // TODO: Step 1 - Declare two stacks for action history and undo functionality
        static Stack<string> actionHistory = new Stack<string>();
        static Stack<string> undoHistory = new Stack<string>();

        // TODO: Step 2 - Add a counter for total operations
        static int totalOperations = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("=== Interactive Stack Demo ===");
            Console.WriteLine("Building an action history system with undo/redo\n");

            bool running = true;
            while (running)
            {
                DisplayMenu();
                string choice = Console.ReadLine()?.ToLower() ?? "";

                switch (choice)
                {
                    case "1":
                    case "push":
                        HandlePush();
                        break;
                    case "2":
                    case "pop":
                        HandlePop();
                        break;
                    case "3":
                    case "peek":
                    case "top":
                        HandlePeek();
                        break;
                    case "4":
                    case "display":
                        HandleDisplay();
                        break;
                    case "5":
                    case "clear":
                        HandleClear();
                        break;
                    case "6":
                    case "undo":
                        HandleUndo();
                        break;
                    case "7":
                    case "redo":
                        HandleRedo();
                        break;
                    case "8":
                    case "stats":
                        ShowStatistics();
                        break;
                    case "9":
                    case "exit":
                        running = false;
                        ShowSessionSummary();
                        break;
                    default:
                        Console.WriteLine("❌ Invalid choice. Please try again.\n");
                        break;
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("┌─ Stack Operations Menu ─────────────────────────┐");
            Console.WriteLine("│ 1. Push      │ 2. Pop       │ 3. Peek/Top    │");
            Console.WriteLine("│ 4. Display   │ 5. Clear     │ 6. Undo        │");
            Console.WriteLine("│ 7. Redo      │ 8. Stats     │ 9. Exit        │");
            Console.WriteLine("└─────────────────────────────────────────────────┘");
            // TODO: Step 3 - add stack size and total operations to our display
            Console.WriteLine($" Current size: {actionHistory.Count} | Undone: {undoHistory.Count} | Ops: {totalOperations}");
            Console.Write("\nChoose operation (number or name): ");
        }

        // TODO: Step 4 - Implement HandlePush method
        static void HandlePush()
        {
            Console.Write(" Enter action to push: ");
            string? input = Console.ReadLine();
            string value = input?.Trim() ?? "";

            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine(" Empty input not allowed.\n");
                return;
            }

            actionHistory.Push(value);
            undoHistory.Clear();
            totalOperations++;
            Console.WriteLine($" Pushed: \"{value}\". Top is now: \"{actionHistory.Peek()}\".\n");
        }

        // TODO: Step 5 - Implement HandlePop method
        static void HandlePop()
        {
            if (actionHistory.Count == 0)
            {
                Console.WriteLine(" Cannot pop. Stack is empty.\n");
                return;
            }

            string removed = actionHistory.Pop();
            undoHistory.Push(removed);
            totalOperations++;

            string nextTop = actionHistory.Count > 0 ? $"Top now: \"{actionHistory.Peek()}\"" : "Stack is now empty";
            Console.WriteLine($" popped: \"{removed}\". {nextTop}.\n");
        }

        // TODO: Step 6 - Implement HandlePeek method
        static void HandlePeek()
        {
            if (actionHistory.Count == 0)
            {
                Console.WriteLine(" Stack is empty. Nothing to peek.\n");
                return;
            }

            Console.WriteLine($" Top item: \"{actionHistory.Peek()}\" (size={actionHistory.Count}).\n");
        }

        // TODO: Step 7 - Implement HandleDisplay method
        static void HandleDisplay()
        {
            Console.WriteLine(" Action History (Top -> Bottom):");
            if (actionHistory.Count == 0)
            {
                Console.WriteLine("  [empty]\n");
                return;
            }

            int index = 0;
            foreach (var item in actionHistory)
            {
                if (index == 0)
                    Console.WriteLine($"  [{index}] {item}  <- TOP");
                else
                    Console.WriteLine($"  [{index}] {item}");
                index++;
            }
            Console.WriteLine($"Total items: {actionHistory.Count}\n");
        }

        // TODO: Step 8 - Implement HandleClear method
        static void HandleClear()
        {
            if (actionHistory.Count == 0 && undoHistory.Count == 0)
            {
                Console.WriteLine(" Nothing to clear.\n");
                return;
            }

            int clearedAction = actionHistory.Count;
            actionHistory.Clear();
            undoHistory.Clear();
            totalOperations++;
            Console.WriteLine($" Cleared {clearedAction} action(s). Undo history reset. \n");
        }

        // TODO: Step 9 - Implement HandleUndo method (Advanced)
        static void HandleUndo()
        {
            if (undoHistory.Count == 0)
            {
                Console.WriteLine(" Nothing to undo.\n");
                return;
            }

            string restored = undoHistory.Pop();
            actionHistory.Push(restored);
            totalOperations++;
            Console.WriteLine($" Undid last removal. Restored: \"{restored}\". Top is now: \"{actionHistory.Peek()}\".\n");
        }

        // TODO: Step 10 - Implement HandleRedo method (Advanced)
        static void HandleRedo()
        {
            if (actionHistory.Count == 0)
            {
                Console.WriteLine(" Nothing to redo.\n");
                return;
            }

            // Redo re-applies a removal on the current top
            string redone = actionHistory.Pop();
            undoHistory.Push(redone);
            totalOperations++;
            string state = actionHistory.Count > 0 ? $"Top now: \"{actionHistory.Peek()}\"" : "Stack is now empty";
            Console.WriteLine($" Redid removal of: \"{redone}\". {state}.\n");
        }

        // TODO: Step 11 - Implement ShowStatistics method
        static void ShowStatistics()
        {
            Console.WriteLine(" Session Statistics");
            Console.WriteLine($" Current stack size: {actionHistory.Count}");
            Console.WriteLine($" Undo stack size:    {undoHistory.Count}");
            Console.WriteLine($" Total operations:   {totalOperations}");
            Console.WriteLine($" Is empty:           {(actionHistory.Count == 0 ? "Yes" : "No")}");
            if (actionHistory.Count > 0)
                Console.WriteLine($"  Current top:      \"{actionHistory.Peek()}\"");
            Console.WriteLine();
        }

        // TODO: Step 12 - Implement ShowSessionSummary method
        static void ShowSessionSummary()
        {
            Console.WriteLine("\n=== Session Summary ===");
            Console.WriteLine($"Total operations: {totalOperations}");
            Console.WriteLine($"Final stack size: {actionHistory.Count}");

            if (actionHistory.Count > 0)
            {
                Console.WriteLine("Remaining actions (Top -> Bottom):");
                int idx = 0;
                foreach (var item in actionHistory)
                {
                    if (idx == 0) Console.WriteLine($"  [{idx}] {item}  <- TOP");
                    else Console.WriteLine($"  [{idx}] {item}");
                    idx++;
                }
            }
            else
            {
                Console.WriteLine("No remaining actions.");
            }

            Console.WriteLine("\nGood work. Press any key to exit.");
            Console.ReadKey(true);
        }
    }
}
