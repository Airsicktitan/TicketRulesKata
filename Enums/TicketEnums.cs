namespace TicketRulesKata.Enums;

public enum TicketPriority
{
    Low,
    Medium,
    High,
    Critical
}

public static class TicketEnum
{
    public static TicketPriority GetPriority(string priority)
    {
        return priority.ToLower() switch
        {
            "low" => TicketPriority.Low,
            "medium" => TicketPriority.Medium,
            "high" => TicketPriority.High,
            "critical" => TicketPriority.Critical,
            _ => throw new ArgumentException($"Invalid priority value: {priority}")
        };
    }
}