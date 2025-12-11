using CashFlowCoach.Models;
using CashFlowCoach.Services.Indexes;
using CashFlowCoach.Persistence;
using System.Diagnostics.CodeAnalysis;

namespace CashFlowCoach.Services;

public class TransactionStore
{
    private readonly Dictionary<Guid, Transaction> _byId = new();
    private readonly TimeIndex _timeIndex = new();
    private readonly CategoryIndex _categoryIndex = new(StringComparer.OrdinalIgnoreCase);

    public void Add(Transaction tx)
    {
        if (_byId.ContainsKey(tx.Id)) throw new InvalidOperationException("Duplicate Id");
        _byId[tx.Id] = tx;
        _timeIndex.Add(tx.Date, tx.Id);
        _categoryIndex.Add(tx.Category, tx.Id);
    }

    public bool TryGet(Guid id, [NotNullWhen(true)] out Transaction? tx) => _byId.TryGetValue(id, out tx);

    public void Update(Transaction updated)
    {
        if (!_byId.TryGetValue(updated.Id, out var old)) throw new KeyNotFoundException("Not found");
        _byId[updated.Id] = updated;
        if (old.Date != updated.Date)
        {
            _timeIndex.Remove(old.Date, updated.Id);
            _timeIndex.Add(updated.Date, updated.Id);
        }
        if (!string.Equals(old.Category, updated.Category, StringComparison.OrdinalIgnoreCase))
        {
            _categoryIndex.Remove(old.Category, updated.Id);
            _categoryIndex.Add(updated.Category, updated.Id);
        }
    }

    public bool Delete(Guid id)
    {
        if (!_byId.TryGetValue(id, out var old)) return false;
        _byId.Remove(id);
        _timeIndex.Remove(old.Date, id);
        _categoryIndex.Remove(old.Category, id);
        return true;
    }

    public IEnumerable<Transaction> ListAllOrdered()
        => _byId.Values.OrderBy(t => t.Date).ThenBy(t => t.Description, StringComparer.Ordinal);

    public IEnumerable<Transaction> SearchByDateRange(DateOnly start, DateOnly end)
    {
        foreach (var id in _timeIndex.Range(start, end))
            if (_byId.TryGetValue(id, out var t)) yield return t;
    }

    public IEnumerable<Transaction> SearchByCategory(string category)
    {
        foreach (var id in _categoryIndex.Get(category))
            if (_byId.TryGetValue(id, out var t)) yield return t;
    }
    public IEnumerable<Transaction> GetUncategorized()
    => _byId.Values
            .Where(t => string.Equals(t.Category, "(uncategorized)", StringComparison.OrdinalIgnoreCase))
            .OrderBy(t => t.Date)
            .ThenBy(t => t.Description, StringComparer.Ordinal);

    public void ApplyCategory(Guid id, string newCategory)
    {
        if (_byId.TryGetValue(id, out var existing))
        {
            var updated = existing with { Category = newCategory };
            Update(updated);
        }
    }
    
    // Returns sum of positive amounts (income) and absolute sum of negative amounts (expenses)
    public (decimal Income, decimal Expenses) TotalsIncomeExpense()
    {
        decimal income = 0, expenses = 0;
        foreach (var t in _byId.Values)
        {
            if (t.Amount > 0) income += t.Amount;
            else if (t.Amount < 0) expenses += -t.Amount;
        }
        return (income, expenses);
    }

    public (IReadOnlyDictionary<string, decimal> TotalsByCategory, decimal NetTotal, int Count) Stats()
    {
        var totals = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
        decimal net = 0;
        foreach (var t in _byId.Values)
        {
            net += t.Amount;
            totals[t.Category] = totals.GetValueOrDefault(t.Category) + t.Amount;
        }
        // Convert totals to expense-positive if you prefer, but net keeps signs here
        return (totals, net, _byId.Count);
    }

    public void LoadFromFile(string filePath)
    {
        var items = JsonStorage.Load<List<Transaction>>(filePath) ?? new List<Transaction>();
        _byId.Clear();
        _timeIndex.Clear();
        _categoryIndex.Clear();
        foreach (var t in items)
        {
            _byId[t.Id] = t;
            _timeIndex.Add(t.Date, t.Id);
            _categoryIndex.Add(t.Category, t.Id);
        }
    }

    public void SaveToFile(string filePath)
    {
        var items = _byId.Values.ToList();
        JsonStorage.Save(filePath, items);
    }
}