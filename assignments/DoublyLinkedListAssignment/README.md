# Assignment 3: Doubly Linked Lists

**Course:** DEV 260 - Data Structures and Algorithms  
**Due Date:** October 24th 2025  
**Points:** 100

## 📚 Learning Objectives

By completing this assignment, you will:

1. **Understand** the structure and benefits of doubly linked lists
2. **Implement** a generic doubly linked list from scratch through guided steps
3. **Apply** doubly linked lists to a real-worl---

## 📚 Learning Resource playlist scenario

4. **Analyze** performance characteristics compared to other data structures
5. **Master** pointer manipulation and memory management concepts

## 🎯 Assignment Overview

This assignment is divided into two progressive parts with procedural, step-by-step implementation:

- **Part A:** Core Implementation - Build your doubly linked list step by step (60 points)
- **Part B:** Music Playlist Manager - Apply your implementation to a practical scenario (40 points)

**Important:** Part A is designed to be completed procedurally. Each step builds on the previous one, and you should test each component before moving to the next step.

---

## 📋 Part A: Core Implementation (60 points)

### Step 1: Node Structure (5 points)

**📚 Reference:** [Doubly Linked List Introduction](https://www.c-sharpcorner.com/article/doubly-linked-list-and-circular-linked-list-in-c-sharp/) - C# Corner

**File:** `DoublyLinkedList.cs`

First, implement the basic node structure that will hold your data and pointers.

```csharp
public class Node<T>
{
    public T Data { get; set; }
    public Node<T>? Next { get; set; }
    public Node<T>? Previous { get; set; }

    public Node(T data)
    {
        Data = data;
        Next = null;
        Previous = null;
    }
    // 📖 See: https://www.geeksforgeeks.org/dsa/doubly-linked-list/
}
```

**Test Step 1:** Create a simple node and verify you can access its properties.

### Step 2: Basic List Structure and Properties (5 points)

**📚 Reference:** [C# Generics](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics) - Microsoft Docs

Add the basic doubly linked list class with essential properties:

```csharp
public class DoublyLinkedList<T>
{
    private Node<T>? head;
    private Node<T>? tail;
    private int count;

    public int Count => count;
    public bool IsEmpty => count == 0;

    public DoublyLinkedList()
    {
        head = null;
        tail = null;
        count = 0;
    }
}
```

**Test Step 2:** Create an empty list and verify Count and IsEmpty work correctly.

### Step 3: Add Methods (15 points)

Implement adding elements in order of complexity:

#### Step 3a: AddFirst Method (5 points)

**📚 Reference:** [Insertion in Doubly Linked List](https://www.geeksforgeeks.org/dsa/introduction-and-insertion-in-a-doubly-linked-list/) - GeeksforGeeks

```csharp
public void AddFirst(T item)
{
    // TODO: Implement adding to the beginning of the list
    // Handle both empty list and non-empty list cases
    // 📖 See: https://www.geeksforgeeks.org/dsa/introduction-and-insertion-in-a-doubly-linked-list/#insertion-at-the-beginning-in-doubly-linked-list
}
```

**Test Step 3a:** Add several items to the beginning and verify order.

#### Step 3b: AddLast Method (5 points)

**📚 Reference:** [Insertion at the End](https://www.geeksforgeeks.org/dsa/introduction-and-insertion-in-a-doubly-linked-list/#insertion-at-the-end-in-doubly-linked-list) - GeeksforGeeks

```csharp
public void AddLast(T item)
{
    // TODO: Implement adding to the end of the list
    // Handle both empty list and non-empty list cases
}

// Convenience method
public void Add(T item) => AddLast(item);
```

**Test Step 3b:** Add several items to the end and verify order.

#### Step 3c: Insert at Position (5 points)

**📚 Reference:** [Insert After Given Node](https://www.geeksforgeeks.org/dsa/introduction-and-insertion-in-a-doubly-linked-list/#insertion-after-a-given-node-in-doubly-linked-list) - GeeksforGeeks

```csharp
public void Insert(int index, T item)
{
    // TODO: Implement inserting at a specific position
    // Handle edge cases: index 0, index == count, invalid indices
}
```

**Test Step 3c:** Insert items at various positions and verify list integrity.

### Step 4: Basic Traversal and Display (10 points)

#### Step 4a: Forward Traversal (5 points)

**📚 Reference:** [Traversal in Doubly Linked List](https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/) - GeeksforGeeks

```csharp
public void PrintForward()
{
    // TODO: Print all elements from head to tail
}

public T[] ToArray()
{
    // TODO: Convert list to array for easy testing
}
```

#### Step 4b: Backward Traversal (5 points)

**📚 Reference:** [Backward Traversal](https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/) - GeeksforGeeks

```csharp
public void PrintBackward()
{
    // TODO: Print all elements from tail to head
    // This demonstrates the power of doubly linked lists!
}
```

**Test Step 4:** Verify you can traverse in both directions and get the same elements.

### Step 5: Search Methods (10 points)

#### Step 5a: Contains Method (5 points)

**📚 Reference:** [Search in Doubly Linked List](https://www.geeksforgeeks.org/dsa/search-an-element-in-a-doubly-linked-list/) - GeeksforGeeks

```csharp
public bool Contains(T item)
{
    // TODO: Return true if item exists in the list
}
```

#### Step 5b: Find Method (5 points)

**📚 Reference:** [Search Operations](https://www.geeksforgeeks.org/dsa/search-an-element-in-a-doubly-linked-list/) - GeeksforGeeks

```csharp
public Node<T>? Find(T item)
{
    // TODO: Return the node containing the item, or null if not found
}

public int IndexOf(T item)
{
    // TODO: Return the index of the item, or -1 if not found
}
```

**Test Step 5:** Search for existing and non-existing items.

### Step 6: Remove Methods (15 points)

Implement removal in order of complexity:

#### Step 6a: RemoveFirst and RemoveLast (5 points)

**📚 Reference:** [Deletion in Doubly Linked List](https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/) - GeeksforGeeks

```csharp
public bool RemoveFirst()
{
    // TODO: Remove and return the first element
    // Handle empty list case
    // 📖 See: https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/#deletion-at-the-beginning-in-doubly-linked-list
}

public bool RemoveLast()
{
    // TODO: Remove and return the last element
    // Handle empty list case
    // 📖 See: https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/#deletion-at-the-end-in-doubly-linked-list
}
```

**Test Step 6a:** Remove from both ends and verify list integrity.

#### Step 6b: Remove by Value (5 points)

**📚 Reference:** [Delete by Key](https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/) - GeeksforGeeks

```csharp
public bool Remove(T item)
{
    // TODO: Remove the first occurrence of the item
    // Return true if removed, false if not found
}
```

**Test Step 6b:** Remove existing and non-existing items.

#### Step 6c: RemoveAt Index (5 points)

**📚 Reference:** [Delete at Position](https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/#deletion-at-a-specific-position-in-doubly-linked-list) - GeeksforGeeks

```csharp
public bool RemoveAt(int index)
{
    // TODO: Remove element at specific index
    // Handle invalid indices
}
```

**Test Step 6c:** Remove items at various positions.

### Step 7: Advanced Operations (Bonus - 5 points)

**📚 Reference:** [Reverse a Doubly Linked List](https://www.geeksforgeeks.org/dsa/reverse-a-doubly-linked-list/) - GeeksforGeeks

```csharp
public void Reverse()
{
    // TODO: Reverse the entire list in-place
    // Swap Next and Previous pointers for each node
}

public void Clear()
{
    // TODO: Remove all elements from the list
}
}
```

**Test Step 7:** Verify reverse operation and clear functionality.

---

## 🎵 Part B: Music Playlist Manager (40 points)

### Overview

Apply your doubly linked list to create a functional music playlist manager that demonstrates the benefits of bidirectional navigation.

### Step 8: Song Class (5 points)

**📚 Reference:** [C# Classes and Objects](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/types/classes) - Microsoft Docs

**File:** `MusicPlaylist.cs`

```csharp
public class Song
{
    public string Title { get; set; }
    public string Artist { get; set; }
    public int Year { get; set; }
    public TimeSpan Duration { get; set; }

    // TODO: Implement constructor and ToString method
    // 📖 See: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/constructors
}
```

### Step 9: Playlist Core Structure (10 points)

**📚 Reference:** [Using Your Data Structure](https://www.geeksforgeeks.org/applications-of-linked-list-data-structure/) - GeeksforGeeks

```csharp
public class MusicPlaylist
{
    private DoublyLinkedList<Song> playlist;
    private Node<Song>? currentSong;

    public MusicPlaylist()
    {
        playlist = new DoublyLinkedList<Song>();
        currentSong = null;
    }

    // TODO: Implement basic properties
    public int TotalSongs => playlist.Count;
    public Song? CurrentSong => currentSong?.Data;
    public bool HasSongs => playlist.Count > 0;
}
```

### Step 10: Playlist Management (15 points)

#### Step 10a: Adding Songs (5 points)

**📚 Reference:** [Linked List Applications](https://www.geeksforgeeks.org/applications-of-linked-list-data-structure/) - GeeksforGeeks

```csharp
public void AddSong(Song song)
{
    // TODO: Add song to end of playlist
}

public void AddSongAt(int position, Song song)
{
    // TODO: Add song at specific position
}
```

#### Step 10b: Removing Songs (5 points)

**📚 Reference:** [Playlist Management](https://www.geeksforgeeks.org/applications-of-linked-list-data-structure/) - GeeksforGeeks

```csharp
public bool RemoveSong(Song song)
{
    // TODO: Remove specific song
    // Handle if it's the current song
}

public bool RemoveSongAt(int position)
{
    // TODO: Remove song at position
}
```

#### Step 10c: Navigation (5 points)

**📚 Reference:** [Bidirectional Navigation](https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/) - GeeksforGeeks

```csharp
public bool Next()
{
    // TODO: Move to next song
    // Return false if at end
}

public bool Previous()
{
    // TODO: Move to previous song
    // Return false if at beginning
}

public void JumpToSong(int position)
{
    // TODO: Jump directly to a song position
}
```

### Step 11: Display and Basic Management (10 points)

**📚 Reference:** [Traversal Display](https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/) - GeeksforGeeks

```csharp
public void DisplayPlaylist()
{
    // TODO: Show all songs with current song highlighted
    // Display song number, title, artist, and duration
    // Mark the current song with an indicator (e.g., "► ")
}

public void DisplayCurrentSong()
{
    // TODO: Display details of the currently selected song
    // Show title, artist, year, and duration
}

public Song? GetCurrentSong()
{
    // TODO: Return the currently selected song
    // Return null if playlist is empty
    return currentSong?.Data;
}
```

**Note:** For this simplified version, assume the current song is always "playing" - no need for play/pause/stop states.

---

## 🧪 Testing Requirements

### Part A Testing

For each step, create test cases that verify:

1. **Empty list behavior** - Methods work correctly on empty lists
2. **Single element** - Operations work with one element
3. **Multiple elements** - Operations work with several elements
4. **Edge cases** - Invalid indices, null values, etc.
5. **Bidirectional integrity** - Forward and backward traversals match

### Part B Testing

Test your playlist with:

1. **Various playlist sizes** - Empty, single song, multiple songs
2. **Navigation scenarios** - Forward, backward, jumping to specific positions
3. **Current song tracking** - Verify current song updates correctly during navigation
4. **Edge cases** - Next at end, previous at beginning, jumping to invalid positions
5. **Display functionality** - Ensure playlist displays correctly with current song highlighted

---

---

## � Learning Resources

### 🌟 Primary References

- **[GeeksforGeeks - Insertion Operations](https://www.geeksforgeeks.org/dsa/introduction-and-insertion-in-a-doubly-linked-list/)** - Step-by-step insertion methods
- **[GeeksforGeeks - Deletion Operations](https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/)** - Complete deletion techniques
- **[GeeksforGeeks - Traversal Methods](https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/)** - Forward and backward traversal
- **[GeeksforGeeks - Search Operations](https://www.geeksforgeeks.org/dsa/search-an-element-in-a-doubly-linked-list/)** - Finding elements efficiently

### 🔧 Implementation Help

- **[Microsoft C# Docs - Generics](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics)** - Understanding generic types
- **[Microsoft C# Docs - Classes](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/types/classes)** - Class design principles
- **[Microsoft C# Docs - Properties](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties)** - Property implementation

### 🎯 Advanced Topics

- **[GeeksforGeeks - Applications](https://www.geeksforgeeks.org/dsa/applications-of-linked-list-data-structure/)** - Real-world use cases
- **[GeeksforGeeks - Performance Analysis](https://www.geeksforgeeks.org/analysis-of-algorithms-set-1-asymptotic-analysis/)** - Algorithm complexity

### 💡 Visual Learning

- **[Algovis.io - Doubly Linked List](https://tobinatore.github.io/algovis/doublylinkedlist.html)** - Interactive data structure visualization

---

## �📊 Performance Analysis (Bonus)

Compare your doubly linked list implementation with:

- Array operations
- List<T> operations
- Your analysis of when to use each data structure

**📚 Reference:** [Algorithm Analysis](https://www.geeksforgeeks.org/analysis-of-algorithms-set-1-asymptotic-analysis/) - GeeksforGeeks

---

## 🚀 Getting Started

1. **Start with the provided skeleton code** in `DoublyLinkedList.cs`
2. **Follow the steps in order** - don't skip ahead
3. **Test each step** before moving to the next
4. **Use the debugger** to visualize pointer connections
5. **Draw diagrams** of your list structure as you build it
6. **Reference the provided links** for detailed explanations and examples

**💡 Pro Tip:** Each TODO comment includes a direct link to relevant documentation. Use these resources to understand the concepts before implementing!

---

## 📝 Submission Requirements

### Code Submission

- `DoublyLinkedList.cs` - Your complete implementation
- `MusicPlaylist.cs` - Music playlist manager
- `Program.cs` - Demonstration of both parts

### Documentation

- **Step-by-step log** - Document your progress through each step
- **Challenges faced** - What problems did you encounter and how did you solve them?
- **Performance reflection** - When would you use doubly linked lists vs other data structures?

### Demonstration

Your program should provide a menu system that demonstrates:

1. All doubly linked list operations (add, remove, search, traversal)
2. Music playlist functionality (navigation, display, management)
3. Error handling and edge cases
4. Current song tracking and display

---

## 🎯 Tips for Success

1. **Start early** - This assignment builds complexity step by step
2. **Test frequently** - Don't write multiple methods before testing
3. **Draw diagrams** - Visualize your pointer connections
4. **Use the debugger** - Step through your code to see how pointers change
5. **Handle edge cases** - Empty lists, single elements, invalid inputs
6. **Document your thinking** - Comment your code as you write it

Remember: The goal is to understand doubly linked lists deeply through hands-on implementation. Take your time with each step and make sure you understand the pointer manipulation before moving forward!

**Good luck, and happy coding!** 🚀
