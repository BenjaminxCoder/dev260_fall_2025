# Project Design & Rationale — CashFlow Coach

A single-user console budgeting app built in C# (.NET) with an optional Python assistant for category suggestions. Core focus: correct, justified data-structure use; clean UX; measurable performance characteristics.

---

## Data Model & Entities

**Core entities**

**Transaction**
- **Fields:** `Id: Guid`, `Date: DateOnly`, `Amount: decimal`, `Description: string`, `Category: string`, `IsIncome: bool`
- **Identifiers:** `Id` (GUID) is the stable primary key
- **Relationships:** None (flat collection); indexes provide alternate views

**Optional (future): Bill/Goal**: not implemented in this slice; can be added behind separate indexes (e.g., `PriorityQueue` for upcoming bills).

**Identifiers (keys) and why they’re chosen**
- **GUID** avoids collisions across sessions and seed data; stable even if other fields change.
- **Category strings** are not used as entity keys (users can rename categories; duplicates are valid). Category is handled through a secondary index.

---

## Data Structures — Choices & Justification

### Structure #1
**Chosen Data Structure:** `Dictionary<Guid, Transaction>` (primary store)

**Purpose / Role in App:**
- Source of truth for all CRUD operations (Add, Update, Delete, Get by Id).

**Why it fits:**
- Hash map gives **O(1) average** for Get/Insert/Update/Delete by `Id`.
- Memory overhead is acceptable for the target scale (≤ ~10k items).
- Keeps modeling simple; other views are derived by indexes.

**Alternatives considered:**
- `List<Transaction>`: Poor random access and deletes by `Id` (O(n)).
- `ConcurrentDictionary` not required (single-user, single-threaded console).

---

### Structure #2
**Chosen Data Structure:** `SortedDictionary<DateOnly, List<Guid>>` (time index)

**Purpose / Role in App:**
- Enables **ordered listing** and **date-range queries** quickly.

**Why it fits:**
- Tree map keeps keys sorted; each date maps to its transaction Ids.
- Range queries via `GetViewBetween(start, end)` are **O(log n + k)** where *k* is items returned.
- Insertion cost per Add is **O(log n)** to find/insert the date bucket, then **O(1)** to append the Id.

**Alternatives considered:**
- `List<Transaction>` sorted on demand: easy but requires resorting or full scan for ranges.
- `SortedList<DateOnly, List<Guid>>`: similar; `SortedDictionary` chosen for familiar API and `GetViewBetween` support.

---

### Structure #3
**Chosen Data Structure:** `Dictionary<string, HashSet<Guid>>` with `StringComparer.OrdinalIgnoreCase` (category index)

**Purpose / Role in App:**
- Fast **category lookups** and filtering (Search by category), insensitive to letter case.

**Why it fits:**
- Hash map of normalized category → set of transaction Ids; **O(1)** average set lookup; iteration is **O(k)**.
- `HashSet<Guid>` prevents duplicates if the same transaction is re-indexed.

**Alternatives considered:**
- `Lookup<string, Guid>`: read-only; doesn’t fit Update/Delete flows.
- `SortedDictionary<string, HashSet<Guid>>`: ordering not needed on category keys.

---

### Additional Structures (future-facing)
- **`PriorityQueue<Bill, DateOnly>`** for upcoming-bills view (not implemented in this slice).
- **`HashSet<(DateOnly, decimal, string)>`** to guard against exact-duplicate entries (optional enhancement).

---

## Comparers & String Handling

**Comparer choices**
- **Categories:** `StringComparer.OrdinalIgnoreCase` to avoid case-fragmentation (e.g., "Groceries" vs "groceries").
- **Description sorting:** `StringComparer.Ordinal` for deterministic UI ordering after date.

**Normalization rules**
- `Category` normalized to `"(uncategorized)"` when blank or whitespace.
- All user inputs are trimmed; dates validated via exact `yyyy-MM-dd`.

**Bad key examples avoided**
- User-facing text (names, descriptions) as keys → unstable and not unique.
- Culture-sensitive comparisons for internal keys → inconsistent grouping.
- Floating-point amounts as keys → precision issues; we use `decimal` for currency.

---

## Performance Considerations

**Expected data scale**
- Bronze/Silver scope: ~100 to ~5,000 transactions per user.

**Performance bottlenecks identified & mitigations**
- **Listing and range queries:** mitigated by the time index; no full scans.
- **Category filter:** direct set access; avoid scanning all transactions.
- **Update/Delete:** require index maintenance (remove from old buckets, add to new), implemented in `Update()` and `Delete()`.
- **Persistence:** JSON read/write is linear in `n`; acceptable at this scale.

**Big-O analysis of core operations**
- **Add:** `Dictionary` put **O(1)** avg + time-index insert **O(log n)** + category-index set add **O(1)**.
- **Search (by Id):** **O(1)** avg.
- **Search (by date range):** **O(log n + k)** via `GetViewBetween` + iteration of `k` results.
- **Search (by category):** **O(1 + k)** (hash lookup + iterate set).
- **List (ordered):** iterate `n` already-ordered by date buckets; effectively **O(n)** to print.
- **Update:** **O(1)** map write + up to **O(log n)** reindex on date + **O(1)** category set move.
- **Delete:** **O(1)** map remove + **O(1)** set remove + **O(1)**/bucket cleanup; time bucket removal is **O(1)** once empty.

Empirical notes to collect (finalize after testing):
- With ~100 vs ~5,000 items: measure wall time for Add 1000x, date-range of a typical month, and category search; record observed trends against the above Big-O.

---

## Design Tradeoffs & Decisions

**Key design decisions**
- **Indexes first-class:** Treat time & category indexes as maintained data, not ad-hoc queries. Guarantees predictable latency.
- **Flat entity model:** Keeps MVP simple; enables focused DS discussion and clean CRUD paths.
- **Optional Python path:** C# remains ≥80%. Python runs out-of-proc with a strict JSON contract and graceful failure.

**Tradeoffs made**
- **Memory vs speed:** Duplicating Ids in indexes increases memory but removes full scans; chosen for user experience.
- **Tree vs array for time:** `SortedDictionary` gives direct range views; insertion is `O(log n)` but worth it for fast queries.
- **String categories vs enum:** Strings allow user-defined categories; comparer normalization required to prevent drift.

**What I would do differently with more time**
- **Persistence:** Move to append-only log or SQLite for robustness and crash safety.
- **Undo/redo:** Add a `Stack`-based command history for reversible edits.
- **Learning layer:** Persist user corrections to a small JSON file and feed back into the Python scorer as weights.
- **Bills/Goals:** Add `PriorityQueue` for next-due bills and a simple budget/goal variance report.