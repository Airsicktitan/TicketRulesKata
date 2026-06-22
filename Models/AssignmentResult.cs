namespace TicketRulesKata.Models;

public class AssignmentResult
{
    public required Ticket Ticket { get; set; }
    public User? AssignedUser { get; set; }
    public bool WasAssigned { get; set; }
    public required string Reason { get; set; }
}