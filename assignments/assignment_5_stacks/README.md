# Assignment 5: Browser Navigation System

## 🌐 Overview

Build a **Browser Navigation System** that simulates how web browsers handle the back/forward navigation using stacks! This assignment applies the Stack<T> concepts from Lab 5 to create a real-world application that mimics browser behavior.

## 🎯 Learning Objectives

By completing this assignment, you will:

- **Apply dual-stack patterns** learned in Lab 5 to solve real-world problems
- **Implement browser-like navigation** using back and forward stacks
- **Handle edge cases** with proper guard clauses and user feedback
- **Design clean user interfaces** for console applications
- **Practice defensive programming** with input validation
- **Implement stretch features** like history limits and data persistence

## 📋 Requirements Overview

### Core Features (Required)

1. **URL Navigation System**

   - Visit new URLs (push to back stack, clear forward stack)
   - Navigate backward through history
   - Navigate forward through previously visited pages
   - Display current page information

2. **Stack Management**

   - Proper LIFO behavior for both back and forward stacks
   - Guard clauses for empty stack operations
   - Clear user feedback for all operations

3. **User Interface**
   - Interactive menu system
   - Clear display of current page and navigation options
   - Professional error messages and confirmations

### Stretch Features (Optional - Extra Credit)

4. **History Management**

   - Limit history size (evict oldest when limit exceeded)
   - Clear all history option
   - Display navigation statistics

5. **Data Persistence**
   - Export browsing session to JSON file
   - Import previous session from JSON file
   - Save/load different browsing profiles

## 🔧 Technical Specifications

### Core Classes

The assignment includes these key components:

- **`BrowserSession`** - Main class managing navigation state (you implement core methods)
- **`WebPage`** - Represents a web page with URL, title, and timestamp (provided)
- **`BrowserNavigator`** - Handles the interactive menu and user commands (provided)
- **`Program`** - Entry point and application orchestration (provided)

### Data Structures

```csharp
// Main navigation stacks
Stack<WebPage> backStack = new Stack<WebPage>();
Stack<WebPage> forwardStack = new Stack<WebPage>();
WebPage? currentPage = null;
```

### Key Operations

1. **Visit URL**: `currentPage` → `backStack`, clear `forwardStack`, set new `currentPage`
2. **Go Back**: `currentPage` → `forwardStack`, pop from `backStack` → `currentPage`
3. **Go Forward**: `currentPage` → `backStack`, pop from `forwardStack` → `currentPage`
4. **Display Back History**: Show back stack contents with numbering and formatting
5. **Display Forward History**: Show forward stack contents with numbering and formatting
6. **Clear History**: Count total pages, clear both stacks, show confirmation

## 🎯 Implementation Requirements

You will need to implement **6 key methods** in the `BrowserSession` class:

### Core Navigation Methods

- `VisitUrl(string url, string title)` - Handle visiting new URLs
- `GoBack()` - Navigate backward through history
- `GoForward()` - Navigate forward through history

### Display & Management Methods

- `DisplayBackHistory()` - Show back navigation history with proper formatting
- `DisplayForwardHistory()` - Show forward navigation history with proper formatting
- `ClearHistory()` - Clear all navigation history with count confirmation

_All other classes and UI components are provided - focus on these core methods!_

## 📝 Detailed Requirements

### 1. Core Navigation Logic

- **Visit new URL**: Save current page to back history, clear forward history
- **Back navigation**: Move current page to forward stack, restore from back stack
- **Forward navigation**: Move current page to back stack, restore from forward stack
- **Display history**: Show back and forward history with proper formatting
- **Clear history**: Remove all navigation history with confirmation
- **Always guard against empty stacks** with helpful error messages

### 2. Input Validation

- Validate URLs (basic format checking)
- Handle empty/null inputs gracefully
- Provide helpful error messages for invalid operations

## 🧪 Testing Requirements

Your application must handle these scenarios:

### Basic Navigation Tests

1. Visit multiple URLs in sequence
2. Navigate back through history
3. Navigate forward through previously visited pages
4. Try back/forward on empty stacks (should show appropriate messages)

### Edge Case Tests

1. Visit URL when no current page exists
2. Navigate back to the beginning of history
3. Navigate forward to the end of history
4. Visit new URL after going back (should clear forward history)

### User Experience Tests

