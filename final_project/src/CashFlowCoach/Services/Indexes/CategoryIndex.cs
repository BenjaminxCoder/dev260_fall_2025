namespace CashFlowCoach.Services.Indexes;

public class CategoryIndex
{
    private readonly Dictionary<string, HashSet<Guid>> _map;

    public CategoryIndex(IEqualityComparer<string> comparer)
    {
        _map = new Dictionary<string, HashSet<Guid>>(comparer);
    }

    public void Add(string category, Guid id)
    {
        var key = Normalize(category);
        if (!_map.TryGetValue(key, out var set))
        {
            set = new HashSet<Guid>();
            _map[key] = set;
        }
        set.Add(id);
    }

    public void Remove(string category, Guid id)
    {
        var key = Normalize(category);
        if (_map.TryGetValue(key, out var set))
        {
            set.Remove(id);
            if (set.Count == 0) _map.Remove(key);
        }
    }

    public IEnumerable<Guid> Get(string category)
    {
        var key = Normalize(category);
        return _map.TryGetValue(key, out var set) ? set : Array.Empty<Guid>();
    }

    public void Clear() => _map.Clear();

    private static string Normalize(string s)
    {
        s = (s ?? string.Empty).Trim();
        return string.IsNullOrEmpty(s) ? "(uncategorized)" : s;
    }
}