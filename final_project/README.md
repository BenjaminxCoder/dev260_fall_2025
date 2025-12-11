# CashFlow Coach — Budget Management Assistant (C# + Python AI)

> A simple, fast, and intelligent budgeting tool designed to help everyday people understand their spending, organize transactions, and receive AI-powered category suggestions.

---

## What I Built (Overview)

**Problem this solves:**  
CashFlow Coach helps users track, analyze, and understand everyday spending. Many budgeting tools are overly complex, expensive, or require cloud accounts. This app provides a lightweight, private, local-first alternative. Users can add transactions, search and filter them, update or delete entries, and receive smart category suggestions powered by a lightweight Python classifier.

**Your Answer:**  
This project was a challenge that took longer than I expected. I spent about 1.5 months trying to determine the right direction. I knew I wanted to build something in the finance space. I originally attempted to create a trading assistant, but that concept proved too large for the project scope. Then I returned to a deeply personal motivation: building a tool that helps people understand and manage their money. My mother earns about $137k a year but still lives paycheck to paycheck, and that reality pushed me to create something meaningful, something simple, reliable, and useful for people like her. What I built here is the foundational seed of that idea: a clean, structured budgeting manager that can grow into a full personal finance assistant.

---

## Core Features
- Add new transactions (income or expenses)
- List all transactions chronologically
- Search by date range
- Search by category
- Update or delete an existing transaction
- View statistics (totals by category, net total, transaction count)
- Automatically categorize uncategorized items using AI (Python classifier)
- JSON file persistence for saved data

---

## How to Run

### Requirements
- .NET 8.0+  
- macOS, Windows, or Linux  
- Python 3 installed and available as `python3`  

**Clone & build:**
```bash
git clone https://github.com/BenjaminxCoder/dev260_fall_2025.git
cd final_project/src/CashFlowCoach
dotnet build
```

**Run:**
```bash
dotnet run
```

**Sample Data:**
- Stored in `final_project/data/transactions.json`
- Automatically created if missing
- Updated whenever the user selects option **[9] Save**

---

## Using the App (Quick Start)

### Typical workflow
1. Choose **[1] Add transaction** to record income or expenses.
2. Choose **[2] List all** or **[3]/[4]** to explore your data.
3. If entries show up as “(uncategorized),” choose **[8] Auto-fill categories (AI)**.
4. Save your data using **[9] Save** before quitting.

### Input Tips
- Dates **must** be entered as `YYYY-MM-DD`
- Amounts: negative = expense, positive = income
- Category is optional; blank becomes `"(uncategorized)"`
- Invalid inputs never crash the app; it reprompts until valid

---

## Data Structures (Brief Summary)

> Full details are documented in DESIGN.md.

- **`Dictionary<Guid, Transaction>`** → primary store for fast CRUD
- **`SortedDictionary<DateOnly, List<Guid>>`** → powers ordered listing and date-range search
- **`Dictionary<string, HashSet<Guid>>`** → powers category search and fast category grouping
- **Python keyword/weight maps** → assists category classification

---

## Manual Testing Summary

### Scenario 1: Add + List
- Steps: Add 3 transactions, then List All.
- Expected: Sorted by date, correct category defaults.
- Actual: Works as expected.

### Scenario 2: Search by category
- Steps: Add two items under “groceries” and one under another category; run search.
- Expected: Only groceries returned.
- Actual: Correct.

### Scenario 3: Update entry
- Steps: Add transaction, copy ID, update amount and category.
- Expected: Changes reflected in list.
- Actual: Correct.

### Scenario 4: Delete
- Steps: Add item, delete it, then list.
- Expected: Item removed.
- Actual: Correct.

### Scenario 5: AI auto-categorization
- Steps: Add several uncategorized items, run Option 8.
- Expected: AI assigns reasonable categories.
- Actual: Correct.

---

## Known Limitations
- No UI beyond console
- AI suggestions rely on keyword/weight heuristics (not true ML)
- No multi-user support
- Persistence is JSON-based (not optimized for very large datasets)

---

## Comparers & String Handling
- Categories use **`StringComparer.OrdinalIgnoreCase`** for case-insensitive grouping
- Category normalization forces blank values into `"(uncategorized)"`
- Trimming applied to all user input to avoid hidden whitespace bugs

---

## Credits & AI Disclosure
- .NET documentation
- Microsoft C# language reference
- StackOverflow (exception patterns, SortedDictionary usage)
- **AI usage:**  
  Ricky Bobby (a personal assistant built on the Mistral framework) assisted in the following ways:

  - **Planning Phase:**  
    Helped structure the overall project direction by evaluating multiple concepts, identifying scope risks, and narrowing the solution to a budgeting tool that fit the course requirements and real-world impact goals.

  - **Brainstorming:**  
    Provided idea expansion during early concept development, including feature exploration, user experience patterns, and potential data-structure strategies that aligned with .NET capabilities.

  - **Spell Checker / Grammar Checker:**  
    Assisted in proofreading documentation, ensuring clarity, correcting grammar, and improving readability across README.md, DESIGN.md, and in-app instructional text.

---

## Challenges and Solutions

### Biggest challenge faced
Choosing the right combination of data structures and ensuring they stayed in sync across CRUD operations. Index maintenance required careful updates.

### How you solved it
I separated concerns into dedicated index classes, wrote predictable update logic, and used hashing + sorted trees for fast lookups. Debug prints and manual tests helped validate consistency.

### Most confusing concept
Guaranteeing **O(log n + k)** range queries using `SortedDictionary<DateOnly, List<Guid>>` and understanding how indexing reduces scan cost.

---

## Code Quality

### What I'm most proud of
Clean architecture: primary store + two indexes + optional Python AI. Every feature is built on top of this structure without hacks.

### What I’d improve
- Add undo/redo via stack-based action history
- Add SQLite persistence
- Enhance AI with a learning model that adapts to user corrections
- Improve menu UX for power users

---

## Real-World Applications
Budgeting systems, expense trackers, fintech mobile apps, ledger systems, bookkeeping tools, and financial dashboards all rely heavily on fast indexing and predictable search performance—the same patterns implemented here.

### What I learned about data structures
- Picking the right structure dramatically affects usability
- Index maintenance matters just as much as main storage
- Big-O predictions matched observed behavior when testing with hundreds of entries

---

## Personal Reflection
This project was a challenge that took longer than I would like. I spent about 1.5 months going through what I wanted to do. I knew it had to involve personal finance. I failed to bring my original trading assistant to life but I realized this budgeting tool meant far more to me. My mother makes about $137k a year yet lives paycheck to paycheck, and that inspired me to create something that helps people understand where their money goes. This app is just the seed of that larger mission.

---

## Submission Checklist
- [x] Public GitHub repo link submitted
- [x] README.md completed
- [x] DESIGN.md completed
- [x] Source code included and builds successfully
- [ ] (Optional) Demo video link added

**Demo Video Link (optional):**