1. Display current page information clearly
2. Show when back/forward options are available
3. Handle invalid URLs gracefully
4. Provide clear confirmation messages

## 🎯 Grading Rubric

### Core Implementation (90 points)

| Component              | Points | Requirements                                                     |
| ---------------------- | ------ | ---------------------------------------------------------------- |
| **Core Navigation**    | 35     | VisitUrl, GoBack, GoForward methods work correctly               |
| **Display Methods**    | 25     | DisplayBackHistory, DisplayForwardHistory with proper formatting |
| **History Management** | 15     | ClearHistory method with count tracking and confirmation         |
| **Stack Management**   | 15     | Proper dual-stack pattern implementation and LIFO behavior       |

### Code Quality and Documentation (15 points)

| Aspect                     | Points | Requirements                                                 |
| -------------------------- | ------ | ------------------------------------------------------------ |
| **Implementation Quality** | 5      | Clean method implementations, proper logic flow              |
| **Error Handling**         | 5      | Guard clauses, edge case handling, meaningful error messages |
| **Documentation**          | 5      | Complete ASSIGNMENT_NOTES.md with thoughtful reflection      |

### Stretch Features (5 points - Extra Credit)

| Feature                | Points | Requirements                             |
| ---------------------- | ------ | ---------------------------------------- |
| **History Limits**     | 2      | Implement max history size with eviction |
| **JSON Export/Import** | 3      | Save/load session data                   |

**Total: 105 points (110 with extra credit)**

## 📚 Implementation Guide

### Phase 1: Basic Structure

1. Review the provided `WebPage`, `BrowserNavigator`, and `Program` classes
2. Understand the `BrowserSession` class structure with dual stacks
3. Implement "Visit URL" functionality

### Phase 2: Core Navigation Logic

1. Implement "Go Back" with proper stack management
2. Implement "Go Forward" with proper stack management
3. Add guard clauses and error handling for navigation

### Phase 3: History Management

1. Implement "DisplayBackHistory" with proper formatting
2. Implement "DisplayForwardHistory" with proper formatting
3. Implement "ClearHistory" with count tracking and confirmation

### Phase 4: Testing & Documentation

1. Test all navigation scenarios thoroughly
2. Verify history display shows correct information
3. Test edge cases (empty stacks, clear operations)
4. Document your implementation and testing in ASSIGNMENT_NOTES.md

## 💡 Tips for Success

### Understanding the Browser Model

- **Current Page**: What user is viewing now
- **Back Stack**: Pages user can go back to (most recent on top)
- **Forward Stack**: Pages user can go forward to (only exists after going back)

### Common Pitfalls to Avoid

1. **Forgetting to clear forward stack** when visiting new URL
2. **Not handling empty stacks** properly
3. **Confusing the direction** of stack operations
4. **Poor user feedback** for unavailable operations

### Testing Strategy

- Test each operation individually first
- Then test combinations (visit → back → forward → visit)
- Always test edge cases (empty stacks, invalid input)
- Use the display feature frequently to verify state

## 📅 Submission Requirements

### What to Submit

1. **All source code files** (.cs files)
2. **Project file** (Assignment5.csproj)
3. **ASSIGNMENT_NOTES.md** with your implementation notes and testing documentation

### Submission Format

- Submit link to your GitHub repository
- Code should be in `assignments/assignment_5_stacks` directory
- Include clear commit messages showing your development process

### Due Date

**Due: October 29, 2024** by 11:59 PM

## 🚀 Getting Started

1. **Review Lab 5 concepts** - This assignment builds directly on the dual-stack pattern
2. **Understand the provided structure** - Examine the WebPage and BrowserNavigator classes
3. **Implement visit functionality** - This is the foundation for everything else
4. **Add navigation incrementally** - Back first, then forward
5. **Test frequently** - Verify each feature works before moving on

## 🎓 Real-World Applications

This assignment teaches concepts used in:

- **Web browsers** (Chrome, Firefox, Safari navigation)
- **Text editors** (undo/redo with branching)
- **Game development** (state management, save systems)
- **Mobile apps** (navigation stacks, view controllers)
- **Database systems** (transaction rollback, change tracking)

---

**Remember**: This assignment is about applying Lab 5 concepts to solve a real problem. Focus on understanding the dual-stack pattern and how it creates the illusion of "traveling through time" in your browsing history!

**Good luck building your browser! 🌐**
