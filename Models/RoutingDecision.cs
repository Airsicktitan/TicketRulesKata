namespace TicketRulesKata.Models;

public class RoutingDecision
{
    public required int TicketId { get; set; }
    public bool WasSuccessful { get; set; }
    public User? SelectedUser { get; set; }
    public Team? SelectedTeam { get; set; }
    public string? Reason { get; set; }
}