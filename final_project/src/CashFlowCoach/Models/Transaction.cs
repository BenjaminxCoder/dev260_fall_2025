namespace CashFlowCoach.Models;

public sealed record Transaction
{
    public Guid Id { get; init; }
    public DateOnly Date { get; init; }
    public decimal Amount { get; init; }
    public string Description { get; init; } = string.Empty;
    public string Category { get; init; } = "(uncategorized)";
    public bool IsIncome { get; init; }
}