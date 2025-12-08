# Project Design & Rationale

**Instructions:** Replace prompts with your content. Be specific and concise. If something doesn't apply, write "N/A" and explain briefly.

---

## Data Model & Entities

**Core entities:**  
_List your main entities with key fields, identifiers, and relationships (1–2 lines each)._

**Your Answer:**

**Entity A:**

- Name:
- Key fields:
- Identifiers:
- Relationships:

**Entity B (if applicable):**

- Name:
- Key fields:
- Identifiers:
- Relationships:

**Identifiers (keys) and why they're chosen:**  
_Explain your choice of keys (e.g., string Id, composite key, case-insensitive, etc.)._

**Your Answer:**

---

## Data Structures — Choices & Justification

_List only the meaningful data structures you chose. For each, state the purpose, the role it plays in your app, why it fits, and alternatives considered._

### Structure #1

**Chosen Data Structure:**  
_Name the data structure (e.g., Dictionary<string, Customer>)._

**Your Answer:**

**Purpose / Role in App:**  
_What user action or feature does it power?_

**Your Answer:**

**Why it fits:**  
_Explain access patterns, typical size, performance/Big-O, memory, simplicity._

**Your Answer:**

**Alternatives considered:**  
_List alternatives (e.g., List<T>, SortedDictionary, custom tree) and why you didn't choose them._

**Your Answer:**

---

### Structure #2

**Chosen Data Structure:**  
_Name the data structure._

**Your Answer:**

**Purpose / Role in App:**  
_What user action or feature does it power?_

**Your Answer:**

**Why it fits:**  
_Explain access patterns, typical size, performance/Big-O, memory, simplicity._

**Your Answer:**

**Alternatives considered:**  
_List alternatives and why you didn't choose them._

**Your Answer:**

---

### Structure #3

**Chosen Data Structure:**  
_Name the data structure._

**Your Answer:**

**Purpose / Role in App:**  
_What user action or feature does it power?_

**Your Answer:**

**Why it fits:**  
_Explain access patterns, typical size, performance/Big-O, memory, simplicity._

**Your Answer:**

**Alternatives considered:**  
_List alternatives and why you didn't choose them._

**Your Answer:**

---

### Additional Structures (if applicable)

_Add more sections if you used additional structures like Queue for workflows, Stack for undo, HashSet for uniqueness, Graph for relationships, BST/SortedDictionary for ordered views, etc._

**Your Answer:**

---

## Comparers & String Handling

**Comparer choices:**  
_Explain what comparers you used and why (e.g., StringComparer.OrdinalIgnoreCase for keys)._

**Your Answer:**

**For keys:**

**For display sorting (if different):**

**Normalization rules:**  
_Describe how you normalize strings (trim whitespace, collapse duplicates, canonicalize casing)._

**Your Answer:**

**Bad key examples avoided:**  
_List examples of bad key choices and why you avoided them (e.g., non-unique names, culture-varying text, trailing spaces, substrings that can change)._

---

## Performance Considerations

**Expected data scale:**  
_Describe the expected size of your data (e.g., 100 items, 10,000 items)._

**Your Answer:**

**Performance bottlenecks identified:**  
_List any potential performance issues and how you addressed them._

**Your Answer:**

**Big-O analysis of core operations:**  
_Provide time complexity for your main operations (Add, Search, List, Update, Delete)._

**Your Answer:**

- Add:
- Search:
- List:
- Update:
- Delete:

---

## Design Tradeoffs & Decisions

**Key design decisions:**  
_Explain major design choices and why you made them._

**Your Answer:**

**Tradeoffs made:**  
_Describe any tradeoffs between simplicity vs performance, memory vs speed, etc._

**Your Answer:**

**What you would do differently with more time:**  
_Reflect on what you might change or improve._

**Your Answer:**
