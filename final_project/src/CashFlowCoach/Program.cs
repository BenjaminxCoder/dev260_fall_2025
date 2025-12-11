using CashFlowCoach.Services;
using CashFlowCoach.UI;

namespace CashFlowCoach;

internal static class Program
{
    private static async Task<int> Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var store = new TransactionStore();
        var dataPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "data");
        Directory.CreateDirectory(dataPath);
        var txFile = Path.Combine(dataPath, "transactions.json");

        // Load persisted data (if any)
        store.LoadFromFile(txFile);

        var menu = new MainMenu(store, txFile);
        await menu.RunAsync();
        return 0;
    }
}