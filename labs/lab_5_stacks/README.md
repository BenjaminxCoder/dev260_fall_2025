# Stack Lab - Interactive Learning

## 📚 What You'll Build

In this hands-on lab, you'll build a complete interactive Stack<T> application featuring an action history system with undo/redo functionality. This is a real-world application pattern used in text editors, image editors, and many other programs!

## 🎯 Learning Objectives

By the end of this lab, you will:

- **Understand LIFO behavior** - Last In, First Out through hands-on building
- **Master core stack operations** - `Push`, `Pop`, `Peek`, `Clear`
- **Build proper safety patterns** - Guard logic for empty stack scenarios
- **Create interactive applications** - Menu-driven console interface
- **Implement advanced patterns** - Dual-stack undo/redo functionality
- **Display data effectively** - Show stack contents without modification
- **Apply real-world concepts** - Recognize when to use stacks vs other data structures

## � Getting Started

### What You Have

- **`StackStarter.cs`** - Your starting point with step-by-step TODO guidance
- **Quick Reference Guide** - Built into the skeleton file with Stack<T> operations and helpful patterns

### Your Mission

Follow along with your instructor to build each method step-by-step. You'll start with empty method stubs and gradually build a complete, working application!

## 📋 What You'll Build Step-by-Step

### Phase 1: Foundation (Steps 1-3)

- Set up your stack variables and operation counter
- Build the main program loop
- Create an attractive menu system

### Phase 2: Core Operations (Steps 4-6)

- **HandlePush** - Add actions to your history with validation
- **HandlePop** - Remove actions safely with proper error handling
- **HandlePeek** - Look at the top action without removing it

### Phase 3: Display & Management (Steps 7-8)

- **HandleDisplay** - Show your action history in LIFO order
- **HandleClear** - Reset your history with confirmation

### Phase 4: Advanced Features (Steps 9-10)

- **HandleUndo** - Restore previously removed actions
- **HandleRedo** - Re-remove actions (undo your undo!)

### Phase 5: Polish (Steps 11-12)

- **ShowStatistics** - Display session information
- **ShowSessionSummary** - Farewell message with final stats

## 🎓 Key Concepts You'll Learn

### Interactive Application Structure

You'll build a menu-driven application using this pattern:

```csharp
// Main program loop pattern
bool running = true;
while (running) {
    DisplayMenu();
    string choice = Console.ReadLine()?.ToLower() ?? "";
    // Handle user choice with switch statement
}
```

### The Dual Stack Pattern (Undo/Redo)

Learn how professional applications implement undo/redo:

```csharp
// Two stacks working together
Stack<string> actionHistory = new Stack<string>();  // Main actions
Stack<string> undoHistory = new Stack<string>();    // For redo functionality

// When user does action: push to actionHistory, clear undoHistory
// When user undos: pop from actionHistory, push to undoHistory
// When user redos: pop from undoHistory, push to actionHistory
```

### Essential Safety Pattern

Always protect your stack operations:

```csharp
// Always guard Pop() and Peek() operations
if (stack.Count > 0) {
    string item = stack.Pop();
    // Safe to use item
} else {
    Console.WriteLine("Stack is empty!");
}
```

### LIFO in Action

See the Last In, First Out principle work:

```csharp
// Push: A, B, C (C is now on top)
// Pop order: C, B, A (Last In, First Out)
```

## ⚠️ Watch Out For These Common Mistakes

1. **Forgetting guard clauses** - Always check `Count > 0` before `Pop()` or `Peek()`
2. **Expecting random access** - Stacks only let you work with the top item
3. **LIFO confusion** - Remember: Last item pushed is first item popped
4. **Poor error handling** - Always give users clear feedback when operations can't be performed

## � How to Follow Along

1. **Start with `StudentSkeleton.cs`** - Your instructor will guide you through each step
2. **Code along actively** - Don't just watch, type the code yourself!
3. **Test frequently** - Run your application after each major step to see it working
4. **Ask questions** - If something doesn't make sense, ask immediately
5. **Use the Quick Reference** - The guide at the top of your file has all the Stack<T> operations

## 🧪 Testing Your Code

As you build each method, test your application by running:

```bash
dotnet run
```

Try these test scenarios:

- **Push several actions** - Add "Open File", "Edit Text", "Save File"
- **Pop actions** - Remove them and see the LIFO order
- **Try empty operations** - Pop from empty stack to see error handling
- **Test undo/redo** - Push some actions, pop them, then undo to restore
- **Check statistics** - View your session stats throughout development

## 🎯 Success Checklist

By the end of the lab, your application should:

- ✅ Accept user input through an interactive menu
- ✅ Push actions to a stack with validation
- ✅ Pop actions safely with proper error messages
- ✅ Peek at the top action without modifying the stack
- ✅ Display all actions in LIFO order with clear formatting
- ✅ Clear the entire history with confirmation
- ✅ Undo previous removals (restore actions)
- ✅ Redo removals (re-remove restored actions)
- ✅ Show session statistics
- ✅ Handle all edge cases gracefully (empty stacks, invalid input)

## 🚀 What's Next?

### Immediate Enhancements You Could Add

- **Input validation** - Prevent empty strings, add length limits
- **Timestamps** - Show when each action was performed
- **Search functionality** - Find specific actions in your history
- **Save/Load** - Persist your session to a file
- **Different data types** - Use Stack<int> for a calculator

### Future Learning

- **Queue<T>** - Learn FIFO (First In, First Out) data structures
- **Custom implementations** - Build your own stack using arrays or linked lists
- **Advanced applications** - Expression evaluation, syntax parsing, graph algorithms

### Real-World Applications

- **Text Editors** - Undo/Redo functionality (VS Code, Word, etc.)
- **Image Editors** - Photoshop's history panel
- **Web Browsers** - Back button navigation
- **Programming Languages** - Function call stack management
- **Calculators** - Expression evaluation and operator precedence
- **Game Development** - State management and game history

## � Development Environment Tips

### Visual Studio Code

- Use the integrated terminal: `Ctrl+`` (backtick)
- Install C# Dev Kit extension for better IntelliSense
- Use `Ctrl+Shift+P` then "C#: Restart OmniSharp" if IntelliSense stops working

### Visual Studio (Full)

- Create new Console App targeting .NET 9.0
- Use `Ctrl+F5` to run without debugging
- Set breakpoints to step through your code

### Online Environments

- All code works in standard .NET environments like Repl.it
- Some online terminals may require pressing Enter instead of any key for `Console.ReadKey()`

---

## 🎉 You Did It!

Congratulations on building a complete interactive Stack application! You've learned fundamental computer science concepts and built something that uses real-world programming patterns. The skills you've developed here apply to many areas of software development.

**Keep coding and exploring! 🚀**
