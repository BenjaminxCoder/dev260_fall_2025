namespace CashFlowCoach.Services.Indexes;

public class TimeIndex
{
    // Use SortedList so we can do near O(log n + k) range scans via index
    private readonly SortedList<DateOnly, List<Guid>> _byDate = new();

    public void Add(DateOnly date, Guid id)
    {
        if (!_byDate.TryGetValue(date, out var list))
        {
            list = new List<Guid>();
            _byDate.Add(date, list);
        }
        list.Add(id);
    }

    public void Remove(DateOnly date, Guid id)
    {
        if (_byDate.TryGetValue(date, out var list))
        {
            list.Remove(id);
            if (list.Count == 0)
                _byDate.Remove(date);
        }
    }

    public IEnumerable<Guid> Range(DateOnly start, DateOnly end)
    {
        if (_byDate.Count == 0 || end < start)
            yield break;

        // Find the first index at or after 'start'
        int i = _byDate.IndexOfKey(start);
        if (i < 0)
        {
            // If not found, IndexOfKey returns bitwise complement of the next larger element
            i = ~i;
        }

        // Iterate forward until key > end
        while (i >= 0 && i < _byDate.Count)
        {
            var key = _byDate.Keys[i];
            if (key > end) yield break;
            foreach (var id in _byDate.Values[i])
                yield return id;
            i++;
        }
    }

    public void Clear() => _byDate.Clear();
}