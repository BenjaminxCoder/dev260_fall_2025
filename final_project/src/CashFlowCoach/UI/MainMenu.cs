using CashFlowCoach.Models;
using CashFlowCoach.Services;
using CashFlowCoach.Utilities;
using System.Globalization;

namespace CashFlowCoach.UI;

public class MainMenu
{
    private readonly TransactionStore _store;
    private readonly string _txFile;

    public MainMenu(TransactionStore store, string txFile)
    {
        _store = store;
        _txFile = txFile;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            RenderMenu();
            Console.Write("Select an option (0-9): ");

            var input = Console.ReadLine();
            Console.WriteLine();
            switch (input)
            {
                case "1": AddTransaction(); Pause(); break;
                case "2": ListAll(); Pause(); break;
                case "3": SearchByDateRange(); Pause(); break;
                case "4": SearchByCategory(); Pause(); break;
                case "5": UpdateTransaction(); Pause(); break;
                case "6": DeleteTransaction(); Pause(); break;
                case "7": ShowStats(); Pause(); break;
                case "8": await AutoCategorizeAsync(); Pause(); break;
                case "9": _store.SaveToFile(_txFile); Console.WriteLine("Saved your data to a file."); Pause(); break;
                case "0": return;
                default: Console.WriteLine("That’s not a valid choice. Try again."); Pause(); break;
            }
        }
    }

    private static void Pause()
    {
        Console.WriteLine();
        Console.Write("Press Enter to go back to the main menu...");
        Console.ReadLine();
    }

    private static DateOnly PromptDate(string prompt)
    {
        while (true)
        {
            Console.WriteLine($"{prompt}");
            Console.WriteLine("Example: 2025-01-03 (year-month-day). Type it carefully.");
            Console.Write("Enter the date: ");
            var s = Console.ReadLine()?.Trim();
            if (DateOnly.TryParseExact(s, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var d))
                return d;
            Console.WriteLine("That date format didn’t work. Let’s try again.");
        }
    }

    private static decimal PromptAmount(string prompt)
    {
        while (true)
        {
            Console.WriteLine($"{prompt}");
            Console.WriteLine("Type a number. Use a minus sign for money you SPENT. Use a plus or just the number for money you GOT.");
            Console.WriteLine("Examples: -12.50 (expense), 40 (income)");
            Console.Write("Enter the amount: ");
            var s = Console.ReadLine()?.Trim();
            if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out var amt) && amt != 0)
                return amt;
            Console.WriteLine("That amount didn’t look right. Try again with numbers only.");
        }
    }

    private static string PromptNonEmpty(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            Console.Write("Type your answer: ");
            var s = (Console.ReadLine() ?? string.Empty).Trim();
            if (!string.IsNullOrWhiteSpace(s)) return s;
            Console.WriteLine("We need something here. Let’s try again.");
        }
    }

    private void AddTransaction()
    {
        Console.WriteLine("Let’s add a money item. We’ll go step by step.");
        var date = PromptDate("1) What date is this for?");
        var amount = PromptAmount("2) How much money? (negative = expense, positive = income)");
        var desc = PromptNonEmpty("3) What is this? (short description like ‘gas’ or ‘paycheck’)");
        Console.WriteLine("4) Category is optional. If you’re not sure, leave it empty.");
        Console.Write("Category (optional): ");
        var cat = (Console.ReadLine() ?? string.Empty).Trim();
        var isIncome = amount > 0;

        var tx = new Transaction
        {
            Id = Guid.NewGuid(),
            Date = date,
            Amount = amount,
            Description = desc,
            Category = string.IsNullOrWhiteSpace(cat) ? "(uncategorized)" : cat,
            IsIncome = isIncome
        };

        _store.Add(tx);
        Console.WriteLine("Done! Your item is saved in memory.");
    }

    private void ListAll()
    {
        Console.WriteLine("Here are your items from oldest to newest:");
        var all = _store.ListAllOrdered();
        PrintTransactions(all);
    }

    private void SearchByDateRange()
    {
        Console.WriteLine("Find items between two dates.");
        var start = PromptDate("Start date");
        var end = PromptDate("End date");
        if (end < start)
        {
            Console.WriteLine("End date must be the same as or after the start date.");
            return;
        }
        var items = _store.SearchByDateRange(start, end);
        PrintTransactions(items);
    }

    private void SearchByCategory()
    {
        Console.WriteLine("Type the category to search. It doesn’t matter if you use capital letters or not.");
        var cat = PromptNonEmpty("Category name:");
        var items = _store.SearchByCategory(cat);
        PrintTransactions(items);
    }

    private void UpdateTransaction()
    {
        Console.WriteLine("Find the item you want to change.");
        var picked = SelectTransactionInteractive();
        if (picked is not Transaction existing) { Console.WriteLine("No item selected."); return; }

        Console.WriteLine("Leave the next fields blank if you want to keep the current value.");
        Console.WriteLine($"Current Date: {existing.Date:yyyy-MM-dd}");
        Console.Write("New Date (YYYY-MM-DD): ");
        var sDate = Console.ReadLine();
        DateOnly? newDate = null;
        if (!string.IsNullOrWhiteSpace(sDate) && DateOnly.TryParseExact(sDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var d))
            newDate = d;

        Console.WriteLine($"Current Amount: {existing.Amount}");
        Console.Write("New Amount: ");
        var sAmt = Console.ReadLine();
        decimal? newAmt = null;
        if (!string.IsNullOrWhiteSpace(sAmt) && decimal.TryParse(sAmt, NumberStyles.Number, CultureInfo.InvariantCulture, out var a))
            newAmt = a;

        Console.WriteLine($"Current Description: {existing.Description}");
        Console.Write("New Description: ");
        var sDesc = Console.ReadLine();

        Console.WriteLine($"Current Category: {existing.Category}");
        Console.Write("New Category: ");
        var sCat = Console.ReadLine();

        var updated = existing with
        {
            Date = newDate ?? existing.Date,
            Amount = newAmt ?? existing.Amount,
            Description = string.IsNullOrWhiteSpace(sDesc) ? existing.Description : sDesc!.Trim(),
            Category = string.IsNullOrWhiteSpace(sCat) ? existing.Category : sCat!.Trim(),
            IsIncome = (newAmt ?? existing.Amount) > 0
        };

        _store.Update(updated);
        Console.WriteLine("Updated.");
    }

    private void DeleteTransaction()
    {
        Console.WriteLine("Pick the item you want to delete.");
        var picked = SelectTransactionInteractive();
        if (picked is not Transaction tx) { Console.WriteLine("No item selected."); return; }

        Console.Write($"Are you sure you want to delete '{tx.Description}' on {tx.Date:yyyy-MM-dd}? Type Y to confirm: ");
        var answer = (Console.ReadLine() ?? string.Empty).Trim();
        if (!answer.Equals("Y", StringComparison.OrdinalIgnoreCase)) { Console.WriteLine("Canceled."); return; }

        if (_store.Delete(tx.Id)) Console.WriteLine("Deleted.");
        else Console.WriteLine("Could not delete (not found).");
    }

    // === Selection helpers (no Id memorization required) ===
    private Transaction? SelectTransactionInteractive()
    {
        // Optional quick filter
        Console.Write("Type a word to filter (description/category/date) or press Enter to see all: ");
        var filter = (Console.ReadLine() ?? string.Empty).Trim();
        IEnumerable<Transaction> source = _store.ListAllOrdered();
        if (!string.IsNullOrWhiteSpace(filter))
        {
            source = source.Where(t =>
                t.Description.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                t.Category.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                t.Date.ToString("yyyy-MM-dd").Contains(filter, StringComparison.OrdinalIgnoreCase));
        }

        var list = source.ToList();
        if (list.Count == 0) { Console.WriteLine("Nothing matched that filter."); return null; }

        // Print numbered choices (cap at first 50 lines to keep it readable)
        Console.WriteLine();
        Console.WriteLine("Pick a number:");
        var max = Math.Min(50, list.Count);
        for (int i = 0; i < max; i++)
        {
            var t = list[i];
            Console.WriteLine($"[{i + 1,2}] {t.Date:yyyy-MM-dd}  {t.Description}  {(t.IsIncome ? "+" : "")} {t.Amount}  {t.Category}  (id {GetShortId(t.Id)})");
        }
        if (list.Count > max) Console.WriteLine($"... and {list.Count - max} more (refine your filter to see them)");

        Console.Write("Enter a number: ");
        var sel = Console.ReadLine();
        if (!int.TryParse(sel, out var idx) || idx < 1 || idx > max) return null;
        return list[idx - 1];
    }

    private static string GetShortId(Guid id) => id.ToString("N").Substring(0, 6);

    private void ShowStats()
    {
        Console.WriteLine("Quick stats:");
        var stats = _store.Stats();
        Console.WriteLine("— Totals by Category —");
        foreach (var (cat, total) in stats.TotalsByCategory)
            Console.WriteLine($"{cat}: {total}");
        Console.WriteLine();
        Console.WriteLine($"Net total (income minus expenses): {stats.NetTotal}");
        Console.WriteLine($"Number of items: {stats.Count}");
    }

    private async Task AutoCategorizeAsync()
    {
        Console.WriteLine("I will look at items without a category and try to fill them in for you.");
        var uncategorized = _store.GetUncategorized().ToList();

        if (uncategorized.Count == 0)
        {
            Console.WriteLine("Nice! You don’t have any uncategorized items right now.");
            return;
        }

        Console.WriteLine($"I found {uncategorized.Count} item(s) without a category.");
        Console.WriteLine("I will guess a category for each based on the description.");

        var payload = uncategorized.Select(t => (t.Id, t.Description, t.Amount)).ToList();
        try
        {
            var suggestions = await PythonRunner.BulkSuggestAsync(payload);
            if (suggestions == null || suggestions.Count == 0)
            {
                Console.WriteLine("I couldn’t make suggestions this time.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Here is what I think:");
            Console.WriteLine("(Date) Description  =>  Suggested Category (confidence)");
            foreach (var s in suggestions)
            {
                var tx = uncategorized.First(x => x.Id == s.Id);
                Console.WriteLine($"({tx.Date:yyyy-MM-dd}) {tx.Description}  =>  {s.Category} ({s.Confidence:P0})");
            }
            Console.WriteLine();
            Console.Write("Do you want me to apply these categories now? Type Y or N: ");
            var answer = (Console.ReadLine() ?? string.Empty).Trim();
            if (answer.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                int applied = 0;
                foreach (var s in suggestions)
                {
                    _store.ApplyCategory(s.Id, s.Category);
                    applied++;
                }
                Console.WriteLine($"Done. I updated {applied} item(s).");
            }
            else
            {
                Console.WriteLine("Okay, I didn’t change anything.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("The helper couldn’t run. Make sure Python is installed as ‘python3’ and the file ‘final_project/python/suggest.py’ exists.");
            Console.WriteLine($"Details: {ex.Message}");
        }
    }

    private static void PrintTransactions(IEnumerable<Transaction> items)
    {
        int count = 0;
        foreach (var t in items)
        {
            Console.WriteLine($"{t.Id} | {t.Date:yyyy-MM-dd} | {(t.IsIncome ? "+" : "")} {t.Amount} | {t.Category} | {t.Description}");
            count++;
        }
        if (count == 0) Console.WriteLine("(none)");
    }

    // Renders a clean, aesthetically uniform menu
    private void RenderMenu()
    {
        // Retro green-on-black ASCII two-column layout: menu (left) + metrics (right)
        var oldFg = Console.ForegroundColor;
        var oldBg = Console.BackgroundColor;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Green;

        // Build left box lines (menu)
        const int leftWidth = 52;
        var left = new List<string>();
        left.Add("+" + new string('-', leftWidth) + "+");
        string title = " CASHFLOW COACH — RETRO MODE ";
        int pad = Math.Max(0, leftWidth - title.Length);
        left.Add("|" + new string(' ', pad / 2) + title + new string(' ', pad - pad / 2) + "|");
        left.Add("+" + new string('-', leftWidth) + "+");
        left.Add(RetroRow("[1]  Add a new money item", leftWidth));
        left.Add(RetroRow("[2]  List all transactions", leftWidth));
        left.Add(RetroRow("[3]  Search by date range", leftWidth));
        left.Add(RetroRow("[4]  Search by category", leftWidth));
        left.Add(RetroRow("[5]  Update a transaction", leftWidth));
        left.Add(RetroRow("[6]  Delete a transaction", leftWidth));
        left.Add(RetroRow("[7]  View stats", leftWidth));
        left.Add(RetroRow("[8]  Auto-fill categories (AI)", leftWidth));
        left.Add(RetroRow("[9]  Save your data", leftWidth));
        left.Add(RetroRow("[0]  Quit the app", leftWidth));
        left.Add("+" + new string('-', leftWidth) + "+");

        // Build right box lines (metrics)
        const int rightWidth = 36;
        var stats = _store.Stats();
        var (income, expenses) = _store.TotalsIncomeExpense();
        string IncomeS(decimal v) => v.ToString("C");
        string ExpenseS(decimal v) => (v == 0 ? 0 : v).ToString("C");
        string NetS(decimal v) => v.ToString("C");

        var right = new List<string>();
        right.Add("+" + new string('-', rightWidth) + "+");
        var rTitle = " SUMMARY ";
        int rpad = Math.Max(0, rightWidth - rTitle.Length);
        right.Add("|" + new string(' ', rpad / 2) + rTitle + new string(' ', rpad - rpad / 2) + "|");
        right.Add("+" + new string('-', rightWidth) + "+");
        right.Add(RetroRow($"Income   : {IncomeS(income)}", rightWidth));
        right.Add(RetroRow($"Expenses : {ExpenseS(expenses)}", rightWidth));
        right.Add(RetroRow($"Net      : {NetS(stats.NetTotal)}", rightWidth));
        right.Add(RetroRow($"Items    : {stats.Count}", rightWidth));
        right.Add("+" + new string('-', rightWidth) + "+");

        // Print side-by-side by zipping lines
        int rows = Math.Max(left.Count, right.Count);
        for (int i = 0; i < rows; i++)
        {
            var l = i < left.Count ? left[i] : new string(' ', leftWidth + 2);
            var r = i < right.Count ? right[i] : string.Empty;
            Console.WriteLine(l + "  " + r);
        }

        Console.ForegroundColor = oldFg;
        Console.BackgroundColor = oldBg;
        Console.WriteLine();
    }

    private static string RetroRow(string text, int width)
    {
        var padded = text.Length > width ? text.Substring(0, width) : text.PadRight(width);
        return "|" + padded + "|";
    }
}