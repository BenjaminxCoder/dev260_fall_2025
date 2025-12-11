using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace CashFlowCoach.Utilities;

public static class PythonRunner
{
    public sealed record SuggestResponse(bool Ok, string Category, double Confidence);
    public sealed record BulkSuggestItem(Guid Id, string Category, double Confidence);

    public static async Task<SuggestResponse?> SuggestCategoryAsync(string description, decimal amount, string? pythonExe = null)
    {
        var exe = ResolvePythonExe(pythonExe);
        var script = ResolveSuggestScriptPath();

        var psi = new ProcessStartInfo
        {
            FileName = exe,
            Arguments = $"\"{script}\"",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var p = new Process { StartInfo = psi };
        p.Start();

        var payload = new { op = "suggest_category", text = description, amount };
        await p.StandardInput.WriteAsync(JsonSerializer.Serialize(payload));
        p.StandardInput.Close();

        var (ok, stdout, stderr) = await ReadProcessAsync(p);
        if (!ok) throw new Exception(string.IsNullOrWhiteSpace(stderr) ? "Python error" : stderr);

        try
        {
            using var doc = JsonDocument.Parse(stdout);
            var root = doc.RootElement;
            var okFlag = root.GetProperty("ok").GetBoolean();
            if (!okFlag) return new SuggestResponse(false, string.Empty, 0);
            var res = root.GetProperty("result");
            var cat = res.GetProperty("category").GetString() ?? string.Empty;
            var conf = res.GetProperty("confidence").GetDouble();
            return new SuggestResponse(true, cat, conf);
        }
        catch { return null; }
    }

    public static async Task<List<BulkSuggestItem>?> BulkSuggestAsync(IEnumerable<(Guid Id, string Description, decimal Amount)> items, string? pythonExe = null)
    {
        var exe = ResolvePythonExe(pythonExe);
        var script = ResolveSuggestScriptPath();

        var psi = new ProcessStartInfo
        {
            FileName = exe,
            Arguments = $"\"{script}\"",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var p = new Process { StartInfo = psi };
        p.Start();

        var txs = items.Select(t => new { id = t.Id, description = t.Description, amount = t.Amount });
        var payload = new { op = "bulk_suggest", transactions = txs };
        await p.StandardInput.WriteAsync(JsonSerializer.Serialize(payload));
        p.StandardInput.Close();

        var (ok, stdout, stderr) = await ReadProcessAsync(p);
        if (!ok) throw new Exception(string.IsNullOrWhiteSpace(stderr) ? "Python error" : stderr);

        try
        {
            using var doc = JsonDocument.Parse(stdout);
            var root = doc.RootElement;
            if (!root.GetProperty("ok").GetBoolean()) return null;
            var arr = root.GetProperty("results");
            var list = new List<BulkSuggestItem>(arr.GetArrayLength());
            foreach (var el in arr.EnumerateArray())
            {
                var id = el.GetProperty("id").GetGuid();
                var cat = el.GetProperty("category").GetString() ?? string.Empty;
                var conf = el.GetProperty("confidence").GetDouble();
                list.Add(new BulkSuggestItem(id, cat, conf));
            }
            return list;
        }
        catch { return null; }
    }

    private static async Task<(bool ok, string stdout, string stderr)> ReadProcessAsync(Process p)
    {
        var stdout = await p.StandardOutput.ReadToEndAsync();
        var stderr = await p.StandardError.ReadToEndAsync();
        var exited = p.WaitForExit(5000);
        if (!exited)
        {
            try { p.Kill(true); } catch { }
            return (false, stdout, "Python timed out.");
        }
        return (p.ExitCode == 0, stdout, stderr);
    }

    private static string ResolvePythonExe(string? overrideExe)
    {
        if (!string.IsNullOrWhiteSpace(overrideExe)) return overrideExe!;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            // most macOS/Linux have python3
            return "python3";
        // Windows often registers as python
        return "python";
    }

    private static string ResolveSuggestScriptPath()
    {
        // AppContext.BaseDirectory is .../src/CashFlowCoach/bin/Debug/netX.Y/
        // Need to reach .../final_project/python/suggest.py
        var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../../python/suggest.py"));
        if (!File.Exists(path))
            throw new FileNotFoundException($"Could not find suggest.py at {path}");
        return path;
    }
}