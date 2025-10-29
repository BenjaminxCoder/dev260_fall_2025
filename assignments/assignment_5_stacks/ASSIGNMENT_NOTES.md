# Assignment 5: Browser Navigation System - Implementation Notes

**Name:** Berry Walker Jr.

## Dual-Stack Pattern Understanding

**How the dual-stack pattern works for browser navigation:**
- Keep two stacks and one current pointer.
- Visit URL: push `currentPage` → `backStack`, clear `forwardStack`, set new `currentPage`.
- Back: push `currentPage` → `forwardStack`, pop from `backStack` → `currentPage`.
- Forward: push `currentPage` → `backStack`, pop from `forwardStack` → `currentPage`.
- LIFO guarantees the most recent history action is always on top and restored first.

## Challenges and Solutions

**Biggest challenge faced:**
- Remembering to invalidate forward history after going back and then visiting a brand‑new URL.

**How you solved it:**
- Added a mandatory `forwardStack.Clear()` in `VisitUrl`. Wrote a quick smoke test sequence: Visit(A→B→C) → Back → Visit(D) and verified forward history is empty.

**Most confusing concept:**
- Display order for stacks. `Stack<T>` enumerates from top to bottom (LIFO). I confirmed enumeration order and labeled outputs accordingly: “most recent first” for back, “next page first” for forward.

## Code Quality

**What you're most proud of in your implementation:**
- Simple, guard‑clause driven methods that only do one thing. Clear messages and no hidden side effects.

**What you would improve if you had more time:**
- Add a max history size with eviction (extra credit).
- Add JSON export/import for session persistence (extra credit).
- Add unit tests with xUnit for each navigation path and edge case.
- Basic URL validation and normalization.

## Testing Approach

**How you tested your implementation:**
- Manual smoke tests via the console UI after each method:
  1) Visit A → B → C
  2) Back twice, then Forward once
  3) Back once, then Visit D (verify forward cleared)
  4) Try Back/Forward on empty stacks to confirm guard behavior
  5) Use the history displays after each step to verify state

**Issues you discovered during testing:**
- Attempting Back with empty `backStack` initially threw; fixed by returning `false` with a guard.
- Initially forgot to clear `forwardStack` on `VisitUrl` after navigating back; added the clear and re‑tested.

## Stretch Features

None implemented yet. Planned next: history cap and JSON persistence.

## Time Spent

**Total time:** [`5` hour]

**Breakdown:**

- Understanding the assignment: [`1` hour]
- Implementing the 6 methods: [`1` hour]
- Testing and debugging: [`2` hour]
- Writing these notes: [`1` hour]

**Most time-consuming part:**
- Verifying forward‑history invalidation and history display order.
