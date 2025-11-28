# Assignment 9: BST File System Navigator - Implementation Notes

**Name:** Berry/Ben Walker

## Binary Search Tree Pattern Understanding

**How BST operations work for file system navigation:**
BSTs give `O(h)` time for insert/find/delete where `h` is tree height (≈ `O(log n)` when the tree is reasonably balanced). I used a single ordering function `CompareFileNodes` to enforce consistent sort rules: directories sort before files, and names are compared case‑insensitively. In‑order traversal yields items in sorted order without an extra sort, which lets the UI display a predictable hierarchy. Although a real OS typically uses B‑trees/B+‑trees for disks, this assignment’s BST still demonstrates the core indexing idea: logarithmic point lookups, linear traversals for filtered scans, and stable ordering for navigation.

## Challenges and Solutions

**Biggest challenge faced:**
Keeping comparator consistency across *all* operations, especially deletion with two children, and deciding ties between a directory and a file that share the same name.

**How I solved it:**
- Centralized all ordering through `CompareFileNodes` and normalized names/extensions up front.
- Wrote typed search probes (file vs directory) so `FindFile` can discriminate correctly.
- For deletion, implemented the standard three cases and used the in‑order successor (min of right subtree) for the two‑child case. After replacing payload, I removed the successor with the same comparator to preserve invariants.
- Used the provided tree visualizer after every operation to validate that in‑order output remained sorted and duplicates were rejected.

**Most confusing concept:**
Understanding that range queries by size cannot be `O(log n)` because the BST is not ordered by size. The fix is to traverse the tree (`O(n)`) and *filter*, which is acceptable and mirrors how secondary predicates are handled in real indexes.

## Code Quality

**What I'm most proud of in my implementation:**
- Pure recursive implementations for insert, search, traversal, sum, and delete.
- A small set of composable helpers: `InsertNode`, `SearchNodeByType`, `TraverseAndCollect`, `SumSizes`, and `DeleteNode` with `MinNode`.
- Deterministic behavior: in‑order traversal for all reporting features, stable tiebreak on name for largest‑files output.

**What I would improve if I had more time:**
- Use a size‑`k` min‑heap for `FindLargestFiles(k)` to improve to `O(n log k)`.
- Add balancing (AVL/Red‑Black) or switch to a B‑tree to guarantee `O(log n)` worst‑case.
- Expand unit tests (duplicates, mixed file/dir names, deep trees) and add property‑based tests for ordering invariants.
- Surface richer errors instead of boolean returns for duplicate/invalid input.

## Real-World Applications

**How this relates to actual file systems:**
- Real file systems maintain ordered metadata structures (often B/B+‑trees) for directories, filenames, and extents. This BST mimics the indexing behavior used for fast name lookups and directory listings.
- Database engines do something similar: clustered indexes give ordered storage; non‑clustered indexes support fast point lookups while filtered scans traverse and apply predicates—exactly like the extension/size filters here.

**What I learned about tree algorithms:**
- Comparator alignment is non‑negotiable—every operation must use the exact same ordering or invariants break.
- In‑order traversal semantics map directly to “sorted view,” which is why the visualizer works without extra sorting.
- The two‑child delete case is easiest to reason about by copying the successor’s payload, then deleting the successor node recursively.

## Stretch Features

None implemented.

## Time Spent

**Total time:**  9 hours

**Breakdown:**
- Understanding BST concepts and assignment requirements: 3 hours
- Implementing the 8 core TODO methods: 1 hours
- Testing with different file scenarios: 2 hours
- Debugging recursive algorithms and BST operations: 2 hours
- Writing these notes: 1 hours

**Most time-consuming part:** 
- Understanding BST concepts and how recursive operations interact with comparison rules.